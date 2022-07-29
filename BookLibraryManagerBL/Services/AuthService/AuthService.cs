using AutoMapper;
using BookLibraryManagerBL.Auth;
using BookLibraryManagerBL.Models;
using BookLibraryManagerBL.Services.HashService;
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

        public AuthService(IDbGenericRepository<User> genericUsersRepository, IDbGenericRepository<Role> genericRolesRepository,
                            ITokenGenerator tokenGenerator, IHashService hashService, IMapper mapper)
        {
            _genericUsersRepository = genericUsersRepository;
            _genericRolesRepository = genericRolesRepository;
            _tokenGenerator = tokenGenerator;
            _hashService = hashService;
            _mapper = mapper;
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

            throw new ArgumentException();
        }

        public async Task<Guid> SignUp(UserDto user)
        {
            user.Password = _hashService.HashString(user.Password);

            var newUser = _mapper.Map<User>(user);

            var existingUser = _genericUsersRepository.GetSingleByPredicate(x => x.Email == user.Email);

            if (existingUser == null)
            {
                return await _genericUsersRepository.Create(newUser);
            }

            throw new ArgumentException();
        }

        public async Task<bool> HashAdminSeedPass(string password)
        {
            var user = await _genericUsersRepository.GetById(new Guid("b003e79a-5d2c-43f3-ae2a-87e5373c094c"));

            user.Password = _hashService.HashString(password);

            return await _genericUsersRepository.Update(user);
        }
    }
}
