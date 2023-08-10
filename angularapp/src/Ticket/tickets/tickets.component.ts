import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DialogService } from 'src/Services/Dialog/dialog.service';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';
@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css'],
})
export class TicketsComponent implements OnInit {
  tokenData: TokenData | null;
  ticketsData: any;

  constructor(
    private http: HttpClient,
    private tokenDataService: TokenDataService,
    private dialogService: DialogService,
    private snackBar:SnackbarService
  ) {
    this.tokenData = null;
    this.ticketsData = null;
  }

  ngOnInit(): void {
    this.tokenData = this.tokenDataService.getTokenData();
    this.fetchTicketsData();
  }

  async fetchTicketsData(): Promise<void> {
    try {
      this.ticketsData = await this.http
        .get(
          'https://localhost:7131/api/Answer/get-all-tickets-by-userId?userId=' +
            this.tokenData?.userId
        )
        .toPromise();

      console.log(this.ticketsData);
    } catch (error) {
      console.log('Error fetching tickets data:', error);
    }
  }

  // //generating QR code
  // generateQRCodeValue(questionAnswers: any[]): string {
  //   const jsonString = JSON.stringify(questionAnswers);
  //   const base64String = btoa(jsonString);
  //   return base64String;
  // }

  generateQRCodeValue(questionAnswers: any[]): string {
    const jsonString = JSON.stringify(questionAnswers);
    return jsonString;
  }

  toggleDetails(ticket: any): void {
    ticket.showDetails = !ticket.showDetails;
  }

  deleteTicket(transacitonId:any): void {
    const title = 'Delete Confirmation';
    const message =
      'Are you sure you want to Cancel your ticket?';

    const dialogRef = this.dialogService.openDialog(title, message);

    dialogRef.afterClosed().subscribe((result) => {
      if (result == 'continue') {
        console.log("transactionId"+transacitonId);
        this.http.delete(`https://localhost:7131/cancel-ticket?transactionId=${transacitonId}`).subscribe(
          () => {
            console.log('Ticket deleted successfully.');
            this.snackBar.openSnackBar('Ticket deleted successfully.');
            this.fetchTicketsData();
          },
          error => {
            console.log('Error deleting ticket:', error);
            // Handle error, display error message, etc.
          }
        );
      }
    });
  }
}
