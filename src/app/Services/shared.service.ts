import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  
  private emailSubject = new BehaviorSubject<string | null>(localStorage.getItem('email'));
  currentEmail = this.emailSubject.asObservable();

  // setEmail(email: string): void {
  //   localStorage.setItem('email', email);
  //   this.emailSubject.next(email);
  // }

  // clearEmail(): void {
  //   localStorage.removeItem('email');
  //   this.emailSubject.next(null);
  // }

  setEmail(email: string): void {
    try {
      localStorage.setItem('email', email);
      this.emailSubject.next(email);
    } catch (error) {
      console.error('Failed to save email to localStorage:', error);
    }
  }
  
  clearEmail(): void {
    try {
      localStorage.removeItem('email');
      this.emailSubject.next(null);
    } catch (error) {
      console.error('Failed to clear email from localStorage:', error);
    }
  }
  
}
