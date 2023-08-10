import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ApprovalStatus } from 'src/enums/approval-status.enum';
import { Organiser } from 'src/models/organizer.model';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';

@Component({
  selector: 'app-edit-modal',
  templateUrl: './edit-modal.component.html',
  styleUrls: ['./edit-modal.component.css']
})
export class EditModalComponent implements OnInit {
  id: string | undefined;
  organiserData: Organiser | undefined;
  constructor(public dialogRef: MatDialogRef<EditModalComponent>, private http: HttpClient, @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,private router:Router,
  private snackBar:SnackbarService) {
    this.id = data.id;
    this.fetchData = data.fetchData;
  }
  @Input() fetchData: Function|undefined;
  formData = this.fb.group({
    id:[''],
    status:['']
  });
  ngOnInit(): void {
    this.http.get("https://localhost:7131/api/User/get-organiser?id=" + this.id).subscribe(
      result => {
        this.organiserData = result as Organiser;
        this.formData.controls.status.setValue(ApprovalStatus[this.organiserData.approvalStatus]);
      },
      (error: HttpErrorResponse) => {
        console.log(error);
      }
    );
  }
  statusValues = Object.keys(ApprovalStatus).filter((item) => {
    return isNaN(Number((item)))
  });
  closeModal() {
    this.dialogRef.close();
  }
  save(){
    if(this.id!=null){
      this.formData.controls.id.setValue(this.id.toString());
    }
    if(this.formData.controls.status.value=="Approved"){
      this.formData.controls.status.setValue('1');
    }
    else if(this.formData.controls.status.value=="Rejected"){
      this.formData.controls.status.setValue('2');
    }
    else if(this.formData.controls.status.value=="InProgress"){
      this.formData.controls.status.setValue('3');
    }
    this.http.put("https://localhost:7131/api/User/change-approval-status", this.formData.value, { responseType: 'text' }).subscribe(
     result => {
       console.log(result);
      if(this.fetchData) {
        this.fetchData();
        this.snackBar.openSnackBar('Approval Status Changed');
        this.closeModal();
      }
     },
     (error: HttpErrorResponse)=>{
       console.log(error);
     }
    );
  }
}
