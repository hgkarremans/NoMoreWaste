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


    public MealBoxController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IProductRepository productRepository,
        ICanteenWorkerRepository canteenWorkerRepository, UserManager<IdentityUser> userManager)
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
        var mealBoxes = await _mealBoxRepository.GetAllAvailableAsync();
        return View(mealBoxes);
    }

    public async Task<IActionResult> ReservateMealBox(int mealBoxId)
    {
        try
        {
            
            var userIdentity = await _userManager.GetUserAsync(User);
            var user = _studentRepository.GetByEmailAsync(userIdentity.Email);
            var mealBox = _mealBoxRepository.ReservateMealBoxAsync(mealBoxId, user.Id);
            TempData["SuccessMessage"] = "Meal box reserved successfully!";
            return RedirectToAction("Index", "home");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}