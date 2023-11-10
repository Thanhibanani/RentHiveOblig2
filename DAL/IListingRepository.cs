using RentHiveV2.Models;

namespace RentHiveV2.DAL

{
    public interface IListingRepository
    {

        Task<IEnumerable<Listing>?> GetAll();
        Task<Listing?> GetById(int id);

        Task<bool> Create(Listing listing);
        Task Update (Listing listing);
        Task <bool> Delete (int id);


    }
}
