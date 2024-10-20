using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.Views.Mealbox;

namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICanteenWorkerRepository _canteenWorkerRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<HomeController> _logger;


    public HomeController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository,
        IStudentRepository studentRepository, IProductRepository productRepository,
        ICanteenWorkerRepository canteenWorkerRepository,
        UserManager<IdentityUser> userManager,
        ILogger<HomeController> logger
    )
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _productRepository = productRepository;
        _canteenWorkerRepository = canteenWorkerRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string location)
{
    var mealBoxes = await _mealBoxRepository.GetAllAsync();
    var sortedMealBoxes = mealBoxes.OrderBy(mb => mb.PickUpDate).ToList();

    foreach (var mealBox in sortedMealBoxes)
    {
        mealBox.Canteen = await _canteenRepository.GetByIdAsync(mealBox.CanteenId);
    }

    if (!string.IsNullOrEmpty(location))
    {
        sortedMealBoxes = sortedMealBoxes.Where(mb => mb.Canteen.Name.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    var canteens = await _canteenRepository.GetAllAsync();
    ViewBag.Canteens = new SelectList(canteens, "Name", "Name");

    var canteenName = sortedMealBoxes.FirstOrDefault()?.Canteen?.Name ?? "Unknown Canteen";
    ViewBag.CanteenName = canteenName;
    return View(sortedMealBoxes);
}
}