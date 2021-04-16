using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeService.Models;
using RecipeService.Services;

namespace RecipeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("postRecipe")]
        public async Task<IActionResult> PostRecipe([FromBody] Recipe recipe)
        {
            await _recipeService.PostRecipe(recipe);

            return Ok();
        }
    }
}
