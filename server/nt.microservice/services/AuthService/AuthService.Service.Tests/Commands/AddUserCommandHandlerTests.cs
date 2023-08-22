using AuthService.Data.Database;
using AuthService.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public void AddUser_Handle_ValidData_Success()
    {
        #region Arrange
        _unitOfWorkFactory.CreateUnitOfWork().Returns(_ => _unitOfWork);
        _unitOfWork.UserRepository.Returns(_ => _userRepository);
        #endregion

        #region Act

        #endregion

        #region Assert
        #endregion
    }
}
