using System.Collections.Generic;

namespace RaceAnalysis.Application
{
    public interface IGetRace
    {
        RaceModel Execute(string raceId);
    }
}