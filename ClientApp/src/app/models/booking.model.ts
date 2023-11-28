




export interface Bookings {
  bookingId: number;
  listingId: number; 
  startDate: Date;
  endDate: Date;
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
