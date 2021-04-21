using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeService.Services
{
    public interface IRecipeService
    {
        Task PostRecipe(Recipe recipe);
        List<Recipe> GetRecipes(int index);
    }
}
