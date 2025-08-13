namespace MovieService.Service.Interfaces.Dtos;

public record MovieDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;

    public string Synopsis { get; set; } = string.Empty;

    public string? MovieLanguage { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public Dictionary<string,List<PersonDto>>? Crew { get; set; }

    public List<PersonDto>? Cast { get; set; }
}


public record PersonDto(string Name);
