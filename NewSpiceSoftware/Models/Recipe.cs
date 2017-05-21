using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NewSpiceSoftware.Models
{
    public class Recipe
    {
        private string configFilePath = "RecipesDatabase"; //TODO: Create SQLITE config file path and put it here.

        [Required]
        private int RecipeID { get; set; }

        [Required]
        [StringLength (1000, ErrorMessage = "Recipe name must be less than 1,000 characters.")]
        public string Name { get; set; }

        [StringLength (8000, ErrorMessage = "Recipe description must be less than 8,000 characters.")]
        public string Description { get; set; }

        [StringLength (8000, ErrorMessage = "Recipe introduction must be less than 8,000 characters.")]
        public string Introduction { get; set; }

        [Required]
        public List<Ingredient> Ingredients { get; set; }

        [Required]
        public Dictionary<int, string> Instructions { get; set; }
        private string CreationDate { get; set; }
        public string LastUpdateDate { get; set; }


        /// <summary>
        /// This constructor is for creating a brand new Recipe record.
        /// </summary>
        /// <param name="name">Recipe Name (limit 1000 characters)</param>
        /// <param name="description">Recipe Description (limit 8000 characters)</param>
        /// <param name="introduction">Recipe Introduction (limit 8000 characters)</param>
        /// <param name="ingredients">List of Ingredient objects</param>
        /// <param name="instructions">Recipe Instructions (Dictionary type: Step Number (int), Instruction Text (string))</param>
        public Recipe(string name, string description, string introduction, List<Ingredient> ingredients, Dictionary<int,string> instructions)
        {
            RecipeID = GetNewRecipeID();
            Name = name;
            Description = description;
            Introduction = introduction;
            Ingredients = ingredients;
            Instructions = instructions;
            CreationDate = DateTime.Now.ToLongTimeString();
            LastUpdateDate = DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// This Recipe constructor is for updating an existing Recipe record.
        /// </summary>
        /// <param name="recipe">Recipe record</param>
        /// <param name="lastUpdateDate">Last Update Date time stamp</param>
        public Recipe(Recipe recipe, string lastUpdateDate)
        {
            RecipeID = GetNewRecipeID();
            Name = recipe.Name;
            Description = recipe.Description;
            Introduction = recipe.Introduction;
            Ingredients = recipe.Ingredients;
            Instructions = recipe.Instructions;
            CreationDate = recipe.CreationDate;
            LastUpdateDate = lastUpdateDate;
        }
        public Recipe()
        {
            //Empty XTOR
        }
        private int GetNewRecipeID()
        {
            int newRecipeID = 0;

            try {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(configFilePath))
                {
                    sqliteConnection.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT CASE WHEN MAX(RecipeID) IS NOT NULL AND MAX(RecipeID) != 0 THEN MAX(RecipeID) + 1 ELSE 1 END AS 'RecipeID' FROM Recipes", sqliteConnection);

                    using (SQLiteDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            newRecipeID = dataReader.GetInt32(0);
                        }
                    }
                    sqliteConnection.Close();
                } //End sqliteConnection
            }
            catch (SQLiteException error)
            {
                System.Diagnostics.Debug.WriteLine("Exception occured: " + error.Message);
            }
            return newRecipeID;
        }
    }
}