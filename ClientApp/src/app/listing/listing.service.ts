import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IListing } from '../models/listing.model'
import { AuthorizeService } from '../../api-authorization/authorize.service';


@Injectable({
  providedIn: 'root'
})


export class ListingService {

  private baseUrl = 'api/listing/';

  constructor(private _http: HttpClient, private authorizeService: AuthorizeService) { }



  getListings(): Observable<IListing[]> {
    return this._http.get<IListing[]>(this.baseUrl);
  }





  getUserListings(): Observable<IListing[]> {
    const getByHostUrl = '/api/listing/getByHost';

    return this._http.get<IListing[]>(getByHostUrl);
  }




  createListing(newListing: IListing, token: string): Observable<any> {
    const createUrl = '/api/listing/create';
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this._http.post<any>(createUrl, newListing, { headers });
  }

  getListingById(id: number): Observable<IListing> {
    const url = `${this.baseUrl}${id}`;
    return this._http.get<IListing>(url);
  }



  deleteListing(listingId: number, token: string): Observable<any> {
    const deleteUrl = `${this.baseUrl}/delete/${listingId}`;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this._http.delete(deleteUrl, { headers });
  }


  updateListing(listingId: number, token: string, newListing: any): Observable<any> {
    const url = `${this.baseUrl}/update/${listingId}`;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    newListing.listingId = listingId; 
    return this._http.put<any>(url, newListing, { headers });
  }


}
