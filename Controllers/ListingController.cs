using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentHiveV2.DAL;
using RentHiveV2.Models;
using System.Security.Claims;

namespace RentHiveV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller

    {


        private readonly IListingRepository _listingRepository;
        private readonly ILogger<ListingController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ListingController(IListingRepository listingRepository, ILogger<ListingController> logger, IWebHostEnvironment hostEnvironment)
        {
            _listingRepository = listingRepository;
            _logger = logger;
            _hostEnvironment = hostEnvironment;

        }



        //GETTING ALL LISTINGS.
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


        // Other actions...




        //GET search method
        /**
         
         public IActionResult Search(string searchPhrase)
            {
                // Perform database query based on search parameters
                var results = _context.Listings
                    .Where(l =>
                        (string.IsNullOrEmpty(keywords) || l.Description.Contains(keywords)) &&
                        (string.IsNullOrEmpty(country) || l.Country == country) &&
                        (string.IsNullOrEmpty(city) || l.City == city)
                    )
                    .ToList();

                return View("Index", results);
            }
       

        */



        //GETTING LISTINGS BELONGING TO THE HOST.
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


        //CREATING A NEW LISTING.
        [HttpPost("/create")]
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
                        var response = new { Success = false, Message = "Listing creation" };
                        return Ok(response);
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

        //GETTING A LISTING BY ITS ID.
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


        //DELETING A LISTING BY ITS ID.
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {

            //Checks if the user is the correct owner of the listing.
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"The listing UserID is: {userId}");

            var listing = await _listingRepository.GetById(id);

            if (listing.ApplicationUserId != userId)
            {
                _logger.LogError("The user is not the owner of the listing");
                _logger.LogInformation($"User {userId} attemped to delete listing {id}");

                return Forbid();
            }

            bool returnOk = await _listingRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("ListingController: listing deletion failed for the listing {listingId:0000}", id);
                return BadRequest("Listing deletion failed.");
            }
            var response = new { success = true, message = "Listing " + id.ToString() + " deleted successfully" };
            return Ok(response);
        }


        //UPDATING A LISTING BY ITS ID.

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Listing newListing)
        {
            _logger.LogInformation($"Updating {newListing}");

            //Checks if the user is the correct owner of the listing.
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"The listings UserID is: {userId}");

            var listing = await _listingRepository.GetById(id);

            if (listing.ApplicationUserId != userId)
            {
                _logger.LogError("The user is not the owner of the listing");
                _logger.LogInformation($"User {userId} attemped to delete listing {id}");

                return Forbid();
            }

            if (newListing == null)
            {
                return BadRequest("Invalid listing data");
            }

            //Loggers for debugging:
            _logger.LogInformation($"The updated listing title is {newListing.Title}");
            _logger.LogInformation($"The updated listing description is: {newListing.Description}");
            _logger.LogInformation($"The updated listing price per night is: {newListing.PricePerNight}");
            _logger.LogInformation($"The updated listing street is: {newListing.Street}");
            _logger.LogInformation($"The updated listing City is: {newListing.City}");
            _logger.LogInformation($"The updated listing Country is: {newListing.Country}");
            _logger.LogInformation($"The updated listing Zipcode is: {newListing.ZipCode}");
            _logger.LogInformation($"The updated listing State is: {newListing.State}");
            _logger.LogInformation($"The updated listing Bedroom is: {newListing.Bedroom}");
            _logger.LogInformation($"The updated listing Bathroom is: {newListing.Bathroom}");
            _logger.LogInformation($"The updated listing Beds is: {newListing.Beds}");

            bool returnOk = await _listingRepository.Update(id, newListing); // To rep.

            if (returnOk)
            {
                var response = new { success = true, message = "Listing " + newListing.Title + " updated successfully." };
                return Ok(response);
            }
            else
            {
                var response = new { success = false, message = "Updating listing failed" };
                return Ok(response);
            }

        }




        private async Task<string> SaveFile(IFormFile file)
        {
            _logger.LogInformation("Attempting to save image file.");
            if (file == null) return null;
            _logger.LogError("Image file was null.");

            var uploadsFolderPath = Path.Combine(_hostEnvironment.WebRootPath, "listingImages");

            //Logging for debugging
            if (!Directory.Exists(uploadsFolderPath))
                _logger.LogError("The folder path does not exist. Check the path!");



            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("listingImages", fileName); // Return the relative path
        }




        [HttpPost("upload-images/{listingId}")]
        public async Task<IActionResult> UploadImages(int listingId, IFormFile file1, IFormFile file2, IFormFile file3)
        {
            _logger.LogInformation($"Got images from client. Attempting to update listing {listingId}");

            var paths = new string[3];
            paths[0] = await SaveFile(file1);
            paths[1] = await SaveFile(file2);
            paths[2] = await SaveFile(file3);

            _logger.LogInformation("Files saved.");

            // After saving the files, we will update the listing with the paths

            bool updateSuccess = await _listingRepository.UpdateListingImages(listingId, paths[0], paths[1], paths[2]);
            if (!updateSuccess)
            {
                return BadRequest("Failed to update listing with image paths.");
            }

            return Ok(new { Image1 = paths[0], Image2 = paths[1], Image3 = paths[2] });
        }



    }
}
