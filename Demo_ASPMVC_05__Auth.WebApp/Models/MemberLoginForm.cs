using System.ComponentModel.DataAnnotations;

namespace Demo_ASPMVC_05__Auth.WebApp.Models
{
    public class MemberLoginForm
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
