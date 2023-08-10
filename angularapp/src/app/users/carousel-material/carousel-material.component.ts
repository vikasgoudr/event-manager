import { Component, OnInit, Renderer2, ElementRef, ViewEncapsulation } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogRef,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { EventRegisterComponent } from 'src/app/dashboard/user-dashboard/event-register/event-register.component';
import { EventDto } from 'src/models/event.model';
import { PagedList } from 'src/models/pagedList.model';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { FormBuilder } from '@angular/forms';
import { filter } from 'rxjs';

@Component({
  selector: 'app-carousel-material',
  templateUrl: './carousel-material.component.html',
  styleUrls: ['./carousel-material.component.css'],
})
export class CarouselMaterialComponent implements OnInit {
  dialogConfig = new MatDialogConfig();
  modalDialog: MatDialogRef<EventRegisterComponent, any> | undefined;

  constructor(
    private http: HttpClient,
    private router: Router,
    private tokenDataService: TokenDataService,
    public matDialog: MatDialog,
    private fb: FormBuilder
  ) { }
  isLoggedIn = false;
  pagerSettings = {
    pageNumber: 1,
    pageSize: 10,
  };
  currentPage: number = 0;
  pageCount: number = 0;
  eventsWithPagedList: PagedList<EventDto> | any;
  events: EventDto[] | any;
  fetchData(): void {
    this.http
      .post('https://localhost:7131/api/Event/all', this.pagerSettings)
      .subscribe((result) => {
        this.eventsWithPagedList = result;
        this.events = this.eventsWithPagedList.data;
        console.log(this.events);
        this.currentPage = this.eventsWithPagedList.currentPage;
        this.pageCount = this.eventsWithPagedList.pageCount;
      });
    var token = this.tokenDataService.getTokenData();
    if (token == null) {
      this.isLoggedIn = false;
    } else {
      this.isLoggedIn = true;
    }
  }
  handleClick(event: EventDto) {
    // Call your function or perform actions when the card is clicked
    console.log('Card clicked!', event);
    if (!this.isLoggedIn) {
      this.router.navigate(['login-comp']);
    }
    if (this.isLoggedIn) {
      this.Register(event);
    }
  }
  ngOnInit(): void {
    // this.fetchData();
    this.ClearFilters();
    if(this.showFilter==true){
      this.showFilter=false;
    }
  }

  Register(event: any) {
    this.dialogConfig.height = 'auto';
    this.dialogConfig.width = 'auto';
    this.modalDialog = this.matDialog.open(EventRegisterComponent, {
      height: '90vh',
      data: event,
      panelClass: 'custom-dialog-panel'
    });
  }

  Next() {
    this.pagerSettings.pageNumber++;
    if (this.showFilter) {
      this.filterForm.controls['pageNumber'].setValue(this.pagerSettings.pageNumber);
      this.ApplyFilters();
    }
    else {
      this.fetchData();
    }
  }
  Previous() {
    this.pagerSettings.pageNumber--;
    if (this.showFilter) {
      this.filterForm.controls['pageNumber'].setValue(this.pagerSettings.pageNumber);
      this.ApplyFilters();
    }
    else {
      this.fetchData();
    }
  }
  filterForm = this.fb.group({
    filterText: "",
    startDate: new Date(Date.UTC(2000, 0, 1, 0, 0, 0)).toISOString().slice(0, 16),
    endDate: new Date(Date.UTC(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), new Date().getHours(), new Date().getMinutes())).toISOString().slice(0, 16),
    pageNumber: 0,
    pageSize: 0,
    freeToAttend: false
  })
  onEventTypeChange(event: any) {
    console.log("triggers");
    if (event.target.value == "true")
      this.filterForm.controls['freeToAttend'].setValue(true);
    else if (event.target.value == "false")
      this.filterForm.controls['freeToAttend'].setValue(false);
    else if (event.target.value == "Both")
      this.filterForm.controls['freeToAttend'].setValue(null);
  }
  ApplyFilters() {
    console.log(this.filterForm);
    console.log(this.filterForm.controls['freeToAttend'].value);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http.post('https://localhost:7131/api/Event/filterByText', JSON.stringify(this.filterForm.value), { headers: headers }).subscribe(
      result => {
        this.eventsWithPagedList = result;
        this.events = this.eventsWithPagedList.data;
        console.log(this.events);
        this.currentPage = this.eventsWithPagedList.currentPage;
        this.pageCount = this.eventsWithPagedList.pageCount;
      },
      (error) => {
        console.log(error);
      }
    )
  }
  ChangePageSettings() {
    this.filterForm.controls['pageNumber'].setValue(1);
    this.filterForm.controls['pageSize'].setValue(10);
  }
  showFilter = false;
  ChangeShowFilterStatus() {

    this.filterForm.controls['pageNumber'].setValue(1);
    this.filterForm.controls['pageSize'].setValue(10);
    this.showFilter = !this.showFilter;
  }
  ClearFilters() {
    this.pagerSettings.pageNumber = 1;
    this.pagerSettings.pageSize = 10;
    this.showFilter = !this.showFilter;
    this.filterForm.controls['filterText'].setValue("");
    this.filterForm.controls['startDate'].setValue(new Date(Date.UTC(2000, 0, 1, 0, 0, 0)).toISOString().slice(0, 16));
    this.filterForm.controls['endDate'].setValue(new Date(Date.UTC(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), new Date().getHours(), new Date().getMinutes())).toISOString().slice(0, 16));
    this.filterForm.controls['freeToAttend'].setValue(null);
    this.fetchData();
  }
}
