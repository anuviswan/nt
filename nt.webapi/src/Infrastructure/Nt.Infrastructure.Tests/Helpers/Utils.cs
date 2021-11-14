namespace Nt.Infrastructure.Tests.Helpers;
public static class Utils
{
    public static string TwoHundredCharacterString = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec";
    public static string MockMovieIdFormat = "MABCDEF{0}";
    public static string MockUserIdFormat = "UABCDEF{0}";
    public static readonly DateTime Date = new DateTime(2020, 8, 20);

    public static string GenerateUserIdString(long userId) => string.Format(MockUserIdFormat, userId);
}
