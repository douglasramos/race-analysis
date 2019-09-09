using RaceAnalysis.Domain;
using RaceAnalysis.Domain.ValueTypes;
using System;

namespace RaceAnalysis.Repository
{
    public class RaceEvent
    {
        public int Sequence { get; set; }

        public TimeSpan Time { get; set; }

        public Pilot Pilot { get; set; }

        public LapRace LapRace { get; set; }


    }

}
