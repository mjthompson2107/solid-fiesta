using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator
{
    class Player
    {
        public int ID { get; set; }
        [Name("First Name")]
        public string firstName { get; set; }
        [Name("Surname")]
        public string lastName { get; set; }
        [Name("Position")]
        public Positions positions { get; set; }
    }
}
