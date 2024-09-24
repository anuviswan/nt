namespace MovieService.Service.Interfaces.Dtos;

public record MovieDto
{
    public string? Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public PersonDto? Director { get; set; }
}


public record PersonDto
{
    public string Name { get; set; }

}