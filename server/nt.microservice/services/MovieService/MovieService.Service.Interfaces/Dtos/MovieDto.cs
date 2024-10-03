namespace MovieService.Service.Interfaces.Dtos;

public record MovieDto
{
    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public Dictionary<string,List<PersonDto>>? Crew { get; set; }

    public List<PersonDto>? Cast { get; set; }
}


public record PersonDto(string Name);
