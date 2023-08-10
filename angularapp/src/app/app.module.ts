import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './users/login/login.component';
import { RegisterComponent } from './users/register/register.component';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './users/navbar/navbar.component';
import { AdminHomeComponent } from './home/admin-home/admin-home.component';
import { OrganiserHomeComponent } from './home/organiser-home/organiser-home.component';
import { UserHomeComponent } from './home/user-home/user-home.component';
import { NavServiceService } from 'src/Services/NavBar/nav-service.service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EditModalComponent } from './modal/edit-modal/edit-modal.component';
import { EventQuestionComponent } from './dashboard/organiser-dashboard/event-question/event-question.component';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import {
  MatNativeDateModule,
  MatOptionModule,
  MAT_DATE_LOCALE,
} from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import { DashboardLayoutComponent } from './dashboard/organiser-dashboard/dashboard-layout/dashboard-layout.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { EventRegisterComponent } from './dashboard/user-dashboard/event-register/event-register.component';
import { DisplayEventsComponent } from './users/display-events/display-events.component';
import { AddEventComponent } from './dashboard/organiser-dashboard/add-event/add-event.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { CarouselMaterialComponent } from './users/carousel-material/carousel-material.component';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarModule,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import { EditEventComponent } from './dashboard/organiser-dashboard/edit-event/edit-event.component';
import { EditQuestionComponent } from './dashboard/organiser-dashboard/edit-question/edit-question.component';
import { RegisteredUsersComponent } from './dashboard/organiser-dashboard/registered-users/registered-users.component';
import {
  MatMomentDateModule,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import { PastEventsComponent } from './dashboard/organiser-dashboard/past-events/past-events.component';
import { TicketsComponent } from 'src/Ticket/tickets/tickets.component';
import { DialogComponent } from 'src/Services/Dialog/dialog/dialog.component';
import ProfileComponent from './users/profile/profile.component';
import {
  SocialLoginModule,
  SocialAuthServiceConfig,
  GoogleSigninButtonModule,
} from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { QRCodeModule } from 'angularx-qrcode';
import { PaymentScreenComponent } from './dashboard/user-dashboard/payment-screen/payment-screen.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { EditProfileComponent } from './users/edit-profile/edit-profile.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    AdminHomeComponent,
    OrganiserHomeComponent,
    UserHomeComponent,
    EditModalComponent,
    EventQuestionComponent,
    EventRegisterComponent,
    DisplayEventsComponent,
    DashboardLayoutComponent,
    AddEventComponent,
    CarouselMaterialComponent,
    EditEventComponent,
    EditQuestionComponent,
    RegisteredUsersComponent,
    PastEventsComponent,
    TicketsComponent,
    DialogComponent,
    ProfileComponent,
    PaymentScreenComponent,
    ChangePasswordComponent,
    EditProfileComponent,
  ],
  imports: [
    MatGridListModule,
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatCardModule,
    MatTableModule,
    MatFormFieldModule,
    FormsModule,
    MatListModule,
    MatGridListModule,
    MatInputModule,
    MatOptionModule,
    MatIconModule,
    MatSelectModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatDialogModule,
    MatTableModule,
    MatFormFieldModule,
    MatToolbarModule,
    FormsModule,
    MatListModule,
    MatInputModule,
    MatButtonToggleModule,
    MatOptionModule,
    MatIconModule,
    MatDialogModule,
    MatSelectModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatDatepickerModule,
    MatMomentDateModule,
    SocialLoginModule,
    GoogleSigninButtonModule,
    QRCodeModule,
    RouterModule.forRoot([
      { path: 'login-comp', component: LoginComponent },
      { path: 'register-comp', component: RegisterComponent },
      { path: 'admin-home', component: AdminHomeComponent },
      { path: 'organiser-home', component: OrganiserHomeComponent },
      { path: 'user-home', component: UserHomeComponent },
      { path: '', component: DisplayEventsComponent },
      { path: 'dashboard-layout', component: DashboardLayoutComponent },
      { path: 'add-event', component: AddEventComponent },
      { path: 'event-question', component: EventQuestionComponent },
      { path: 'registered-users', component: RegisteredUsersComponent },
      { path: 'past-events', component: PastEventsComponent },
      { path: 'app-tickets', component: TicketsComponent },
      { path: 'user-profile', component: ProfileComponent },
      { path: 'payment-screen', component: PaymentScreenComponent },
      { path: 'change-password', component: ChangePasswordComponent },
      { path: 'edit-profile', component: EditProfileComponent }
    ]),
    BrowserAnimationsModule,
  ],
  providers: [
    NavServiceService,
    { provide: MAT_DATE_LOCALE, useValue: 'en-US' },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '351890265637-sd52e48opas4dbn2itta1vpspsqbqmlb.apps.googleusercontent.com'
            ),
          },
        ],
      } as SocialAuthServiceConfig,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
