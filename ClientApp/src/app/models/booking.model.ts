
export interface Bookings {
  guestId: string; 
  bookingId: number;
  listingId: number; 
  startDate: string; //THIS HAS TO BE A STRING BECAUSE CONVERTING TO ISOSTRING
  endDate: string; //THIS HAS TO BE A STRING BECAUSE CONVERTING TO ISOSTRING
  totalPrice: number;
  bookingStatus: BookingStatus;
  quantityDays: number;

}

// Enum for BookingStatus: This is to define the different statuses a booking can have. 
export enum BookingStatus {
  Pending,
  Accepted,
  Declined
}
