
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.Iservices.IUsers
{
    public interface IUsersRepo
    {
        List<IdentityUser> GetUsers(string searchString, int pageIndix, int pagesize);
        Task<IdentityUser> GetUsersbyID(string UserId);
        void UpdateUser(IdentityUser dto);
        Task<bool> DeleteUSerAsync(string UserId);
        bool DeleteUSer(IdentityUser UserId);
        int Userscount();
    }
}
