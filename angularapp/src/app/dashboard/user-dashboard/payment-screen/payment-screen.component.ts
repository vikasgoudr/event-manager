import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';

@Component({
  selector: 'app-payment-screen',
  templateUrl: './payment-screen.component.html',
  styleUrls: ['./payment-screen.component.css'],
})
export class PaymentScreenComponent implements OnInit {
  tokenData!: TokenData | null;
  totalAmount: number = 0;

  constructor(
    private dialogRef: MatDialogRef<PaymentScreenComponent>,
    private router: Router,
    private snackbar: SnackbarService,
    private tokenDataService: TokenDataService,
    @Inject(MAT_DIALOG_DATA) public registerationData: any,
    private http: HttpClient
  ) {
    console.log(registerationData.newTicketId); // Access the ticketId sent from EventRegisterComponent
    console.log(registerationData.selectedPaymentTier);
    console.log(registerationData.dynamicForm); // Access the form data sent from EventRegisterComponent
  }

  ngOnInit(): void {
    this.tokenData = this.tokenDataService.getTokenData();
    if (!this.registerationData.EventData.isFreeToAttend) {
      const a = this.registerationData.selectedPaymentTier.amount;
      this.totalAmount = a + 2.0 + (a * 18) / 100;
    }
  }

  async OnSubmit() {
    try {
      const paymentdone: boolean = false;
      //  await this.payment(newTicketId);
      for (const data in this.registerationData.dynamicForm.controls) {
        if (this.registerationData.dynamicForm.controls.hasOwnProperty(data)) {
          const answer = {
            response: this.registerationData.dynamicForm.controls[data].value,
            questionId: data,
            answererId: this.tokenData?.userId,
            ticketId: this.registerationData.newTicketId,
          };

          this.http
            .post('https://localhost:7131/api/Answer/create-answer', answer)
            .subscribe(
              (result) => {
                this.snackbar.openSnackBar('Registered Successfully');
              },
              (error) => {
                console.log(error);
              }
            );
        }
      }
      // Emit the success event when the payment is successful
      this.dialogRef.close({ paymentSuccessful: true });
      this.router.navigate(['app-tickets']);
    } catch (error) {
      console.log(error);
    }
  }

  close() {
    this.dialogRef.close();
  }
}
