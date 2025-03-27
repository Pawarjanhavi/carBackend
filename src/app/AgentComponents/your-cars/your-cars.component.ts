import { Component, inject, OnInit } from '@angular/core';
import { CarService } from '../../Services/car.service';
import { carModel } from '../../Models/CarWithLocation';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AgentNavBarComponent } from "../agent-nav-bar/agent-nav-bar.component";
import { Observable } from 'rxjs';
import { SharedService } from '../../Services/shared.service';
import { HttpClient } from '@angular/common/http';
import { carModels } from '../../Models/carModels';

@Component({
  selector: 'app-your-cars',
  standalone: true,
  imports: [CommonModule, FormsModule, AgentNavBarComponent],
  templateUrl: './your-cars.component.html',
  styleUrls: ['./your-cars.component.css']
})
export class YourCarsComponent implements OnInit {

  carService = inject(CarService);
  router = inject(Router);
  sharedService = inject(SharedService); 
  http = inject(HttpClient); 

  carList: carModels[] = [];
  successMessage: string = ''; // Property to hold the success message
  errorMessage: string = '';   // Property to handle error message (optional)
  email: string | null = null; 

  ngOnInit(): void {
    this.sharedService.currentEmail.subscribe((email) => {
      if (email) {
        this.email = email; // Assign the email
        this.GetAgentCarsByEmail(); // Automatically fetch cars
      } else {
        this.errorMessage = 'Email is not available. Please log in again.';
      }
    });
  }
  GetAgentCarsByEmail(): void {
    if (!this.email) {
      this.errorMessage = 'Email is not available.';
      return;
    }

    const url = `https://localhost:7273/api/Agent/GetCarsByEmail?email=${encodeURIComponent(this.email)}`;
    this.http.get<carModels[]>(url).subscribe(
      (response) => {
        this.carList = response; // Populate carList with API response
        this.successMessage = 'Cars fetched successfully.';
        this.errorMessage = '';
      },
      (error) => {
        console.error('Failed to fetch cars:', error);
        this.errorMessage = 'Failed to fetch car list. Please try again.';
        this.carList = [];
      }
    );
  }

  UpdateCarDetails(selectedCar: carModels) {
    this.router.navigate(['/app-update-car-details'], { state: { data: selectedCar } });
  }

  DeleteCar(selectedCar: carModels) {
    if (confirm('Are you sure you want to delete this car?')) {
      this.carService.DeleteCar(selectedCar.licensePlate).subscribe(
        (response: any) => {
          // After successful deletion, update the car list
          this. GetAgentCarsByEmail();

          // Set the success message
          this.successMessage = 'Car deleted successfully';
          
          // Optionally, remove the deleted car from the list immediately
          this.carList = this.carList.filter(car => car.licensePlate !== selectedCar.licensePlate);
          
          // Clear the error message if any
          this.errorMessage = '';

          setTimeout(() => {
            this.successMessage = ''; // Hide success message after 5 seconds
            document.querySelector('.alert-success')?.classList.add('fade-out'); // Add fade-out class
          }, 5000);
        },
        (error) => {
          this.errorMessage = 'Failed to delete car. Please try again.';
        }
      );
    }
  }
}
