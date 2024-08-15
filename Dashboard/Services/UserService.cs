using Dashboard.Connect;
using Dashboard.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dashboard.Services
{
    public class UserService
    {
        private readonly DashboardDbContext _context;

        public UserService(DashboardDbContext context)
        {
            _context = context;
        }

        public void CreateUserWithLocation(User user, int zipCode, string cityName, string country)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Add the User to the DbContext
            _context.Users.Add(user);
            _context.SaveChanges(); // Save changes to generate the User Id

            // Create a Location for the user
            var location = new Location
            {
                UserId = user.Id,
                ZipCode = zipCode,
                CityName = cityName,
                Country = country
            };

            // Add the Location to the DbContext
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

    }
}
