using BookLibraryManagerBL.Models;
using System;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> SignIn(string login, string password);

        Task<Guid> SignUp(UserDto user);

        Task<bool> ConfirmUserEmail(string encryptedEmail);

        Task<bool> HashAdminSeedPass(string password);
    }
}