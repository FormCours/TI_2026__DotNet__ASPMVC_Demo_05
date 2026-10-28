using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Soenneker.Hashing.Argon2;

namespace Demo_ASPMVC_05__Auth.BLL.Helpers
{
    public class HasherHelper : IHasherHelper
    {
        public class Config
        {
            public required string Pepper { get; set; }
        }


        private readonly Config _Config;
        public HasherHelper(Config config)
        {
            _Config = config;
        }


        public string Hash(string password)
        {
            return Argon2HashingUtil.Hash(password + _Config.Pepper).Result;
        }

        public bool Verify(string passwordHash, string password)
        {
            return Argon2HashingUtil.Verify(password + _Config.Pepper, passwordHash).Result;
        }
    }
}
