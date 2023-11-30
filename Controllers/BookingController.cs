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
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingsController> _logger;
        private readonly IListingRepository _listingRepository;

        public BookingsController(IBookingRepository bookingRepository, ILogger<BookingsController> logger, IListingRepository listingRepository)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
            _listingRepository = listingRepository;
        }



        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookings>> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetById(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }



        //CREATING A BOOKING
        // POST: api/Bookings
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<Bookings>> CreateBooking([FromBody] Bookings booking)
        {

            _logger.LogInformation("[BookingController]: CreateBooking action method invoked.");

            if (booking == null)
            {
                _logger.LogError("[BookingController]: No content in booking model");
                return BadRequest("Invalid item data.");
            }

            //Logging for debugging and more information and monitoring: 
            _logger.LogInformation("Attempting to create a booking with the following model:");
            _logger.LogInformation($"The booking is for listing {booking.ListingId}");
            _logger.LogInformation($"The booking start-date is {booking.StartDate}");
            _logger.LogInformation($"The booking end-date is {booking.EndDate}");
            _logger.LogInformation($"The booking status is {booking.BookingStatus}");
            _logger.LogInformation($"The booking total price is {booking.TotalPrice}");

            //Getting the guestId (The account/user who is logged in is the guest)
            string guestId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"The booker (guest)'s guestId is: {guestId}");

            if (string.IsNullOrEmpty(guestId))
            {
                _logger.LogError("The guestId is null or empty.");

                return Forbid();
            }
            try
            {
                booking.GuestId = guestId; //Setting the guestId to the user logged in. 

                _logger.LogInformation("Checking if modelstate is valid.");
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Modelstate is valid.");

                    //Method in DAL
                    bool returnOk = await _bookingRepository.Create(booking);

                    if (returnOk)
                    {
                        var response = new { success = true, message = "Booking created successfully" };
                        return Ok(response);
                    }
                    else
                    {
                        var respone = new { success = false, message = "Booking creation" };
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
                _logger.LogError(ex, "An error occured while creating the booking.");
                //Need to return something here too. 
                return StatusCode(500, ex.Message);
            }

        }



        [Authorize]
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveBookingsForGuest()
        {
            string guestId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(guestId))
            {
                _logger.LogError("The guestId is null or empty.");

                return Forbid();
            }



            var bookings = await _bookingRepository.GetAllActiveByGuest(guestId);
            return Ok(bookings);
        }


        [Authorize]
        [HttpGet("previous")]
        public async Task<IActionResult> GetPreviousBookingsForGuest()
        {

            string guestId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(guestId))
            {
                _logger.LogError("The guestId is null or empty.");

                return Forbid();
            }

            var bookings = await _bookingRepository.GetAllPreviousByGuest(guestId);
            return Ok(bookings);
        }



        [Authorize]
        [HttpGet("pendingRequests")]
        public async Task<IActionResult> GetPendingRequests()
        {

            string hostId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(hostId))
            {
                _logger.LogError("The hostId is null or empty.");

                return Forbid();
            }

            var bookings = await _bookingRepository.GetAllPendingByHost(hostId);
            return Ok(bookings);
        }


        [Authorize]
        [HttpGet("acceptedRequests")]
        public async Task<IActionResult> GetAcceptedRequests()
        {

            string hostId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(hostId))
            {
                _logger.LogError("The hostId is null or empty.");

                return Forbid();
            }

            var bookings = await _bookingRepository.GetAllAcceptedByHost(hostId);
            return Ok(bookings);
        }

        [Authorize]
        [HttpGet("declinedRequests")]
        public async Task<IActionResult> GetDeclinedRequests()
        {

            string hostId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(hostId))
            {
                _logger.LogError("The hostId is null or empty.");

                return Forbid();
            }

            var bookings = await _bookingRepository.GetAllDeclinedByHost(hostId);
            return Ok(bookings);
        }

        [Authorize]
        [HttpGet("dueRequests")]
        public async Task<IActionResult> GetDueRequests()
        {

            string hostId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(hostId))
            {
                _logger.LogError("The hostId is null or empty.");

                return Forbid();
            }

            var bookings = await _bookingRepository.GetAllDueByHost(hostId);
            return Ok(bookings);
        }




        //CHANGES TO THE BOOKING (ACCEPT OG DECLINE)

        //I am aware that this probably could of been done with one function, with some extra parameters retrieved from the client, but I found this much easier. 

        //Accept booking

        [Authorize]
        [HttpPost("accept/{bookingId}")]
        public async Task<IActionResult> AcceptBooking(int bookingId)
        {
            _logger.LogInformation($"Attempting to accept booking with id {bookingId}.");

            //Finding the booking related to the id.
            var booking = await _bookingRepository.GetById(bookingId); 

            if(booking == null)
            {
                _logger.LogError($"Booking with ID {bookingId} was not found."); 
                return NotFound("The booking was not found.");
            }

            var listing = await _listingRepository.GetById(booking.ListingId);
            _logger.LogInformation($"Found listingId {listing.ListingId}"); 

            if(listing == null)
            {
                _logger.LogError($"ListingId with ID {listing.ListingId} is null"); 
            }

            //Extra control to prevent other users to accept booking on other's behalf. 
            string hostId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (hostId == null || hostId != listing.ApplicationUserId)
            {
                _logger.LogError($"Not-Authorized user tried to Accept Booking ID {bookingId}. The user was: {hostId}. The user for the listing is: {listing.ApplicationUserId}");
                return Forbid();
            }

            //Updating the bookingStatus
            _logger.LogInformation("Attempting to update the BookingStatus"); 

            var response = await _bookingRepository.AcceptBooking(bookingId);
            if (response)
            {
                return Ok(new { message = "Booking accepted successfully." });
            }
            else
            {
                _logger.LogError("Failed to accept booking.");
                return StatusCode(500, "An error occurred while accepting the booking.");
            }
          

        }

        //Decline booking
        [Authorize]
        [HttpPost("decline/{bookingId}")]
        public async Task<IActionResult> DeclineBooking(int bookingId)
        {
            _logger.LogInformation($"Attempting to decline booking with id {bookingId}.");

            //Finding the booking related to the id.
            var booking = await _bookingRepository.GetById(bookingId);

            if (booking == null)
            {
                _logger.LogError($"Booking with ID {bookingId} was not found.");
                return NotFound("The booking was not found.");
            }

            var listing = await _listingRepository.GetById(booking.ListingId);
            _logger.LogInformation($"Found listingId {listing.ListingId}");

            if (listing == null)
            {
                _logger.LogError($"ListingId with ID {listing.ListingId} is null");
            }

            //Extra control to prevent other users to accept booking on other's behalf. 
            string hostId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (hostId == null || hostId != listing.ApplicationUserId)
            {
                _logger.LogError($"Not-Authorized user tried to decline Booking ID {bookingId}. The user was: {hostId}. The user for the listing is: {listing.ApplicationUserId}");
                return Forbid();
            }

            //Updating the bookingStatus
            _logger.LogInformation("Attempting to update the BookingStatus");

            var response = await _bookingRepository.DeclineBooking(bookingId);
            if (response)
            {
                return Ok(new { message = "Booking declined successfully." });
            }
            else
            {
                _logger.LogError("Failed to decline booking.");
                return StatusCode(500, "An error occurred while declined the booking.");
            }


        }










    }
}










