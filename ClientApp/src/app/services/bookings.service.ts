import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bookings } from '../models/booking.model';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {

  private apiUrl = '/api/booking';

  constructor(private http: HttpClient, private authorizeService: AuthorizeService) { }

  getBookingsByGuest(guestId: string): Observable<Bookings[]> {
    return this.http.get<Bookings[]>(`${this.apiUrl}/ByGuest/${guestId}`);
  }





  //CLIENT CREATE (POST) REQUEST TO THE SERVER

  createBooking(newBooking: Bookings, token: string): Observable<any> {

    const createUrl = "/api/bookings/create"

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post<any>(createUrl, newBooking, { headers });
  }




  getBookingsByListingId(listingId: number): Observable<any[]> {
    const url = `${this.apiUrl}/bookings?listingId=${listingId}`;
    return this.http.get<any[]>(url);
  }
}
