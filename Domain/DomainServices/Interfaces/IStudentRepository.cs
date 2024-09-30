using NoMoreWaste.Domain.DomainModels;

namespace Application;

public interface IStudentRepository
{
    Task<Student> GetByIdAsync(string id);
    Task<List<Student>> GetAllAsync();
    Task<Student> GetByEmailAsync(string email);
    Task<Student> CreateAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task<Student> DeleteAsync(Student student);
    
}