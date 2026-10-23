using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Demo_ASPMVC_05__Auth.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_ASPMVC_05__Auth.BLL.Services
{
    public class AuthService : IAuthService
    {
        #region Fake Data
        private static List<MemberModel> _Members = [
            new MemberModel { Id = 1, Email = "della@test", Role = "Admin", PasswordHash = "$argon2id$v=19$m=16,t=2,p=1$UWRuemdZU1JkZ1Z5VjRHZw$Yrlh9yrJRGBs/ZPMSymHqw"},
            new MemberModel { Id = 2, Email = "donald@test", Role = "User", PasswordHash = "$argon2id$v=19$m=16,t=2,p=1$N1QyM0Jlczc4VE1hTXdHMg$OmydvYpQwzb3koGTrdKw1A"}
        ];
        private static int _NextId = 3;
        #endregion

        private IPasswordHasher passwordHasherService;
        public AuthService(IPasswordHasher passwordHasherService)
        {
            this.passwordHasherService = passwordHasherService;
        }

        public MemberModel? Login(string email, string password)
        {
            MemberModel? member = _Members.SingleOrDefault(m => m.Email == email);
            if (member is null) return null;

            bool passwordCheck = passwordHasherService.Verify(member.PasswordHash!, password);
            if (!passwordCheck) return null;

            // Remark : Cas réel - Ne pas réenvoyer le mot de passe haché !
            return member;
        }

        public MemberModel? Register(MemberModel data, string password)
        {
            MemberModel? memberAlreadyExists = _Members.SingleOrDefault(m => m.Email == data.Email);
            if (memberAlreadyExists is not null) return null;

            data.Id = _NextId++;
            data.PasswordHash = passwordHasherService.Hash(password);
            _Members.Add(data);

            // Remark : Cas réel - Ne pas réenvoyer le mot de passe haché !
            return data;
        }
    }
}
