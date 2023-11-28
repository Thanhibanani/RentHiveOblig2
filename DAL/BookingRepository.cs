using RentHiveV2.Models;

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


        //CREATE A BOOKING
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




        public Task<IEnumerable<Bookings>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Bookings?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }










}
