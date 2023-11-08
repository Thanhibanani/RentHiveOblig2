using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentHiveV2.Data;
using RentHiveV2.Models;
using System.Security.Claims;
using RentHiveV2.ViewModels; 

namespace RentHiveV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller
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


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ListingViewModel model)
        {

            _logger.LogInformation("Create action method invoked.");

            if(model == null)
            {
                return BadRequest("Invalid item data.");
            }


            _logger.LogInformation("Attempting to create a Listing with the following model:");

            //A lot of loggers for debugging...
            _logger.LogInformation($"The listing title is {model.Title}");
            _logger.LogInformation($"The listing description is: {model.Description}");
            _logger.LogInformation($"The listing price per night is: {model.PricePerNight}");
            _logger.LogInformation($"The listing street is: {model.Street}");
            _logger.LogInformation($"The listing City is: {model.City}");
            _logger.LogInformation($"The listing Country is: {model.Country}");
            _logger.LogInformation($"The listing Zipcode is: {model.ZipCode}");
            _logger.LogInformation($"The listing State is: {model.State}");
            _logger.LogInformation($"The listing Bedroom is: {model.Bedroom}");
            _logger.LogInformation($"The listing Bathroom is: {model.Bathroom}");
            _logger.LogInformation($"The listing Beds is: {model.Bed}");


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

                Listing listing = new Listing
                {
                    ApplicationUserId = userId,
                    Title = model.Title,
                    Description = model.Description,
                    PricePerNight = model.PricePerNight,
                    Street = model.Street,
                    City = model.City,
                    Country = model.Country,
                    ZipCode = model.ZipCode,
                    State = model.State,
                    Bedroom = model.Bedroom,
                    Bathroom = model.Bathroom,
                    Beds = model.Bed,
                    CreatedDateTime = DateTime.Now
                };


                _context.Listing.Add(listing);

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
