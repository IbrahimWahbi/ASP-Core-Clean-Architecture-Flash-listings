using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Service.DTO.Users
{
    public class EditUserDto
    {
        [Required(ErrorMessage = "معرف المستخدم الزامي")]
        public string Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "يجب أن يكون طول الاسم بين 3 و30 حرفا")]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(25, ErrorMessage = "رقم الهاتف طويل جدا")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "يجب أن يكون طول الاسم بين 3 و30 حرفا")]
        public string UserName { get; set; }
    }
}
