import { Component, OnInit } from '@angular/core';
import { Bookings } from '../models/booking.model';
import { BookingsService } from '../services/bookings.service';

@Component({
  selector: 'app-userhistory-component',
  templateUrl: './userhistory.component.html',
})
export class UserhistoryComponent implements OnInit {
  activeBookings: Bookings[] = [];
  previousBookings: Bookings[] = [];

  constructor(private bookingsService: BookingsService) { }

  ngOnInit() {
    this.loadBookingsForGuest('guestId'); // Replace 'guestId' with the actual guest ID
  }

  loadBookingsForGuest(guestId: string) {
    this.bookingsService.getBookingsByGuest(guestId).subscribe((bookings: Bookings[]) => {
      const currentDate = new Date();
      this.activeBookings = bookings.filter(booking => new Date(booking.endDate) >= currentDate);
      this.previousBookings = bookings.filter(booking => new Date(booking.endDate) < currentDate);
    });
  }
}
