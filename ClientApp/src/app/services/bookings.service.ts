import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bookings } from '../models/booking.model';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {
  private apiUrl = 'http://your-api-url/api/Bookings';

  constructor(private http: HttpClient) { }

  getBookingsByGuest(guestId: string): Observable<Bookings[]> {
    return this.http.get<Bookings[]>(`${this.apiUrl}/ByGuest/${guestId}`);
  }
}
