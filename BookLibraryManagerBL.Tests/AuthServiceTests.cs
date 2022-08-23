using AutoFixture;
using AutoMapper;
using BookLibraryManagerBL.Auth;
using BookLibraryManagerBL.Models;
using BookLibraryManagerBL.Services.AuthService;
using BookLibraryManagerBL.Services.EncryptionService;
using BookLibraryManagerBL.Services.HashService;
using BookLibraryManagerBL.Services.SMTPService;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Tests
{
    public class AuthServiceTests
    {
        private Fixture _fixture;
        private Mock<IDbGenericRepository<User>> _genericUsersRepositoryMock;
        private Mock<IDbGenericRepository<Role>> _genericRolesRepositoryMock;
        private Mock<ITokenGenerator> _tokenGeneratorMock;
        private Mock<IHashService> _hashServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ISmtpService> _smtpServiceMock;
        private Mock<IEncryptionService> _encryptionServiceMock;
        private AuthService _authService;

        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _genericUsersRepositoryMock = new Mock<IDbGenericRepository<User>>();
            _genericRolesRepositoryMock = new Mock<IDbGenericRepository<Role>>();
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            _hashServiceMock = new Mock<IHashService>();
            _mapperMock = new Mock<IMapper>();
            _smtpServiceMock = new Mock<ISmtpService>();
            _encryptionServiceMock = new Mock<IEncryptionService>();

            _authService = new AuthService(_genericUsersRepositoryMock.Object,
                _genericRolesRepositoryMock.Object,
                _tokenGeneratorMock.Object,
                _hashServiceMock.Object,
                _mapperMock.Object,
                _smtpServiceMock.Object,
                _encryptionServiceMock.Object);
        }

        [Test]
        public async Task SignIn_WhenUserWithoutRole_ShouldLoginUserWithReader()
        {
            var login = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var hashPassword = _fixture.Create<string>();
            var expectedToken = _fixture.Create<string>();
            var role = "Reader";

            var userInDb = _fixture.Build<User>()
                .With(u => u.Email, login)
                .With(u => u.Password, hashPassword)
                .Without(u => u.Role)
                .Without(u => u.RoleId)
                .Without(u => u.RentBooks)
                .Create();

            _hashServiceMock.Setup(
                x => x.HashString(password))
                .Returns(hashPassword)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.GetSingleByPredicate(
                    y => y.Email == login && y.Password == hashPassword))
                .ReturnsAsync(userInDb)
                .Verifiable();

            _tokenGeneratorMock.Setup(
                x => x.GenerateToken(userInDb.Email, role))
                .Returns(expectedToken)
                .Verifiable();

            var actualToken = await _authService.SignIn(login, password);

            _hashServiceMock.Verify();
            _genericUsersRepositoryMock.Verify();
            _tokenGeneratorMock.Verify();
            _genericRolesRepositoryMock.Verify(
                x => x.GetById(It.IsAny<Guid>()), Times.Never());

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public async Task SignIn_WhenUserWithRole_ShouldLoginUserWithWithRoleFromDb()
        {
            var login = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var hashPassword = _fixture.Create<string>();
            var expectedToken = _fixture.Create<string>();
            var role = _fixture.Create<Role>();

            var userInDb = _fixture.Build<User>()
                .With(u => u.Email, login)
                .With(u => u.Password, hashPassword)
                .With(u => u.RoleId, role.Id)
                .With(u => u.Role, role)
                .Without(u => u.RentBooks)
                .Create();

            _hashServiceMock.Setup(
                x => x.HashString(password))
                .Returns(hashPassword)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.GetSingleByPredicate(
                    y => y.Email == login && y.Password == hashPassword))
                .ReturnsAsync(userInDb)
                .Verifiable();

            _genericRolesRepositoryMock.Setup(
                x => x.GetById(userInDb.RoleId.Value))
                .ReturnsAsync(role)
                .Verifiable();

            _tokenGeneratorMock.Setup(
                x => x.GenerateToken(userInDb.Email, role.Name))
                .Returns(expectedToken)
                .Verifiable();

            var actualToken = await _authService.SignIn(login, password);

            _hashServiceMock.Verify();
            _genericUsersRepositoryMock.Verify();
            _genericRolesRepositoryMock.Verify();
            _tokenGeneratorMock.Verify();

            Assert.AreEqual(expectedToken, actualToken);
        }
        
        [Test]
        public async Task SignIn_WhenUserDoesNotExist_ShouldThrowUnauthorizedAccessException()
        {
            var login = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var hashPassword = _fixture.Create<string>();

            _hashServiceMock.Setup(
                x => x.HashString(password))
                .Returns(hashPassword)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.GetSingleByPredicate(
                    y => y.Email == login && y.Password == hashPassword))
                .Verifiable();

            Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await _authService.SignIn(login, password));

            _hashServiceMock.Verify();
            _genericUsersRepositoryMock.Verify();
            _genericRolesRepositoryMock.Verify(
                x => x.GetById(
                    It.IsAny<Guid>()), Times.Never());
            _tokenGeneratorMock.Verify(
                x => x.GenerateToken(
                    It.IsAny<string>(), 
                    It.IsAny<string>()), Times.Never());            
        }

        [Test]
        public async Task SignUp_WhenUserAlreadyExist_ShouldThrowUnauthorizedAccessException()
        {
            var userDto = _fixture.Build<UserDto>()
                .With(x => x.Id, Guid.Empty)
                .Create();
            var userId = Guid.NewGuid();
            var hashedPassword = _fixture.Create<string>();
            var existingUser = new User()
            {
                Id = userId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = hashedPassword,
                BirthDate = userDto.BirthDate
            };

            _hashServiceMock.Setup(
                x => x.HashString(userDto.Password))
                .Returns(hashedPassword)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.GetSingleByPredicate(
                    p => p.Email == userDto.Email))
                .ReturnsAsync(existingUser)
                .Verifiable();

            Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await _authService.SignUp(userDto));

            _hashServiceMock.Verify();
            _genericUsersRepositoryMock.Verify();
            _mapperMock.Verify(
                x => x.Map<User>(
                    It.IsAny<UserDto>()), Times.Never());
            _genericUsersRepositoryMock.Verify(
                x => x.Create(
                    It.IsAny<User>()), Times.Never());
            _smtpServiceMock.Verify(
                x => x.SendMail(
                    It.IsAny<MailInfo>()), Times.Never());
            _encryptionServiceMock.Verify(
                x => x.EncryptString(
                    It.IsAny<string>()), Times.Never());
        }
        
        [Test]
        public async Task SignUp_WhenUserDoesNotExist_ShouldRegister()
        {
            var userDto = _fixture.Build<UserDto>()
                .With(x => x.Id, Guid.Empty)
                .Create();
            var userId = Guid.NewGuid();
            var hashedPassword = _fixture.Create<string>();
            var newUser = new User()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = hashedPassword,
                BirthDate = userDto.BirthDate
            };
            var encryptedMail = _fixture.Create<string>();
            var mailInfo = new MailInfo()
            {
                ClientName = $"{newUser.FirstName} {newUser.LastName}",
                Email = newUser.Email,
                Subject = "BookKibraryManager - Email confirmation",
                Body = "https://localhost:5001/users/confirm?email="+encryptedMail
            };

            _hashServiceMock.Setup(
                x => x.HashString(userDto.Password))
                .Returns(hashedPassword)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.GetSingleByPredicate(
                    p => p.Email == userDto.Email))
                .Verifiable();

            _mapperMock.Setup(
                x => x.Map<User>(userDto))
                .Returns(newUser)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.Create(newUser))
                .ReturnsAsync(userId)
                .Verifiable();

            _smtpServiceMock.Setup(
                x => x.SendMail(
                    It.Is<MailInfo>(
                        m => m.Email == mailInfo.Email
                        && m.ClientName == mailInfo.ClientName
                        && m.Body == mailInfo.Body
                        && m.Subject == mailInfo.Subject)))
                .Verifiable();

            _encryptionServiceMock.Setup(
                x => x.EncryptString(userDto.Email))
                .Returns(encryptedMail)
                .Verifiable();

            var actualId = await _authService.SignUp(userDto);

            _hashServiceMock.Verify();
            _genericUsersRepositoryMock.Verify();
            _mapperMock.Verify();
            _smtpServiceMock.Verify();
            _encryptionServiceMock.Verify();

            Assert.AreEqual(userId, actualId);
        }

        [Test]
        public async Task ConfirmUserEmail_WhenUserExist_ShouldConfirmEmail()
        {
            var encryptedMail = _fixture.Create<string>();
            var encryptedMailModified = encryptedMail.Replace(' ', '+');
            var userMail = _fixture.Create<string>();

            var user = _fixture.Build<User>()
                .With(x => x.Email, userMail)
                .With(x => x.IsConfirmed, false)
                .Without(x => x.Role)
                .Without(x => x.RoleId)
                .Without(x => x.RentBooks)
                .Create();


            _encryptionServiceMock.Setup(
                x => x.DecryptString(encryptedMailModified))
                .Returns(userMail)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.GetSingleByPredicate(
                    x => x.Email == userMail))
                .ReturnsAsync(user)
                .Verifiable();

            _genericUsersRepositoryMock.Setup(
                x => x.Create(
                    It.Is<User>(x => 
                    x.Id == user.Id
                    && x.IsConfirmed == true
                    && x.Email == user.Email)))
                .Verifiable();

            var actualResult = await _authService.ConfirmUserEmail(encryptedMail);

            _encryptionServiceMock.Verify();
            _genericUsersRepositoryMock.Verify();

            Assert.IsTrue(actualResult);
        }
    }
}
