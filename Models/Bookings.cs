using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentHiveV2.Models
{
    public class Bookings
    {
        [Key] public int BookingId { get; set; }



        [JsonPropertyName("guestId")]
        [ForeignKey("ApplicationUser")]
        public string GuestId { get; set; } //Foreign key



        //The propertyId
        [JsonPropertyName("listingId")]
        [ForeignKey("Listing")]
        public int ListingId { get; set; } //Foreign key


        [JsonPropertyName("startDate")]
        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required.")]
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "TotalPrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = ("The value of Totalprice must be positive."))]
        [JsonPropertyName("totalPrice")]
        public double TotalPrice { get; set; }

        //This will change when the host accepts or declines the booking.

        [JsonPropertyName("bookingStatus")]
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;

        //Difference between the days
        [JsonPropertyName("quantityDays")]
        public int QuantityDays { get; set; }

        //NAV. PROP.

        [JsonIgnore]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        
        [JsonIgnore]
        public virtual Listing? Listing { get; set; }
    }
    public enum BookingStatus
    {
        Pending,
        Accepted,
        Declined
    }
}