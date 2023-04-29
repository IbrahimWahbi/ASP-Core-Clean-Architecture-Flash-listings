using Albayan_Task.Service.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.Iservices.IUsers
{
    public interface IUsersService
    {
        Task<DTOUserDetiled> Get();
        List<DTOUserDetiled> GetUsers(string name, int pagenum, int pagesize);
        Task<DTOUserDetiled> Update(EditUserDto entity);
        Task AddUserAsync(DTOUserDetiled entity);
        Task<bool> RemoveUser(string id);
        int Userscount();
    }
}
