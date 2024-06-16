using MovieService.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Api.Tests.ControllerTests;

[TestClass]
public class MovieControllerTests : ControllerTestsBase
{
    public Task CreateMovie_ValidMovieTitle_Success(CreateMovieRequest createMovieRequest, CreateMovieResponse expectedResults)
    {
        
    }
}
