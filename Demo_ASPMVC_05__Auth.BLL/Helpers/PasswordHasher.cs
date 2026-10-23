using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Soenneker.Hashing.Argon2;

namespace Demo_ASPMVC_05__Auth.BLL.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return Argon2HashingUtil.Hash(password).Result;
        }

        public bool Verify(string passwordHash, string password)
        {
            return Argon2HashingUtil.Verify(password, passwordHash).Result;
        }
    }
}
