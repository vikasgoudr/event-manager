import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { EditModalComponent } from 'src/app/modal/edit-modal/edit-modal.component';
import { Organiser } from 'src/models/organizer.model';
import { PagedList } from 'src/models/pagedList.model';
@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
})
export class AdminHomeComponent implements OnInit {
  dialogConfig = new MatDialogConfig();
  modalDialog: MatDialogRef<EditModalComponent, any> | undefined;
  data: PagedList<Organiser> | undefined;
  dataSource: MatTableDataSource<Organiser>;
  constructor(private http: HttpClient, public matDialog: MatDialog, private matPaginatorIntl: MatPaginatorIntl) {
    this.dataSource = new MatTableDataSource<Organiser>();
  }
  ShowPaginator = false;
  fetchData = () => {
    this.http.post<PagedList<Organiser>>("https://localhost:7131/api/User/all-organisers", this.PagerSettings).subscribe(
      result => {
        console.log(result);
        this.data = result;
        if (this.data.data != undefined && this.data.data != null) {
          this.dataSource.data = this.data.data;
          this.PagerSettings.PageNumber = this.data.currentPage;
          this.paginationData.currentPage = this.data.currentPage;
          this.paginationData.previousPage = this.data.currentPage - 1;
          this.paginationData.nextPage = this.data.currentPage + 1;
          this.paginationData.pageLimit = this.data.pageCount;
          console.log(this.data.rowCount);
          
          if (this.data.rowCount > 10) { this.ShowPaginator = true;
           }
        }
      },
      (error: HttpErrorResponse) => {
        console.log(error);
      }
    );
  }
  PagerSettings = {
    PageNumber: 1,
    PageSize: 10
  };
  paginationData = {
    previousPage: 0,
    currentPage: 1,
    nextPage: 2,
    pageLimit: 1
  }
  ngOnInit(): void {
    this.fetchData();
  }
  displayedColumns: string[] = ['name', 'age', 'status', 'edit'];
  Edit(id: number) {
    console.log(id);
    this.dialogConfig.id = id.toString();
    this.dialogConfig.height = "auto";
    this.dialogConfig.width = "auto";
    this.modalDialog = this.matDialog.open(EditModalComponent, {
      data: {
        id: id,
        fetchData: this.fetchData
      }
    });
  }
  Next() {
    this.PagerSettings.PageNumber++;
    this.fetchData();
  }
  Previous() {
    this.PagerSettings.PageNumber--;
    this.fetchData();
  }
  getEnumValue(id: number) {
    switch (id) {
      case 1: return 'Approved';
      case 2: return 'Rejected';
      case 3: return 'Inprogress';
      default: return '';
    }
  }
}
