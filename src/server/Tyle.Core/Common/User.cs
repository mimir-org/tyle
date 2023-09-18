namespace Tyle.Core.Common;

public readonly struct User
{
    public string UserId { get; }
    public string? Name { get; }

    /// <summary>
    /// Creates a new user struct.
    /// </summary>
    /// <param name="userId">The user ID of the user.</param>
    /// <param name="name">The name of the user. This parameter is optional.</param>
    public User(string userId, string? name = null)
    {
        UserId = userId;
        Name = name;
    }

    public static bool operator ==(User left, User right)
    {
        return left.UserId == right.UserId;
    }

    public static bool operator !=(User left, User right)
    {
        return left.UserId != right.UserId;
    }
}
