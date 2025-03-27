import { Component, inject, OnInit } from '@angular/core';
import { carModel } from '../../Models/CarWithLocation';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from "../../CustomerComponents/header/header.component";
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CarService } from '../../Services/car.service';
import { RouterLink,RouterLinkActive } from '@angular/router';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { HeaderComponentAdmin } from '../header/header.component';
import { carModels } from '../../Models/carModels';


@Component({
  selector: 'app-cars',
  imports: [CommonModule, HeaderComponentAdmin, FormsModule, NavigationbarComponentAdmin],
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit{
 
  carService = inject(CarService);
  router = inject(Router);

  carList: carModels[] = [];
  successMessage: string = ''; // Property to hold the success message
  errorMessage: string = '';   // Property to handle error message (optional)

  ngOnInit(): void {
    this.getAllCars();
  }


 

  getAllCars() {
    this.carService.getAllCars().subscribe((result: any) => {
      this.carList = result;
    });
  }

  deleteCarForAdmin(selectedCar: carModels) {
    if (confirm('Are you sure you want to delete this car?')) {
      this.carService.DeleteCar(selectedCar.licensePlate).subscribe(
        (response: any) => {
          // After successful deletion, update the car list
          this.getAllCars();

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

  updateCarForAdmin(selectedCar: carModels) {
    this.router.navigate(['/app-admin-update-car'], { state: { data: selectedCar } });
  }

}


 





