import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bookings } from '../models/booking.model';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {

  private apiUrl = '/api/bookings';

  constructor(private http: HttpClient, private authorizeService: AuthorizeService) { }

  getBookingsByGuest(guestId: string): Observable<Bookings[]> {
    return this.http.get<Bookings[]>(`${this.apiUrl}/ByGuest/${guestId}`);
  }





  //CLIENT CREATE (POST) REQUEST TO THE SERVER
  createBooking(newBooking: Bookings, token: string): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post<any>(this.apiUrl, newBooking, { headers });
  }
}
