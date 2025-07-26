namespace ReviewService.Application.Services.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a DateTime to a Unix timestamp.
    /// </summary>
    /// <param name="dateTime">The DateTime to convert.</param>
    /// <returns>The Unix timestamp as a long.</returns>
    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
    /// <summary>
    /// Converts a Unix timestamp to a DateTime.
    /// </summary>
    /// <param name="unixTimestamp">The Unix timestamp to convert.</param>
    /// <returns>The DateTime representation of the Unix timestamp.</returns>
    public static DateTime FromUnixTimestamp(long unixTimestamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
    }
}
