// Assuming you have equivalent TypeScript types for ApplicationUser and Listing
interface ApplicationUser {
  // Define properties of ApplicationUser that you need
}

interface Listing {
  // Define properties of Listing that you need
}

// Enum for BookingStatus
enum BookingStatus {
  Pending,
  Accepted,
  Declined
}

// TypeScript interface for Bookings
export interface Bookings {
  bookingId: number;
  guestId: string; // Foreign key reference to ApplicationUser
  propertyId: number; // Foreign key reference to Listing
  startDate: Date;
  endDate: Date;
  totalPrice: number;
  bookingStatus: BookingStatus;
  quantityDays: number;

  // Navigation properties
  applicationUser?: ApplicationUser;
  listing?: Listing;
}
