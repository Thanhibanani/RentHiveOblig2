using Microsoft.EntityFrameworkCore;
using RentHiveV2.Models;
using System.Linq.Expressions;

namespace RentHiveV2.DAL
{
    public class BookingRepository : IBookingRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ListingRepository> _logger;


        public BookingRepository(ApplicationDbContext context, ILogger<ListingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        //---CREATE A BOOKING---//
        public async Task<bool> Create(Bookings booking)
        {
            try
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ListingRepository] booking creation failed for {@booking}, Error Message: {ex}", booking, ex.Message);
                return false;
            }
        }




        public async Task<IEnumerable<Bookings>?> GetAll()
        {
            var allBookings = await _context.Bookings.ToListAsync();

            if (allBookings.Any())
            {
                return allBookings;
            }
            else { return null; }
        }

        //--------GUEST---------//:

        //All bookings by guest. 
        public async Task<IEnumerable<Bookings>?> GetAllByGuest(string guestId)
        {
            var guestsBooking = await _context.Bookings.Where(b => b.GuestId == guestId).ToListAsync();

            if (guestsBooking.Any())
            {
                return guestsBooking;
            }
            else { return null; }
        }


        //Active
        //Where Today < endDate
        public async Task<IEnumerable<Bookings>?> GetAllActiveByGuest(string guestId)
        {

            var activeBookings = await _context.Bookings.Where(b => b.GuestId == guestId 
                                                                 && b.EndDate >= DateTime.Today
                                                                 && b.BookingStatus != BookingStatus.Declined).ToListAsync();
            return activeBookings;
        }

        //Due
        //Where Today > endDate
        public async Task<IEnumerable<Bookings>?> GetAllPreviousByGuest(string guestId)
        {

            var previousBookings = await _context.Bookings.Where(b => b.GuestId == guestId 
                                                                    && b.EndDate < DateTime.Today
                                                                    && b.BookingStatus != BookingStatus.Declined).ToListAsync();
            return previousBookings;

        }




        public async Task<Bookings?> GetById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            return booking; 
        }




        //--------HOST---------//:


        //For fetching

        public async Task<IEnumerable<Bookings>?> GetAllPendingByHost(string hostId)
        {

            var pendingBookings = await _context.Bookings.Where(b => b.Listing.ApplicationUserId == hostId 
                                                                    && b.EndDate >= DateTime.Today
                                                                    && b.BookingStatus == BookingStatus.Pending).ToListAsync();
           
            return pendingBookings;
        }

        public async Task<IEnumerable<Bookings>?> GetAllAcceptedByHost(string hostId)
        {

            var acceptedBookings = await _context.Bookings.Where(b => b.Listing.ApplicationUserId == hostId
                                                                    && b.EndDate >= DateTime.Today
                                                                    && b.BookingStatus == BookingStatus.Accepted).ToListAsync();

            return acceptedBookings;
        }

        public async Task<IEnumerable<Bookings>?> GetAllDeclinedByHost(string hostId)
        {
            var declinedBookings = await _context.Bookings.Where(b => b.Listing.ApplicationUserId == hostId
                                                                    && b.EndDate >= DateTime.Today
                                                                    && b.BookingStatus == BookingStatus.Declined).ToListAsync();

            return declinedBookings;
        }

        public async Task<IEnumerable<Bookings>?> GetAllDueByHost(string hostId)
        {
            var dueBookings = await _context.Bookings.Where(b => b.Listing.ApplicationUserId == hostId
                                                              && b.EndDate < DateTime.Today).ToListAsync();

            return dueBookings;
        }


        //---UPDATE THE BOOKING STATUS---//
        public async Task<bool> AcceptBooking(int bookingId)
        {
            var newStatus = BookingStatus.Accepted;
            var booking = await GetById(bookingId);       
            booking.BookingStatus = newStatus;
            try{
                await _context.SaveChangesAsync();
                _logger.LogInformation($"BookingRepository: Bookingstatus changed to {booking.BookingStatus}.");
                return true;
            }
            catch(Exception ex) {
                _logger.LogError(ex, $"Updating the bookinstatus failed. The booking status is {booking.BookingStatus}");
                return false; 
            }
        }

        public async Task<bool> DeclineBooking(int bookingId)
        {
            var newStatus = BookingStatus.Declined;
            var booking = await GetById(bookingId);
            booking.BookingStatus = newStatus;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"BookingRepository: Bookingstatus changed to {booking.BookingStatus}.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Updating the bookinstatus failed. The booking status is {booking.BookingStatus}");
                return false;
            }
        }
    }


}
