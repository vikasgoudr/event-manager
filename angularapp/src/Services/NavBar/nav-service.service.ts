import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class NavServiceService {
  private isLoggedInSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private roleSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  private organiserApprovalStatus: BehaviorSubject<number> = new BehaviorSubject<number>(0);
  constructor() { }
  setIsLoggedIn(value: boolean): void {
    this.isLoggedInSubject.next(value);
  }
  setRole(role: string): void {
    this.roleSubject.next(role);
  }
  getIsLoggedIn(): boolean {
    return this.isLoggedInSubject.getValue();
  }
  getRole(): string {
    return this.roleSubject.getValue();
  }
  getIsLoggedInObservable(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }
  getRoleObservable(): Observable<string> {
    return this.roleSubject.asObservable();
  }
  setOrganiserStatus(value: number): void {
    this.organiserApprovalStatus.next(value);
  }
  getOrganiserStatus(): number {
    return this.organiserApprovalStatus.getValue();
  }
  getOrganiserStatusObservable(): Observable<number> {
    return this.organiserApprovalStatus.asObservable();
  }
}
