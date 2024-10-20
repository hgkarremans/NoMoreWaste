using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace Application.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Student> GetByIdAsync(int id)
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
        var studentToUpdate = await _context.Students.FirstOrDefaultAsync(x => x.Id == student.Id);

        if (studentToUpdate == null)
        {
            throw new Exception("Student not found");
        }

        // Update the properties of the tracked entity
        _context.Entry(studentToUpdate).CurrentValues.SetValues(student);

        // Save changes to the context
        await _context.SaveChangesAsync();

        return studentToUpdate;
    }


    public async Task<Student> DeleteAsync(Student student)
    {
        var studentToDelete = await _context.Students.FirstOrDefaultAsync(x => x.Id == student.Id);
        if (studentToDelete == null)
        {
            throw new Exception("Student not found");
        }
        _context.Students.Remove(studentToDelete);
        await _context.SaveChangesAsync();
        return studentToDelete;
    }
    public async Task<City> GetCityAsync(int userId)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == userId);
        if (student == null)
        {
            throw new Exception("Student not found");
        }
        return student.City;
    }
    
}