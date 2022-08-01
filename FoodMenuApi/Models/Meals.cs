namespace FoodMenuApi.Models
{
    public class Meals
    {
        public Meal Meal { get; set; }
        public IEnumerable<Meal> MealsByCategory { get; set; }
        public IEnumerable<Meal> MealsByArea { get; set; }
    }
}
