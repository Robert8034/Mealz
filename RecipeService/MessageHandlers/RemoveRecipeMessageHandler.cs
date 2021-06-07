using RecipeService.Services;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeService.MessageHandlers
{
    public class RemoveRecipeMessageHandler : IMessageHandler<string>
    {
        private readonly IRecipeService _recipeService;

        public RemoveRecipeMessageHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public Task HandleMessageAsync(string messageType, string message)
        {
            var postId = Guid.Parse(message);

            _recipeService.RemoveRecipe(postId);

            return Task.CompletedTask;
        }
    }
}
