import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CarService } from '../../Services/car.service';
import { carModel } from '../../Models/CarWithLocation';
import { AgentNavBarComponent } from "../agent-nav-bar/agent-nav-bar.component";
import { carModels } from '../../Models/carModels';


@Component({
  selector: 'app-update-car-details',
  imports: [ReactiveFormsModule],
  templateUrl: './update-car-details.component.html',
  styleUrls: ['./update-car-details.component.css']
})
export class UpdateCarDetailsComponent implements OnInit {

  
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
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.carForm.valid) {
      const updatedCar = this.carForm.value as carModels;
      this.carService.updateCar(updatedCar).subscribe(response => {
        alert('Car updated successfully!');
        this.router.navigate(['/app-your-cars']); // Redirect after successful update
      }, error => {
        alert('Failed to update car. Please try again.');
      });
    }
  }

 
}
