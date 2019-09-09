using RaceAnalysis.Application.RepositoryInterfaces;
using System.Linq;
using System.Collections.Generic;

namespace RaceAnalysis.Application
{
    /// <summary>
    /// Hnadles the GetRace information UseCase
    /// </summary>
    public class GetRace : IGetRace
    {
        private readonly IDataBaseService _dataBaseService;
        public GetRace(IDataBaseService databaseService)
        {
            _dataBaseService = databaseService;
        }

        public RaceModel Execute(string raceId)
        {
            var pilotRaceStats = _dataBaseService.PilotRaceStats.GetAll()
                                    .Where(s => s.Race.Id == raceId)
                                    .Select(s =>
                                            new PilotRaceStatsModel
                                            {
                                                PilotId = s.Pilot.Id,
                                                LapRaceQuantity = s.LapRaceQuantity,
                                                Name = s.Pilot.Name,
                                                BestLapRace = s.BestLapRace.Number,
                                                MeanVelocity = s.MeanVelocity,
                                                Position = s.Position,
                                                TimeAfterWinner = s.TimeAfterWinner,
                                                TotalRaceTime = s.TotalRaceTime,
                                            })
                                    .OrderBy(p => p.Position);

            return new RaceModel
            {
                PilotStats = pilotRaceStats,
                BestLapRace = _dataBaseService.Races.Get(raceId).BestLap
            };


        }
    }
}
