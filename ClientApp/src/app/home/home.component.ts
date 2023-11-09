import { Component, OnInit } from '@angular/core';
import { SharedListingsService } from '../services/shared-listings.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  listings: any[] = [];

  constructor(private listingsService: SharedListingsService) { }

  ngOnInit() {
    this.listingsService.listings$.subscribe((listings) => {
      this.listings = listings;
    });
  }
}
