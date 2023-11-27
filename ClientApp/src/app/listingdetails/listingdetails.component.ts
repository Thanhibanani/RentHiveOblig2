
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
    bookings: any[] | undefined;

  constructor(
    private route: ActivatedRoute,
    private listingService: ListingService,
    private BookingService: BookingsService,
    private authorizeService: AuthorizeService,
    
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
 }




