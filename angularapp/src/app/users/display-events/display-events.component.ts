import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-display-events',
  templateUrl: './display-events.component.html',
  styleUrls: ['./display-events.component.css'],
})
export class DisplayEventsComponent implements OnInit {
  dialogConfig = new MatDialogConfig();
  modalDialog: MatDialogRef<EventRegisterComponent, any> | undefined;
  constructor(
    private http: HttpClient,
    private router: Router,
    private tokenDataService: TokenDataService,
    public matDialog: MatDialog
  ) {}
  isLoggedIn = false;
  pagerSettings = {
    pageNumber: 1,
    pageSize: 10,
  };
  carousel: EventDto[] | any;
  currentSlide: any;
  slideId: number = 0;
  PreviousSlide() {
    console.log(this.slideId + ' ' + this.currentSlide + ' 33');
    if (this.slideId > 0) this.slideId--;
    console.log(this.slideId + ' ' + this.currentSlide + ' 35');
    this.currentSlide = this.carousel[this.slideId].id;
    console.log(this.slideId + ' ' + this.currentSlide + ' 37');
  }
  NextSlide() {
    console.log(this.slideId + ' ' + this.currentSlide + ' 40');
    if (this.slideId < this.carousel.length - 1) this.slideId++;
    console.log(
      this.slideId + ' ' + this.currentSlide + ' 42 ' + this.carousel.length
    );
    this.currentSlide = this.carousel[this.slideId].id;
    console.log(this.slideId + ' ' + this.currentSlide + ' 44');
  }

  ngOnInit(): void {
    this.fetchData();
  }

  Login() {
    this.router.navigate(['login-comp']);
  }
  Register(event: any) {
    this.dialogConfig.height = 'auto';
    this.dialogConfig.width = 'auto';
    this.modalDialog = this.matDialog.open(EventRegisterComponent, {
      height: '90vh',
      data: event,
      panelClass: 'custom-dialog-panel',
    });
    this.modalDialog.afterClosed().subscribe(()=>{
      this.fetchData();
    });
    // this.modalDialog.afterClosed().subscribe(() => {
    //   // Code to execute after the modal is closed
    //   this.ngOnInit();
    // });
  }
  fetchData() {this.http
    .get('https://localhost:7131/api/Event/top/' + '5')
    .subscribe((result) => {
      this.carousel = result;
      this.currentSlide = this.carousel[this.slideId].id;
      console.log(this.currentSlide);
    });

  var token = this.tokenDataService.getTokenData();
  if (token == null) {
    this.isLoggedIn = false;
  } else {
    this.isLoggedIn = true;
  }
  }

  // ScrollDown = () => {
  //   const height = document.getElementById("main-carousel")?.clientHeight ?? 0;
  //   window.scrollBy(0, height);
  // }
}
