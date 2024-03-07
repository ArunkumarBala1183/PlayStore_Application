using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playstore.Contracts.DTO
{
    public record struct AllUsersDTO
    (
         string Name,
         DateOnly DateOfBirth, //1944 to 2006
         string EmailId,
         string MobileNumber
    );
}