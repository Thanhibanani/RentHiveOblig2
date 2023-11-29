using Microsoft.AspNetCore.Identity;

namespace RentHiveV2.Models
{
    public class ApplicationUser : IdentityUser
    {

        //NAV. PROP. 
        public virtual ICollection<Listing>? Listings { get; set; }
        public ICollection<Bookings>? Bookings { get; set; }
    }
}