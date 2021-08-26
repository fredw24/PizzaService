using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaService.Models
{

    public class Pizza
    {
        [Key]
        public int PizzaId {get; set;}
        [Required]
        [Display(Name="Name:")]
        public string Name {get; set;}

  
        [Required]
        [Display(Name="Crust:")]
        public string CrustStyle {get; set;}
        [Display(Name="Size:")]
        public int Size {get; set;}
        [Display(Name="Toppings:")]
        public string Toppings {get; set;}
        [Display (Name="Cost:")]
        public double Price {get; set;}
        [Display (Name="Ordered By:")]
        public DateTime OrderedAt{get; set;} = DateTime.Now;
        public DateTime UpdatedAt{get; set;} = DateTime.Now;

        public int UserId {get; set;}
        public User myUser {get; set;}


    }


}