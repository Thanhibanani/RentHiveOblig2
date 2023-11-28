using Microsoft.AspNetCore.Identity;

namespace RentHiveV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? ProfilePicture { get; set; }

        //NAV. PROP. 
        public virtual ICollection<Listing>? Listings { get; set; }
        public ICollection<Bookings>? Bookings { get; set; }
    }
}