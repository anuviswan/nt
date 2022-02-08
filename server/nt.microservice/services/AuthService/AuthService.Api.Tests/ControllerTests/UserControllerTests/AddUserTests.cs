using AuthService.Api.Controllers;
using AuthService.Api.ViewModels.AddUser;
using AuthService.Domain.Entities;
using AuthService.Service.Command;
using FluentAssertions;
using MapsterMapper;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AuthService.Api.Tests.ControllerTests.UserControllerTests;

[TestFixture]
public class AddUserTests:ControllerTestsBase
{
    private IMediator? _mediator;
    private IMapper? _mapper;

    [SetUp]
    public void Initialize()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
    }

    [TestCaseSource(nameof(AddUser_ValidData_ResponseStatus_200_TestData))]
    public async Task AddUser_ValidData_ResponseStatus_200(AddUserRequestViewModel request,AddUserResponseViewModel expectedResult)
    {
        // Arrange
        _mapper!.Map<User>(Arg.Any<AddUserRequestViewModel>()).Returns(r=> new User 
        {
            UserName = ((AddUserRequestViewModel)r[0]).UserName,
            Password = ((AddUserRequestViewModel)r[0]).Password
        });

        _mapper!.Map<AddUserResponseViewModel>(Arg.Any<User>()).Returns(r => new AddUserResponseViewModel
        {
            UserName = ((User)r[0]).UserName,
            Id = Guid.Empty
        });

        _mediator!.Send(Arg.Any<AddUserCommand>()).Returns(r => new User 
        {
            UserName = ((AddUserCommand)r[0]).User.UserName,
            Password = ((AddUserCommand)r[0]).User.Password,
        });

        // Act
        var sut = new UserController(_mapper!,_mediator!);
        var actualResult = await sut.AddUserAsync(request);

        // Assert
        actualResult.Should().NotBeNull();
        var okObjectResult = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<AddUserResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
        await _mediator!.Received(1).Send(Arg.Any<AddUserCommand>());
        _mapper!.Received(1).Map<User>(Arg.Any<AddUserRequestViewModel>());
        _mapper!.Received(1).Map<AddUserResponseViewModel>(Arg.Any<User>());
    }

    static IEnumerable<object> AddUser_ValidData_ResponseStatus_200_TestData()
    {
        yield return new object[]
        {
            new AddUserRequestViewModel
            {
                UserName = "john.doe",
                Password = "12345678"
            },
            new AddUserResponseViewModel
            {
                UserName = "john.doe",
                Id = Guid.Empty
            }
        };
    }

    public void AddUser_ValidData_ResponseStatus_400()
    {

    }
}
