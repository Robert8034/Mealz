using NUnit.Framework;
using RecipeService.Test.MockServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeService.Test.UnitTests
{
    [TestFixture]
    public class RecipeServiceTests
    {
        [Test]
        public void PostRecipeSuccesTest()
        {
            //Arrange
            MockRecipeRepository mockRecipeRepository = new MockRecipeRepository();
            Services.RecipeService recipeService = new Services.RecipeService(mockRecipeRepository);

            //Act
            _ = recipeService.PostRecipe(new Models.Recipe { Content = "Hallo", Title = "Title" });

            //Assert
            Assert.AreEqual(1, mockRecipeRepository.recipes.Count);
            Assert.AreEqual("Hallo", mockRecipeRepository.recipes[0].Content);
            Assert.AreEqual("Title", mockRecipeRepository.recipes[0].Title);
        }

        [Test]
        public void GetRecipesSuccesTest()
        {
            //Arrange
            MockRecipeRepository mockRecipeRepository = new MockRecipeRepository();
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "1", Content = "1" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "2", Content = "2" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "3", Content = "3" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "4", Content = "4" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "5", Content = "5" });
            Services.RecipeService recipeService = new Services.RecipeService(mockRecipeRepository);

            //Act
            var result = recipeService.GetRecipes(0);

            //Assert
            Assert.AreEqual(5, mockRecipeRepository.recipes.Count);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void GetRecipesSuccesBigRangeTest()
        {
            //Arrange
            MockRecipeRepository mockRecipeRepository = new MockRecipeRepository();
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "1", Content = "1" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "2", Content = "2" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "3", Content = "3" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "4", Content = "4" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "5", Content = "5" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "6", Content = "6" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "7", Content = "7" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "8", Content = "8" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "9", Content = "9" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "10", Content = "10" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "11", Content = "11" });
            mockRecipeRepository.recipes.Add(new Models.Recipe { Title = "12", Content = "12" });

            Services.RecipeService recipeService = new Services.RecipeService(mockRecipeRepository);

            //Act
            var result = recipeService.GetRecipes(0);

            //Assert
            Assert.AreEqual(12, mockRecipeRepository.recipes.Count);
            Assert.AreEqual(10, result.Count);
        }
    }
}
