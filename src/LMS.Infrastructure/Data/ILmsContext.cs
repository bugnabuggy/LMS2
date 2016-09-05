using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Core.Entities;

namespace LMS.Infrastructure.Data
{
    public interface ILmsContext
    {
        IQueryable<Area> Areas { get; }

    }
}
