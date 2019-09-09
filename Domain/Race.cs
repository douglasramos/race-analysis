using RaceAnalysis.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceAnalysis.Domain
{
    /// <summary>
    /// Domain Class that represents a Race
    /// </summary>
    public class Race : IDomainModel
    {
        #region Fields
        private List<Pilot> _pilots;
        private List<LapRace> _lapRaces;
        private Dictionary<int, Pilot> _positionPilots;
        #endregion

        #region Properties
        public string Id { get; }

        /// <summary>
        /// dictionary that relates a given race position and a pilot object
        /// The position is the key and the pilot object is the value
        /// </summary>
        public IReadOnlyDictionary<int, Pilot> PositionPilots
        {
            get
            {
                return _positionPilots;
            }
        }

        public IReadOnlyCollection<Pilot> Pilots
        {
            get
            {
                return _pilots.AsReadOnly();
            }
        }

        public IReadOnlyCollection<LapRace> LapRaces
        {
            get
            {
                return _lapRaces.AsReadOnly();
            }
        }
        public LapRace BestLap { get; }
        #endregion

        #region Constructor
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
        #endregion

        #region Auxiliary Methods
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

        /// <summary>
        /// Calculate the BestLap of the race that is the lap made in less time by any pilot
        /// </summary>
        private LapRace GetBestLap()
        {
            return _lapRaces.Select(lap => (lap.TimeDuration, lap)).Min().lap; ;
        }
        #endregion
    }
}
