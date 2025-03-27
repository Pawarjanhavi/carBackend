import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  HeaderComponentAdmin } from '../header/header.component';
import {  NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { AgentModel } from '../../Models/AgentModel';
import { NavigationBarComponent } from "../../HomePageComponents/navigation-bar/navigation-bar.component";  // Import the AgentModel

@Component({
  selector: 'app-agents',
  imports: [CommonModule, HeaderComponentAdmin, NavigationbarComponentAdmin],
  templateUrl: './agents.component.html',
  styleUrls: ['./agents.component.css']
})
export class AgentsComponent implements OnInit {
  http = inject(HttpClient);

  receivedAgents: AgentModel[] = [];  // Use AgentModel here

  ngOnInit(): void {
    this.http.get('https://localhost:7273/api/User/get-agents-by-role').subscribe((result: any) => {
      console.log(result);
      this.receivedAgents = result;  // Assuming result is an array of AgentModel data
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
            this.receivedAgents = this.receivedAgents.filter(agent => agent.email !== email);
  
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
