using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Chef,Moderator,Admin")]
        [HttpPost("postRecipe")]
        public async Task<IActionResult> PostRecipe([FromBody] Recipe recipe)
        {
            if (recipe.UserId != Guid.Empty)
            {
                await _recipeService.PostRecipe(recipe);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("getRecipes")]
        public IActionResult GetRecipes([FromBody] int index)
        {
            var recipes = _recipeService.GetRecipes(index);

            return Ok(recipes);
        }

        [HttpPost("getRecipe")]
        public IActionResult GetRecipeById([FromBody] Guid recipeId)
        {
            if (recipeId != Guid.Empty)
            {
                return Ok(_recipeService.GetRecipeById(recipeId));
            }

            return BadRequest();
        }
    }
}
