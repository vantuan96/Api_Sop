
using Api_SOP.Sop_model;
using Kztek_Data.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sop_Data;
using Sop_Data.Infrastructure;

namespace Kztek_Data.Repository
{
    public interface IRatingResultRepository : IRepository<RatingResult>
    {
    }

    public class RatingResultRepository : Repository<RatingResult>, IRatingResultRepository
    {
        public RatingResultRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}