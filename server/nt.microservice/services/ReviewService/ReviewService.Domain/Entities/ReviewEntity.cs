namespace ReviewService.Domain.Entities;
public class ReviewEntity : IEntity
{
    private string _title = null!;
    private string _movieId = null!;
    private string _content = null!;
    private string _author = null!;
    private int _rating = 0;

    public Guid Id { get; set; }

    public string MovieId
    {
        get => _movieId;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("MovieId cannot be null or empty.");
            }
            _movieId = value;
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }
            _title = value;
        }
    }

    public string Content
    {
        get => _content;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Content cannot be null or empty.");
            }
            _content = value;
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Author cannot be null or empty.");
            }
            _author = value;
        }
    }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public IEnumerable<string> UpvotedBy { get; set; } = [];
    public IEnumerable<string> DownvotedBy { get; set; } = [];

    public int Rating
    {
        get => _rating;
        set
        {
            if (value is < 0 or > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(Rating), "Rating must be between 0 and 5.");
            }
            _rating = value;
        }
    }
}
