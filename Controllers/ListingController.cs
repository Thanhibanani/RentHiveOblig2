    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            private readonly ApplicationDbContext _context;
        


            public ListingController(IListingRepository listingRepository, ILogger<ListingController> logger, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
            {
                _listingRepository = listingRepository;
                _logger = logger;
                _hostEnvironment = hostEnvironment;
                _context = context;
            

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
                    return NotFound(new Responses { Success = false, Message = "No listings found." });
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
                return NotFound(new Responses { Success = false, Message = "No listings found." });
            }

            return Ok(listings);
        }


        //CREATING A NEW LISTING.
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Listing newListing)
        {

            _logger.LogInformation("[ListingController]: Create action method invoked.");

            if (newListing == null)
            {
                return BadRequest(new Responses { Success = false, Message = "Invalid item data." });
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
                        var response = new Responses { Success = true, Message = $"Listing {newListing.Title} created successfully" };
                        return Ok(response);

                    }
                    else
                    {
                        var response = new Responses { Success = false, Message = "Listing creation failed" };
                        return BadRequest(response); // Return BadRequest with the failure response
                    }
                }
                //If Modelstate is invalid.
                else
                {
                    _logger.LogError("ModelState is invalid");
                    var response = new { Success = false, Message = "Listing creation failed", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) };
                    return BadRequest(response); // Return BadRequest with validation errors
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the listing.");
                return StatusCode(500, new Responses { Success = false, Message = ex.Message });
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
                _logger.LogError($"ListingController: listing deletion failed for the listing {id}");
                return BadRequest(new Responses { Success = false, Message = $"Listing {id} deletion failed." });
            }
            return Ok(new Responses { Success = true, Message = $"Listing {id} deleted successfully" });
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

            if (listing == null || listing.ApplicationUserId != userId)
            {
                _logger.LogError("The user is not the owner of the listing");
                _logger.LogInformation($"User {(userId ?? "null")} attempted to delete listing {id}");

                return Forbid();
            }


            if (newListing == null)
            {
                return BadRequest(new Responses { Success = false, Message = "Invalid listing data" });
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
                var response = new Responses { Success = true, Message = $"Listing {newListing.Title} updated successfully." };
                return Ok(response);
            }
            else
            {
                var response = new Responses { Success = false, Message = $"Failed to update listing {newListing.Title}." };
                return BadRequest(response); ;
            }

        }



        //SAVING IMAGE AND UPDATING IMAGE PATH

        [HttpPost("upload-images/{listingId}")]
        public async Task<IActionResult> SaveImage(IFormFile? file1, IFormFile? file2, IFormFile? file3, int listingId)
        {

            if (listingId == 0)
            {
                return NotFound("The listing does not exist.");
            }


            var listing = await _listingRepository.GetById(listingId);
            if (listing == null)
            {
                return NotFound("The property cannot be found.");

            }



            if (file1 != null)
            {
                var pathForwardSlash1 = "/Images/" + file1.FileName;

                var webPath1 = "Images" + Path.DirectorySeparatorChar + file1.FileName;
                var fullPath1 = Path.Combine(_hostEnvironment.WebRootPath, webPath1);

                //Some logging used for debugging on earlier issues
                _logger.LogInformation("webpath is: " + webPath1);
                _logger.LogInformation("fullpath is: " + fullPath1);
                _logger.LogInformation("hostEnvironment webrootpath: " + _hostEnvironment.WebRootPath);

                using (var stream = new FileStream(fullPath1, FileMode.Create))
                {
                    await file1.CopyToAsync(stream);
                }

                listing.Image1 = pathForwardSlash1;
                _context.Update(listing);

            }
            if (file2 != null)
            {
                var pathForwardSlash2 = "/Images/" + file2.FileName;

                var webPath2 = "Images" + Path.DirectorySeparatorChar + file2.FileName;
                var fullPath2 = Path.Combine(_hostEnvironment.WebRootPath, webPath2);

                //Some logging used for debugging on earlier issues
                _logger.LogInformation("webpath is: " + webPath2);
                _logger.LogInformation("fullpath is: " + fullPath2);
                _logger.LogInformation("hostEnvironment webrootpath: " + _hostEnvironment.WebRootPath);

                using (var stream = new FileStream(fullPath2, FileMode.Create))
                {
                    await file2.CopyToAsync(stream);
                }

                listing.Image2 = pathForwardSlash2;
                _context.Update(listing);

            }

            if (file3 != null)
            {

                var pathForwardSlash3 = "/Images/" + file3.FileName;

                var webPath3 = "Images" + Path.DirectorySeparatorChar + file3.FileName;
                var fullPath3 = Path.Combine(_hostEnvironment.WebRootPath, webPath3);

                //Some logging used for debugging on earlier issues
                _logger.LogInformation("webpath is: " + webPath3);
                _logger.LogInformation("fullpath is: " + fullPath3);
                _logger.LogInformation("hostEnvironment webrootpath: " + _hostEnvironment.WebRootPath);

                using (var stream = new FileStream(fullPath3, FileMode.Create))
                {
                    await file3.CopyToAsync(stream);
                }
                listing.Image3 = pathForwardSlash3;
                _context.Update(listing);
            }

            //Finally save the changes.

            await _context.SaveChangesAsync(); // Save the changes


            return Ok(new Responses { Success = true, Message = "Images uploaded" });
        }








    }
}