using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle
{
    public record SearchMovieByTitleRequest
    {
        [Required(ErrorMessage = "Search term missing")]
        [MinLength(3, ErrorMessage = "Search term should be minimum 3 characters")]
        public string SearchString { get; init; }
        public MovieSearchCriteria Criteria { get; init; }
    }

    public record MovieSearchCriteria
    {
        public int MaxItems { get; init; }
    }
}
