namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != this.GetType()) return false;

        var otherDependent = obj as Dependent;

        if (otherDependent == null) return false;

        return this.Id == otherDependent.Id
            && this.FirstName == otherDependent.FirstName
            && this.LastName == otherDependent.LastName
            && this.DateOfBirth == otherDependent.DateOfBirth
            && this.Relationship == otherDependent.Relationship
            && this.EmployeeId == otherDependent.EmployeeId;
    }

    public override int GetHashCode()
    {
        // Note: I realize this probably should include all property values
        return this.Id.GetHashCode();
    }
}
