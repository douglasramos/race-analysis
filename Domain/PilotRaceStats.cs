using RaceAnalysis.Domain.ValueTypes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RaceAnalysis.Domain
{
    public class PilotRaceStats: IDomainModel
    {
        public PilotRaceStats(string id, Pilot pilot, Race race)
        {
            Pilot = pilot;
            Race = race;

            // Calculated properties

            Id = pilot.Id + race.Id;

            Position = GetPilotPosition();

            LapRaces = race.LapRaces.Where(lr => lr.PilotId == pilot.Id).ToList();

            MeanVelocity = GetMeanVelocity();

            TotalRaceTime = GetTotalRaceTime();

            BestLapRace = GetBestLapRace();

            TimeAfterWinner = GetTimeAfterWinner();
        }

        public readonly Pilot Pilot;

        public readonly Race Race;

        public string Id { get; }

        public int Position { get; }

        public ICollection<LapRace> LapRaces { get; }

        public double MeanVelocity { get; }
        public TimeSpan TotalRaceTime { get; }

        public LapRace BestLapRace { get;  }

        public TimeSpan TimeAfterWinner { get;  }

        private int GetPilotPosition()
        {
            return Race.PositionPilots.First(p => p.Value.Id == Pilot.Id).Key;
        }


        private double GetMeanVelocity()
        {
            var velocitySum = LapRaces.Select(lr => lr.MeanVelocity).Sum();
            return velocitySum / LapRaces.Count;
        }
        private TimeSpan GetTotalRaceTime()
        {
            // The total race time is the sum of all laps time
            return LapRaces.Select(lr => lr.TimeDuration).Aggregate((sum, time) => sum + time);
        }

        private LapRace GetBestLapRace()
        {
            // The best LapRace is which the pilot finhed in less time
            return LapRaces.Select(lr => (lr.TimeDuration, lr)).Min().lr;
        }

        private TimeSpan GetTimeAfterWinner()
        {
            // get the winner pilot Id
            var winnerPilot = Race.PositionPilots.GetValueOrDefault(1);

            // get his last lap race
            var winnerLastLapRace = Race.LapRaces.Where(lr => lr.PilotId == winnerPilot.Id).OrderByDescending(lr => lr.Number).First();

            // get the time Event of that lap race
            var timeEventWinner = winnerLastLapRace.TimeEvent;

            // get the las lap race from the current pilot
            var pilotLastLapRace = LapRaces.OrderByDescending(lr => lr.Number).First();

            // get the time Event of the last lap race from the current pilot
            var timeEventPilot = pilotLastLapRace.TimeEvent;

            return timeEventPilot - timeEventWinner;           
        }
    }
}
