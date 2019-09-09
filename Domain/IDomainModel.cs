using System;
using System.Collections.Generic;
using System.Text;

namespace RaceAnalysis.Domain
{
    /// <summary>
    /// Common interface to all Domain Models
    /// </summary>
    public interface IDomainModel
    {
        string Id { get; }
    }
}
