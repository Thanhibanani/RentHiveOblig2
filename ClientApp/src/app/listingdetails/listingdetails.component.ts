
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IListing } from '../models/listing.model';
import { ListingService } from '../listing/listing.service';
import { Bookings, BookingStatus } from '../models/booking.model';
import { BookingsService } from '../services/bookings.service';

@Component({
  selector: 'app-listing-listingdetails',
  templateUrl: './listingdetails.component.html',
})
export class ListingdetailsComponent implements OnInit {
  listingId!: number;
  listing: IListing | undefined;
  startDate: string = '';
  endDate: string = '';
  totalPrice: number = 0;
    bookings: any[] | undefined;

  constructor(private route: ActivatedRoute, private listingService: ListingService,private BookingService: BookingsService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.listingId = +params['id'];

      // Fetch listing
      this.listingService.getListingById(this.listingId).subscribe(
        (listing) => {
          this.listing = listing;
        },
        (error) => {
          console.error('Error fetching listing:', error);
        }
      );

      // Fetch bookings
      this.BookingService.getBookingsByListingId(this.listingId).subscribe(
        (bookings) => {
          this.bookings = bookings;
        },
        (error) => {
          console.error('Error fetching bookings:', error);
        }
      );
    });
  }

  calculateTotalPrice(): void {
    const startDateObj = new Date(this.startDate);
    const endDateObj = new Date(this.endDate);

    const timeDifference = endDateObj.getTime() - startDateObj.getTime();
    const diffDays = Math.ceil(timeDifference / (1000 * 3600 * 24));

    if (diffDays >= 0) {
      this.totalPrice = diffDays * this.listing!.pricePerNight;
    } else {
      this.totalPrice = 0;
    }
  }


  createNewBooking(
    guestId: string,
    propertyId: number,
    startDate: Date,
    endDate: Date,
    totalPrice: number,
    quantityDays: number
  ) {
    const newBooking: Bookings = {
      bookingId: 0, // 0 or null if it's a new booking
      guestId: guestId,
      propertyId: propertyId,
      startDate: startDate,
      endDate: endDate,
      totalPrice: totalPrice,
      bookingStatus:BookingStatus.Pending,
      quantityDays: quantityDays,
      applicationUser: undefined, 
      listing: undefined, 
    };

    //error handling
    this.BookingService.createBooking(newBooking).subscribe(
      (createdBooking: Bookings) => {
        console.log('Booking created successfully:', createdBooking);
      
      },
      (error: any) => {
        console.error('Error creating booking:', error);
        
      }
    );
  }
  }




