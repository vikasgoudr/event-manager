import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';

@Component({
  selector: 'app-edit-question',
  templateUrl: './edit-question.component.html',
  styleUrls: ['./edit-question.component.css'],
})
export class EditQuestionComponent implements OnInit {
  // Properties
  numberOfOptions: number = 0;
  options: any[] = [];

  // Constructor
  constructor(
    public dialogRef: MatDialogRef<EditQuestionComponent>,
    @Inject(MAT_DIALOG_DATA) public question: any,
    private http: HttpClient,
    private snackbar: SnackbarService
  ) {}

  /// Lifecycle hook - called after the component has been initialized
  ngOnInit(): void {
    // Parse options from JSON string to object
    this.question.options = JSON.parse(this.question.options);
  }

  /// Method to handle the "Cancel" button click
  onNoClick(): void {
    this.dialogRef.close();
  }

  /// Method to generate options based on the number of options selected
  generateOptions() {
    this.question.options = Array.from({ length: this.numberOfOptions }, () => ({
      value: '',
    }));
  }

  /// Method to update the question
  update() {
    console.log(this.question);
    // Convert options to JSON string before sending
    this.question.options = JSON.stringify(this.question.options);

    // Send the updated question via HTTP PUT request
    this.http.put("https://localhost:7131/api/Question/update", this.question).subscribe(
      (response) => {
        console.log('Question updated successfully');
        this.snackbar.openSnackBar('Question updated successfully');
        this.dialogRef.close();
      },
      (error) => {
        this.snackbar.openSnackBar('Failed updating the Question:');
      }
    );
  }
}
