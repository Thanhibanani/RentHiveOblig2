using RentHiveV2.Models;

namespace RentHiveV2.DAL

{
    public interface IListingRepository
    {

        Task<IEnumerable<IListing>?> GetAll();
        Task<IListing?> GetById(int id);

        Task<bool> Create(IListing listing);
        Task <bool> Update (int id, IListing listing);
        Task <bool> Delete (int id);

        Task<IEnumerable<IListing>?> GetByHost(string hostId);

        Task<bool> UpdateListingImages(int listingId, string imagePath1, string imagePath2, string imagePath3);

    }
}
