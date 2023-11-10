using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentHiveV2.Data;
using RentHiveV2.Models;
using System.Security.Claims;
using RentHiveV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer;
using Microsoft.EntityFrameworkCore; 


namespace RentHiveV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller

    {


        private readonly ApplicationDbContext _context;
        private readonly ILogger<ListingController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListingController(ApplicationDbContext context, ILogger<ListingController> logger, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        //JUST FOR TESTING: 


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Listing newListing)
        {

            _logger.LogInformation("Create action method invoked.");

            if(newListing == null)
            {
                return BadRequest("Invalid item data.");
            }


            _logger.LogInformation("Attempting to create a Listing with the following model:");

            //Loggers for debugging:
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

            //FOR SOME REASON THIS DOES NOT WORK. IT DOES NOT GET THE ID OF THE USER.
            //WHEN I DO [AUTHORIZE] IM STILL GETTING FORBIDDEN. I KNOW THE USER IS LOGGED IN. 

            //string userId = User.GetSubjectId(); 

            // _logger.LogInformation($"The USER IDENTITY IS: {User.Identity.GetSubjectId()}");


            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"The listing UserID is: {userId}");

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


                var response = new { success = true, message = "Listing " + newListing.Title + " created successfully" };


                _context.Listing.Add(newListing);

                await _context.SaveChangesAsync();
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
