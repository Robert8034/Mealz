using RecipeService.DAL;
using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeService.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task PostRecipe(Recipe recipe)
        {
            recipe.RecipeId = Guid.NewGuid();
            await _recipeRepository.Save(recipe);
        }

        public List<Recipe> GetRecipes(int index)
        {
            return _recipeRepository.GetRange(index, 10);
        }
    }
}
