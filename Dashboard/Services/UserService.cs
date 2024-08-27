using Dashboard.Connect;
using Dashboard.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dashboard.Services
{
    public class UserService
    {
        private readonly DashboardDbContext _context;

        // Constructor to initialize UserService with DbContext
        public UserService(DashboardDbContext context)
        {
            _context = context;
        }

        // Creates a user and associates them with a location
        public void CreateUserWithLocation(User user, int zipCode, string cityName, string country)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user)); // Ensure user is not null

            // Add the User to the DbContext
            _context.Users.Add(user);
            _context.SaveChanges(); // Save changes to generate the User Id

            // Create a Location for the user
            var location = new Location
            {
                UserId = user.Id, // Associate Location with the newly created User
                ZipCode = zipCode,
                CityName = cityName,
                Country = country
            };

            // Add the Location to the DbContext
            _context.Locations.Add(location);
            _context.SaveChanges(); // Persist( maintaining or saving data over time) the Location to the database
        }
    }
}
