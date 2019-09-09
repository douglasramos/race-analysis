using RaceAnalysis.Domain;

namespace RaceAnalysis.Application.RepositoryInterfaces
{
    public interface IDataBaseService
    {
        IRepository<PilotRaceStats> PilotRaceStats { get; }
        IRepository<Pilot> Pilots { get; }
        IRepository<Race> Races { get; }
    }
}