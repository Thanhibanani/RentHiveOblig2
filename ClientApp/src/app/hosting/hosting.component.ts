import { Component, OnInit  } from '@angular/core'
import { IListing } from './../models/listing.model';
import { Router } from '@angular/router';
import { ListingService } from '../listing/listing.service';
import { AuthorizeService } from '../../api-authorization/authorize.service'
import { Observable, catchError, switchMap, throwError } from 'rxjs';



@Component({
  selector: 'app-hosting-component',
  templateUrl: './hosting.component.html',
})

export class HostingComponent implements OnInit {
  listings: IListing[] = [];


  constructor(private _router: Router, private _listingService: ListingService, private authorizeService: AuthorizeService,) { }


  ngOnInit() {
    this.getUserListings();

  }



  //Retriving all the user's listings (Where current user is host). 
  getUserListings(): void {
    this._listingService.getUserListings()
      .subscribe(data => {
        console.log('All', JSON.stringify(data)); // For debugging purposes
        this.listings = data;
      });
  }


  navigateToCreateListingForm(): void {
    this._router.navigate(['/createListing'])
  }

  editListing(listingId: number): void {
    
  }

  viewListing(listing: IListing): void {
    const id = listing.listingId; 
    this._router.navigate(['/listingdetails', id])
  }


  deleteListing(listing: IListing): void {

    const confirmDelete = confirm(`Are you sure you want to delete the listing "${listing.title}" ?`);
    if (confirmDelete) {

      this.authorizeService.getAccessToken()
        .pipe(
          switchMap(token => {
            if (!token) {
              throw new Error('Authorization token not found!');
            }
            return this._listingService.deleteListing(listing.listingId, token);
          }),
          catchError(error => {
            console.error('HTTP error', error);
            return throwError('Deleting a listing failed');
          })
          )
        .subscribe(
          (response) => {
            if (response.success) {
              console.log(response.message);
              // this.filteredListings = this.filteredListings.filter(i => i !== listing); --> To update the list locally after deleting.
              this.getUserListings(); //Temporarily (fetching all the listings again). 
            }
          },
          (error) => {
            console.log("Error deleting listing:", error);
          });
    }
    
  }







}
