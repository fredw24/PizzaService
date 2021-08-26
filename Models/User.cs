

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaService.Models
{

    public class User
    {
        [Key]
        public int UserId {get; set;}
        [Required]
        [MinLength(3)]
        [Display(Name="First Name:")]
        public string FirstName {get; set;}
        [Required]
        [MinLength(3)]
        [Display(Name="Last Name:")]
        public string LastName {get; set;}
        [EmailAddress]
        [Required]
        [Display(Name="Email:")]
        public string Email {get; set;}
        [Required]
        [MinLength(8)]
        [Display(Name="Password:")]
        public string Password {get; set;}
        [Required]
        [Age]
        [MinAge(18)]
        [Display(Name="Date of Birth:")]
        public DateTime DateOfBirth {get; set;}
        
        public DateTime CreatedAt {get; set;}  = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public List<Pizza> Pizzas {get; set;}

    }
    public class AgeAttribute : ValidationAttribute
    {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime birth = Convert.ToDateTime(value);

                if (birth >= DateTime.Now)
                    return new ValidationResult("the Date is too ahead!");
                return ValidationResult.Success;
            }
        
    }

     public class MinAge : ValidationAttribute
    {
        private int _Limit;
        public MinAge(int Limit) { // The constructor which we use in modal.
            this._Limit = Limit;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
                DateTime bday = DateTime.Parse(value.ToString());
                DateTime today = DateTime.Today;
                int age = today.Year - bday.Year;
                if (bday > today.AddYears(-age))
                {
                   age--; 
                }
                if (age < _Limit)
                {
                    var result = new ValidationResult("Sorry you are not old enough");
                    return result; 
                }
               
            
            return null;

        }
    }
    
}