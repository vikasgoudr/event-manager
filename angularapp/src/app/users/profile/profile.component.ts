import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Gender } from 'src/enums/gender.enum';
import { Profile } from 'src/models/profile.model';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export default class ProfileComponent implements OnInit {
  ProfileData: Profile | any;
  ProfilePicture: any;
  ShowOriginalPicture = false;
  constructor(private http: HttpClient, private tokenDataService: TokenDataService, private router: Router) { }
  ngOnInit(): void {
    console.log(this.tokenDataService.getTokenData()?.userId + " id");
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http.post<Profile>('https://localhost:7131/api/User/user-profile', JSON.stringify(this.tokenDataService.getTokenData()?.userId), { headers: headers }).subscribe(
      (result) => {
        console.log(result);
        this.ProfileData = result;
        if (result.displayPicture == "" || result.displayPicture == null) {
          this.ShowOriginalPicture = false;
        }
        else {
          this.ShowOriginalPicture = true;
          this.ProfilePicture = "data:image/jpeg;base64," + result.displayPicture;
        }
        console.log(this.ProfilePicture + " " + this.ShowOriginalPicture);
      },
      (error) => {
        console.log(error);
      }
    )
  }
  getGender(id: number) {
    switch (id) {
      case 1: return 'Female';
      case 2: return 'Male';
      case 3: return 'Other';
      default: return '';
    }
  }
  getRole(id: number) {
    switch (id) {
      case 1: return 'User';
      case 2: return 'Organiser';
      case 3: return 'Admin';
      default: return '';
    }
  }
  ShowTickets() {
    this.router.navigate(['app-tickets']);
  }
  ChangePassword() {
    this.router.navigate(['change-password']);
  }
  EditProfile() {
    this.router.navigate(['edit-profile']);
  }
}
