﻿namespace ReviewService.Presenation.Api.Models;

public record CreateReviewRequest
{
    public Guid MovieId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string UserName { get; set; } = string.Empty;
}



