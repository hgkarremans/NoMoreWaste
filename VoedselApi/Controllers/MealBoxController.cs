using Application;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoMoreWaste.Domain.DomainModels;
using Core.Domain.Exceptions;

namespace VoedselApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealBoxController
{
    private readonly IMealBoxRepository _mealBoxRepository;

    public MealBoxController(IMealBoxRepository mealBoxRepository)
    {
        _mealBoxRepository = mealBoxRepository;
    }

    [HttpGet]
    public IEnumerable<MealBox> GetMealBoxes()
    {
        return _mealBoxRepository.GetAllAsync().Result;
    }

    [HttpGet("{id}", Name = "GetMealBox")]
    public MealBox GetMealBox([FromRoute] int id)
    {
        return _mealBoxRepository.GetByIdAsync(id).Result;
    }

    [HttpPatch("{mealBoxId}", Name = "ReservateMealbox")]
    public IActionResult ReservateMealbopx(int mealBoxId, int studentId)
    {
        try
        {
            return new OkObjectResult(_mealBoxRepository.ReservateMealBoxAsync(mealBoxId, studentId));
        }
        catch (NullReferenceException e)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}", Name = "DeleteMealBox")]
    public void DeleteMealBox([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}", Name = "UpdateMealBox")]
    public void UpdateMealBox([FromBody] MealBox mealBox)
    {
        throw new NotImplementedException();
    }
}