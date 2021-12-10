
using Api_SOP.Sop_model;
using Kztek_Data.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sop_Data;
using Sop_Data.Infrastructure;

namespace Kztek_Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}