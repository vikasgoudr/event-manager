import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { Buffer } from 'buffer';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css'],
})
export class EditEventComponent {
  eventFormModel: any = {}; // Object to store form data
  addressFormModel: any = {};
  imageBlob: any;
  imageBuffer: any;
  eventForm!: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditEventComponent>,
    private http: HttpClient,
    private snackbar: SnackbarService
  ) {}

  /// Summary: Triggered when the user selects an image for the event poster.
  /// Comments: Converts the selected image to a Base64 format and updates the eventForm's posterImage property.
  onImageSelect(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.convertImageToBlob(file);
    }
  }

  /// Summary: Converts the selected image to a Base64 format.
  /// Comments: Uses FileReader to read the image file, convert it to an ArrayBuffer, and then to Base64 format.
  convertImageToBlob(file: File) {
    const reader = new FileReader();
    reader.onloadend = () => {
      if (reader.result) {
        const arrayBuffer = reader.result as ArrayBuffer;
        this.imageBuffer = Array.from(new Uint8Array(arrayBuffer));

        // Convert the image buffer to Base64 format
        const base64Image = Buffer.from(this.imageBuffer).toString('base64');
        console.log(base64Image);

        // Assign the Base64 image to the posterImage property
        this.eventFormModel.posterImage = base64Image;
      }
    };
    reader.readAsArrayBuffer(file);
  }

  /// Summary: Submits the event form and updates the event data in the database.
  /// Comments: Sends a PATCH request to update the event data and closes the dialog on success.
  submitForm() {
    console.log(this.data);

    this.http.patch("https://localhost:7131/api/Event", this.data).subscribe(
      (response) => {
        this.snackbar.openSnackBar('Event Updated Successfully');
        this.dialogRef.close();
      },
      (error) => {
        console.error('Error updating event:', error);
      }
    );
  }

  /// Summary: Closes the dialog.
  back() {
    this.dialogRef.close();
  }
}
