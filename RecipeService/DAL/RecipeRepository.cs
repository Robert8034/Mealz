using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace RecipeService.DAL
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IRecipeContext _context;

        public RecipeRepository(IRecipeContext context)
        {
            _context = context;
        }

        public async Task Save(Recipe recipe)
        {
            await _context.Recipes.InsertOneAsync(recipe);
        }

        public List<Recipe> GetRange(int lowerIndex, int count)
        {
            var recipes = _context.Recipes.AsQueryable().ToList();

            var recipeCount = recipes.Count;

            if (recipeCount == 0 || lowerIndex > recipeCount) return new List<Recipe>();

            if (recipeCount < (lowerIndex + count))
            {
                count = recipeCount - lowerIndex;
            }

            return recipes.GetRange(lowerIndex, count);
        }

        public Recipe GetRecipeById(Guid recipeId)
        {
            return _context.Recipes.Find(e => e.RecipeId == recipeId).FirstOrDefault();
        }

        public async Task RemoveRecipe(Recipe recipe)
        {
            await _context.Recipes.DeleteOneAsync(e => e.RecipeId == recipe.RecipeId);
        }
    }
}
