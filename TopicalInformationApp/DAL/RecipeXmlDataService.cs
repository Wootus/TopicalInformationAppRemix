using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using TopicalInformationApp.Models;

namespace TopicalInformationApp.DAL
{
    public class RecipeXmlDataService : IRecipeDataServices, IDisposable
    {
        //Read Data from XML Doc
        public List<Recipe> Read()
        {
            //defnition of a model for the xml serializer to use
            Recipes RecipesModel;

            //Prep a FireStream object
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamReader sReader = new StreamReader(xmlFilePath);

            //Prep a XML Deserialization object
            XmlSerializer deserializer = new XmlSerializer(typeof(Recipes));

            //using statement to connect the stream reader and deserializer to read the xml file
            using (sReader)
            {
                //Deserializer gets the dataset into object form
                object xmlObject = deserializer.Deserialize(sReader);

                //A cast to turn the generic object form into a recipe object
                RecipesModel = (Recipes)xmlObject;
            }

            return RecipesModel.recipes;
        }




        //Method to write the database to an xml file
        public void Write(List<Recipe> recipes)
        {
            //Declaration and instatiation of a stream writer object that points to correct dataset location
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamWriter sWriter = new StreamWriter(xmlFilePath, false);

            //Serializer object to prepare objects for storage
            XmlSerializer serializer = new XmlSerializer(typeof(List<Recipe>), new XmlRootAttribute("Recipes"));

            //using statement that uses the serializer and sWriter object to create and write the new xml database
            using (sWriter)
            {
                serializer.Serialize(sWriter, recipes);
            }
        }

        public void Dispose()
        {

        }

    }
}