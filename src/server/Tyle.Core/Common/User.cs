namespace Tyle.Core.Common;

public readonly struct User
{
    public string UserId { get; }
    public string? Name { get; }

    public User(string userId, string? name = null)
    {
        UserId = userId;
        Name = name;
    }
}
