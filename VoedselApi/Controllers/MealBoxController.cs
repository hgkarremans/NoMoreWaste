using Application;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoMoreWaste.Domain.DomainModels;

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
    public async Task<IActionResult> ReservateMealbox(int mealBoxId, int studentId)
    {
        try
        {
            var result = await _mealBoxRepository.ReservateMealBoxAsync(mealBoxId, studentId);
            return new OkObjectResult(result);
        }
        catch (NullReferenceException e)
        {
            return new NotFoundObjectResult(e.Message);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e.Message);
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