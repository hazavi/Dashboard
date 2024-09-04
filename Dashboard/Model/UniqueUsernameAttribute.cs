using Dashboard.Connect;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Model
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (DashboardDbContext)validationContext.GetService(typeof(DashboardDbContext));
            var username = value as string;

            if (dbContext.Users.Any(u => u.Username == username))
            {
                return new ValidationResult("Username is already taken.");
            }

            return ValidationResult.Success;
        }
    }
}
