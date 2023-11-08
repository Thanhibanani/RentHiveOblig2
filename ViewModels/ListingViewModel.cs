using Duende.IdentityServer.Models;
using System.ComponentModel.DataAnnotations;

namespace RentHiveV2.ViewModels
{
    public class ListingViewModel
    {



        [Required(ErrorMessage = "A title for the listing is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A description text for the listing is required")]
        public string? Description { get; set; }


        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Please enter valid number")]
        public double PricePerNight { get; set; }



        //Address
        [Required(ErrorMessage = "Street address is required.")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "ZipCode is required.")]
        public string? ZipCode { get; set; }

        public string? State { get; set; }


        //More info

        [Required(ErrorMessage = "Filling out number of bathroom is required.")]
        [Range(0, 30)]
        public int Bathroom { get; set; }

        [Required(ErrorMessage = "Filling out number of bedrooms is required.")]
        [Range(0, 30)]
        public int Bedroom { get; set; }

        [Required(ErrorMessage = "Filling out number of bed is required.")]
        [Range(0, 30)]
        public int Bed { get; set; }


        /*  BRUKES DETTE? 
         *
        public string? SearchString { get; set; }

        public SelectList? TestList { get; set; }


        */

    }
}
