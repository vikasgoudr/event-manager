import { Component, OnInit } from '@angular/core';
import { TokenData } from 'src/Services/TokenData/TokenData';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private router: Router) { }


  ngOnInit(): void {
    // this.router.navigate(['']);
  }
}
