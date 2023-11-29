import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { ListingService } from "../listing/listing.service";
import { HttpClient } from '@angular/common/http';






@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  keywords: string = '';
  country: string = '';
  city: string = '';
  searchResults: any[] = [];



  constructor(private http: HttpClient) { }

  search() {
    this.http.get('/api/search', { params: { keywords: this.keywords, country: this.country, city: this.city } })
      .subscribe((results: any) => {
        this.searchResults = results;
      });
  }
}


/**

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})

  //search component class for defining input properties
 //searchResults for holding the results from server
export class searchComponent {
  keywords: string = '';
  country: string = '';
  city: string = '';
  searchResults: any[] = [];


  //search constructor for httpclient dpendency injection
  constructor(private http: HttpClient) { }

  //search method that sends http get request with keywords
  // and updates searchResults with data from server

  search() {
    this.http.get('/api/search', { params: { keywords: this.keywords, country: this.country, city: this.city } })
      .subscribe((results: any) => {
        this.searchResults = results;
      });
  }
}

*/


