using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo_ASPMVC_05__Auth.WebApp.Models
{
    public class MemberLoginForm
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Couriel")]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Passiciel")]
        public required string Password { get; set; }
    }
}
