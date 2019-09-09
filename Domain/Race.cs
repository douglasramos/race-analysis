using RaceAnalysis.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceAnalysis.Domain
{
    public class Race : IDomainModel
    {
        public string Id { get; }

        private Dictionary<int, Pilot> _positionPilots;

        public IReadOnlyDictionary<int, Pilot> PositionPilots
        {
            get
            {
                return _positionPilots;
            }
        }

        private List<Pilot> _pilots;

        public IReadOnlyCollection<Pilot> Pilots
        {
            get
            {
                return _pilots.AsReadOnly();
            }
        }

        private List<LapRace> _lapRaces;

        public IReadOnlyCollection<LapRace> LapRaces
        {
            get
            {
                return _lapRaces.AsReadOnly();
            }
        }

        public Race(string id, List<Pilot> pilots, List<LapRace> lapRaces)
        {
            // this property is not handle automatically because we dont have a real database to handle
            // this for us
            Id = id;

            _lapRaces = lapRaces;

            _positionPilots = GetPilotsPositions(pilots);
            _pilots = pilots;

            // calculate the BestLap [which we are considering the Lap in which some pilot finished in less time]
            BestLap = GetBestLap();
        }

        private Dictionary<int, Pilot> GetPilotsPositions(ICollection<Pilot> pilots)
        {
            var positionPilots = new Dictionary<int, Pilot>();

            //get the number of Pilots
            var numberOfPilots = pilots.Count;

            // order the lapRaces by the correct criteria to classify the pilots
            // and  select distincts based on pilot code
            var lapRacesTemp = _lapRaces
                                    .OrderByDescending(lr => lr.Number)
                                    .ThenBy(lr => lr.TimeDuration)
                                    .GroupBy(lr => lr.PilotId)
                                    .Select(g => g.First());

            // populate the dictionary
            var position = 1;
            foreach (var lr in lapRacesTemp)
            {
                var pilot = pilots.Where(p => p.Id == lr.PilotId).First();
                positionPilots.Add(position, pilot);
                position++;
            };

            return positionPilots;
        }

        public LapRace BestLap { get; }

        private LapRace GetBestLap()
        {
            return _lapRaces.Select(lap => (lap.TimeDuration, lap)).Min().lap; ;
        }
    }
}