/*


  [Authorize]
[HttpPost]
public async Task<IActionResult> DeclineBooking(int bookingId)
{
    _logger.LogInformation($"Attempting to decline booking with id {bookingId}.");

    //Finding the booking related to the id.
    var booking = await _context.Booking.FindAsync(bookingId);


    if (booking == null)
    {
        _logger.LogError($"Booking with ID {bookingId} was not found.");
        return NotFound("The booking was not found.");
    }

    var eiendom = await _context.Eiendom.FindAsync(booking.EiendomId);
    _logger.LogInformation($"Found eiendomId {eiendom.EiendomID}");

    if (eiendom == null)
    {
        _logger.LogError($"EiendomID with ID {eiendom.EiendomID} is null");
    }

    //Extra control to prevent other users to accept booking on other's behalf. 
    var userId = _userManager.GetUserId(User);

    if (userId == null || userId != eiendom.ApplicationUserId)
    {
        _logger.LogError($"Not-Authorized user tried to Decline Booking ID {bookingId}. The user was: {userId}. The user for the listing is: {eiendom.ApplicationUserId}");
        return Forbid();
    }

    //Updating the bookingStatus
    _logger.LogInformation("Attempting to update the BookingStatus");
    try
    {
        booking.BookingStatus = BookingStatus.Declined;
        _context.Booking.Update(booking);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Bookingstatus changed to {booking.BookingStatus}.");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, $"Updating the bookinstatus failed. The booking status is {booking.BookingStatus}");
        return RedirectToAction("Index", "Hosting");
    }

    //Redirecting back to the hosting dashboard
    return RedirectToAction("Index", "Hosting");
}





 */




