using RaceAnalysis.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceAnalysis.Application
{
    public class RaceModel
    {
        public IEnumerable<PilotRaceStatsModel> PilotStats { get; set; }

        public LapRace  BestLapRace { get; set; }
    }
}
