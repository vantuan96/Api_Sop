using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Shared.Models
{
    public class User_SOP
    {
        public int User_Id { get; set; }

        public string User_FullName { get; set; }

        public string User_PassWord { get; set; }

        public string User_Email { get; set; }

        public DateTime User_Birthday { get; set; }

        public int User_Gender { get; set; }

        public string User_IdentityNumber { get; set; }

        public DateTime User_IdentityCreatedOn { get; set; }

        public bool User_Religion { get; set; }

        public string User_ReligionDetail { get; set; }

        public string User_Address { get; set; }

        public int User_DistrictId { get; set; }
        public int User_ProvinceId { get; set; }
        public int User_EthnicId { get; set; }
        public string User_PhoneNumber { get; set; }

        public string User_Mobile { get; set; }
        public string User_CurrentOrganizationId { get; set; }

        public bool User_Active { get; set; }

        public int User_CurrentDepartmentId { get; set; }
        public string User_UserName { get; set; }
    }

    public class User_SOP_RequestByUserNameModel
    {
        public string Username { get; set; }
    }

    public class User_SOP_RequestByListIdModel
    {
        public List<int> ListIds { get; set; }
    }
}
