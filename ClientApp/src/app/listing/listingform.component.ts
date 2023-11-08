import { Component } from "@angular/core";
import { FormGroup, FormControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: "app-listing-listingform",

  templateUrl: "./listingform.component.html"

})

export class ListingformComponent {

  listingForm: FormGroup;

  constructor(private _formBuilder: FormBuilder) {
    this.listingForm = _formBuilder.group({

      title: ['', Validators.required],
      description: ['', Validators.required],
      pricePerNight: [0, Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      zipCode: ['', Validators.required],
      state: ['', Validators.required],
      bedroom: [0, Validators.required],
      bathroom: [0, Validators.required],
      beds: [0, Validators.required],

    })
  }

  onSubmit() {
    console.log("Listing is created, form is submitted.")
    console.log(this.listingForm);
  }


}




