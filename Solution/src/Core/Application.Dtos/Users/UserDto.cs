namespace CoreSharp.Templates.Blazor.Application.Dtos.Users;

public sealed class UserDto
{
    // Properties 
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Guid TeacherId { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (UserDto)obj;
        return Id == other.Id
            && Email == other.Email
            && TeacherId == other.TeacherId;
    }

    public override int GetHashCode()
        => HashCode.Combine(Id, Email, TeacherId);
}
