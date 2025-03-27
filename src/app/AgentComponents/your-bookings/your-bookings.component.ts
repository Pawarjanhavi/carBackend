import { Component, inject, OnInit } from '@angular/core';
import { CarService } from '../../Services/car.service';
import { carModel } from '../../Models/CarWithLocation';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AgentNavBarComponent } from "../agent-nav-bar/agent-nav-bar.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-your-bookings',
  imports: [CommonModule, FormsModule, AgentNavBarComponent],
  templateUrl: './your-bookings.component.html',
  styleUrl: './your-bookings.component.css'
})
export class YourBookingsComponent {

  http = inject(HttpClient);
  
  receivedCars: any[] = []; // Declare the cars array to hold the response data

  ngOnInit(): void {
    // Fetching car details from the API
    this.http.get('https://localhost:7273/api/Agent/Reserved-AgentCar').subscribe((result: any) => {
      console.log(result);
      this.receivedCars = result; // Assign the response to receivedCars
    });
  }

}
