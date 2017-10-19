using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchCodeFilms.Models
{
    public class UserProfile
    {
        public int ID { get; set; }
        public string ThemeColor { get; set; }
        public char Gender { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public int UserId { get; set; } //FK
        public ApplicationUser User { get; set; }
    }
}
