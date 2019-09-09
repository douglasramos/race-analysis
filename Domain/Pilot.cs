using System;
using System.Collections.Generic;

namespace RaceAnalysis.Domain
{
    public  class Pilot: IDomainModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var obj2 = obj as Pilot;
            if (obj2 == null) return false;

            return Id == obj2.Id;
        }

        public override int GetHashCode()
        {
            return int.Parse(Id);
        }

    }
}