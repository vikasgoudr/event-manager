import { Injectable } from '@angular/core';
import { TokenData } from './TokenData';

@Injectable({
  providedIn: 'root',
})
export class TokenDataService {
  public getTokenData() {
    const token = localStorage.getItem('JwtToken');
    if (token != null) {
      const result = JSON.parse(atob(token.split('.')[1]));
      return new TokenData(result.UserId,
        result.Email,
        result.Name,
        result.Role,
        result.Age,
        result.Gender,
        result.ApprovalStatus);
    }
    else{
      return null;
    }
  }
}
