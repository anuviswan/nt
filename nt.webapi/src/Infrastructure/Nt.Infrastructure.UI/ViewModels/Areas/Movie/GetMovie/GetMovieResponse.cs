using System;
using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.GetMovie
{
    public record GetMovieResponse
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Language { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string Genre { get; init; }
        public IEnumerable<string> Tags { get; init; }
        public double Rating { get; init; }
        public IEnumerable<ReviewItem> Reviews { get; init; }
    }

    public record ReviewItem
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Detail { get; init; }
        public double Rating { get; init; }
        public int UpVotes { get; init; }
        public int DownVotes { get; init; }
        public UserItem Author { get; init; }
    }

    public record UserItem
    {
        public string Id { get; init; }
        public string UserName { get; init; }
        public string DisplayName{get;init;}
    }
}
