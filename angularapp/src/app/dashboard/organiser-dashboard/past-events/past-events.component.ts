import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { EventDto } from 'src/models/event.model';
import { PagedList } from 'src/models/pagedList.model';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';

@Component({
  selector: 'app-past-events',
  templateUrl: './past-events.component.html',
  styleUrls: ['./past-events.component.css']
})
export class PastEventsComponent {
  tokenData!: TokenData | null;
  eventsWithPagedList: PagedList<EventDto> | any;
  events: EventDto[] | any;
  showPaginator= false;
  constructor(
    private http: HttpClient,
    private tokenDataService: TokenDataService,
    private router: Router
  ) { }

  pagerSettings = {
    pageNumber: 1,
    pageSize: 10,
  };
  paginationData = {
    previousPage: 0,
    currentPage: 1,
    nextPage: 2,
    pageLimit: 1
  }
  getEvents(): void {
    console.log(this.pagerSettings.pageNumber);
    this.http
      .post<PagedList<EventDto>>(
        `https://localhost:7131/api/Event/past-events/${this.tokenData?.userId}`,
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
          if(this.pagerSettings.pageSize>10){ this.showPaginator = true};
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
  addEvent(): void {
    this.router.navigate(['add-event']);
  }
  Next() {
    this.pagerSettings.pageNumber++;
    console.log("Trigeered");
    this.getEvents();
  }
  Previous() {
    console.log("Trigeeredddddddddd");
    this.pagerSettings.pageNumber--;
    this.getEvents();
  }
  registeredUsers(id: number) {
    this.router.navigate(['registered-users'], { queryParams: { Id: id } });
  }
}
