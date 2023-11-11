using Microsoft.EntityFrameworkCore;
using RentHiveV2.Models;

namespace RentHiveV2.DAL
{
    public class ListingRepository : IListingRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ListingRepository> _logger;


        public ListingRepository(ApplicationDbContext context, ILogger<ListingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        //CREATE A LISTING
        public async Task<bool> Create(Listing listing)
        {
            try
            {
                _context.Listing.Add(listing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ListingRepository] listing creation failed for {@listing}, Error Message: {ex}", listing, ex.Message);
                return false; 
            }
        }


        //GET ALL THE LISTINGS
        public async Task<IEnumerable<Listing>> GetAll()
        {
            return await _context.Listing.ToListAsync();
        }



        //GET ALL LISTINGS MADE BY THE USER LOGGED IN
        public async Task<IEnumerable<Listing?>> GetByHost(string hostId)
        {

            var listings = await _context.Listing.Where(e => e.ApplicationUserId == hostId).ToListAsync();

            if (listings == null)
            {
                return null;

            }
            return listings;

        }


        //DELETE A LISTING BY ITS ID.
        public async Task<bool> Delete(int id)
        {
            var listing = await _context.Listing.FindAsync(id);

            if(listing == null) {
            return false;
            }

            _context.Listing.Remove(listing);
            await _context.SaveChangesAsync();
            return true;

        }

        //GET A LISTING BY ITS ID
        public async Task<Listing?> GetById(int id)
        {

            var listings = await _context.Listing.FindAsync(id);

            if(listings == null)
            {
               return null;

            }
            return listings; 

        }

        //UPDATE A LISTING
        public async Task Update(Listing listing)
        {
            _context.Listing.Update(listing);
            await _context.SaveChangesAsync();
        }






    }
}
