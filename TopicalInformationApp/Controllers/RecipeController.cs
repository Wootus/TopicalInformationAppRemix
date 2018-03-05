using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopicalInformationApp.DAL;
using TopicalInformationApp.Models;

namespace TopicalInformationApp.Controllers
{
    public class RecipeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string sortOrder, int? page)
        {
            RecipeRepository recipeRepository = new RecipeRepository();

            //Creates a unique list of beer Styles
            ViewBag.Styles = ListOfStyles(sortOrder);

            IEnumerable<Recipe> recipes;

            //using statement to populate recipes and clean up after itself
            using (recipeRepository)
            {
                recipes = recipeRepository.SelectAll(sortOrder) as IList<Recipe>;
            }

            // sort by name unless posted as a new sort
            switch (sortOrder)
            {
                case "NAME":
                    recipes = recipes.OrderBy(s => s.Name);
                    break;
                case "STYLE":
                    recipes = recipes.ToList().OrderBy(s => s.Style);
                    break;
                case "ABV":
                    recipes = recipes.ToList().OrderBy(s => s.ABV);
                    break;

                case "IBU":
                    recipes = recipes.ToList().OrderBy(s => s.IBU);
                    break;

                default:
                    recipes = recipes.ToList().OrderBy(s => s.Name);
                    break;
            }

            //PAGEINATE
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            recipes = recipes.ToPagedList(pageNumber, pageSize);


            return View(recipes);
        }

        [HttpPost]
        public ActionResult Index(string sortOrder, string searchKey, string styleFilter, int? page)
        {
            if(sortOrder == null)
            {
                sortOrder = "NAME";
            }
            RecipeRepository recipeRepository = new RecipeRepository();

            //Creates a unique list of beer Styles
            ViewBag.Styles = ListOfStyles(sortOrder);

            IEnumerable<Recipe> recipes;

            //using statement to populate recipes and clean up after itself
            using (recipeRepository)
            {
                recipes = recipeRepository.SelectAll(sortOrder) as IList<Recipe>;
            }

            //if a search key is returned
            if (searchKey != null)
            {
                recipes = recipes.Where( s => s.Style.ToUpper().Contains(searchKey.ToUpper()));
            }

            //if a filter command is returned
            if (styleFilter != "" || styleFilter == null)
            {
                recipes = recipes.Where(s => s.Style == styleFilter);
            }

            //for pagination
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            recipes = recipes.ToPagedList(pageNumber, pageSize);

            return View(recipes);
        }



        [NonAction]
        private IEnumerable<string> ListOfStyles(string sortOrder)
        {
            if (sortOrder == null || sortOrder == "")
            {
                sortOrder = "NAME";
            }

            RecipeRepository recipeRepository = new RecipeRepository();

            IEnumerable<Recipe> recipes;

            //using statement to populate recipes and clean up after itself
            using (recipeRepository)
            {
                recipes = recipeRepository.SelectAll(sortOrder) as IEnumerable<Recipe>;
            }

            var styles = recipes.Select(s => s.Style).Distinct().OrderBy(x => x);
            return styles;
        }



        // GET: Recipe/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            RecipeRepository recipeRepository = new RecipeRepository();
            Recipe recipe = new Recipe();

            //finds requested recipe
            using (recipeRepository)
            {
                recipe = recipeRepository.SelectOne(id);
            }

            return View(recipe);
        }





        // GET: Recipe/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            try
            {
                RecipeRepository recipeRepository = new RecipeRepository();

                using (recipeRepository)
                {
                    recipeRepository.Insert(recipe);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // GET: Recipe/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            RecipeRepository recipeRepository = new RecipeRepository();
            Recipe recipe = new Recipe();

            //finds requested recipe
            using (recipeRepository)
            {
                recipe = recipeRepository.SelectOne(id);
            }

            return View(recipe);
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        public ActionResult Edit(Recipe recipe)
        {
            try
            {
                RecipeRepository recipeRepository = new RecipeRepository();

                using (recipeRepository)
                {
                    recipeRepository.Update(recipe);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            RecipeRepository recipeRepository = new RecipeRepository();
            Recipe recipe = new Recipe();

            //finds requested recipe
            using (recipeRepository)
            {
                recipe = recipeRepository.SelectOne(id);
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Recipe recipe)
        {
            try
            {
                RecipeRepository recipeRepository = new RecipeRepository();

                using (recipeRepository)
                {
                    recipeRepository.Delete(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
