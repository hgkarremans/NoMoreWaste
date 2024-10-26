using Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> Index(string location, NoMoreWaste.Domain.DomainModels.Enums.MealType? mealType)
    {
        var mealBoxes = await _mealBoxRepository.GetAllAsync();
        var sortedMealBoxes = mealBoxes.OrderBy(mb => mb.PickUpDate).ToList();

        foreach (var mealBox in sortedMealBoxes)
        {
            mealBox.Canteen = await _canteenRepository.GetByIdAsync(mealBox.CanteenId);
        }

        var userIdentity = await _userManager.GetUserAsync(User);
        if (userIdentity != null && string.IsNullOrEmpty(location))
        {
            if (User.IsInRole("student"))
            {
                if (userIdentity.UserName != null)
                {
                    var user = await _studentRepository.GetByEmailAsync(userIdentity.UserName);
                    var city = await _studentRepository.GetCityAsync(user.Id);
                    var canteen = await _canteenRepository.GetByCityAsync(city.ToString());
                    ViewBag.SelectedLocation = canteen.Name;
                }
                else
                {
                    throw new ArgumentNullException(nameof(userIdentity.UserName), "UserName is null");
                }
            }
            else
            {
                if (userIdentity.UserName != null)
                {
                    var canteenId = await _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);
                    var canteen = await _canteenRepository.GetByIdAsync(canteenId);
                    ViewBag.SelectedLocation = canteen.Name;
                }
                else
                {
                    throw new ArgumentNullException(nameof(userIdentity.UserName), "UserName is null");
                }
            }
        }


        // Filter by location
        if (!string.IsNullOrEmpty(location) && location != "All Locations")
        {
            sortedMealBoxes = sortedMealBoxes
                .Where(mb => mb.Canteen != null && mb.Canteen.Name.Equals(location, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        // Filter by MealType
        if (mealType.HasValue)
        {
            sortedMealBoxes = sortedMealBoxes.Where(mb => mb.MealType == mealType.Value).ToList();
            ViewBag.SelectedMealType = mealType.Value;
        }
        else
        {
            ViewBag.SelectedMealType = ""; // This sets the default to an empty string
        }

        var canteens = await _canteenRepository.GetAllAsync();
        ViewBag.Canteens = new SelectList(canteens, "Name", "Name");

        // Pass all the meal types to the view
        ViewBag.MealTypes = Enum.GetValues(typeof(NoMoreWaste.Domain.DomainModels.Enums.MealType))
            .Cast<NoMoreWaste.Domain.DomainModels.Enums.MealType>();

        return View(sortedMealBoxes);
    }


    [HttpGet]
    public async Task<IActionResult> GetMyMealBoxes()
    {
        try
        {
            var userIdentity = await _userManager.GetUserAsync(User);
            if (userIdentity == null || userIdentity.UserName == null)
            {
                throw new ArgumentNullException(nameof(userIdentity.UserName), "UserName is null");
            }

            var user = await _studentRepository.GetByEmailAsync(userIdentity.UserName);
            var mealBoxes = await _mealBoxRepository.GetMyMealboxes(user.Id);
            foreach (var mealBox in mealBoxes)
            {
                mealBox.Canteen = await _canteenRepository.GetByIdAsync(mealBox.CanteenId);
            }

            var canteenName = mealBoxes.FirstOrDefault()?.Canteen?.Name ?? "Unknown Canteen";
            ViewBag.CanteenName = canteenName;
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
            var canteenName = await _canteenRepository.GetByIdAsync(mealBox.CanteenId);
            ViewBag.CanteenName = canteenName.Name;
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
            if (userIdentity == null || userIdentity.UserName == null)
            {
                throw new ArgumentNullException(nameof(userIdentity.UserName), "User identity or username is null");
            }

            var canteenId = await _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);
            var mealBoxes = await _mealBoxRepository.GetCanteenMealboxesAsync(canteenId);
            var sortedMealBoxes = mealBoxes.OrderBy(mb => mb.PickUpDate).ToList();
            foreach (var mealBox in sortedMealBoxes)
            {
                mealBox.Canteen = await _canteenRepository.GetByIdAsync(mealBox.CanteenId);
            }

            var canteenName = sortedMealBoxes.FirstOrDefault()?.Canteen?.Name ?? "Unknown Canteen";
            ViewBag.CanteenName = canteenName;
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
            if (userIdentity == null || userIdentity.UserName == null)
            {
                throw new ArgumentNullException(nameof(userIdentity.UserName), "User identity or username is null");
            }

            var user = await _studentRepository.GetByEmailAsync(userIdentity.UserName);
            await _mealBoxRepository.ReservateMealBoxAsync(mealBoxId, user.Id);
            TempData["SuccessMessage"] = "Meal box reserved successfully!";
            return RedirectToAction("Index", "Mealbox");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Mealbox");
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
        if (userIdentity == null || userIdentity.UserName == null)
        {
            throw new ArgumentNullException(nameof(userIdentity.UserName), "User identity or username is null");
        }

        var canteenId = await _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);
        var canteen = await _canteenRepository.GetByIdAsync(canteenId);

        var selectedProducts = new List<Product>();
        foreach (var productId in viewModel.SelectedProducts!)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            selectedProducts.Add(product);
        }

        var isWarmFood = viewModel.IsWarmFood;

        if (isWarmFood && !canteen.IsWarmFood)
        {
            TempData["ErrorMessage"] = "The canteen does not support warm food.";
            return View(viewModel);
        }

        var mealBox = new MealBox
        {
            Name = viewModel.Name,
            City = canteen.City,
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
        return RedirectToAction("Index", "Mealbox");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateMealBox(int mealBoxId)
    {
        var mealBox = await _mealBoxRepository.GetByIdAsync(mealBoxId);
        var viewModel = new MealBoxViewModel()
        {
            Name = mealBox.Name,
            PickUpDate = mealBox.PickUpDate,
            ExpireDate = mealBox.ExpireDate,
            EighteenPlus = mealBox.EighteenPlus,
            IsWarmFood = mealBox.IsWarmFood,
            Price = mealBox.Price,
            MealType = mealBox.MealType,
            Products = mealBox.Products.ToList(),
            SelectedProducts = mealBox.Products.Select(p => p.Id).ToList()
        };
        ViewBag.MealBoxId = mealBoxId;
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMealBox(int id, MealBoxViewModel viewModel)
    {
        try
        {
            var mealBox = await _mealBoxRepository.GetByIdAsync(id);


            // Apply updates from the view model
            mealBox.Name = viewModel.Name;
            mealBox.PickUpDate = viewModel.PickUpDate;
            mealBox.ExpireDate = viewModel.ExpireDate;
            mealBox.EighteenPlus = viewModel.EighteenPlus;
            mealBox.IsWarmFood = viewModel.IsWarmFood;
            mealBox.Price = viewModel.Price;
            mealBox.MealType = viewModel.MealType;


            var userIdentity = await _userManager.GetUserAsync(User);
            if (userIdentity == null || userIdentity.UserName == null)
            {
                throw new ArgumentNullException(nameof(userIdentity.UserName), "User identity or username is null");
            }

            var canteenId = await _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);
            var canteen = await _canteenRepository.GetByIdAsync(canteenId);

            var isWarmFood = viewModel.IsWarmFood;

            if (isWarmFood && !canteen.IsWarmFood)
            {
                TempData["ErrorMessage"] = "The canteen does not support warm food.";
                return View(viewModel);
            }

            var selectedProducts = new List<Product>();
            foreach (var productId in viewModel.SelectedProducts!)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                selectedProducts.Add(product);
            }

            mealBox.Products = selectedProducts;

            await _mealBoxRepository.UpdateAsync(mealBox);
            TempData["SuccessMessage"] = "Meal box updated successfully!";
            return RedirectToAction("Index", "Mealbox");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Mealbox");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMealBox(int mealBoxId)
    {
        try
        {
            var mealBox = await _mealBoxRepository.GetByIdAsync(mealBoxId);


            await _mealBoxRepository.DeleteAsync(mealBox);
            TempData["SuccessMessage"] = "Meal box deleted successfully!";
            return RedirectToAction("Index", "Mealbox");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Mealbox");
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetReservedMealboxesForCanteen()
    {
        try
        {
            var userIdentity = await _userManager.GetUserAsync(User);
            if (userIdentity == null || userIdentity.UserName == null)
            {
                throw new ArgumentNullException(nameof(userIdentity.UserName), "User identity or username is null");
            }

            var canteenId = await _canteenWorkerRepository.GetCanteenByUserEmail(userIdentity.UserName);
            var mealBoxes = await _mealBoxRepository.GetReservedMealboxesForCanteens(canteenId);
            var sortedMealBoxes = mealBoxes.OrderBy(mb => mb.PickUpDate).ToList();
            foreach (var mealBox in sortedMealBoxes)
            {
                mealBox.Canteen = await _canteenRepository.GetByIdAsync(mealBox.CanteenId);
            }

            var canteenName = sortedMealBoxes.FirstOrDefault()?.Canteen?.Name ?? "Unknown Canteen";
            ViewBag.CanteenName = canteenName;
            return View("ReservedCanteenMealboxes", sortedMealBoxes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}