using RentHiveV2.Models;

namespace RentHiveV2.DAL

{
    public interface IListingRepository
    {

        Task<IEnumerable<Listing>?> GetAll();
        Task<Listing?> GetById(int id);

        Task<bool> Create(Listing listing);
        Task <bool> Update (int id, Listing listing);
        Task <bool> Delete (int id);

        Task<IEnumerable<Listing>?> GetByHost(string hostId);

        Task<bool> UpdateListingImages(int listingId, string imagePath1, string imagePath2, string imagePath3);

    }
}
