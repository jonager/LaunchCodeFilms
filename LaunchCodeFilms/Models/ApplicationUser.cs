using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LaunchCodeFilms.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public override string UserName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int YearOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserProfile Profile { get; set; }

        public IList<Review> Movies { get; set; }

        public IList<Queue> Queues { get; set; }

    }
}
