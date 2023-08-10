import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { Gender } from '../../../enums/gender.enum';
import { Role } from '../../../enums/role.enum';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  genderValues = Object.keys(Gender).filter((item)=>{
    return isNaN(Number(item));
  });
  roleValues = Object.keys(Role).filter((item)=>{
    return isNaN(Number((item)))
  });
  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    userName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}$")]],
    age: ['', [Validators.required,Validators.maxLength(2)]],
    phoneNumber: ['', [Validators.required,Validators.minLength(10),Validators.maxLength(10)]],
    gender: ['',Validators.required],
    role:['',Validators.required]
  });
  constructor(private http: HttpClient, private fb: FormBuilder,private snackbar:SnackbarService, private router: Router) {

  }
  register() 
  {
    if(this.registerForm.controls.gender.value=="Female"){
      this.registerForm.controls.gender.setValue('1');
    }
    else if(this.registerForm.controls.gender.value=="Male"){
      this.registerForm.controls.gender.setValue('2');
    }
    else if(this.registerForm.controls.gender.value=="Other"){
      this.registerForm.controls.gender.setValue('3');
    }
    if(this.registerForm.controls.role.value=="User"){
      this.registerForm.controls.role.setValue('1');
    }
    else if(this.registerForm.controls.role.value=="Organiser"){
      this.registerForm.controls.role.setValue('2');
    }
    else if(this.registerForm.controls.role.value=="Admin"){
      this.registerForm.controls.role.setValue('3');
    }
    this.http.post("https://localhost:7131/api/User/Register", this.registerForm.value, { responseType: 'text' }).subscribe(
     result => {
       console.log(result)
       this.snackbar.openSnackBar('Registered Successfully');
       this.router.navigate(['login-comp']);
     },
     (error: HttpErrorResponse)=>{
       console.log(error);
       this.snackbar.openSnackBar('Registration Failed');
     }
    );
    console.log(this.registerForm);
  }
}
