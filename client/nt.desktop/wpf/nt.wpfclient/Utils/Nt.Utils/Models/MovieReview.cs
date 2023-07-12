using Nt.Utils.Interfaces;

namespace Nt.Utils.Models;

public class MovieReview:IHasId
{
    public Movie Movie { get; set; }
    public User Author { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Id { get; set; }
    public double Rating { get; set; }
}
