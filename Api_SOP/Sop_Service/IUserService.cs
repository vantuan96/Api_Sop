using Api_SOP.LibaryHelper;
using Api_SOP.Sop_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_SOP.Sop_Service
{
    public interface IUserService
    {
        Task<MessageReport> SignIn(AuthModel_LowSecurity value);
    }
}
