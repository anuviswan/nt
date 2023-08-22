using AuthService.Data.Database;
using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using AuthService.Service.Command;

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
        var _unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
        var _unitOfWork = Substitute.For<IUnitOfWork>();
        var _userRepository = Substitute.For<IUserRepository>();
    }

    [Test]
    [TestCaseSource(nameof(ChangePasswordHandler_Handle_ValidData_Success_TestData))]
    public async Task ChangePasswordHandler_Handle_ValidData_Success(ChangePasswordCommand command)
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);

        _userRepository.ValidateUserAsync(Arg.Any<Domain.Entities.User>()).Returns(_ => new User
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
}
