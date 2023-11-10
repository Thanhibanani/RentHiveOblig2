import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IListing } from '../models/listing.model'


@Injectable({
  providedIn: 'root'
})
export class ListingService {

  private baseUrl = 'api/listing/';

  constructor(private _http: HttpClient) { }



  getListings(): Observable<IListing[]> {
    return this._http.get<IListing[]>(this.baseUrl); 
  }


  createListing(newListing: IListing, token: string): Observable<any> {
    const createUrl = '/api/listing/create'; 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });


    return this._http.post<any>(createUrl, newListing, { headers });

  }


}
