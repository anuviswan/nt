using AuthService.Data.Database;
using AuthService.Data.Repository;
using AuthService.Domain.Entities;
using AuthService.Service.Command;

namespace AuthService.Service.Tests.Commands;

[TestFixture]
public class ChangePasswordHandlerTests
{
    [SetUp]
    public void Initialize()
    {

    }

    [Test]
    [TestCaseSource(nameof(ChangePasswordHandler_Handle_ValidData_Success_TestData))]
    public async Task ChangePasswordHandler_Handle_ValidData_Success(ChangePasswordCommand command)
    {
        #region Arrange
        var uowFactory = Substitute.For<IUnitOfWorkFactory>();
        var uow = Substitute.For<IUnitOfWork>();
        var userRepository = Substitute.For<IUserRepository>();
        
        uowFactory.CreateUnitOfWork().Returns(_ => uow);
        uow.UserRepository.Returns(_ => userRepository);
        
        userRepository.ValidateUserAsync(Arg.Any<Domain.Entities.User>()).Returns(_ => new User
        {
            UserName = "Test",
            Password = "Test",
        });
        userRepository.ChangePasswordAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(_ => true);
        #endregion


        #region Act
        var sut = new ChangePasswordCommandHandler(uowFactory);
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
                UserName = "ABEC"
            }
        };
    }
}
