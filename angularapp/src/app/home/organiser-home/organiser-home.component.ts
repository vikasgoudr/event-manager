import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TokenDataService } from 'src/Services/TokenData/token-data.service';
import { TokenData } from 'src/Services/TokenData/TokenData';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}
@Component({
  selector: 'app-organiser-home',
  templateUrl: './organiser-home.component.html',
  styleUrls: ['./organiser-home.component.css'],
})
export class OrganiserHomeComponent implements OnInit {
  tokenData!: TokenData | null;

  constructor(private tokenDataService: TokenDataService) {}

  ngOnInit(): void {
    this.tokenData = this.tokenDataService.getTokenData();
  }

 
  
}
