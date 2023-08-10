import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TokenData } from '../../../Services/TokenData/TokenData';
import { TokenDataService } from '../../../Services/TokenData/token-data.service';
import { Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { NavServiceService } from 'src/Services/NavBar/nav-service.service';
import { Role } from '../../../enums/role.enum';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {
  tokenData!: TokenData | null;
  socialUser!: SocialUser;
  constructor(private http: HttpClient,
    private fb: FormBuilder,
    private tokenDataService: TokenDataService,
    private router: Router,
    private socialAuthService: SocialAuthService,
    private nav: NavServiceService,
    private snackbar: SnackbarService) { }
  ngOnInit(): void {
    this.LoginWithGoogle();
  }
  registerForm = this.fb.group({
    firstName: [""],
    lastName: "",
    age: 0,
    phoneNumber: "",
    gender: 0,
    role: 1,
    userName: "",
    email: "",
    password: "",
    displayPicture: ""
  });
  loginForm = this.fb.group({
    Email: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]]
  });
  async LoginWithGoogle() {
    this.socialAuthService.authState.subscribe((user: SocialUser | null) => {
      if (user) {
        this.socialUser = user;
        console.log(this.socialUser);
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        this.http.post('https://localhost:7131/api/User/email-already-exists', JSON.stringify(this.socialUser.email), { headers: headers }).subscribe(
          async (response: any) => {
            console.log(this.socialUser.email);
            console.log(response + " response");
            if (response === true) {
              console.log("asdas");
              this.loginForm.controls['Email'].setValue(this.socialUser.email);
              this.loginForm.controls['Password'].setValue(this.socialUser.id + this.socialUser.name.replace(/\s/g, "").toUpperCase() + this.socialUser.name.replace(/\s/g, "") + this.socialUser.email);
              this.login();
            } else if (response === false) {
              console.log("ergad");
              this.registerForm.controls['userName'].setValue(this.socialUser.name.replace(/[^a-zA-Z]/g, '').replace(/\s/g, ""));
              this.registerForm.controls['email'].setValue(this.socialUser.email);
              this.registerForm.controls['displayPicture'].setValue(await this.fetchImageAsBase64(this.socialUser.photoUrl));
              this.registerForm.controls['password'].setValue(this.socialUser.id + this.socialUser.name.replace(/\s/g, "").toUpperCase() + this.socialUser.name.replace(/\s/g, "") + this.socialUser.email);
              this.loginForm.controls['Email'].setValue(this.socialUser.email);
              this.loginForm.controls['Password'].setValue(this.socialUser.id + this.socialUser.name.replace(/\s/g, "").toUpperCase() + this.socialUser.name.replace(/\s/g, "") + this.socialUser.email);
              this.http.post('https://localhost:7131/api/User/register-using-google', this.registerForm.value, { responseType: 'text' }).subscribe(
                result => {
                  console.log(result);
                  this.snackbar.openSnackBar("Registered Successfully Using Google");
                  this.login();
                },
                (error) => {
                  console.log(error);
                  this.snackbar.openSnackBar("Registration Failed Using Google");
                }
              )
            }
          })
      }
    })
  }
  login() {
    this.http.post('https://localhost:7131/api/User/Login', this.loginForm.value, { responseType: 'text' }).subscribe(
      response => {
        localStorage.setItem('JwtToken', response);
        this.tokenData = this.tokenDataService.getTokenData();
        if (this.tokenData !== null) {
          if (this.tokenData.role == 1) {
            this.router.navigate(['user-home']);
            this.nav.setRole(Role.User.toString());
            this.nav.setIsLoggedIn(true);
          }
          else if (this.tokenData.role == 2) {
            this.router.navigate(['organiser-home']);
            this.nav.setRole(Role.Organiser.toString());
            this.nav.setIsLoggedIn(true);
            this.nav.setOrganiserStatus(this.tokenData.approvalStatus);
          }
          else if (this.tokenData.role == 3) {
            this.router.navigate(['admin-home']);
            this.nav.setRole(Role.Admin.toString());
            this.nav.setIsLoggedIn(true);
          }
          this.snackbar.openSnackBar('LoggedIn Successfully');
        }
        else {
          console.log("token data is null");
          this.snackbar.openSnackBar('Login Failed');
        }
      },
      (error: HttpErrorResponse) => {
        console.error('Login failed', error);
        this.snackbar.openSnackBar('Login Failed');
      }
    );
  }
  Return() {
    this.router.navigate(['']);
  }
  fetchImageAsBase64(imageUrl: string): Promise<string> {
    return fetch(imageUrl)
      .then(response => response.blob())
      .then(blob => {
        return new Promise<string>((resolve, reject) => {
          const reader = new FileReader();
          reader.onloadend = () => {
            if (reader.result) {
              const base64Image = reader.result.toString().split(',')[1];
              resolve(base64Image);
            } else {
              reject('Failed to convert image to Base64.');
            }
          };
          reader.onerror = () => {
            reject('Error reading image file.');
          };
          reader.readAsDataURL(blob);
        });
      });
  }
}