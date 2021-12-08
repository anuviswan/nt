using Nt.Domain.Entities.User;

namespace Nt.Infrastructure.Tests.Helpers.TestData;
public static class Users
{
    public static IEnumerable<UserProfileEntity> GetUserCollection()
    {
        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 1),
            UserName = $"{nameof(UserProfileEntity.UserName)} 1",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 1",
            Bio = $"{nameof(UserProfileEntity.Bio)} 1",
            IsDeleted = false,
            Followers = new[]
            {
                "UserName 3"
            }
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 2),
            UserName = $"{nameof(UserProfileEntity.UserName)} 2",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 2",
            Bio = $"{nameof(UserProfileEntity.Bio)} 2",
            IsDeleted = false,
            Followers = new[]
            {
                "UserName 3"
            }
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 3),
            UserName = $"{nameof(UserProfileEntity.UserName)} 3",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 3",
            Bio = $"{nameof(UserProfileEntity.Bio)} 3",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 4),
            UserName = $"{nameof(UserProfileEntity.UserName)} 4",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 4",
            Bio = $"{nameof(UserProfileEntity.Bio)} 4",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 5),
            UserName = $"{nameof(UserProfileEntity.UserName)} 5",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 5",
            Bio = $"{nameof(UserProfileEntity.Bio)} 5",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 6),
            UserName = $"{nameof(UserProfileEntity.UserName)} 6",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 6",
            Bio = $"{nameof(UserProfileEntity.Bio)} 6",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 7),
            UserName = $"{nameof(UserProfileEntity.UserName)} 7",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 7",
            Bio = $"{nameof(UserProfileEntity.Bio)} 7",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 8),
            UserName = $"{nameof(UserProfileEntity.UserName)} 8",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 8",
            Bio = $"{nameof(UserProfileEntity.Bio)} 8",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };

        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 9),
            UserName = $"{nameof(UserProfileEntity.UserName)} 9",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 9",
            Bio = $"{nameof(UserProfileEntity.Bio)} 9",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockUserIdFormat, 10),
            UserName = $"{nameof(UserProfileEntity.UserName)} 10",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 10",
            Bio = $"{nameof(UserProfileEntity.Bio)} 10",
            IsDeleted = false,
            Followers = Enumerable.Empty<string>()
        };
    }

}
