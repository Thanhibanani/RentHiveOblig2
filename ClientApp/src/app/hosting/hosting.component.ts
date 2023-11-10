import { Component, OnInit  } from '@angular/core'
import { IListing } from './../models/listing.model';
import { Router } from '@angular/router';
import { ListingService } from '../listing/listing.service';
import { AuthorizeService } from '../../api-authorization/authorize.service'


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

  deleteListing(listingId: number): void {
    
  }







}
