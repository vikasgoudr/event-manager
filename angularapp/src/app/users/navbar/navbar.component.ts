import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavServiceService } from 'src/Services/NavBar/nav-service.service';
import { TokenData } from 'src/Services/TokenData/TokenData';
import { TokenDataService } from '../../../Services/TokenData/token-data.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  role: string = '';
  tokenData!: TokenData | null;
  validOrganiser:number=0;
  constructor(
    private tokenDataService: TokenDataService,
    private navService: NavServiceService,
    private router: Router,
    private socialAuthService: SocialAuthService
  ) {}
  logout(): void {
    this.socialAuthService.signOut();
    localStorage.removeItem('JwtToken');
    this.navService.setIsLoggedIn(false);
    this.navService.setRole('');
    this.navService.setOrganiserStatus(0);
    this.router.navigate(['']);
  }
  organiserHome(): void {
    this.router.navigate(['organiser-home']);
  }
  userHome(): void {
    this.router.navigate(['user-home']);
  }
  pastEvents(){
    this.router.navigate(['past-events']);
  }
  root():void{
    this.router.navigate(['']);
  }
  tickets(): void {
    this.router.navigate(['app-tickets']);
  }
  userProfile():void
  {
   this.router.navigate(['user-profile']);
  }
  adminHome(){
    this.router.navigate(['admin-home']);
  }
  ngOnInit(): void {
    this.tokenData = this.tokenDataService.getTokenData();
    if (this.tokenData != null) {
      this.navService.setIsLoggedIn(true);
      this.navService.setRole(this.tokenData.role.toString());
    }
    this.navService
    .getIsLoggedInObservable()
    .subscribe((isLoggedIn: boolean) => {
      this.isLoggedIn = isLoggedIn;
    });
    this.navService.getRoleObservable().subscribe((role: string) => {
      this.role = role;
    });
    this.navService.getOrganiserStatusObservable().subscribe((value:number)=>{
      this.validOrganiser=value;
    })
  }
}
