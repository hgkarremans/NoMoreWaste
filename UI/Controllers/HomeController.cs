using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class HomeController : Controller
{
    private readonly IMealBoxRepository _mealBoxRepository;
    private readonly ICanteenRepository _canteenRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICanteenWorkerRepository _canteenWorkerRepository;
    

    public HomeController(IMealBoxRepository mealBoxRepository, ICanteenRepository canteenRepository, IStudentRepository studentRepository, IProductRepository productRepository, ICanteenWorkerRepository canteenWorkerRepository)
    {
        _mealBoxRepository = mealBoxRepository;
        _canteenRepository = canteenRepository;
        _studentRepository = studentRepository;
        _productRepository = productRepository;
        _canteenWorkerRepository = canteenWorkerRepository;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var mealBoxes = await _mealBoxRepository.GetAllAsync();
        return View(mealBoxes);
    } 
    
}