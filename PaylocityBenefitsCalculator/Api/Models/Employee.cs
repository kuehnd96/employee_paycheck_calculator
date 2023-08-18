namespace Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != this.GetType()) return false;

        var otherEmployee = obj as Employee;

        if (otherEmployee == null) return false;

        var areTheSame = this.Id == otherEmployee.Id
            && this.FirstName == otherEmployee.FirstName
            && this.LastName == otherEmployee.LastName
            && this.DateOfBirth == otherEmployee.DateOfBirth
            && this.Salary == otherEmployee.Salary
            && this.Dependents.Count == otherEmployee.Dependents.Count;
        
        if (!areTheSame) return false;

        foreach (var dependent in Dependents)
        {
            var otherDependent = otherEmployee
                .Dependents
                .FirstOrDefault(d => d.Id == dependent.Id);

            if (otherDependent == null) return false;

            if (!dependent.Equals(otherDependent)) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        // Note: I realize this probably should include all property values
        return this.Id.GetHashCode();
    }
}
