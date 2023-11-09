import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedListingsService {
  private listingsSubject = new BehaviorSubject<any[]>([]);
  listings$ = this.listingsSubject.asObservable();

  // Get the current listings
  getListings(): any[] {
    return this.listingsSubject.getValue();
  }

  // Add a new listing to the listings array
  addListing(newListing: any): void {
    const currentListings = this.getListings();
    currentListings.push(newListing);
    this.listingsSubject.next(currentListings);
  }
}
