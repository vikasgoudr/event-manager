import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, tap, catchError, throwError ,of} from 'rxjs';
import { PagedList } from 'src/models/pagedList.model';
import { EventDto } from 'src/models/event.model';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  private eventsWithPagedList$: BehaviorSubject<PagedList<EventDto> | null> = new BehaviorSubject<PagedList<EventDto> | null>(null);

  constructor(private http: HttpClient) {}

  getUserEvents(userId: number, pagerSettings: any): Observable<PagedList<EventDto>> {
    const cachedEvents = this.eventsWithPagedList$.getValue();
    if (cachedEvents) {
      return of(cachedEvents); // Wrap the cached events in `of` function to create a new observable
    } else {
      return this.fetchUserEvents(userId, pagerSettings);
    }
  }
  

  refreshUserEvents(userId: number, pagerSettings: any): Observable<PagedList<EventDto>> {
    return this.fetchUserEvents(userId, pagerSettings);
  }

  private fetchUserEvents(userId: number, pagerSettings: any): Observable<PagedList<EventDto>> {
    return this.http.post<PagedList<EventDto>>(
      `https://localhost:7131/api/Event/user-events/${userId}`,
      pagerSettings
    ).pipe(
      tap((result) => {
        this.eventsWithPagedList$.next(result);
      }),
      catchError((error: HttpErrorResponse) => {
        console.log(error);
        return throwError(error);
      })
    );
  }
}
