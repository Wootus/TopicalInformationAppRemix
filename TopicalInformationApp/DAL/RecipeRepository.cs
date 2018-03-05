using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopicalInformationApp.Models;

namespace TopicalInformationApp.DAL
{
    public class RecipeRepository : IRecipeRepository , IDisposable
    {
        private List<Recipe> _recipes;

        //Default constructor
        public RecipeRepository()
        {
            //When instatiated will create xml dataservice object
            RecipeXmlDataService recipeXMLDataService = new RecipeXmlDataService();

            //with a using statement the xmldataservice is used to get a list of objects that is stored internaly
            using (recipeXMLDataService)
            {
                _recipes = recipeXMLDataService.Read();
            }
        }

        public IEnumerable<Recipe> SelectAll( string sortOrder)
        {
            //switch (sortOrder)
            //{
            //    case "NAME":
            //        _recipes = _recipes.ToList().OrderBy(s => s.Name);
            //        break;
            //    case "STYLE":
            //        _recipes = _recipes.ToList().OrderBy(s => s.Style);
            //        break;
            //    case "ABV":
            //        _recipes = _recipes.ToList().OrderBy(s => s.ABV);
            //        break;

            //    case "IBU":
            //        _recipes = _recipes.ToList().OrderBy(s => s.IBU);
            //        break;

            //    default:
            //        _recipes = _recipes.ToList().OrderBy(s => s.Name);
            //        break;
            //}
            return _recipes as IEnumerable<Recipe>;
        }

        public Recipe SelectOne(int id)
        {
            Recipe recipe = _recipes.ToList().Where(s => s.Id == id).FirstOrDefault();

            if(recipe != null)
            {
                return recipe;
            }
            else
            {
                Recipe tempRecipe = new Recipe() { Name = "ERROR" };
                return tempRecipe;
            }
        }

        public void Insert(Recipe recipe)
        {
            recipe.Id = NextIdValue();
            _recipes.Add(recipe);

            Save();
        }

        public void Update(Recipe UpdatedRecipe)
        {
            var oldRecipe = _recipes.Where(s => s.Id == UpdatedRecipe.Id).FirstOrDefault();

            if (oldRecipe != null)
            {
                _recipes.Remove(oldRecipe);
                _recipes.Add(UpdatedRecipe);
            }

            Save();
        }

        public void Delete(int id)
        {
            var recipe = _recipes.Where(s => s.Id == id).FirstOrDefault();

            if(recipe != null)
            {
                _recipes.Remove(recipe);
            }

            Save();
        }


        private int NextIdValue()
        {
            int currentMaxId = _recipes.OrderByDescending(s => s.Id).FirstOrDefault().Id;
            return currentMaxId + 1;
        }

        public void Save()
        {
            RecipeXmlDataService recipeXmlDataService = new RecipeXmlDataService();

            using (recipeXmlDataService)
            {
                recipeXmlDataService.Write(_recipes);
            }
        }

        public void Dispose()
        {
            _recipes = null;
        }
    }
}