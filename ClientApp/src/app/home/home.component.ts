import { Component, OnInit } from '@angular/core';
import { IListing } from '../models/listing.model';
import { Router } from '@angular/router';
import { ListingService } from '../listing/listing.service';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
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



  /*

  TODO:
          NEED TO IMPLEMENT A ROUTING TO OTHER PAGE METHOD FOR WHEN THESE LISTINGS ARE CLICKED ON. 



   */

}
