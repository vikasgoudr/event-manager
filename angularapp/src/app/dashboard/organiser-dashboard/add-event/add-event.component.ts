/// Summary: This component allows users to add events along with their payment tiers and event questions.

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';
import { Buffer } from 'buffer';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { PaymentTier } from 'src/models/paymentTier.model';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css'],
})
export class AddEventComponent implements OnInit {
  eventForm!: FormGroup;
  tokenData!: TokenData | null;
  showAgeLimitColumns: number = 0;
  imageBlob: any;
  imageBuffer: any;
  showTierInputColumns: number = 0;
  paymentTiers: PaymentTier[] = [];
  numberOfOptions: number = 0;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private tokenDataService: TokenDataService,
    private snackbar: SnackbarService
  ) {}

  ngOnInit(): void {
    /// Initialize the form with default values and validations
    this.tokenData = this.tokenDataService.getTokenData();
    this.eventForm = this.formBuilder.group({
      id: [0],
      name: [''],
      startDate: [''],
      endDate: [''],
      organizerId: [0],
      ageLimitLower: [0],
      ageLimitUpper: [0],
      hasAgeLimit: [false],
      posterImage: [null],
      isFreeToAttend: [true],
      eventCapacity: [0],
      address: this.formBuilder.group({
        id: [0],
        line1: [''],
        line2: [''],
        city: [''],
        state: [''],
        country: [''],
      }),
    });
  }

  /// Summary: Triggered when the user selects an image for the event poster.
  /// Comments: Converts the selected image to a Base64 format and updates the eventForm's posterImage property.
  onImageSelect(event: any) {
    const file = event.target.files[0];

    if (file) {
      this.convertImageToBase64(file);
    }
  }

  /// Summary: Converts the selected image to a Base64 format.
  /// Comments: Uses FileReader to read the image file, convert it to an ArrayBuffer, and then to Base64 format.
  convertImageToBase64(file: File) {
    const reader = new FileReader();
    reader.onloadend = () => {
      if (reader.result) {
        const arrayBuffer = reader.result as ArrayBuffer;
        this.imageBuffer = Array.from(new Uint8Array(arrayBuffer));

        const base64Image = Buffer.from(this.imageBuffer).toString('base64');
        console.log(base64Image);
        this.eventForm.patchValue({
          posterImage: base64Image,
        });
      }
    };
    reader.readAsArrayBuffer(file);
  }

  /// Summary: Triggered when the user selects Yes/No for age limit.
  /// Comments: Updates the showAgeLimitColumns and eventForm properties based on user selection.
  onAgeLimitChange(event: any): void {
    console.log(event.value);
    console.log(event.target.value);
    if (event.target.value == 'true') {
      this.showAgeLimitColumns = 1;
      this.eventForm.patchValue({ hasAgeLimit: true });
    } else {
      this.showAgeLimitColumns = 0;
      this.eventForm.patchValue({ hasAgeLimit: false });
    }
    console.log(this.showAgeLimitColumns);
  }

  /// Summary: Triggered when the user selects Yes/No for event type (free or paid).
  /// Comments: Updates the showTierInputColumns and eventForm properties based on user selection.
  onEventTypeChange(event: any): void {
    console.log(event.value);
    console.log(event.target.value);
    if (event.target.value == 'true') {
      this.showTierInputColumns = 0;
      this.eventForm.patchValue({ isFreeToAttend: true });
    } else {
      this.showTierInputColumns = 1;
      this.eventForm.patchValue({ isFreeToAttend: false });
    }
    console.log(this.showTierInputColumns);
  }

  /// Summary: Generates an array of payment tiers based on the user's input.
  /// Comments: Called when the user enters the number of payment tiers they want to add.
  generateOptions() {
    this.paymentTiers = Array.from({ length: this.numberOfOptions }, () => ({
      name: '',
      amount: 0,
    }));
  }

  /// Summary: Saves the payment tiers for the event in the database.
  /// Comments: Loops through the paymentTiers array and adds each payment tier to the database.
  async paymentTiersSave(eventId: any): Promise<void> {
    for (const tier of this.paymentTiers) {
      tier.eventId = eventId;
      await this.addPaymentTier(tier);
    }
  }

  /// Summary: Adds a single payment tier to the database.
  /// Comments: Uses the HttpClient to make a POST request to add a payment tier to the database.
  async addPaymentTier(tier: PaymentTier): Promise<void> {
    try {
      await this.http
        .post('https://localhost:7131/api/PaymentTier/add-payment-tier', tier)
        .toPromise();
      console.log(`Payment Tier added: ${tier.name}`);
    } catch (error) {
      console.log(`Failed to add Payment Tier: ${tier.name}`);
      console.error(error);
    }
  }

  /// Summary: Submits the event form and saves the event along with its payment tiers and redirects to the next step.
  /// Comments: Checks if the form is valid, updates the organizerId, and makes a POST request to save the event to the database. Then, calls the paymentTiersSave function to save the payment tiers and redirects to the next step.
  async submitForm(): Promise<void> {
    if (this.eventForm.valid) {
      this.eventForm.value.organizerId = this.tokenData?.userId;
      console.log(this.eventForm.value);

      try {
        const result = await this.http
          .post('https://localhost:7131/api/Event/create', this.eventForm.value)
          .toPromise();

        await this.paymentTiersSave(result);

        this.router.navigate(['event-question'], {
          queryParams: { Id: result },
        });
      } catch (error) {
        console.error(error);
        this.snackbar.openSnackBar('Adding Event Failed');
      }
    }
  }

  /// Summary: Navigates back to the organizer's home page.
  Back() {
    this.router.navigate(['organiser-home']);
  }
}
