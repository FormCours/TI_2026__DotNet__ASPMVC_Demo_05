using System.ComponentModel.DataAnnotations;

namespace Demo_ASPMVC_05__Auth.WebApp.Models
{
    public class MemberRegisterForm
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^A-Za-z0-9\s]).*$")]
        [DataType(DataType.Password)]
        public required string Pwd { get; set; }

        [Required]
        [Compare(nameof(Pwd))]
        [DataType(DataType.Password)]
        public required string PwdConfirm { get; set; }
    }
}
