﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ApplicationDbContext _context; //NEED TO REMOVE THIS LINE BECAUSE IT IS NOW IMPLEMENTED IN WITH REPOSITORY INSTEAD!
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IBookingRepository bookingRepository, ApplicationDbContext context, ILogger<BookingsController> logger)
        {
            _context = context;
            _bookingRepository = bookingRepository;
            _logger = logger;
        }






        // GET: api/Bookings/ByGuest/{guestId}
        /*
        [HttpGet("ByGuest/{guestId}")]
        public async Task<ActionResult<IEnumerable<Bookings>>> GetPendingBookingsByGuest(string guestId)
        {

            _logger.LogInformation($"Attempting to get all pending bookings for guest {guestId}");

            const pendingBookings = await Bookings.getBookingByUser(guestId)
                     
            
                
 

            if (!bookings.Any())
            {
                return NotFound();
            }

            return bookings;
        }
        */


        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookings>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }


        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookings>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

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




    }
}