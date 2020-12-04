using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Domain.Entities.Dto
{
    public class MovieDetailedDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PlotSummary { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Language { get; set; }
        public List<string> Actors { get; set; } = new List<string>();
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }

    public record ReviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> UpvotedBy { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> DownvotedBy { get; set; } = Enumerable.Empty<string>();
        public UserDto Author { get; set; }
    }

    public record UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
