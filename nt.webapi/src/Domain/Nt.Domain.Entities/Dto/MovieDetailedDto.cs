﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.Entities.Dto
{
    public record MovieDetailedDto:MovieEntity
    {
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }

    public record ReviewDto
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<string> UpvotedBy { get; init; } = Enumerable.Empty<string>();
        public IEnumerable<string> DownvotedBy { get; init; } = Enumerable.Empty<string>();
        public UserDto Author { get; set; }
        public double Rating { get; init; }
    }

    public record UserDto
    {
        public string Id { get; init; }
        public string UserName { get; init; }
        public string DisplayName { get; init; }
    }
}
