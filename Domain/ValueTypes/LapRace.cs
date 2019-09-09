using System;

namespace RaceAnalysis.Domain.ValueTypes
{
    /// <summary>
    /// A ValueType class that serves as a data bag for the Domain Models
    /// </summary>
    public class LapRace
    {
        public TimeSpan TimeEvent { get; set; }

        public int Number { get; set; }       

        public string PilotId { get; set; }

        public TimeSpan TimeDuration { get; set; }

        public double MeanVelocity { get; set; }
    }
}