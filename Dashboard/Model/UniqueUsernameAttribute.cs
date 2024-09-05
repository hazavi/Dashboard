using Dashboard.Connect;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Model
{
    // Custom validation attribute to ensure the username is unique in the database
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the DbContext from the validation context to access the database
            var dbContext = (DashboardDbContext)validationContext.GetService(typeof(DashboardDbContext));

            // Cast the input value as a string representing the username
            var username = value as string;

            // Check if the username already exists in the Users table
            if (dbContext.Users.Any(u => u.Username == username))
            {
                // If the username is found, return a validation error
                return new ValidationResult("Username is already taken.");
            }
            // If the username is unique, return success
            return ValidationResult.Success;
        }
    }
}
