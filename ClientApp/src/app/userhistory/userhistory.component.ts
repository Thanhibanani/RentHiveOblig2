import { Component, OnInit } from '@angular/core'
import { Bookings } from '../models/booking.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userhistory-component',
  templateUrl: './userhistory.component.html',
})

export class UserhistoryComponent {
  // In your Angular component
  activeBookings: Bookings[] = [];
  previousBookings: Bookings[] = [];
  bookingsService: any;

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    // Assuming `this.bookingsService.getBookings()` is a method that fetches the bookings
    this.bookingsService.getBookings().subscribe((bookings: any[]) => {
      const currentDate = new Date();
      this.activeBookings = bookings.filter(booking => new Date(booking.endDate) >= currentDate);
      this.previousBookings = bookings.filter(booking => new Date(booking.endDate) < currentDate);
    });
  }
}
