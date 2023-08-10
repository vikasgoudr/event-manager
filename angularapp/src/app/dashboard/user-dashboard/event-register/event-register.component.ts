import { NumberInput } from '@angular/cdk/coercion';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';
import { PaymentScreenComponent } from '../payment-screen/payment-screen.component';

@Component({
  selector: 'app-event-register',
  templateUrl: './event-register.component.html',
  styleUrls: ['./event-register.component.css'],
})
export class EventRegisterComponent implements OnInit {
  formData: any;
  dynamicForm!: FormGroup;
  tokenData!: TokenData | null;
  isreadyToRegister: boolean = false;
  paymentTierData: any;
  newTicketId: any;
  selectedPaymentTierId: any;

  constructor(
    private dialogRef: MatDialogRef<EventRegisterComponent>,
    private http: HttpClient,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tokenDataService: TokenDataService,
    private router: Router,
    private snackbar: SnackbarService,
    public matDialog: MatDialog
  ) {}

  onPaymentTierChange(tier: any) {
    this.selectedPaymentTierId = tier;
    this.selectedPaymentTierId = this.selectedPaymentTierId.value;
  }

  registrationform() {
    console.log('registration from called');
    this.isreadyToRegister = !this.isreadyToRegister;
  }

  genUniqueId(): string {
    const dateStr = Date.now().toString(36); // convert num to base 36 and stringify

    const randomStr = Math.random().toString(36).substring(2, 8); // start at index 2 to skip decimal point

    return `${dateStr}-${randomStr}`;
  }

  getPaymentTiers() {
    this.http
      .get(
        `https://localhost:7131/api/PaymentTier/get-payment-tier?eventId=${this.data.id}`
      )
      .subscribe(
        (result) => {
          this.paymentTierData = result;
        },
        (error) => {
          console.error(error);
        }
      );
  }

  ngOnInit(): void {
    this.tokenData = this.tokenDataService.getTokenData();
    this.getPaymentTiers();
    this.http
      .get('https://localhost:7131/api/Question/get-form-data/' + this.data.id)
      .subscribe(
        (result) => {
          console.log(result);
          this.formData = result;
          for (var field of this.formData) {
            if (field.options) {
              field.options = JSON.parse(field.options); // Parse the 'options' property if it exists
            }
          }
          this.CreateForm();
        },
        (error) => {
          console.log(error);
        }
      );
  }
  CreateForm() {
    this.dynamicForm = this.fb.group({});
    for (var field of this.formData) {
      const control = new FormControl('', Validators.required);
      this.dynamicForm.addControl(field.id, control);
    }
  }

  ticketGen() {
    const ticket = {
      id: 0,
      eventId: this.data.id,
      transactionId: this.genUniqueId(),
      paymentTierId: this.selectedPaymentTierId,
    };

    return new Promise((resolve, reject) => {
      this.http.post('https://localhost:7131/create-ticket', ticket).subscribe(
        (response) => {
          const newTicketId = Number(response);
          resolve(newTicketId);
        },
        (error) => {
          reject(error);
        }
      );
    });
  }

  async payment() {
    if (this.selectedPaymentTierId || this.data.isFreeToAttend) {
      console.log('this.selectedPaymentTierId:', this.selectedPaymentTierId);
      console.log('this.data.isFreeToAttend:', this.data.isFreeToAttend);
      console.log('payment called');
      var selectedTier: any = null;
      selectedTier = this.paymentTierData.find(
        (tier: any) => tier.id == this.selectedPaymentTierId
      );

      this.newTicketId = await this.ticketGen();
      console.log(this.newTicketId);

      if ((selectedTier || this.data.isFreeToAttend) && this.newTicketId) {
        console.log('entered if event is free to attend');
        const dialogRef = this.matDialog.open(PaymentScreenComponent, {
          height: '90vh',
          data: {
            newTicketId: this.newTicketId,
            dynamicForm: this.dynamicForm,
            EventData: this.data,
            selectedPaymentTier: selectedTier,
          },
          panelClass: 'custom-dialog-panel',
        });

        // Listen for the dialog close event
        dialogRef.afterClosed().subscribe((result) => {
          console.log('dialog after closed in event register');
          if (result && result.paymentSuccessful) {
            console.log(result.paymentSuccessful);
            this.dialogRef.close(); // Close the current dialog when payment is successful
          }
        });
      }
    }
  }

  // async OnSubmit() {

  //   try {
  //     const paymentdone: Boolean = false;
  //     //  await this.payment(newTicketId);
  //     for (const data in this.dynamicForm.controls) {
  //       if (this.dynamicForm.controls.hasOwnProperty(data)) {
  //         const answer = {
  //           response: this.dynamicForm.controls[data].value,
  //           questionId: data,
  //           answererId: this.tokenData?.userId,
  //           ticketId: this.newTicketId,
  //         };

  //         this.http
  //           .post('https://localhost:7131/api/Answer/create-answer', answer)
  //           .subscribe(
  //             (result) => {
  //               this.snackbar.openSnackBar('Registered Successfully');
  //             },
  //             (error) => {
  //               console.log(error);
  //             }
  //           );
  //       }
  //     }

  //     this.dialogRef.close();
  //   } catch (error) {
  //     console.log(error);
  //   }
  // }

  CloseModel() {
    this.dialogRef.close();
  }
}
