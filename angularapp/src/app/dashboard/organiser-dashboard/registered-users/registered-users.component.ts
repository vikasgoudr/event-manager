// import { HttpClient, HttpErrorResponse } from '@angular/common/http';
// import { Component } from '@angular/core';
// import { MatTableDataSource } from '@angular/material/table';
// import { ActivatedRoute, Router } from '@angular/router';
// import { PagedList } from 'src/models/pagedList.model';
// import { registeredUsers } from 'src/models/registeredUsers.model';

// @Component({
//   selector: 'app-registered-users',
//   templateUrl: './registered-users.component.html',
//   styleUrls: ['./registered-users.component.css']
// })
// export class RegisteredUsersComponent {
//   showTable:number|undefined;
//   eventId:any|undefined;
//   data: PagedList<registeredUsers> | undefined;
//   dataSource: MatTableDataSource<registeredUsers>;
//   ticketsData:any;
//   expanded: boolean = false;
//   constructor(private http:HttpClient,private route: ActivatedRoute,private router:Router){
//     this.dataSource=new MatTableDataSource<registeredUsers>();
//     this.route.queryParams.subscribe((params) => {
//       this.eventId = params['Id'];
//     });
//   }
//   back(){
//     this.router.navigate(['organiser-home']);
//   }
//   fetchData = () => {
//     this.http.post<PagedList<registeredUsers>>(`https://localhost:7131/api/Event/registered-users/${this.eventId}`, this.PagerSettings).subscribe(
//       result => {
//         console.log(result);
//         this.data = result;
//         if (this.data.data != undefined && this.data.data != null) {
//           this.dataSource.data = this.data.data;
//           this.PagerSettings.PageNumber = this.data.currentPage;
//           this.paginationData.currentPage = this.data.currentPage;
//           this.paginationData.previousPage = this.data.currentPage - 1;
//           this.paginationData.nextPage = this.data.currentPage + 1;
//           this.paginationData.pageLimit = this.data.pageCount;
//           this.showTable=this.data.rowCount;
//         }
//       },
//       (error: HttpErrorResponse) => {
//         console.log(error);
//       }
//     );
//   }
//   PagerSettings = {
//     PageNumber: 1,
//     PageSize: 10
//   };
//   paginationData = {
//     previousPage: 0,
//     currentPage: 1,
//     nextPage: 2,
//     pageLimit: 1
//   }

//   getTicketDetailsByEvent(){
   
//   }
//   ngOnInit(): void {
//     this.fetchData();
//     this. getTicketDetailsByEvent();
//   }
//   displayedColumns: string[] = ['name', 'email', 'gender', 'age','phonenumber','details'];
//   Next() {
//     this.PagerSettings.PageNumber++;
//     this.fetchData();
//   }
//   Previous() {
//     this.PagerSettings.PageNumber--;
//     this.fetchData();
//   }
//   getEnumValue(id:number){
//     switch (id) {
//       case 1: return 'Female';
//       case 2: return 'Male'; 
//       case 3: return 'Other';
//       default: return ''; }
//   }
// showRegistrations(){
  
// }
// toggleDetails(userId:number) {
//   if(!this.expanded)
//   this.http.get(`https://localhost:7131/api/Answer/get-all-ticket-details?eventId=${this.eventId}&userId=${userId}`).subscribe(
//     result => {
//       console.log(result);
//       this.ticketsData = result;
//     },
//     (error: HttpErrorResponse) => {
//       console.log(error);
//     });
//    this.expanded = !this.expanded;
// }

  
// }
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

interface registeredUsers {
  id: number;
  userName: string;
  email: string;
  gender: number;
  age: number;
  phoneNumber: string;
  expanded: boolean;
  ticketsData?: any;
}

@Component({
  selector: 'app-registered-users',
  templateUrl: './registered-users.component.html',
  styleUrls: ['./registered-users.component.css']
})
export class RegisteredUsersComponent implements OnInit {
  showTable: number | undefined;
  eventId: any | undefined;
  data: any | undefined;
  dataSource: MatTableDataSource<registeredUsers>;
  expanded: boolean = false;
  showPaginator = false;
  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) {
    this.dataSource = new MatTableDataSource<registeredUsers>();
    this.route.queryParams.subscribe((params) => {
      this.eventId = params['Id'];
    });
  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.http
      .post(`https://localhost:7131/api/Event/registered-users/${this.eventId}`, this.PagerSettings)
      .subscribe(
        (result) => {
          console.log(result);
          this.data = result;
          if (this.data.data != undefined && this.data.data != null) {
            this.dataSource.data = this.data.data.map((user: registeredUsers) => {
              return {
                ...user,
                expanded: false,
                ticketsData: undefined
              };
            });
          }
          this.paginationData.currentPage = this.data.currentPage;
          this.paginationData.previousPage = this.data.currentPage - 1;
          this.paginationData.nextPage = this.data.currentPage + 1;
          this.paginationData.pageLimit = this.data.pageCount;
          this.showTable = this.data.rowCount;

          if (this.data.rowCount>10) {this.showPaginator= true;}
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        }
      );
  }

  toggleDetails(user: registeredUsers) {
    user.expanded = !user.expanded;

    if (user.expanded && !user.ticketsData) {
      this.http
        .get(`https://localhost:7131/api/Answer/get-all-ticket-details?eventId=${this.eventId}&userId=${user.id}`)
        .subscribe(
          (result) => {
            console.log(result);
            user.ticketsData = result;
          },
          (error: HttpErrorResponse) => {
            console.log(error);
          }
        );
    }
  }

  back() {
    this.router.navigate(['organiser-home']);
  }

  Previous() {
    this.PagerSettings.PageNumber--;
    this.fetchData();
  }

  Next() {
    this.PagerSettings.PageNumber++;
    this.fetchData();
  }

  getEnumValue(id: number) {
    switch (id) {
      case 1:
        return 'Female';
      case 2:
        return 'Male';
      case 3:
        return 'Other';
      default:
        return '';
    }
  }

  displayedColumns: string[] = ['name', 'email', 'gender', 'age', 'phonenumber', 'details'];
  PagerSettings = {
    PageNumber: 1,
    PageSize: 10
  };
  paginationData = {
    previousPage: 0,
    currentPage: 1,
    nextPage: 2,
    pageLimit: 1
  };
}
