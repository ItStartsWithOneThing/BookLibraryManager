using AutoMapper;
using BookLibraryManagerBL.Auth;
using BookLibraryManagerBL.Models;
using BookLibraryManagerBL.Services.EncryptionService;
using BookLibraryManagerBL.Services.HashService;
using BookLibraryManagerBL.Services.SMTPService;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.CachingSystem;
using BookLibraryManagerDAL.Entities;
using System;
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
        private readonly ICacheRepository _cacheRepository;

        public AuthService(IDbGenericRepository<User> genericUsersRepository,
                            IDbGenericRepository<Role> genericRolesRepository,
                            ITokenGenerator tokenGenerator, 
                            IHashService hashService, 
                            IMapper mapper, 
                            ISmtpService smtpService,
                            IEncryptionService encryptionService,
                            ICacheRepository cacheRepository)
        {
            _genericUsersRepository = genericUsersRepository;
            _genericRolesRepository = genericRolesRepository;
            _tokenGenerator = tokenGenerator;
            _hashService = hashService;
            _mapper = mapper;
            _smtpService = smtpService;
            _encryptionService = encryptionService;
            _cacheRepository = cacheRepository;
        }

        public async Task<string> SignIn(string login, string password)
        {
            var hashedPassword = _hashService.HashString(password);

            var user = await _genericUsersRepository.GetSingleByPredicate(
                x => x.Email == login && x.Password == hashedPassword);

            if (user != null)
            {
                var role = user.RoleId.HasValue ? (await GetRole(user.RoleId.Value)) : Roles.Reader;

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
                        ClientName = $"{newUser.FirstName} {newUser.LastName}",
                        Email = newUser.Email,
                        Subject = "BookKibraryManager - Email confirmation",
                        Body = "https://localhost:5001/users/confirm?email="+GenerateConfirmationString(newUser.Email)
                    });

                return response;
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


        private async Task<string> GetRole(Guid roleId)
        {
            var cachedRole = await _cacheRepository.GetAsync(roleId.ToString());

            if (string.IsNullOrEmpty(cachedRole))
            {
                cachedRole = (await _genericRolesRepository.GetById(roleId)).Name;
                await _cacheRepository.SaveAsync(roleId.ToString(), cachedRole);
            }

            return cachedRole;
        }


        public async Task<bool> HashAdminSeedPass(string password)
        {
            var user = await _genericUsersRepository.GetById(new Guid("b003e79a-5d2c-43f3-ae2a-87e5373c094c"));

            user.Password = _hashService.HashString(password);

            return await _genericUsersRepository.Update(user);
        }
    }
}
