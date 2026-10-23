namespace Demo_ASPMVC_05__Auth.BLL.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? PasswordHash { get; set; }
    }
}
