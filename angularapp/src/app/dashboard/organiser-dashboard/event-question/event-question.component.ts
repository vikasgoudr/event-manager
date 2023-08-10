import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Question } from '../../../../models/question.model';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import {
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import { EditQuestionComponent } from '../edit-question/edit-question.component';
import { CommonModule } from '@angular/common';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { PaymentTier } from 'src/models/paymentTier.model';

@Component({
  selector: 'app-event-question',
  templateUrl: './event-question.component.html',
  styleUrls: ['./event-question.component.css'],
})
export class EventQuestionComponent implements OnInit {
  questions!: Question[];
  eventId!: number;
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  paymentTiers: PaymentTier[] = [];
  constructor(
    public dialog: MatDialog,
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private snackbar: SnackbarService
  ) {}
  closeEventInputs() {
    this.router.navigate(['dashboard-layout']);
  }
  getQuestions() {
    this.http
      .get<Question[]>(
        `https://localhost:7131/api/Question/get-form-data/${this.eventId}`
      )
      .subscribe(
        (result: Question[]) => {
          this.questions = result;
          console.log(result);
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        }
      );
  }

  editQuestion(question: Question) {
    console.log(question.id);

    const dialogRef = this.dialog.open(EditQuestionComponent, {
      width: '300px',
      height: '400px',
      data: question, // Pass the selected question as data to the dialog
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      this.getQuestions(); // Refresh questions on success
    });
  }

  deleteQuestion(question: Question) {
    console.log(question.id);
    const confirmDelete = window.confirm(
      'Are you sure you want to delete this question?'
    );
    if (confirmDelete) {
      this.http
        .delete(`https://localhost:7131/api/Question/${question.id}`)
        .subscribe(
          () => {
            this.snackbar.openSnackBar('Question deleted successfully');
            // Refresh the questions after successful deletion
            this.getQuestions();
          },
          (error: HttpErrorResponse) => {
            this.snackbar.openSnackBar(
              'An error occurred while deleting the question:'
            );
          }
        );
      this.getQuestions();
    }
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.eventId = params['Id'];
    });
  
    this.getQuestions();
  }

  save() {
    this.router.navigate(['dashboard-layout']);
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(EventQuestionDialog, {
      width: '300px',
      height: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      this.getQuestions(); // Refresh questions on success
    });
  }
}

//dialog box component

@Component({
  selector: 'dialog-overview-example-dialog',
  templateUrl: 'each-question.html',
  styleUrls: ['each-question.css'],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    FormsModule,
    MatCardModule,
    CommonModule,
  ],
  standalone: true,
})
export class EventQuestionDialog implements OnInit {
  eventId!: number;
  numberOfOptions: number = 0;

  question: Question = {
    type: '',
    name: '',
    isRequired: false,
    eventId: 0,
    options: '',
  };

  options: any[] = [];

  generateOptions() {
    this.options = Array.from({ length: this.numberOfOptions }, () => ({
      value: '',
    }));
  }

  constructor(
    public dialogRef: MatDialogRef<EventQuestionDialog>,
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.eventId = params['Id'];
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  add() {
    this.question.options = JSON.stringify(this.options);
    this.question.eventId = this.eventId;
    this.http
      .post('https://localhost:7131/api/Question/create', this.question, {
        responseType: 'text',
      })
      .subscribe(
        (result) => {
          console.log(result);
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        }
      );
    this.onNoClick();
  }
}
