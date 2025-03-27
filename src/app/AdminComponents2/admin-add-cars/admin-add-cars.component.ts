import { Component } from '@angular/core';

import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { CarService } from '../../Services/car.service';
import { HeaderComponentAdmin } from "../header/header.component";
import { NavigationbarComponentAdmin } from "../navigationbar/navigationbar.component";

@Component({
  selector: 'app-admin-add-cars',
  imports: [ReactiveFormsModule, HeaderComponentAdmin, NavigationbarComponentAdmin],
  templateUrl: './admin-add-cars.component.html',
  styleUrl: './admin-add-cars.component.css'
})
export class AdminAddCarsComponent {
  carForm!: FormGroup;
  imageFile: File | null = null;
  

  constructor(private fb: FormBuilder, 
              private carService: CarService) { }

              ngOnInit(): void {
                this.carForm = this.fb.group({
                  make: ['', Validators.required],
                  model: ['', Validators.required],
                  year: [null, [Validators.required, Validators.min(1900)]],
                  colour: ['', Validators.required],
                  totalSeats: [null, [Validators.required, Validators.min(5), Validators.max(7)]],
                  licensePlate: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
                  pricePerDay: [null, Validators.required],
                  availableStatus: [false, Validators.required],
                  availableDate: ['', Validators.required],
                  city: ['', Validators.required],
                  address: ['', Validators.required],
                  state: ['', Validators.required],
                  country: ['', Validators.required],
                  zipCode: ['', Validators.required],
                });
              }
            
              onFileSelected(event: any): void {
                this.imageFile = event.target.files[0];
              }
            
              submitForm(): void {
                if (this.carForm.invalid || !this.imageFile) {
                  alert('Please fill all required fields and upload an image.');
                  return;
                }
            
                const formData = new FormData();
                Object.keys(this.carForm.value).forEach((key) => {
                  formData.append(key, this.carForm.get(key)?.value);
                });
            
                formData.append('CarImageFile', this.imageFile);
            
                this.carService.createCarForAdmin(formData).subscribe({
                  next: (response) => {
                    alert('Car created successfully!');
                    this.carForm.reset();
                    this.imageFile = null;
                    this.goToHomepage();
                  },
                  error: (error) => {
                    console.error('Request error:', error);
                    alert('An error occurred: ' + error.message);
                  },
                });
              }
            
              goToHomepage(): void {
                window.location.href = '/app-cars';
              }
            }


 

