using Albayan_Task.Model.Data;
using Albayan_Task.Service.Iservices.IUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Model.Repositories
{
    public class UsersRepository : IUsersRepo
    {
        private readonly MyDbContext _dbcontext;
        public UsersRepository(MyDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> DeleteUSerAsync(string UserId)
        {
            var user = await _dbcontext.Users.FindAsync(UserId);
            if (user == null) { return false; }
            _dbcontext.Users.Remove(user);
            return true;
        }
        public bool DeleteUSer(IdentityUser user)
        {

            try
            {
                _dbcontext.Users.Remove(user);
                return true;
            }
            catch { }

            return false;
        }
        public int Userscount()
        {
            return _dbcontext.Users.Count();
        }
        public List<IdentityUser> GetUsers(string searchString, int pageIndix, int pagesize)
        {
            var users = _dbcontext.Users.Where
                (s => (searchString.IsNullOrEmpty() || s.Id.Contains(searchString) ||
                s.Email.Contains(searchString)  || s.UserName.Contains(searchString) ) 
                && s.UserName.ToLower() != "admin").Skip((pageIndix - 1) * pagesize).Take(pagesize).ToList();
            return users;
        }

        public async Task<IdentityUser> GetUsersbyID(string UserId)
        {
            return await _dbcontext.Users.FindAsync(UserId);
        }

        public void UpdateUser(IdentityUser dto)
        {
            var xx = _dbcontext.Attach<IdentityUser>(dto);

            _dbcontext.Entry(dto).State = EntityState.Modified;
        }
    }
}
