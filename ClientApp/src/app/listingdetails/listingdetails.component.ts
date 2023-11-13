import { Component } from '@angular/core';
import { IListing } from '../models/listing.model';
import { Router } from '@angular/router';
import { ListingService } from '../listing/listing.service';

@Component({
  selector: 'app-listingdetails',
  templateUrl: './listingdetails.component.html',
  styleUrls: ['./listingdetails.component.css']
})
export class HomeComponent {//implements OnInit {
  listings: IListing[] = [];

  constructor(private _router: Router, private _listingService: ListingService) { }


  ngOnInit() {
    this.getListings();
  }


  //Method to get the all listings:

  getListings(): void {
    this._listingService.getListings()
      .subscribe(data => {
        console.log('All', JSON.stringify(data)); //Just for debugging purposes, remember to REMOVE or COMMENT it out. 
        this.listings = data;
      });
  }
}
