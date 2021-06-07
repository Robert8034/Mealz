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

        public Recipe GetRecipeById(Guid recipeId)
        {
            return _recipeRepository.GetRecipeById(recipeId);
        }

        public async Task RemoveRecipe(Guid recipeId)
        {
            var recipe = _recipeRepository.GetRecipeById(recipeId);

            await _recipeRepository.RemoveRecipe(recipe);
        }

    }
}
