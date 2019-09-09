using System;
using System.Collections.Generic;
using System.Text;

namespace RaceAnalysis.Application
{
    public class PilotRaceStatsModel
    {
        public string PilotId { get; set; }

        public string Name {get; set;}

        public int Position { get; set; }        

        public double MeanVelocity { get; set; }

        public TimeSpan TotalRaceTime { get; set; }

        public int BestLapRace { get; set; }

        public TimeSpan TimeAfterWinner { get; set; }
    }
}
