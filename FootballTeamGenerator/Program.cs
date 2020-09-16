using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using CsvHelper;

namespace FootballTeamGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerLength = 10;
            List<Player> listPlayers = new List<Player>();
            string teamName = "";
            int noOfStrikers = 0;
            int noOfMidfielders = 0;
            int noOfDefenders = 0;
            Methods method = new Methods();

            startOfCode:

            Console.WriteLine("Welcome to the random football team generator.\nPlease enter a Team Name:");
            teamName = Console.ReadLine();

            startOfPositionInputs:
            defenders:
            Console.WriteLine("Please enter number of defenders:");
            noOfDefenders = Int32.TryParse(Console.ReadLine(), out Int32 defendersIntTest) ? defendersIntTest : -1;

            if (noOfDefenders == -1)
            {
                Console.WriteLine("The number of defenders entered is not a number greater than zero. Please reenter:");
                goto defenders;
            }

            midfielders:
            Console.WriteLine("Please enter number of midfielders:");
            noOfMidfielders = Int32.TryParse(Console.ReadLine(), out Int32 midfielderIntTest) ? midfielderIntTest : -1;

            if (noOfMidfielders == -1)
            {
                Console.WriteLine("The number of midfielders entered is not a number greater than zero. Please reenter:");
                goto midfielders;
            }

            strikers:
            Console.WriteLine("Please enter number of strikers:");
            noOfStrikers = Int32.TryParse(Console.ReadLine(), out Int32 strikersInt) ? strikersInt : -1;
            if (noOfStrikers == -1)
            {
                Console.WriteLine("The number of strikers entered is not a number greater than zero. Please reenter:");
                goto strikers;
            }

            if (noOfDefenders + noOfMidfielders + noOfStrikers != playerLength)
            {
                Console.WriteLine("The composition of positions entered does not add up to 10. Please enter a team of 10 players.");
                goto startOfPositionInputs;
            } 

            List<Player> records; //to hold the players from the csv
            using (var reader = new StreamReader("Technical test data (1).csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Player>().ToList();
            }

            List<Player> listOfReturn = new List<Player>(); //list to return to the user

            //retrieve random players from each position
            listOfReturn = Methods.removeAndAddtoLists(records, listOfReturn, 1, Positions.Goalkeeper);
            listOfReturn = Methods.removeAndAddtoLists(records, listOfReturn, noOfDefenders, Positions.Defender);
            listOfReturn = Methods.removeAndAddtoLists(records, listOfReturn, noOfMidfielders, Positions.Midfielder);
            listOfReturn = Methods.removeAndAddtoLists(records, listOfReturn, noOfStrikers, Positions.Attacker);
            //display message to the user that their team selection is about to be returned
            Console.WriteLine($"You have selected the following players for team, {teamName}.");

            string returnString = ""; //variable to return the output to the user
            listOfReturn = listOfReturn.OrderBy(p => p.positions).ToList();
            foreach (Player playerReturn in listOfReturn)
            {
                returnString = $"Name: {playerReturn.firstName} {playerReturn.lastName}  Position: {playerReturn.positions.ToString()}";
                Console.WriteLine(returnString);
            }

            Console.WriteLine("Would you like another random football team generated. Please enter Y for Yes and N for ");
        nextGeneration:
            string nextGenerationInput = Console.ReadLine();
            if (nextGenerationInput == "Y")
            {
                goto startOfCode;
            }
            else if (nextGenerationInput == "N")
            {
                System.Environment.Exit(1);
            }
            else
            {
                goto nextGeneration;
            }
        }
    }
}
