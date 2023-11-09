import { Component } from "@angular/core";
import { FormGroup, FormControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { SharedListingsService } from "../services/shared-listings.service";


@Component({
  selector: "app-listing-listingform",
  templateUrl: "./listingform.component.html"

})

export class ListingformComponent {

  listingForm: FormGroup;
  

  constructor(private _formBuilder: FormBuilder, private _router: Router, private _http: HttpClient, private listingsService: SharedListingsService) {

    
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
    const createUrl = 'api/listing/create';

    this._http.post<any>(createUrl, newListing).subscribe((response) => {
      if (response.success) {
        console.log(response.message);

        // Use the shared service to add the new listing to the listings array
        this.listingsService.addListing(newListing);

        this._router.navigate(['/hosting']);
      } else {
        console.log('Creating a listing failed');
      }
    });
  }

  backToHostingDashboard() {
    this._router.navigate(['/hosting']);
  }


}




