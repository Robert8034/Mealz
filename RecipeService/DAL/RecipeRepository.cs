using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
