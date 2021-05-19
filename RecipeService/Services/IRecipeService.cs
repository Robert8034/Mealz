using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeService.Services
{
    public interface IRecipeService
    {
        /// <summary>
        /// Saves a recipe to the database
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        Task PostRecipe(Recipe recipe);
        /// <summary>
        /// Gets corrosponding recipes starting from the given index. Gets the 10 recipes or the maximum available recipes from the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        List<Recipe> GetRecipes(int index);
    }
}
