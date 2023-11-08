using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentHiveV2.Data;
using RentHiveV2.Models;
using System.Security.Claims;
using RentHiveV2.ViewModels; 

namespace RentHiveV2.Controllers
{
    [ApiController][Route("api/[controller]")]public class ListingController : Controller

    {


        private readonly ApplicationDbContext _context;
        private readonly ILogger<ListingController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public ListingController(ApplicationDbContext context, ILogger<ListingController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;

        }

        //JUST FOR TESTING: 

        public static List<Listing> Listings = new List<Listing>(); 


        [HttpPost("create")]
        public IActionResult Create([FromBody] Listing newListing)
        {

            _logger.LogInformation("Create action method invoked.");

            if(newListing == null)
            {
                return BadRequest("Invalid item data.");
            }


            _logger.LogInformation("Attempting to create a Listing with the following model:");

            //A lot of loggers for debugging...
            _logger.LogInformation($"The listing title is {newListing.Title}");
            _logger.LogInformation($"The listing description is: {newListing.Description}");
            _logger.LogInformation($"The listing price per night is: {newListing.PricePerNight}");
            _logger.LogInformation($"The listing street is: {newListing.Street}");
            _logger.LogInformation($"The listing City is: {newListing.City}");
            _logger.LogInformation($"The listing Country is: {newListing.Country}");
            _logger.LogInformation($"The listing Zipcode is: {newListing.ZipCode}");
            _logger.LogInformation($"The listing State is: {newListing.State}");
            _logger.LogInformation($"The listing Bedroom is: {newListing.Bedroom}");
            _logger.LogInformation($"The listing Bathroom is: {newListing.Bathroom}");
            _logger.LogInformation($"The listing Beds is: {newListing.Beds}");


            //Check if it finds userId. This might already be done by [Authorize].
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("The userId is null or empty.");

                return Forbid();

            }

            _logger.LogInformation($"The userId is {userId}");


            //Try to create the model.

            try
            {

                newListing.ApplicationUserId = userId;
                newListing.CreatedDateTime = DateTime.Now;

                Listings.Add(newListing);


                var response = new { success = true, message = "Listing " + newListing.Title + " created successfully" };


                _context.Listing.Add(newListing);

                _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occured while creating the listing.");
                //Need to return something here too. 
                return StatusCode(500, ex.Message);
            }

        }


    }
}
