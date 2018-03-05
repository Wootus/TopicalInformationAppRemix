using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TopicalInformationApp.Models
{
    [XmlRoot("Recipes")]
    public class Recipes
    {
        [XmlElement("Recipe")]
        public List<Recipe> recipes;
    }
}