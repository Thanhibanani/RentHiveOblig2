using RentHiveV2.Models;

namespace RentHiveV2.DAL
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Bookings>?> GetAll();
        Task<Bookings?> GetById(int id);

        Task<bool> Create(Bookings booking);




        //NEED TO IMPLEMENT: 

        //GET BOOKING BY HOSTID AND STATUS.
        //GET BOOKING BY GUESTID AND STATUS.
        //GET BOOKING BY GUESTID, STATUS AND DATE.
        //GET BOOKING BY HOSTID, STATUS AND DATE.



    }
}
