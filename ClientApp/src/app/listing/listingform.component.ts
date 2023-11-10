import { Component } from "@angular/core";
import { FormGroup, FormControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { SharedListingsService } from "../services/shared-listings.service";
import { AuthorizeService } from '../../api-authorization/authorize.service'


@Component({
  selector: "app-listing-listingform",
  templateUrl: "./listingform.component.html"

})

export class ListingformComponent {

  listingForm: FormGroup;
  

  constructor(private _formBuilder: FormBuilder, private _router: Router, private _http: HttpClient, private listingsService: SharedListingsService, private authorizeService: AuthorizeService) {

    
    this.listingForm = _formBuilder.group({

      title: ['', Validators.required],
      description: ['', Validators.required],
      pricePerNight: [, Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      zipCode: ['', Validators.required],
      state: ['',],
      bedroom: [, Validators.required],
      bathroom: [, Validators.required],
      beds: [, Validators.required],

    })
  }


  onSubmit() {
    console.log('Listing is created, form is submitted.');
    console.log(this.listingForm);
    const newListing = this.listingForm.value;
    const createUrl = '/api/listing/create'; 

    // Retrieving the access token
    this.authorizeService.getAccessToken().subscribe(
      token => {
        if (token) {
          const headers = { 'Authorization': `Bearer ${token}` };

          this._http.post<any>(createUrl, newListing, { headers }).subscribe((response) => {
            if (response.success) {
              console.log(response.message);
              this.listingsService.addListing(response.data);
              this._router.navigate(['/hosting']);
            } else {
              console.log('Creating a listing failed');
            }
          }, error => {
            console.error('HTTP error:', error);
          });
        } else {
          console.error('Authorization token not found');
        }
      },
      error => console.error('Error fetching access token:', error)
    );
  }




   backToHostingDashboard() {
    this._router.navigate(['/hosting']);
   }

}
