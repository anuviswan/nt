using AuthService.Data.Database;
using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using AuthService.Service.Command;
using AuthService.Service.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Service.Tests.Commands;

[TestFixture]
public class AddUserCommandHandlerTests
{
    private IUnitOfWorkFactory _unitOfWorkFactory = null!;
    private IUnitOfWork _unitOfWork = null!;
    private IUserRepository _userRepository = null!;

    [SetUp]
    public void Initialize()
    {
        _unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _userRepository = Substitute.For<IUserRepository>();
    }

    [Test]
    [TestCaseSource(nameof(AddUser_Handle_ValidData_Success_TestData))]
    public async Task AddUser_Handle_ValidData_Success(AddUserCommand request, Domain.Entities.User expectedResult)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);
        _userRepository.GetAll().Returns(x => Enumerable.Empty<User>());
        _userRepository.AddAsync(Arg.Any<Domain.Entities.User>()).Returns(x => expectedResult);
        #endregion

        #region Act
        var sut = new AddUserCommandHandler(_unitOfWorkFactory);
        var result = await sut.Handle(request, default).ConfigureAwait(false);
        #endregion

        #region Assert
        Assert.IsNotNull(result);
        Assert.Greater(result.Id, Guid.Empty);
        Assert.That(result.UserName, Is.EqualTo(expectedResult.UserName));
        Assert.That(result.Password, Is.EqualTo(expectedResult.Password));
        #endregion
    }

    static IEnumerable<object[]> AddUser_Handle_ValidData_Success_TestData()
    {
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new Domain.Entities.User
                {
                    UserName = "Test",
                    Password = "password",
                }
            },
            new User
            {
                Id = Guid.Parse("0696936b-b847-48f5-a5c9-cdc7e0eb0bdb"),
                UserName = "Test",
                Password = "password",
            }
        };
    }

    [Test]
    [TestCaseSource(nameof(AddUser_Handle_UserAlreadyExists_ThrowsException_TestData))]
    public void AddUser_Handle_UserAlreadyExists_ThrowsException(AddUserCommand request)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);
        _userRepository.GetAll()
                       .Returns(x => new User[] { request.User! });
        _userRepository.AddAsync(Arg.Any<User>())
                       .Returns<User>(x => request.User!);
        #endregion

        #region Act And Assert
        var sut = new AddUserCommandHandler(_unitOfWorkFactory);
        Assert.ThrowsAsync<UserAlreadyExistsException>(async () => await sut.Handle(request, default).ConfigureAwait(false));
        #endregion
    }

    static IEnumerable<object[]> AddUser_Handle_UserAlreadyExists_ThrowsException_TestData()
    {
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new Domain.Entities.User
                {
                    UserName = "Test",
                    Password = "password",
                }
            }
        };
    }


    [Test]
    [TestCaseSource(nameof(AddUser_Handle_InvalidData_ThrowsException_TestData))]
    public void AddUser_Handle_InvalidData_ThrowsException(AddUserCommand request, Type expectedExceptionType)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);
        _userRepository.GetAll()
                       .Returns(x => new User[] { request.User! });
        _userRepository.AddAsync(Arg.Any<User>())
                       .Returns<User>(x => request.User!);
        #endregion

        #region Act And Assert
        var sut = new AddUserCommandHandler(_unitOfWorkFactory);
        Assert.ThrowsAsync(expectedExceptionType, async () => await sut.Handle(request, default).ConfigureAwait(false));
        #endregion
    }

    static IEnumerable<object[]> AddUser_Handle_InvalidData_ThrowsException_TestData()
    {
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new Domain.Entities.User
                {
                    UserName = string.Empty,
                    Password = "password",
                }
            },
            typeof(ArgumentException)
        };

        yield return new object[]
        {
            new AddUserCommand
            {
                User = new Domain.Entities.User
                {
                    Password = "password",
                }
            },
            typeof(ArgumentNullException)
        };

        yield return new object[]
        {
            new AddUserCommand
            {
                User = new Domain.Entities.User
                {
                    UserName = "Test",
                    Password = string.Empty,
                }
            },
            typeof(ArgumentException)
        };

        yield return new object[]
        {
            new AddUserCommand
            {
                User = new Domain.Entities.User
                {
                    UserName = "Test",
                }
            },
            typeof(ArgumentNullException)
        };
    }
}
