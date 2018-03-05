using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicalInformationApp.Models;

namespace TopicalInformationApp.DAL
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> SelectAll(string sortOrder);
        Recipe SelectOne(int id);
        void Insert(Recipe recipe);
        void Update(Recipe recipe);
        void Delete(int id);
    }
}
