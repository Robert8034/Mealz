using RecipeService.DAL;
using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeService.Test.MockServices
{
    public class MockRecipeRepository : IRecipeRepository
    {
        public List<Recipe> recipes;

        public MockRecipeRepository()
        {
            recipes = new List<Recipe>();
        }

        public List<Recipe> GetRange(int lowerIndex, int count)
        {
            if (recipes.Count < 10)
            {
                count = recipes.Count;
            }

            return recipes.GetRange(lowerIndex, count);
        }

        public Task Save(Recipe recipe)
        {
            recipes.Add(recipe);
            return Task.CompletedTask;
        }
    }
}
