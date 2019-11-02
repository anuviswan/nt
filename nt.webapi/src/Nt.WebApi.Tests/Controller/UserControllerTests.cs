using AutoMapper;
using Moq;
using Nt.WebApi.Controllers;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nt.WebApi.Tests.Controller
{
    public class UserControllerTests
    {
        private IMapper _mapper;
        private List<UserEntity> _userEntityCollection;
        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieEntityProfile());
                mc.AddProfile(new UserEntityProfile());
            });

            _mapper = mappingConfig.CreateMapper();
            InitliazeCollection();
        }

        private void InitliazeCollection()
        {
            _userEntityCollection = new List<UserEntity>
            {
                new UserEntity { DisplayName = "John Doe", UserName = "johndoe" , PassKey =  ToBase64("test")},
                new UserEntity { DisplayName = "David D", UserName = "davidd" , PassKey = ToBase64("test")}
            };
        }

        private string ToBase64(string source)
        {
            return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(source));
        }


        [Test]
        public void GetAll_ShouldReturnMoreThanOne()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.Get()).Returns(_userEntityCollection);
            var userController = new UserController(_mapper, mockRepository.Object);

            var result = userController.Get().ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test]
        public void CreateUser_NewUserName_ShouldAddUserToCollection()
        {
            var userProfile = new CreateUserProfileRequest
            {
                DisplayName = "New User",
                PassKey = "Passkey",
                UserName = "NewUser"
            };
            var expectedUserEntity = _mapper.Map<UserEntity>(userProfile);
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.Get()).Returns(_userEntityCollection);
            mockRepository.Setup(x => x.Create(It.IsAny<UserEntity>()))
                            .Callback<UserEntity>((user) => _userEntityCollection.Add(user))
                            .Returns(_mapper.Map<UserEntity>(userProfile));
            mockRepository.Setup(x => x.CheckIfUserExists(userProfile.UserName)).Returns(_userEntityCollection.Any(x => x.UserName == userProfile.UserName));

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.CreateUser(userProfile);
            Assert.IsTrue(_userEntityCollection.Any(x => x.UserName.Equals(userProfile.UserName, StringComparison.InvariantCultureIgnoreCase) && x.DisplayName.Equals(userProfile.DisplayName)));

        }

        [Test]
        public void CreateUser_ExistingUserName_ShouldNotAddUserToCollection()
        {
            var random = new Random();
            var userEntity = _userEntityCollection[random.Next(0, _userEntityCollection.Count - 1)];
            var userCount = _userEntityCollection.Count;
            var userProfile = new CreateUserProfileRequest
            {
                DisplayName = userEntity.DisplayName,
                UserName = userEntity.UserName
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.Get())
                          .Returns(_userEntityCollection);
            mockRepository.Setup(x => x.Create(It.IsAny<UserEntity>()))
                          .Callback<UserEntity>((user) => _userEntityCollection.Add(user))
                          .Returns(_mapper.Map<UserEntity>(userEntity));
            mockRepository.Setup(x => x.CheckIfUserExists(userEntity.UserName))
                          .Returns(_userEntityCollection.Any(x => x.UserName == userEntity.UserName));

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.CreateUser(userProfile);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.AreEqual(userCount, _userEntityCollection.Count);

        }


        [Test]
        public void CreateUser_CaseSensitiveUserName_ShouldNotAddUserToCollection()
        {
            var random = new Random();
            var userEntity = _userEntityCollection[random.Next(0, _userEntityCollection.Count - 1)];
            var userCount = _userEntityCollection.Count;
            var userProfile = new CreateUserProfileRequest
            {
                DisplayName = userEntity.DisplayName,
                UserName = new string(userEntity.UserName.Select(c => char.IsLetter(c) ? char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c) : c).ToArray())
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.Get())
                          .Returns(_userEntityCollection);
            mockRepository.Setup(x => x.Create(It.IsAny<UserEntity>()))
                          .Callback<UserEntity>((user) => _userEntityCollection.Add(user))
                          .Returns(_mapper.Map<UserEntity>(userEntity));
            mockRepository.Setup(x => x.CheckIfUserExists(userEntity.UserName))
                          .Returns(_userEntityCollection.Any(x => x.UserName == userEntity.UserName));

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.CreateUser(userProfile);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.AreEqual(userCount, _userEntityCollection.Count);

        }


        [Test]
        public void ValidateUser_ValidUser_ShouldSucceed()
        {
            var random = new Random();
            var userEntity = _userEntityCollection[random.Next(0, _userEntityCollection.Count - 1)];
            var userCount = _userEntityCollection.Count;
            var loginRequest = new LoginRequest
            {
                PassKey = userEntity.PassKey,
                UserName = userEntity.UserName,

            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(userEntity);

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.ValidateUser(loginRequest);

            Assert.IsTrue(result.IsAuthenticated);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));

        }

        [Test]
        public void ValidateUser_InvalidUser_ShouldFail()
        {
            var random = new Random();
            var userCount = _userEntityCollection.Count;
            var loginRequest = new LoginRequest
            {
                PassKey = "invalid",
                UserName = "invaliduser",

            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                          .Throws<InvalidUserException>();

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.ValidateUser(loginRequest);

            Assert.IsFalse(result.IsAuthenticated);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));

        }

        [Test]
        public void ValidateUser_CaseSensitiveValidUser_ShouldSucceed()
        {
            var random = new Random();
            var userEntity = _userEntityCollection[random.Next(0, _userEntityCollection.Count - 1)];
            var userCount = _userEntityCollection.Count;
            var loginRequest = new LoginRequest
            {
                PassKey = userEntity.PassKey,
                UserName = new string(userEntity.UserName.Select(c => char.IsLetter(c) ? char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c) : c).ToArray())
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(userEntity);

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.ValidateUser(loginRequest);

            Assert.IsTrue(result.IsAuthenticated);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));

        }


    }
}