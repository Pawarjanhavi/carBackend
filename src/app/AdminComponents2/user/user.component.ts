import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponentAdmin } from '../header/header.component';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { UserModel } from '../../Models/UserModel';


@Component({
  selector: 'app-user',
  imports: [CommonModule, HeaderComponentAdmin, NavigationbarComponentAdmin],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  http = inject(HttpClient);

  receivedUsers: UserModel[] = [];

  ngOnInit(): void {
    this.http.get('https://localhost:7273/api/User/get-users-by-role').subscribe((result: any) => {
      console.log(result);
      this.receivedUsers = result;
    });
  }
  onDelete(email: string): void {
    if (email) {
      this.http.delete('https://localhost:7273/api/User/delete-user?email=' + email, { responseType: 'text' })
        .subscribe({
          next: (response) => {
            // handle success
            console.log('User Deleted Successfully', response);
  
            // Remove the user from the array after deletion
            this.receivedUsers = this.receivedUsers.filter(user => user.email !== email);
  
          },
          error: (error) => {
            console.error('Error deleting user:', error);
          }
        });
    } else {
      console.error('Error: Email is null or undefined');
    }
  }
  
}  