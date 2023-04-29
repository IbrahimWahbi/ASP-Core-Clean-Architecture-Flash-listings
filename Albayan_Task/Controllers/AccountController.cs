using Albayan_Task.Domain.Interfaces;
using Albayan_Task.Errors;
using Albayan_Task.Service.DTO.Users;
using Albayan_Task.Service.Iservices.IUsers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Albayan_Task.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController: Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;
        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ITokenService tokenService, IMapper mapper, IUsersService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _userService = userService;
        }


        [Authorize]
        [HttpPost(nameof(DeleteUser))]
        public async Task<ActionResult<bool>> DeleteUser(string userid)
        {
            //() var user = await _userManager.FindByEmailAsync(HttpContext.User);
            var deleted = await _userService.RemoveUser(userid);
            return deleted;
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmialExistAsync([FromQuery] string Email, string username, string id)
        {

            var user1 = await _userManager.FindByEmailAsync(Email);
            var user2 = await _userManager.FindByNameAsync(username);
            if (user1 != null && user2 != null)
            {
                if (user1.Id == id && user2.Id == id)
                    return false;
            }
            return user1 != null || user2 != null;
        }

        [Authorize]
        [HttpPost(nameof(EditUser))]
        public async Task<ActionResult<DTOUserDetiled>> EditUser(EditUserDto dto)
        {
            if (CheckEmialExistAsync(dto.Email, dto.UserName, dto.Id).Result.Value)
            {
                return BadRequest(new APIValidationErrorResponce
                {
                    Errors = new
                    [] { "اسم المستخدم أو الايميل مستخدم سابقا" }
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new APIValidationErrorResponce
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ToString() ?? "").ToList()
                });
            }
            return await _userService.Update(dto);

        }


        [HttpPost(nameof(Login))]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user1 = await _userManager.FindByIdAsync(loginDto.Email);
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null && user1 == null) return Unauthorized(new APIResponce(401));
            user = user ?? user1;
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new APIResponce(401));
            var role = await _userManager.GetRolesAsync(user);
            return new UserDto
            {
                Emial = user.Email,
                Token = _tokenService.CreateToken(user),
                Roll = role.FirstOrDefault(),
            };
        }

        //[Authorize("Admin")]
        [HttpPost(nameof(Register))]
        public async Task<ActionResult<DTOUserDetiled>> Register(RegsiterDto regsiterDto)
        {
            if (CheckEmialExistAsync(regsiterDto.Email, regsiterDto.UserName, "").Result.Value)
            {
                return BadRequest(new APIValidationErrorResponce
                {
                    Errors = new
                    [] { "اسم المستخدم أو الايميل مستخدم سابقا" }
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new APIValidationErrorResponce
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ToString() ?? "").ToList()
                });
            }

            var user = new IdentityUser
            {
                Email = regsiterDto.Email,
                UserName = regsiterDto.UserName,
                PhoneNumber = regsiterDto.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, regsiterDto.Password);
            if (!result.Succeeded) return BadRequest(new APIValidationErrorResponce()
            {
                Errors = result.Errors.Select(x => x.Description ?? "").ToList()
            });
            return _mapper.Map<DTOUserDetiled>(user);
        }
    }
}
