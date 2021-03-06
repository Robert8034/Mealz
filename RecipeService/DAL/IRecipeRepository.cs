using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeService.DAL
{
    public interface IRecipeRepository
    {
        Task Save(Recipe recipe);

        List<Recipe> GetRange(int lowerIndex, int count);

        Recipe GetRecipeById(Guid recipeId);

        Task RemoveRecipe(Recipe recipe);
    }
}
