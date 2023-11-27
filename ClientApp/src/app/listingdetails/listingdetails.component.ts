
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IListing } from '../models/listing.model';
import { ListingService } from '../listing/listing.service';
import { Bookings, BookingStatus } from '../models/booking.model';
import { BookingsService } from '../services/bookings.service';
import { AuthorizeService } from '../../api-authorization/authorize.service';

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

  constructor(
    private route: ActivatedRoute,
    private listingService: ListingService,
    private BookingService: BookingsService,
    private authorizeService: AuthorizeService,
    
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.listingId = +params['id'];
      this.listingService.getListingById(this.listingId).subscribe(
        (listing) => {
          this.listing = listing;
        },
        (error) => {
          console.error('Error fetching listing:', error);
        }
      );
    });
  }

  //To calculate the difference in days between start and end-date. This will be used to set the quantitydays,
  //but also to calculate the total price.

  calculateDiffDays(): number {
    const startDateObj = new Date(this.startDate);
    const endDateObj = new Date(this.endDate);

    const timeDifference = endDateObj.getTime() - startDateObj.getTime();
    const diffDays = Math.ceil(timeDifference / (1000 * 3600 * 24));

    return diffDays; 
  }


  //We calculate the total price by multiplying the difference between start and enddate with the listing's pricePerNight.
  calculateTotalPrice(): void {
    const diffDays = this.calculateDiffDays(); 

    if (diffDays >= 0) {
      this.totalPrice = diffDays * this.listing!.pricePerNight;
    } else {
      this.totalPrice = 0;
    }
  }



  createNewBooking() {
    const newBooking: Bookings = {
      bookingId: 0,
      propertyId: this.listingId,
      startDate: new Date(this.startDate),
      endDate: new Date(this.endDate),
      totalPrice: this.totalPrice,
      bookingStatus: BookingStatus.Pending,
      quantityDays: this.calculateDiffDays(),
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




