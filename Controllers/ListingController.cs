using Microsoft.AspNetCore.Mvc;
using RentHiveV2.Models;
using System.Security.Claims;

using RentHiveV2.DAL;

namespace RentHiveV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller

    {


        private readonly IListingRepository _listingRepository;
        private readonly ILogger<ListingController> _logger;


        public ListingController(IListingRepository listingRepository, ILogger<ListingController> logger)
        {
            _listingRepository = listingRepository;
            _logger = logger;
        }




        /// <summary>
        /// Gets all the listings in the DB. 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll action method invoked in Listing");

            var listings = await _listingRepository.GetAll();

            if (listings == null)
            {
                _logger.LogError("There were no Listings found when executing _listingRepository.GetAll()");
                return NotFound();
            }

            return Ok(listings);
        }



        [HttpGet("getByHost")]
        public async Task<IActionResult> GetByHost()
        {
            _logger.LogInformation("GetByHost action method invoked in Listing");

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _logger.LogInformation($"Attempting to get the listings for host: {userId} ");

            var listings = await _listingRepository.GetByHost(userId);

            if (listings == null)
            {
                _logger.LogError("There were no Listings found when executing _listingRepository.GetByHost()");
                return NotFound();
            }

            return Ok(listings);
        }






        /// <summary>
        /// Create listing method
        /// </summary>
        /// <param name="newListing"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Listing newListing)
        {

            _logger.LogInformation("[ListingController]: Create action method invoked.");

            if (newListing == null)
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


            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"The listing UserID is: {userId}");

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("The userId is null or empty.");

                return Forbid();
            }

            //Try to create the model.
            try
            {
                newListing.ApplicationUserId = userId;
                newListing.CreatedDateTime = DateTime.Now;

                if (ModelState.IsValid)
                {
                    //Method in DAL
                    bool returnOk = await _listingRepository.Create(newListing);

                    if (returnOk)
                    {
                        var response = new { success = true, message = "Listing " + newListing.Title + " created successfully" };
                        return Ok(response);
                    }
                    else
                    {
                        var respone = new { success = false, message = "Listing creation" };
                        return Ok(respone);
                    }


                }
                //If Modelstate is invalid.
                else
                {
                    _logger.LogError("ModelState in invalid");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while creating the listing.");
                //Need to return something here too. 
                return StatusCode(500, ex.Message);
            }



        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"GetById action method invoked in Listing for ID: {id}");

            var listing = await _listingRepository.GetById(id);

            if (listing == null)
            {
                _logger.LogError($"No Listing found for ID: {id}");
                return NotFound();
            }

            return Ok(listing);
        }


        [HttpDelete("delete/{id}")]
        public async Task <IActionResult> DeleteListing(int id)
        {
            bool returnOk = await _listingRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("ListingController: listing deletion failed for the listing {listingId:0000}", id);
                return BadRequest("Listing deletion failed."); 
            }
            var response = new { success = true, message = "Listing " + id.ToString() + " deleted successfully" };
            return Ok(response); 
        }









    }
}
