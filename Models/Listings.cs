using Duende.IdentityServer.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentHiveV2.Models
{
    public class Listing
    {

        //Primary Key
        [Key]
        public int ListingId { get; set; }


        //Foreign Key from ApplicationUser
        public string? ApplicationUserId { get; set; }

        [JsonPropertyName("Title")]
        [Required(ErrorMessage = "Required to fill out Title for the listing.")]
        public string? Title { get; set; }

        [JsonPropertyName("Description")]
        public string? Description { get; set; }


        [JsonPropertyName("PricePerNight")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Please enter valid price.")]
        [Required(ErrorMessage = "Required to fill out the price of the listing.")]
        public double PricePerNight { get; set; }


        //Address
        [JsonPropertyName("Street")] 
        public string? Street { get; set; }

        [JsonPropertyName("City")]
        public string? City { get; set; }

        [JsonPropertyName("Country")]
        public string? Country { get; set; }

        [JsonPropertyName("ZipCode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("State")]
        public string? State { get; set; }



        //More information about the apartment
        [JsonPropertyName("Bedroom")]
        [Required(ErrorMessage = "Required to fill out how many bedrooms.")]
        public int Bedroom { get; set; }

        [JsonPropertyName("Bathroom")]
        [Required(ErrorMessage = "Required to fill out how many bathrooms.")]
        public int Bathroom { get; set; }

        [JsonPropertyName("Beds")]
        [Required(ErrorMessage = "Required to fill out how many beds.")]
        public int Beds { get; set; }




        //Optional images - We should rather add a image class and make a one-to-many relationship.
        //Not a scaleable option. 
        [JsonPropertyName("Image1")]
        public string? Image1 { get; set; } = "/Images/PlaceholderApartmentImage.png"; //Adding a default image to the first image. 
        
        [JsonPropertyName("Image2")]
        public string? Image2 { get; set; }
        
        [JsonPropertyName("Image3")]
        public string? Image3 { get; set; }


        [JsonPropertyName("CreatedDateTime")]
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
