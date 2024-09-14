using MovieService.Data.Interfaces.Entities;

namespace MovieService.Data.Interfaces.Services;

public interface IMovieCrudService
{
    Task CreateAsync(Movie newBook);
}
