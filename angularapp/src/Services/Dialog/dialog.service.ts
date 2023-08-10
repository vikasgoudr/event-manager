


import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';


@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(private dialog: MatDialog) {}
  openDialog(title: string, message: string): MatDialogRef<DialogComponent> {
    return this.dialog.open(DialogComponent, {
      width: '400px',
      data: { title: title, message: message }
    });
  }
  
}
