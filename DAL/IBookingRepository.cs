using RentHiveV2.Models;

namespace RentHiveV2.DAL
{
    public interface IBookingRepository
    {

        Task<bool> Create(Bookings booking);
        Task<IEnumerable<Bookings>?> GetAll();
        Task<Bookings?> GetById(int id);





        //FOR THE GUEST: 
        Task<IEnumerable<Bookings>?> GetAllByGuest(string guestId);
        Task<IEnumerable<Bookings>?> GetAllActiveByGuest(string guestId);
        Task<IEnumerable<Bookings>?> GetAllPreviousByGuest(string guestId);



        //FOR THE HOST: 


        //NEED TO IMPLEMENT: 
        //GET BOOKING BY HOSTID AND STATUS.
        //GET BOOKING BY HOSTID, STATUS AND DATE.


    }
}
