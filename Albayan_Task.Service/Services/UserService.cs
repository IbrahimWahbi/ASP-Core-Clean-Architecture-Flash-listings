using Albayan_Task.Domain.Interfaces;
using Albayan_Task.Service.DTO.Users;
using Albayan_Task.Service.Iservices.IUsers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUsersRepo _UsersRepo;
        public UsersService(IUnitOfWork unitOfWork, IMapper mapper, IUsersRepo UsersRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UsersRepo = UsersRepo;
        }

        public Task AddUserAsync(DTOUserDetiled entity)
        {
            throw new NotImplementedException();
        }

        public Task<DTOUserDetiled> Get()
        {
            throw new NotImplementedException();
        }

        public List<DTOUserDetiled> GetUsers(string name, int pagenum = 1, int pagesize = 50)
        {
            if (pagenum < 1)
                pagenum = 1;
            if (pagesize > 50)
                pagesize = 50;
            var users = _UsersRepo.GetUsers(name, pagenum, pagesize);
            return _mapper.Map<List<DTOUserDetiled>>(users);
        }
        public async Task<bool> RemoveUser(string id)
        {
            var user = await _UsersRepo.GetUsersbyID(id);
            bool deleted = _UsersRepo.DeleteUSer(user);
            await _unitOfWork.Complete();
            return deleted;

        }

        public async Task<DTOUserDetiled> Update(EditUserDto entity)
        {
            var user = await _UsersRepo.GetUsersbyID(entity.Id);
            user.Email = entity.Email;
            user.UserName = entity.UserName;
            user.PhoneNumber = user.PhoneNumber;
            _UsersRepo.UpdateUser(user);
            await _unitOfWork.Complete();
            return _mapper.Map<DTOUserDetiled>(user);

        }

        public int Userscount()
        {
            return _UsersRepo.Userscount();
        }
    }
}
