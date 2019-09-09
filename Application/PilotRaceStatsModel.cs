using System;
using System.Collections.Generic;
using System.Text;

namespace RaceAnalysis.Application
{
    /// <summary>
    /// Model Class for Application/Presenter Layer that represents a Pilot stats on a race
    /// </summary>
    public class PilotRaceStatsModel
    {
        public string PilotId { get; set; }

        public string Name {get; set;}

        public int Position { get; set; }        

        public double MeanVelocity { get; set; }

        public int LapRaceQuantity { get; set; }

        public TimeSpan TotalRaceTime { get; set; }

        public int BestLapRace { get; set; }

        public TimeSpan TimeAfterWinner { get; set; }
    }
}
