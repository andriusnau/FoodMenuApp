using FoodMenuApi.Api;
using FoodMenuApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenuApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly MealApi _mealApi;

        public MealsController(IConfiguration config)
        {
            _mealApi = new MealApi(config);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            Meals meals = await _mealApi.GetMeals(name);
            if (meals == null || meals.Meal == null)
            {
                return NotFound(name);
            }

            return Ok(meals);
        }
    }
}
