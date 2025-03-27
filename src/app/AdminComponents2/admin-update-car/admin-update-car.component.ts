import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CarService } from '../../Services/car.service';
import { carModel } from '../../Models/CarWithLocation';
import { carModels } from '../../Models/carModels';

@Component({
  selector: 'app-admin-update-car',
  imports: [ReactiveFormsModule],
  templateUrl: './admin-update-car.component.html',
  styleUrl: './admin-update-car.component.css'
})
export class AdminUpdateCarComponent implements OnInit{

  
    carForm!: FormGroup;
    carData: carModels;
  
    constructor(
      private fb: FormBuilder,
      private route: ActivatedRoute,
      private carService: CarService,
      private router: Router
    ) { 
      // Get the car data from the route state, passed from the previous page.
      this.carData = this.router.getCurrentNavigation()?.extras.state?.['data'];
    }
  
    ngOnInit(): void {
      this.initForm();
      if (this.carData) {
        this.carForm.patchValue(this.carData); // Populate form with existing car data
      }
    }
  
    initForm() {
      this.carForm = this.fb.group({
        make: ['', Validators.required],
        model: ['', Validators.required],
        year: ['', [Validators.required, Validators.min(1886)]],
        colour: ['', Validators.required],
        totalSeats: [5, [Validators.required, Validators.min(5), Validators.max(7)]],
        licensePlate: ['', Validators.required],
        pricePerDay: ['', Validators.required],
        
        availableStatus: [false],
        availableDate: ['', Validators.required],
        // city: ['', Validators.required],
        // address: ['', Validators.required],
        // state: ['', Validators.required],
        // country: ['', Validators.required],
        // zipCode: ['', Validators.required],
       
      });
    }
  
    onSubmit() {
      if (this.carForm.valid) {
        const updatedCar = this.carForm.value as carModels;
        this.carService.updateCarForAdmin(updatedCar).subscribe({
          next:(response) => {
             alert('Car updated successfully!');
            this.router.navigate(['/app-cars']); // Redirect after successful update
         }, 
         error:(error) => {
           alert('Failed to update car. Please try again.');
        },
      });
    }

    // this.carService.createCarForAdmin(formData).subscribe({
    //   next: (response) => {
    //     alert('Car created successfully!');
    //     this.carForm.reset();
    //     this.imageFile = null;
    //     this.goToHomepage();
    //   },
    //   error: (error) => {
    //     console.error('Request error:', error);
    //     alert('An error occurred: ' + error.message);
    //   },
    // });
  
   
  }
  

}