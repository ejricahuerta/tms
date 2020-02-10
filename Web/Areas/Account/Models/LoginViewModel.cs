using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Account.Models
{
    public class LoginViewModel

    {
        [Required]
        [StringLength(40)]
        public string Username { get; set; }
        [Required]
        [StringLength(40)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
