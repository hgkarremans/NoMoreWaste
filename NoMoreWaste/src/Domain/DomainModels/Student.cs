namespace NoMoreWaste.Domain.DomainModels;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public int StudentNumber { get; set; }
}