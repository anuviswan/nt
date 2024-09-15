using AuthService.Data.Database;
using AuthService.Data.Interfaces.Database;
using AuthService.Data.Interfaces.Entities;
using AuthService.Data.Interfaces.Repository;
using AuthService.Service.Command;
using AuthService.Service.Exceptions;

namespace AuthService.Service.Tests.Commands;

[TestFixture]
public class ChangePasswordHandlerTests
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
    [TestCaseSource(nameof(ChangePasswordHandler_Handle_ValidData_Success_TestData))]
    public async Task ChangePasswordHandler_Handle_ValidData_Success(ChangePasswordCommand command)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);

        _userRepository.ValidateUserAsync(Arg.Any<User>()).Returns(_ => new User
        {
            UserName = "Test",
            Password = "Test",
        });
        _userRepository.ChangePasswordAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(_ => true);
        #endregion


        #region Act
        var sut = new ChangePasswordCommandHandler(_unitOfWorkFactory);
        var result = await sut.Handle(command, default).ConfigureAwait(false);
        #endregion

        #region Assert
        Assert.IsTrue(result);
        #endregion
    }

    static IEnumerable<object[]> ChangePasswordHandler_Handle_ValidData_Success_TestData()
    {
        yield  return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = "Test1",
                OldPassword = "Test",
                UserName = "Test"
            }
        };
    }


    [Test]
    [TestCaseSource(nameof(ChangePasswordHandler_Handle_InvalidOldPassword_Exception_TestData))]
    public void  ChangePasswordHandler_Handle_InvalidOldPassword_Exception(ChangePasswordCommand command)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);

        _userRepository.ValidateUserAsync(Arg.Any<User>()).Returns<User?>(_ => null);
        _userRepository.ChangePasswordAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(_ => true);
        #endregion


        #region Act And Assert
        var sut = new ChangePasswordCommandHandler(_unitOfWorkFactory);
        Assert.ThrowsAsync<IncorrectPasswordException>(async ()=> await sut.Handle(command, default).ConfigureAwait(false));
        #endregion

    }

    static IEnumerable<object[]> ChangePasswordHandler_Handle_InvalidOldPassword_Exception_TestData()
    {
        yield return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = "Test1",
                OldPassword = "Test",
                UserName = "Test"
            }
        };
    }



    [Test]
    [TestCaseSource(nameof(ChangePasswordHandler_Handle_InvalidData_Exception_TestData))]
    public void ChangePasswordHandler_Handle_InvalidData_Exception(ChangePasswordCommand command, Type exceptionType)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);

        _userRepository.ValidateUserAsync(Arg.Any<User>()).Returns<User?>(_ => null);
        _userRepository.ChangePasswordAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(_ => true);
        #endregion


        #region Act And Assert
        var sut = new ChangePasswordCommandHandler(_unitOfWorkFactory);
        Assert.ThrowsAsync(exceptionType,async () => await sut.Handle(command, default).ConfigureAwait(false));
        #endregion

    }

    static IEnumerable<object[]> ChangePasswordHandler_Handle_InvalidData_Exception_TestData()
    {
        yield return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = string.Empty,
                OldPassword = "Test",
                UserName = "Test"
            },
            typeof(ArgumentException)
        };

        yield return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = "Test",
                OldPassword = string.Empty,
                UserName = "Test"
            },
            typeof(ArgumentException)
        };

        yield return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = "Test",
                OldPassword = "Test",
                UserName = string.Empty
            },
            typeof(ArgumentException)
        };

        yield return new object[]
       {
            new ChangePasswordCommand
            {
                OldPassword = "Test",
                UserName = "Test"
            },
            typeof(ArgumentNullException)
       };

        yield return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = "Test",
                UserName = "Test"
            },
            typeof(ArgumentNullException)
        };

        yield return new object[]
        {
            new ChangePasswordCommand
            {
                NewPassword = "Test",
                OldPassword = "Test",
            },
            typeof(ArgumentNullException)
        };
    }
}
