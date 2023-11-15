import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { catchError, switchMap, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service'
import { ListingService } from "./listing.service";

@Component({
  selector: "app-listing-listingform",
  templateUrl: "./listingform.component.html"
})

export class ListingformComponent {
  listingForm: FormGroup;
  isLoading: boolean = false;

  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private authorizeService: AuthorizeService,
    private _listingService: ListingService,
  ) {
    this.listingForm = _formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      pricePerNight: [, Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      zipCode: ['', Validators.required],
      state: [''],
      bedroom: [, Validators.required],
      bathroom: [, Validators.required],
      beds: [, Validators.required],
    });
  }

  onSubmit() {
    console.log('Listing is created, form is submitted.');
    console.log(this.listingForm.value);

    this.isLoading = true;

    this.authorizeService.getAccessToken()
      .pipe(
        switchMap(token => {
          if (!token) {
            throw new Error('Authorization token not found');
          }

          const newListing = this.listingForm.value;

          return this._listingService.createListing(newListing, token);
        }),
        catchError(error => {
          console.error('HTTP error:', error);
          return throwError('Creating a listing failed');
        }),
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe(
        response => {
          if (response.success) {
            console.log(response.message);
            this._router.navigate(['/hosting']);
          } else {
            console.log('Creating a listing failed');
          }
        },
        error => console.error('HTTP error:', error)
      );
  }

  backToHostingDashboard() {
    this._router.navigate(['/hosting']);
  }
}
