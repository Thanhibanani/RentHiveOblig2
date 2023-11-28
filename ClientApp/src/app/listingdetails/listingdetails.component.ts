
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IListing } from '../models/listing.model';
import { ListingService } from '../listing/listing.service';
import { Bookings, BookingStatus } from '../models/booking.model';
import { BookingsService } from '../services/bookings.service';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { catchError, finalize, switchMap, throwError } from 'rxjs';

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
    private _router: Router,
    
  ) { }

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

  //To calculate the difference in days between start and end-date. This will be used to set the quantitydays,
  // but also to calculate the total price.

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



  RequestBooking() {

    console.log("Initiating booking request.");

    this.authorizeService.getAccessToken()
      .pipe(switchMap(token => {
        if (!token) {
          throw new Error('Authorization token not found');
        }

        const newBooking: Bookings = {
          bookingId: 0,
          listingId: this.listingId,
          startDate: new Date(this.startDate),
          endDate: new Date(this.endDate),
          totalPrice: this.totalPrice,
          bookingStatus: BookingStatus.Pending,
          quantityDays: this.calculateDiffDays(),
        };

        console.log("Booking request: ", newBooking)

        return this.BookingService.createBooking(newBooking, token);

      }),
        catchError(error => {
          console.error('HTTP error:', error);
          return throwError('Creating a booking failed');
        }),
      )
      .subscribe(
        response => {
          if (response.success) {
            console.log(response.message);
            this._router.navigate(['']);
          } else {
            console.log('Creating a listing failed');
          }
        },
        error => console.error('HTTP error:', error)
      );
  }
}




