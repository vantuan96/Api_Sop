using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_SOP.Sop_model
{
    [Table("User")]
    public class User
    {
      
        [Key]
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
}
