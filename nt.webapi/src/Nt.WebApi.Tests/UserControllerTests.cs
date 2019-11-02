using AutoMapper;
using Moq;
using Nt.WebApi.Controllers;
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

    }
}