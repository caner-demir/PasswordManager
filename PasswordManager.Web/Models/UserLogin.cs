using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Web.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
