using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace RentHiveV2.Models
{
    public class Listing
    {

        //Primary Key
        [Key]
        public int ListingId { get; set; }


        //Foreign Key from ApplicationUser
        public string? ApplicationUserId { get; set; }


        //Title for the property.
        [Required(ErrorMessage = "Required to fill out Title for the listing.")]
        public string? Title { get; set; }

        //Description for the property. 
        public string? Description { get; set; }

        //Price per night.
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Please enter valid price.")]

        [Required(ErrorMessage = "Required to fill out the price of the listing.")]
        public double PricePerNight { get; set; }


        //Address
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? State { get; set; }



        //More information about the apartment


        [Required(ErrorMessage = "Required to fill out how many bedrooms.")]
        public int Bedroom { get; set; }


        [Required(ErrorMessage = "Required to fill out how many bathrooms.")]
        public int Bathroom { get; set; }


        [Required(ErrorMessage = "Required to fill out how many beds.")]
        public int Beds { get; set; }




        //Optional images - We should rather add a image class and make a one-to-many relationship.
        //Not a scaleable option. 

        public string? Image1 { get; set; } = "/Images/PlaceholderApartmentImage.png"; //Adding a default image to the first image. 
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }



        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


        //NAV. PROP.
        public virtual ApplicationUser? ApplicationUser { get; set; } //Enables lazyloading. 




        //public ICollection<Review>? Reviews { get; set; }  //Allows Entity Frameowrk load related reviews. 

        //public ICollection<WishlistEiendom>? WishlistEiendom { get; set; }//Allows Entity Frameowrk load related reviews. 


        /// <summary>
        /// The booking will be used to keep track of all bookings for the apartment. 
        /// It will also be used to find Unavailable days for the calendar. 
        /// </summary>
        /// 


        public ICollection<Bookings>? Bookings { get; set; }

    }
}
