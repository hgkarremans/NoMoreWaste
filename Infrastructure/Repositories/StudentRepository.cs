using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Application.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Student> GetByIdAsync(string id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null)
        {
            throw new Exception("Student not found");
        }
        return student;
    }
    public async Task<Student> GetByEmailAsync(string email)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
        if (student == null)
        {
            throw new Exception("Student not found");
        }
        return student;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> CreateAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return student;
    }
    
}