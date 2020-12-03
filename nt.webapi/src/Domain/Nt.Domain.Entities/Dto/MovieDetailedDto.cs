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
        public List<string> Actors { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }

    public record ReviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> UpvotedBy { get; set; }
        public IEnumerable<string> DownvotedBy { get; set; }
        public UserDto Author { get; set; }
    }

    public record UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
