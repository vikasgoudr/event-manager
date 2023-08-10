import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Profile } from 'src/models/profile.model';
import { SnackbarService } from 'src/Services/Snackbar/snackbar.service';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { Buffer } from 'buffer';
@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  imageBuffer: any;
  imageBlob:any;
  constructor(private http: HttpClient,private router:Router,private snackBarService:SnackbarService, private fb: FormBuilder, private tokenDataService: TokenDataService, private route: ActivatedRoute) { 
  }
  ProfileData: Profile | any;
  editProfileForm = this.fb.group({
    id: this.tokenDataService.getTokenData()?.userId,
    firstName: "",
    lastName: "",
    gender: 0,
    userName: "",
    displayPicture: "",
    phoneNumber: "",
    age: 0,
    role: this.tokenDataService.getTokenData()?.role,
    // address:"",
    email: this.tokenDataService.getTokenData()?.email
  })
  ngOnInit(): void {
    this.UpdateForm();
  }
  img: any;
  showDefault = false;
  selectedGender: number = 0;
  data:any;
  UpdateForm() {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    this.http.post<Profile>('https://localhost:7131/api/User/user-profile', JSON.stringify(this.tokenDataService.getTokenData()?.userId),{headers:headers}).subscribe(
      (result) => {
        console.log(result);
        const receivedData = result;
        this.editProfileForm.patchValue(receivedData);
        this.selectedGender=receivedData.gender;
        if(receivedData.displayPicture==""||receivedData.displayPicture==null){
          this.showDefault=true;
          console.log("true");
        }
        else{
          this.img="data:image/jpeg;base64,"+receivedData.displayPicture;
          this.showDefault=false;
          console.log("false "+this.img);
        }
      },
      (error) => {
        console.log(error);
      }
    )
      // console.log(this.receivedData.data);
      // this.editProfileForm.controls['firstName'].setValue(this.receivedData.firstName);
      // this.editProfileForm.patchValue(this.receivedData);
    //   this.selectedGender = receivedData.gender;
    //   if (receivedData.displayPicture == "" || receivedData.displayPicture == null) {
      //     this.showDefault = true;
      //     console.log("true");
      //   }
      //   else {
        //     this.img = "data:image/jpeg;base64," + receivedData.displayPicture;
        //     this.showDefault = false;
        //     console.log("false " + this.img);
        //   }
        // });
      }
      onImageSelect(event: any) {
        const file = event.target.files[0];
        
        if (file) {
          this.convertImageToBase64(file);
        }
      }
  convertImageToBase64(file: File) {
    const reader = new FileReader();
    reader.onloadend = () => {
      if (reader.result) {
        const arrayBuffer = reader.result as ArrayBuffer;
        this.imageBuffer = Array.from(new Uint8Array(arrayBuffer));

        // Convert the image buffer to Base64 format
        const base64Image = Buffer.from(this.imageBuffer).toString('base64');
        console.log(base64Image);
        // Assign the Base64 image to the posterImage property
        this.editProfileForm.patchValue({
          displayPicture: base64Image,
        });
        this.img="data:image/jpeg;base64,"+base64Image;
      }
    };
    reader.readAsArrayBuffer(file);
  }
  EditProfile() {
    console.log(this.editProfileForm.value);
    this.http.patch('https://localhost:7131/api/User/update-profile',this.editProfileForm.value,{responseType:'text'}).subscribe(
      result=>{
        console.log(result);
        this.snackBarService.openSnackBar('Profile Updated');
        this.router.navigate(['user-profile']);
      },
      (error)=>{
        console.log(error);
        this.snackBarService.openSnackBar('Issue In Updating Profile');
      }
    )
  }
  Back(){
    this.router.navigate(['user-profile']);
  }
}
