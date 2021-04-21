using System.ComponentModel.DataAnnotations;

namespace Windows.User.Application
{
    public class LoginRequest
    {
        [Display(Name = "用户名")]
        [MaxLength(50)]
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
        [MaxLength(50)]
        [Required(ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }
    }
}
