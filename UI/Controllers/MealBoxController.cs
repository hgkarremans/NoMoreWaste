using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoMoreWaste.Domain.DomainModels;
using UI.Models;

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

    [HttpGet]
    public async Task<IActionResult> GetMyMealBoxes()
    {
        try
        {
            var userIdentity = await _userManager.GetUserAsync(User);
            var user = await _studentRepository.GetByEmailAsync(userIdentity.UserName);
            var mealBoxes = await _mealBoxRepository.GetMyMealboxes(user.Id);
            return View("MyMealboxes", mealBoxes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(int mealBoxId)
    {
        try
        {
            var mealBox = await _mealBoxRepository.GetByIdAsync(mealBoxId);
            return View("MealBox", mealBox);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCanteenMealboxes()
    {
        try
        {
            var userIdentity = await _userManager.GetUserAsync(User);
            var canteenId = _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);
            var mealBoxes = await _mealBoxRepository.GetCanteenMealboxesAsync(canteenId);
            var sortedMealBoxes = mealBoxes.OrderBy(mb => mb.PickUpDate).ToList();
            return View("CanteenMealboxes", sortedMealBoxes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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

    [HttpGet]
    public async Task<IActionResult> CreateMealBox()
    {
        var products = await _productRepository.GetAllAsync();
        var viewModel = new MealBoxViewModel()
        {
            Products = products
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMealBox(MealBoxViewModel viewModel)
    {
        viewModel.Products = await _productRepository.GetAllAsync();

        if (!ModelState.IsValid)
        {
            // Log ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(viewModel);
        }

        var userIdentity = await _userManager.GetUserAsync(User);
        var canteenId = _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);

        // Retrieve existing products from the database
        var selectedProducts = new List<Product>();
        foreach (var productId in viewModel.SelectedProducts)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                selectedProducts.Add(product);
            }
        }

        var mealBox = new MealBox
        {
            Name = viewModel.Name,
            PickUpDate = viewModel.PickUpDate,
            ExpireDate = viewModel.ExpireDate,
            EighteenPlus = viewModel.EighteenPlus,
            Price = viewModel.Price,
            MealType = viewModel.MealType,
            CanteenId = canteenId,
            Products = selectedProducts
        };

        await _mealBoxRepository.CreateAsync(mealBox);
        TempData["SuccessMessage"] = "Meal box created successfully!";
        return RedirectToAction("Index", "Home");
    }
}