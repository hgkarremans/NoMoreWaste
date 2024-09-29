using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class MealBoxController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICanteenWorkerRepository _canteenWorkerRepository;
    private readonly UserManager<IdentityUser> _userManager;
    

    public MealBoxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository, IStudentRepository studentRepository, IProductRepository productRepository, ICanteenWorkerRepository canteenWorkerRepository, UserManager<IdentityUser> userManager)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _productRepository = productRepository;
        _canteenWorkerRepository = canteenWorkerRepository;
        _userManager = userManager;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var mealBoxes = await _mealBoxRepository.GetAllAsync();
        return View(mealBoxes);
    }

    [Authorize]
[HttpPut]
public async Task<IActionResult> ReservateMealBox(string mealBoxId)
{
    var mealBox = await _mealBoxRepository.GetByIdAsync(mealBoxId);
    
    var user = await _userManager.GetUserAsync(User); 
    var userId = user.Id;
    

    mealBox.ReservedStudent = userId;
    await _mealBoxRepository.UpdateAsync(mealBox);
    
    return Ok(mealBox);
}
    
}