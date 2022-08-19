using AutoMapper;
using BookLibraryManagerBL.Auth;
using BookLibraryManagerBL.Models;
using BookLibraryManagerBL.Services.EncryptionService;
using BookLibraryManagerBL.Services.HashService;
using BookLibraryManagerBL.Services.SMTPService;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IDbGenericRepository<User> _genericUsersRepository;
        private readonly IDbGenericRepository<Role> _genericRolesRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        private readonly ISmtpService _smtpService;
        private readonly IEncryptionService _encryptionService;

        public AuthService(IDbGenericRepository<User> genericUsersRepository,
                            IDbGenericRepository<Role> genericRolesRepository,
                            ITokenGenerator tokenGenerator, 
                            IHashService hashService, 
                            IMapper mapper, 
                            ISmtpService smtpService,
                            IEncryptionService encryptionService)
        {
            _genericUsersRepository = genericUsersRepository;
            _genericRolesRepository = genericRolesRepository;
            _tokenGenerator = tokenGenerator;
            _hashService = hashService;
            _mapper = mapper;
            _smtpService = smtpService;
            _encryptionService = encryptionService;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var user = await _genericUsersRepository.GetSingleByPredicate(
                x => x.Email == login && x.Password == _hashService.HashString(password));

            if (user != null)
            {
                var role = user.RoleId.HasValue ? (await _genericRolesRepository.GetById(user.RoleId.Value)).Name : Roles.Reader;

                return _tokenGenerator.GenerateToken(user.Email, role);
            }

            throw new UnauthorizedAccessException("Wrong Email or Password");
        }

        public async Task<Guid> SignUp(UserDto user)
        {
            user.Password = _hashService.HashString(user.Password);

            var existingUser = await _genericUsersRepository.GetSingleByPredicate(x => x.Email == user.Email);

            if (existingUser == null)
            {
                var newUser = _mapper.Map<User>(user);

                var response = await _genericUsersRepository.Create(newUser);

                await _smtpService.SendMail(
                    new MailInfo()
                    {
                        ClientName = $"{user.FirstName} {user.LastName}",
                        Email = user.Email,
                        Subject = "BookKibraryManager - Email confirmation",
                        Body = "https://localhost:5001/users/confirm?email="+GenerateConfirmationString(user.Email)
                    });

                return Guid.Empty;
            }

            throw new UnauthorizedAccessException("Use another Email address");
        }

        private string GenerateConfirmationString(string email)
        {
            return _encryptionService.EncryptString(email);
        }

        public async Task<bool> ConfirmUserEmail(string encryptedEmail)
        {
            encryptedEmail = encryptedEmail.Replace(' ', '+');
            var userEmail = _encryptionService.DecryptString(encryptedEmail);

            var user = await _genericUsersRepository.GetSingleByPredicate(
                x => x.Email == userEmail);

            if (user != null)
            {
                user.IsConfirmed = true;
                await _genericUsersRepository.Update(user);
            }

            return user != null;
        }





        public async Task<bool> HashAdminSeedPass(string password)
        {
            var user = await _genericUsersRepository.GetById(new Guid("b003e79a-5d2c-43f3-ae2a-87e5373c094c"));

            user.Password = _hashService.HashString(password);

            return await _genericUsersRepository.Update(user);
        }
    }
}
