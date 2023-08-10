/// Summary: This component represents the dashboard layout where the user can view and manage their events.

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventDto } from 'src/models/event.model';
import { PagedList } from 'src/models/pagedList.model';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';
import { MatDialog } from '@angular/material/dialog';
import { EditEventComponent } from '../edit-event/edit-event.component';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';

@Component({
  selector: 'app-dashboard-layout',
  templateUrl: './dashboard-layout.component.html',
  styleUrls: ['./dashboard-layout.component.css'],
})
export class DashboardLayoutComponent implements OnInit {
  tokenData!: TokenData | null;
  eventsWithPagedList: PagedList<EventDto> | any;
  events: EventDto[] | any;
  showPaginator = false;

  constructor(
    private http: HttpClient,
    private tokenDataService: TokenDataService,
    private router: Router,
    private dialog: MatDialog,
    private snackbar: SnackbarService
  ) { }

  pagerSettings = {
    pageNumber: 1,
    pageSize: 10,
  };
  paginationData = {
    previousPage: 0,
    currentPage: 1,
    nextPage: 2,
    pageLimit: 1,
  };

  /// Summary: Fetches the events of the current user from the server and updates the events and pagination data.
  getEvents(): void {
    this.http
      .post<PagedList<EventDto>>(
        `https://localhost:7131/api/Event/user-events/${this.tokenData?.userId}`,
        this.pagerSettings
      )
      .subscribe(
        (result) => {
          this.eventsWithPagedList = result;
          this.events = this.eventsWithPagedList.data;
          console.log(this.events);
          this.pagerSettings.pageNumber = this.eventsWithPagedList.currentPage;
          this.paginationData.currentPage = this.eventsWithPagedList.currentPage;
          this.paginationData.previousPage = this.eventsWithPagedList.currentPage - 1;
          this.paginationData.nextPage = this.eventsWithPagedList.currentPage + 1;
          this.paginationData.pageLimit = this.eventsWithPagedList.pageCount;
          if (this.pagerSettings.pageSize > 10) { this.showPaginator = true; }

        },
        (error: HttpErrorResponse) => {
          console.log(error);
        }
      );
  }

  ngOnInit(): void {
    this.tokenData = this.tokenDataService.getTokenData();
    this.getEvents();
  }

  /// Summary: Navigates to the event-question page with the selected event's Id.
  addtoEvent(eventId: number): void {
    this.router.navigate(['event-question'], { queryParams: { Id: eventId } });
  }

  /// Summary: Navigates to the add-event page.
  addEvent(): void {
    this.router.navigate(['add-event']);
  }

  /// Summary: Increases the page number for pagination and fetches the next set of events.
  Next() {
    this.pagerSettings.pageNumber++;
    console.log("Triggered");
    this.getEvents();
  }

  /// Summary: Decreases the page number for pagination and fetches the previous set of events.
  Previous() {
    console.log("Triggered");
    this.pagerSettings.pageNumber--;
    this.getEvents();
  }

  /// Summary: Opens the edit-event dialog with the selected event's data.
  editEvent(event: EventDto): void {
    console.log("Edit event called!");
    const dialogRef = this.dialog.open(EditEventComponent, {
      width: '530px',
      data: event // Pass the event object to the dialog
    });

    dialogRef.afterClosed().subscribe(result => {
      // Handle the result when the dialog is closed, if needed
    });
  }

  /// Summary: Deletes the selected event from the server and updates the events list.
  deleteEvent(Id: number) {
    const confirmDelete = window.confirm(
      'Are you sure you want to delete this Event?'
    );
    if (confirmDelete) {
      this.http.delete(`https://localhost:7131/api/Event/${Id}`).subscribe(
        () => {
          this.snackbar.openSnackBar("Event Deletion Successful");
          this.getEvents();
        },
        (error: HttpErrorResponse) => {
          this.snackbar.openSnackBar('Event deletion failed');
        }
      );
    }
  }

  /// Summary: Changes the publish status of the event on the server and updates the events list.
  PublishStatus(id: number, bool: boolean) {
    const data = {
      eventId: id,
      publishStatus: bool
    };
    this.http.patch("https://localhost:7131/api/Event/Publish", data).subscribe(
      result => {
        console.log(result);
        this.getEvents();
        if (data.publishStatus == true) {
          this.snackbar.openSnackBar('Event Published');
        }
        if (data.publishStatus == false) {
          this.snackbar.openSnackBar('Event Unpublished');
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  /// Summary: Navigates to the registered-users page with the selected event's Id.
  registeredUsers(id: number) {
    this.router.navigate(['registered-users'], { queryParams: { Id: id } });
  }
}
