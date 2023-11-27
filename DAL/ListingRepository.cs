using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RentHiveV2.Models;

namespace RentHiveV2.DAL
{
    public class ListingController : IListingRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ListingController> _logger;


        public ListingController(ApplicationDbContext context, ILogger<ListingController> logger)
        {
            _context = context;
            _logger = logger;
        }


        //CREATE A LISTING
        public async Task<bool> Create(IListing listing)
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


        //GET ALL THE LISTINGS. 

        public async Task<IEnumerable<IListing>> GetAll()
        {
            return await _context.Listing.ToListAsync();
        }



        //GET ALL LISTINGS MADE BY THE USER LOGGED IN
        public async Task<IEnumerable<IListing?>> GetByHost(string hostId)
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

            if (listing == null)
            {
                return false;
            }

            _context.Listing.Remove(listing);
            await _context.SaveChangesAsync();
            return true;

        }

        //GET A LISTING BY ITS ID
        public async Task<IListing?> GetById(int id)
        {

            var listings = await _context.Listing.FindAsync(id);

            if (listings == null)
            {
                return null;

            }
            return listings;

        }

        //UPDATE A LISTING

        [Authorize]
        public async Task<bool> Update(int id, IListing listing)
        {

            var existingListing = await _context.Listing.FindAsync(id); 
                if (existingListing == null) 
                { _logger.LogError($"Listing with listingId {id} not found.");
                    return false;
                }
            try
            {
                existingListing.Title = listing.Title;
                existingListing.Description = listing.Description;
                existingListing.Street = listing.Street;
                existingListing.City = listing.City;
                existingListing.Country = listing.Country;
                existingListing.ZipCode = listing.ZipCode;
                existingListing.State = listing.State;
                existingListing.Bedroom = listing.Bedroom;
                existingListing.Beds = listing.Beds;
                existingListing.Bathroom = listing.Bathroom;
                existingListing.PricePerNight = listing.PricePerNight;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[ListingRepository] listing FindAsync(id) failed when updating the ListingId {ListingId:0000}, error message: {ex}", listing, ex.Message) ;
                return false;
            }

        }



        //UPDATING THE LISTING IMAGES OF A LISTING

        public async Task<bool> UpdateListingImages(int listingId, string imagePath1, string imagePath2, string imagePath3)
        {

            _logger.LogInformation("LISTINGREPOSITORY: UpdateListingImages invoked."); 
            
            var listing = await GetById(listingId);

            if (listing == null)
            {
                _logger.LogError($"LISTINGREPOSITORY: Listing {listingId} does not exist");
                return false;
            }


            // Updating thep ath if the path has changed
            listing.Image1 = imagePath1 ?? listing.Image1;
            listing.Image2 = imagePath2 ?? listing.Image2;
            listing.Image3 = imagePath3 ?? listing.Image3;

            try
            {
                _context.Listing.Update(listing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LISTINGREPOSITORY: Error updating images for listing {listingId}");
                return false;
            }
        }







    }
}
