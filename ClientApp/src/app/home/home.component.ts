import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {//implements OnInit {
  listings: any[] = [];

  constructor( ) { }

  /*
  ngOnInit() {
    this.listingsService.listings$.subscribe((listings) => {
      this.listings = listings;
    });
  }

  */
}
