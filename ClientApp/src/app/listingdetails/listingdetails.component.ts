
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IListing } from '../models/listing.model';
import { ListingService } from '../listing/listing.service';

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

  constructor(private route: ActivatedRoute, private listingService: ListingService) { }

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


  bookNow(): void {
    // Add your booking logic here
  }



}
