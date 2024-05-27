using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhumaloCraftPOE.Models;
using Microsoft.AspNetCore.Identity;

namespace KhumaloCraftPOE.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}

