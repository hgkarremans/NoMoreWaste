using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace Application;

public interface IStudentRepository
{
    Task<Student> GetByIdAsync(int id);
    Task<List<Student>> GetAllAsync();
    Task<Student> GetByEmailAsync(string email);
    Task<Student> CreateAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task<Student> DeleteAsync(Student student);
    Task<City> GetCityAsync(int id);
    
}