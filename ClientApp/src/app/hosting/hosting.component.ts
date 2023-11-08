import { Component } from '@angular/core'
import { Listing } from './../models/listing.model';

@Component({
  selector: 'app-hosting-component',
  templateUrl: './hosting.component.html',
})

export class HostingComponent {



  //test to show lisitng

  listings: Listing[] = [
    {
      listingId: 1,
      title: 'houseTest1',
      description: 'House Description1.',
      pricePerNight: 750,
      street: 'streettt',
      city: 'cityyy',
      country: 'countryyy',
      zipCode: '123123123',
      state: 'Stateee',
      bedroom: 30,
      bathroom: 40,
      beds: 50,
      image1: 'path/to/image.jpg',
      createdDateTime: new Date()
    },

  ];



  constructor() { }


  createListing(): void {

  }

  editListing(listingId: number): void {
    
  }

  deleteListing(listingId: number): void {
    
  }







}
