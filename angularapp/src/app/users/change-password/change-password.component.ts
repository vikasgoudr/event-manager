import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {
  constructor(private http:HttpClient,private router:Router,private fb:FormBuilder,private tokenDataService:TokenDataService,private snackBarService:SnackbarService){}
  changePasswordForm=this.fb.group({
    id:this.tokenDataService.getTokenData()?.userId,
    email:this.tokenDataService.getTokenData()?.email,
    currentPassword:["",[Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
    newPassword:["",[Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
    confirmNewPassword:["",[Validators.required,Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]]
  });
  tokenData!: TokenData | null;
  ChangePassword(){
    if(this.changePasswordForm.controls.newPassword.value==this.changePasswordForm.controls.confirmNewPassword.value){
      this.http.patch('https://localhost:7131/api/User/change-password',this.changePasswordForm.value,{responseType:'text'}).subscribe(
        result=>{
          console.log(result);
          this.snackBarService.openSnackBar("Password Changed Successfully");
          this.router.navigate(['user-profile']);
        },
        error=>{
          console.log(error);
          this.snackBarService.openSnackBar("Error in Password Changing");
        }
      )
    }
    else{
      this.snackBarService.openSnackBar("Passwords Doesn't Match");
    }
  }
}
