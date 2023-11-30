using RentHiveV2.Models;

namespace RentHiveV2.DAL
{
    public interface IBookingRepository
    {

        Task<bool> Create(Bookings booking);
        Task<IEnumerable<Bookings>?> GetAll();
        Task<Bookings?> GetById(int id);





        //GETS FOR THE GUEST: 
        Task<IEnumerable<Bookings>?> GetAllByGuest(string guestId);
        Task<IEnumerable<Bookings>?> GetAllActiveByGuest(string guestId);
        Task<IEnumerable<Bookings>?> GetAllPreviousByGuest(string guestId);



        //GETS FOR THE HOST: 

        Task<IEnumerable<Bookings>?> GetAllPendingByHost(string hostId);
        Task<IEnumerable<Bookings>?> GetAllAcceptedByHost(string hostId);

        Task<IEnumerable<Bookings>?> GetAllDeclinedByHost(string hostId);

        Task<IEnumerable<Bookings>?> GetAllDueByHost(string hostId);


        //UPDATE THE BOOKING STATUS

        Task <bool> AcceptBooking(int bookingId);
        Task<bool> DeclineBooking(int bookingId);



    }
}
