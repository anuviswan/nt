using AutoMapper;
using Moq;
using Nt.WebApi.Controllers;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controller
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
                new UserEntity { DisplayName = "John Doe", UserName = "JohnDoe" , PassKey = "test"},
                new UserEntity { DisplayName = "David D", UserName = "DavidD" , PassKey = "test"}
            };
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
            Assert.IsTrue(_userEntityCollection.Any(x=>x.UserName.Equals(userProfile.UserName) && x.DisplayName.Equals(userProfile.DisplayName))); 

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
            mockRepository.Setup(x => x.Get()).Returns(_userEntityCollection);
            mockRepository.Setup(x => x.Create(It.IsAny<UserEntity>()))
                            .Callback<UserEntity>((user) => _userEntityCollection.Add(user))
                            .Returns(_mapper.Map<UserEntity>(userEntity));
            mockRepository.Setup(x => x.CheckIfUserExists(userEntity.UserName)).Returns(_userEntityCollection.Any(x => x.UserName == userEntity.UserName));

            var userController = new UserController(_mapper, mockRepository.Object);
            var result = userController.CreateUser(userProfile);

            Assert.IsFalse(string.IsNullOrEmpty(result.ErrorMessage));
            Assert.AreEqual(userCount, _userEntityCollection.Count);

        }

    }
}