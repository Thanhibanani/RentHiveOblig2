import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { ListingService } from "./listing.service";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

}





@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class searchComponent {
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
