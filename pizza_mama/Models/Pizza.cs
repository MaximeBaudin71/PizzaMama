using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizza_mama.Models
{
    public class Pizza
    {
        [JsonIgnore]
        public int PizzaID { get; set; }

        [Display(Name = "Nom")]
        public string nom { get; set; }

        [Display(Name = "Prix (€)")]
        public float prix { get; set; }

        [Display(Name = "Végétarienne")]
        public bool vegetarienne { get; set; }

        [JsonIgnore]
        [Display(Name = "Ingrédients")]
        public string ingredients { get; set; }

        [NotMapped]
        [JsonPropertyName("Ingrédients")]
        public string[] listeIngredients
        {
            get
            {
                if (ingredients == null || ingredients.Count() == 0)
                {
                    return null;
                }
                return ingredients.Split(",");

            }
        }

        public Pizza()
        {

            

        }
    }
}

