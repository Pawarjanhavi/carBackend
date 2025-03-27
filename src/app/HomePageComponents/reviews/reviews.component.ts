import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-reviews',
  imports: [CommonModule],
  templateUrl: './reviews.component.html',
  styleUrl: './reviews.component.css'
})
export class ReviewsComponent {
  reviews = [
    {
      name: 'Tejas Gaikwad',
      feedback:
        'The car rental process was seamless! The vehicle was clean, and the customer service was excellent. Highly recommend!',
      vehicle: 'Rented a Sedan',
      photo: 'images/male.png',
    },
    {
      name: 'Riddhi Patil',
      feedback:
        'I had a great experience renting an SUV for my family trip. The prices were competitive, and the pickup was hassle-free and convinent',
      vehicle: 'Rented an SUV',
      photo: 'images/female.jpg',
    },
    {
      name: 'Sakshi Sharma',
      feedback:
        'Our family trip was made even better by renting an SUV. The service was seamless, the cost was fair, and getting the vehicle was incredibly straightforward',
      vehicle: 'Rented an SUV',
      photo: 'images/female.jpg',
    },
  ];
}
