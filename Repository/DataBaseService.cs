using RaceAnalysis.Application.RepositoryInterfaces;
using RaceAnalysis.Domain;
using RaceAnalysis.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace RaceAnalysis.Repository
{
    public class DataBaseService: IDataBaseService
    {
        public IRepository<Race> Races { get; }
        public IRepository<Pilot> Pilots { get; }
        public IRepository<PilotRaceStats> PilotRaceStats { get; }

        // The code is prepared to receive info of multiple races. But, in this project
        // by now, we only fetch one file that represent info of only one race
        private readonly ICollection<RaceEvent> _raceEvents;


        public DataBaseService()
        {
            // get race events from file. Each line represents a race event
            _raceEvents = getRaceEvents();

            // create our repositories from  the race events. 
            // This is a reasonable way to do this since we don't have a structured database
            Races = FactoryRaces(_raceEvents);
            Pilots = FactoryPilots(_raceEvents);
            PilotRaceStats = FactoryRacePilotStats(_raceEvents, Races.GetAll(), Pilots.GetAll());
        }

        private IRepository<PilotRaceStats> FactoryRacePilotStats(ICollection<RaceEvent> raceEvents, ICollection<Race> races, ICollection<Pilot> pilots )
        {
            var pilotRaceStatsCollection = new List<PilotRaceStats>();

            foreach (var race in races)
            {
                foreach (var pilot in pilots)
                {
                    pilotRaceStatsCollection.Add(new PilotRaceStats(race.Id + pilot.Id, pilot, race));
                }
            }

            return new Repository<PilotRaceStats>(dbCollection: pilotRaceStatsCollection);
        }

        /// <summary>
        /// Pilotys Repository Factory
        /// </summary>
        private IRepository<Pilot> FactoryPilots(ICollection<RaceEvent> raceEvents)
        {
            // since there's ony one race, all pilots derived from that race
            var pilots = raceEvents.Select(re => re.Pilot).Distinct().ToList();

            return new Repository<Pilot>(dbCollection: pilots);
        }

        /// <summary>
        /// Race Repository Factory
        /// </summary>
        private IRepository<Race> FactoryRaces(ICollection<RaceEvent> raceEvents)
        {
            var racesColletion = new List<Race>();

            // get all pilots from raceEvents
            var pilots = raceEvents.Select(raceEvent => raceEvent.Pilot).Distinct().ToList();

            // get all LapRaces from raceEvents
            var lapRaces = raceEvents.Select(re => re.LapRace).ToList();

            // populate races [in this example only one race
            var race = new Race("1", pilots, lapRaces);

            // Add race to the collection
            racesColletion.Add(race);

            return new Repository<Race>(dbCollection: racesColletion);
        }

        /// <summary>
        /// Fetch RaceEvent from source data file. The RaceEvent will be used to create all repositories
        /// </summary>
        /// <returns></returns>
        private  ICollection<RaceEvent> getRaceEvents()
        {

            // create the list race Events
            var raceEvents = new List<RaceEvent>();

            // set lines
            IEnumerable<string> lines = null;

            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "racing_data.txt");

                // get all lines from .txt data source
                lines = File.ReadLines(path);
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem in read the data source file", ex);
            }

            // if the lines is still null, then return null
            if (lines == null) return null;

            try
            {
                // iterate through every line
                var lineNumber = 1;
                foreach (var line in lines)
                {
                    // skip the first line
                    if (lineNumber == 1)
                    {
                        lineNumber++;
                        continue;
                    }

                    // Split string separated by multiple spaces, ignoring single spaces
                    var fields = System.Text.RegularExpressions.Regex.Split(line, @"\s{2,}");

                    // create the pilot
                    var pilot = new Pilot
                    {
                        Id = fields[1].Split(" ").First(),
                        Name = fields[1].Split(" ").Last()
                    };

                    // create lap race
                    var lapRace = new LapRace
                    {
                        TimeEvent = TimeSpan.Parse(fields[0]),
                        PilotId = fields[1].Split(" ").First(),
                        Number = int.Parse(fields[2]),
                        TimeDuration = TimeSpan.ParseExact(fields[3], @"%m\:ss\.fff", CultureInfo.InvariantCulture),
                        MeanVelocity = double.Parse(fields[4], new CultureInfo("pt-BR"))
                    };


                    // create the race event
                    var raceEvent = new RaceEvent
                    {
                        Sequence = lineNumber - 1,
                        Pilot = pilot,
                        LapRace = lapRace

                    };

                    // add event Race to the collection
                    raceEvents.Add(raceEvent);

                    // increment lineNumber
                    lineNumber++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem on parse values from the data source", ex);
            }

            return raceEvents;
        }
    }
}
