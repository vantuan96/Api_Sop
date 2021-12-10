using Api_SOP.LibaryHelper;
using Api_SOP.Sop_model;
using Kztek_Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_SOP.Sop_Service.sql
{
    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;
        public UserService(IUserRepository _UserRepository)
        {
            this._UserRepository = _UserRepository;
        }
        public async Task<MessageReport> SignIn(AuthModel_LowSecurity model)
        {
            return null;
        }

        private async Task<User> GetByUsername(string username)
        {
            var query = from n in _UserRepository.Table
                        where n.User_UserName == username
                        select n;

            return await Task.FromResult( query.FirstOrDefault());
        }
    }
}
