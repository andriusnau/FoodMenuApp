using FoodMenuApi.Models;
using Newtonsoft.Json;

namespace FoodMenuApi.Api
{
    public class MealApi : ApiClient, IMealApi
    {
        private const int CATEGORY_MEAL_COUNT = 5;
        private const int AREA_MEAL_COUNT = 3;

        private readonly IConfiguration _config;

        public MealApi(IConfiguration config) : base()
        {
            _config = config;
        }

        public async Task<Meals> GetMeals(string name)
        {
            Meals meals = new Meals();
            var mealFromApi = await GetMeal(Filter.ByName, name);
            if (mealFromApi != null)
            {
                Meal meal = mealFromApi.FirstOrDefault();
                var mealsByCategory = GetMeal(Filter.ByCategory, meal.StrCategory);
                var mealsByArea = GetMeal(Filter.ByArea, meal.StrArea);
                Task.WaitAll(mealsByCategory, mealsByArea);

                meals.Meal = meal;
                meals.MealsByCategory = mealsByCategory.Result.Take(CATEGORY_MEAL_COUNT);
                meals.MealsByArea = mealsByArea.Result.Take(AREA_MEAL_COUNT);
            }

            return meals;
        }

        private async Task<IEnumerable<Meal>> GetMeal(Filter filter, string prop)
        {
            string url = Url(filter, prop);
            var meal = await GetData(url);
            if (meal == null)
            {
                throw new Exception("No meal found");
            }

            return meal;
        }

        private async Task<IEnumerable<Meal>> GetData(string url)
        {
            string ApiResult = await GetRequest(url);
            if (ApiResult == null)
            {
                throw new Exception("Api not working");
            }

            var result = JsonConvert.DeserializeObject<MealApiResult>(ApiResult);
            if (result == null && result.Meals.Count == 0)
            {
                throw new Exception("No meal found");
            }

            return result.Meals;
        }

        private string Url(Filter filter, string prop)
        {
            string url = _config.GetSection("MealApiUrl").Value;
            switch (filter)
            {
                case Filter.ByName:
                    url += $"search.php?s={prop}";
                    break;
                case Filter.ByCategory:
                    url += $"filter.php?c={prop}";
                    break;
                case Filter.ByArea:
                    url += $"filter.php?a={prop}";
                    break;
            }
            return url;
        }
    }
}
