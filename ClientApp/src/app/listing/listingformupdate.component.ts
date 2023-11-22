import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { AuthorizeService } from '../../api-authorization/authorize.service'
import { ListingService } from "./listing.service";
import { catchError, finalize, switchMap, throwError } from "rxjs";



@Component({
  selector: "app-listing-listingformupdate",
  templateUrl: "./listingformupdate.component.html"
})

export class ListingformUpdateComponent {
  listingFormUpdate: FormGroup;
  isLoading: boolean = false;
  listingId: number =-1; 


  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private authorizeService: AuthorizeService,
    private _listingService: ListingService,
    private _route: ActivatedRoute,
  ) {
    this.listingFormUpdate = _formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      pricePerNight: ['', Validators.required],
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

  ngOnInit(): void {


    /* IMPORTANT: NEED TO FIND A WAY TO PREVENT USERS WHO'S LISTING IS NOT OF THEIR OWN TO ACCESS IT BY THE URL !!
    */

    this._route.params.subscribe(params => {
      this.listingId = +params['id'];
      this.loadItemForEdit(this.listingId);
    })
  }

  onSubmit() {
    console.log('Listing is updated, form is submitted.');
    console.log(this.listingFormUpdate.value);

    this.isLoading = true;

    this.authorizeService.getAccessToken()
      .pipe(
        switchMap(token => {
          if (!token) {
            throw new Error('Authorization token not found');
          }

          const newListing = this.listingFormUpdate.value;

          return this._listingService.updateListing(this.listingId, token, newListing);
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
            console.log('Updating a listing failed');
          }
        },
        error => console.error('HTTP error:', error)
      );
  }


  backToHostingDashboard() {
    this._router.navigate(['/hosting']);
  }

  loadItemForEdit(listingId: number) {
    this._listingService.getListingById(listingId)
      .subscribe(
        (listing: any) => {
          console.log(`retrieved listing: `, listing);
          this.listingFormUpdate.patchValue({
            title: listing.title,
            description: listing.description,
            pricePerNight: listing.pricePerNight,
            street: listing.street,
            city: listing.city,
            country: listing.country,
            zipCode: listing.zipCode,
            state: listing.state,
            bedroom: listing.bedroom,
            bathroom: listing.bathroom,
            beds: listing.beds

          });
        },
        (error: any) => {
          console.error(`Error loading listing for edit`, error);
        }
      );
  }

}