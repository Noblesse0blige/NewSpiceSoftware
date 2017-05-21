using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NewSpiceSoftware.Models
{
    public class Ingredient
    {
        [Required]
        public int RecipeID { get; set; }

        [Required]
        [StringLength (1000, ErrorMessage = "Ingredient name cannot be more than 1,000 characters.")]
        public string Name { get; set; }

        [Required]
        public double Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public string Preparation { get; set; }

        public Ingredient(int recipeID, string name, double quantity, string measurementUnit, string preparation)
        {
            RecipeID = recipeID;
            Name = name;
            Quantity = quantity;
            MeasurementUnit = measurementUnit;
            Preparation = preparation;
        }
        public Ingredient()
        {
            //Empty XTOR
        }

    }
}