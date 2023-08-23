using AuthService.Data.Database;
using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using AuthService.Service.Command;
using AuthService.Service.Helpers.Exceptions;

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

        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);
    }

    [Test]
    [TestCaseSource(nameof(AddUser_Handle_ValidData_Success_TestData))]
    public async Task AddUser_Handle_ValidData_Success(AddUserCommand request,User expectedResponse)
    {
        #region Arrange
        _userRepository.GetAll().Returns(x=> Enumerable.Empty<User>());
        _userRepository.AddAsync(Arg.Any<User>()).Returns(x=> expectedResponse);
        #endregion

        #region Act
        var sut = new AddUserCommandHandler(_unitOfWorkFactory);
        var response = await sut.Handle(request, default).ConfigureAwait(false);
        #endregion

        #region Assert
        Assert.IsNotNull(response);
        Assert.That(response, Is.EqualTo(expectedResponse));
        #endregion
    }

    static IEnumerable<object[]> AddUser_Handle_ValidData_Success_TestData()
    {
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new User
                {
                    UserName = "John Doe",
                    Password = "Random"
                },

            },
            new User
            {
                UserName = "John Doe",
                Password = "Random"
            }
        };
    }


    [Test]
    [TestCaseSource(nameof(AddUser_Handle_UserExists_ThrowsException_TestData))]
    public void AddUser_Handle_UserExists_ThrowsException(AddUserCommand request, User existingUser)
    {
        #region Arrange
        _userRepository.GetAll().Returns(x => new[] { existingUser });
        #endregion

        #region Act
        var sut = new AddUserCommandHandler(_unitOfWorkFactory);
        Assert.ThrowsAsync<UserAlreadyExistsException>(async () => await sut.Handle(request, default).ConfigureAwait(false));
        #endregion
    }

    static IEnumerable<object[]> AddUser_Handle_UserExists_ThrowsException_TestData()
    {
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new User
                {
                    UserName = "John Doe",
                    Password = "Random"
                },

            },
            new User
            {
                UserName = "John Doe",
                Password = "Random"
            }
        };
    }


    [Test]
    [TestCaseSource(nameof(AddUser_Handle_InvalidData_ThrowsException_TestData))]
    public void AddUser_Handle_InvalidData_ThrowsException(AddUserCommand request, Type expectedException)
    {
        #region Arrange
        #endregion

        #region Act
        var sut = new AddUserCommandHandler(_unitOfWorkFactory);
        Assert.ThrowsAsync(expectedException, async () => await sut.Handle(request, default).ConfigureAwait(false));
        #endregion
    }

    static IEnumerable<object[]> AddUser_Handle_InvalidData_ThrowsException_TestData()
    {
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new User
                {
                    UserName = string.Empty,
                    Password = "Random"
                },

            },
            typeof(ArgumentException)
        };

        yield return new object[]
        {
            new AddUserCommand
            {
                User = new User
                {
                    Password = "Random"
                },

            },
            typeof(ArgumentNullException)
        };
        yield return new object[]
        {
            new AddUserCommand
            {
                User = new User
                {
                    UserName = "Random",
                    Password = string.Empty
                },

            },
            typeof(ArgumentException)
        };

        yield return new object[]
        {
            new AddUserCommand
            {
                User = new User
                {
                    UserName = "Random"
                },

            },
            typeof(ArgumentNullException)
        };
    }
}
