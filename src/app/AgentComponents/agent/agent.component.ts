import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarService } from '../../Services/car.service';
import { SharedService } from '../../Services/shared.service'; // Import SharedService
import { Subscription } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-agent',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './agent.component.html',
  styleUrls: ['./agent.component.css']
})
export class AgentComponent implements OnInit, OnDestroy {

    carForm!: FormGroup;
    imageFile: File | null = null;
    email: string | null = null; // Variable to store the email
    emailSubscription: Subscription | undefined;

    constructor(private fb: FormBuilder, 
                private carService: CarService, 
                private sharedService: SharedService) { }

    ngOnInit(): void {
      // Subscribe to currentEmail observable to get the email
      this.emailSubscription = this.sharedService.currentEmail.subscribe((email: string | null) => {
        this.email = email;
      });

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

      this.carService.createCar(formData).subscribe({
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
      // Logic to navigate to the agent homepage (replace with actual route)
      window.location.href = '/app-agent-home-page';
    }

    ngOnDestroy(): void {
      // Unsubscribe from the observable to avoid memory leaks
      if (this.emailSubscription) {
        this.emailSubscription.unsubscribe();
      }
    }
}
