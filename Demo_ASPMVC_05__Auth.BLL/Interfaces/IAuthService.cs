using Demo_ASPMVC_05__Auth.BLL.Models;

namespace Demo_ASPMVC_05__Auth.BLL.Interfaces
{
    public interface IAuthService
    {
        // Service pour les actions liées à l'authentification

        MemberModel? Login(string email, string password);
        MemberModel? Register(MemberModel data, string password);
    }
}
