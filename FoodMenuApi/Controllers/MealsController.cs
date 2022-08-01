using FoodMenuApi.Api;
using FoodMenuApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenuApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
            if (name == null || name == String.Empty)
            {
                return NotFound();
            }

            Meals meals = await _mealApi.GetMeals(name);
            if (meals == null)
            {
                return NotFound(name);
            }

            return Ok(meals);
        }
    }
}
