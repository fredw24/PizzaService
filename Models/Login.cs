using System.ComponentModel.DataAnnotations;

namespace PizzaService.Models
{
    public class Login
    {
        [EmailAddress]
        public string Email{get; set;}
        
        public string Password{get; set;}
    }
    
}