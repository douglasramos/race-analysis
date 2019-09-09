using RaceAnalysis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceAnalysis.Application.RepositoryInterfaces
{
    public interface IRepository<T> where T: IDomainModel
    {
        T Get(string id);

        ICollection<T> GetAll();
    }
}
