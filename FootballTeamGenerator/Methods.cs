using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FootballTeamGenerator
{
    class Methods
    {
        private static Player returnRandomPlayer(List<Player> playerList, Positions positions)
        {

            IEnumerable<Player> playerIEnumerable = playerList.Where(a => a.positions == positions);
            int playerCount = playerIEnumerable.Count();
            var rand = new Random();
            //create a random number between 1 and {length - 1} of the array
            int randomNumber = rand.Next(0, playerCount - 1);
            return playerIEnumerable.ElementAt(randomNumber);
        }
        public static List<Player> removeAndAddtoLists(List<Player> playerListSelection, List<Player> playerListReturn, int noOfPositions, Positions positions)
        {
            for (var i = 0; i< noOfPositions; i++)
            {
                Player player;
                player = returnRandomPlayer(playerListSelection, positions);
                //remove player that has been selected from the list
                playerListReturn.Add(player);
                playerListSelection.Remove(player);
            }
            return playerListReturn;
        }
    }
}
