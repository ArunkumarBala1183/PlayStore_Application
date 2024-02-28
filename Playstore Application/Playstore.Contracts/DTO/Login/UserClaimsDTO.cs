using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstore.Contracts.DTO
{
    public class UserClaimsDTO
    {
        public string EmailId { get; set; }
        public string RoleCode { get; set; }
        public string Password { get; set; }
    }

}