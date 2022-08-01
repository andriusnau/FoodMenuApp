using FoodMenuApi.Models;

namespace FoodMenuApi.Api
{
    public interface IMealApi
    {
        public Task<Meals> GetMeals(string name);
    }
}
