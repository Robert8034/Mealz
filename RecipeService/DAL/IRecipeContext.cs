using MongoDB.Driver;
using RecipeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeService.DAL
{
    public interface IRecipeContext
    {
        IMongoCollection<Recipe> Recipes { get; }
    }
}
