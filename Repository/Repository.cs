using System;
using System.Collections.Generic;
using RaceAnalysis.Domain;
using System.Linq;
using RaceAnalysis.Application.RepositoryInterfaces;

namespace RaceAnalysis.Repository
{
    public class Repository<T> : IRepository<T> where T : IDomainModel
    {
        private readonly ICollection<T> _dbCollection;

        public Repository(ICollection<T> dbCollection)
        {
            _dbCollection = dbCollection;
        }

        public ICollection<T> GetAll()
        {
            return _dbCollection;
        }

        public T Get(string id)
        {
            return _dbCollection.Where(item => item.Id == id).FirstOrDefault();
        }
    }
}
