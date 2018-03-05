using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopicalInformationApp.Models;

namespace TopicalInformationApp.DAL
{
    public interface IRecipeDataServices
    {
        List<Recipe> Read();

        void Write(List<Recipe> recipes);
    }
}