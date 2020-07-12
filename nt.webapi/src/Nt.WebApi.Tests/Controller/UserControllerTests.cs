using AutoMapper;
using Moq;
using Nt.WebApi.Controllers;
using Nt.WebApi.Exceptions;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Tests.ExtensionMethods;
using Nt.WebApi.ViewModels.RequestObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Tests.Controller
{
    [TestFixture]
    public class UserControllerTests : BaseControllerTests<UserEntity>
    {
        protected override void InitializeCollection()
        {
            EntityCollection = Enumerable.Range(1, 10).Select(x => new UserEntity
            {
                DisplayName = $"User Name {x}",
                UserName = $"username{x}",
                PassKey = $"passKey{x}"
            }).ToList();
                
        }



        [Test]
        public async Task GetAll_ShouldReturnMoreThanOne()
        {   
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.GetAsync()).Returns(Task.FromResult(EntityCollection.AsEnumerable()));
            var userController = new UserController(Mapper, mockRepository.Object);

            var result = (await userController.Get()).ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test]
        public async Task CreateUser_NewUserName_ShouldAddUserToCollection()
        {
            var userProfile = new CreateUserProfileRequest
            {
                DisplayName = "New User",
                PassKey = "Passkey",
                UserName = "NewUser"
            };
            var expectedUserEntity = Mapper.Map<UserEntity>(userProfile);
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.GetAsync()).Returns(Task.FromResult(EntityCollection.AsEnumerable()));
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<UserEntity>()))
                            .Callback<UserEntity>((user) => EntityCollection.Add(user))
                            .Returns(Task.FromResult(Mapper.Map<UserEntity>(userProfile)));
            mockRepository.Setup(x => x.CheckIfUserExistsAsync(userProfile.UserName)).Returns(Task.FromResult(EntityCollection.Any(x => x.UserName == userProfile.UserName)));

            var userController = new UserController(Mapper, mockRepository.Object);
            var result = userController.CreateUser(userProfile);
            Assert.IsTrue(EntityCollection.Any(x => x.UserName.Equals(userProfile.UserName, StringComparison.InvariantCultureIgnoreCase) && x.DisplayName.Equals(userProfile.DisplayName)));

        }

        [Test]
        public async Task CreateUser_ExistingUserName_ShouldNotAddUserToCollection()
        {
            var random = new Random();
            var userEntity = EntityCollection[random.Next(0, EntityCollection.Count - 1)];
            var userCount = EntityCollection.Count;
            var userProfile = new CreateUserProfileRequest
            {
                DisplayName = userEntity.DisplayName,
                UserName = userEntity.UserName,
                PassKey = userEntity.PassKey
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.GetAsync())
                          .Returns(Task.FromResult(EntityCollection.AsEnumerable()));
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<UserEntity>()))
                          .Callback<UserEntity>((user) => EntityCollection.Add(user))
                          .Returns(Task.FromResult(Mapper.Map<UserEntity>(userEntity)));
            mockRepository.Setup(x => x.CheckIfUserExistsAsync(userEntity.UserName))
                          .Returns(Task.FromResult(EntityCollection.Any(x => x.UserName == userEntity.UserName)));

            var userController = new UserController(Mapper, mockRepository.Object);
            var result = await userController.CreateUser(userProfile);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.AreEqual(userCount, EntityCollection.Count);

        }


        [Test]
        public async Task CreateUser_CaseSensitiveUserName_ShouldNotAddUserToCollection()
        {
            var random = new Random();
            var userEntity = EntityCollection[random.Next(0, EntityCollection.Count - 1)];
            var userCount = EntityCollection.Count;
            var userProfile = new CreateUserProfileRequest
            {
                DisplayName = userEntity.DisplayName,
                UserName = userEntity.UserName.ChangeCase(),
                PassKey = userEntity.PassKey
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.GetAsync())
                          .Returns(Task.FromResult(EntityCollection.AsEnumerable()));
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<UserEntity>()))
                          .Callback<UserEntity>((user) => EntityCollection.Add(user))
                          .Returns(Task.FromResult(Mapper.Map<UserEntity>(userEntity)));
            mockRepository.Setup(x => x.CheckIfUserExistsAsync(userEntity.UserName))
                          .Returns(Task.FromResult(EntityCollection.Any(x => x.UserName == userEntity.UserName)));

            var userController = new UserController(Mapper, mockRepository.Object);
            var result = await userController.CreateUser(userProfile);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.AreEqual(userCount, EntityCollection.Count);

        }


        [Test]
        public async Task ValidateUser_ValidUser_ShouldSucceed()
        {
            var random = new Random();
            var userEntity = EntityCollection[random.Next(0, EntityCollection.Count - 1)];
            var userCount = EntityCollection.Count;
            var loginRequest = new LoginRequest
            {
                PassKey = userEntity.PassKey,
                UserName = userEntity.UserName,
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.ValidateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(Task.FromResult(userEntity));

            var userController = new UserController(Mapper, mockRepository.Object);
            var result = await userController.ValidateUser(loginRequest);

            Assert.IsTrue(result.IsAuthenticated);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));

        }

        [Test]
        public async Task ValidateUser_InvalidUser_ShouldFail()
        {
            var random = new Random();
            var userCount = EntityCollection.Count;
            var loginRequest = new LoginRequest
            {
                PassKey = "invalid",
                UserName = "invaliduser",

            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.ValidateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                          .Throws<InvalidUserException>();

            var userController = new UserController(Mapper, mockRepository.Object);
            var result = await userController.ValidateUser(loginRequest);

            Assert.IsFalse(result.IsAuthenticated);
            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));

        }

        [Test]
        public async Task ValidateUser_CaseSensitiveValidUser_ShouldSucceed()
        {
            var random = new Random();
            var userEntity = EntityCollection[random.Next(0, EntityCollection.Count - 1)];
            var userCount = EntityCollection.Count;
            var loginRequest = new LoginRequest
            {
                PassKey = userEntity.PassKey,
                UserName = userEntity.UserName.ChangeCase()
            };
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(x => x.ValidateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(Task.FromResult(userEntity));

            var userController = new UserController(Mapper, mockRepository.Object);
            var result = await userController.ValidateUser(loginRequest);

            Assert.IsTrue(result.IsAuthenticated);
            Assert.IsTrue(string.IsNullOrEmpty(result.ErrorMessage));

        }


    }
}