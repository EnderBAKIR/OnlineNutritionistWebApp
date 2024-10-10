using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IRatingRepository
    {
        Task AddAsync(Rating rating);
        void UpdateAsync(Rating rating);
    }
}
