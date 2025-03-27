import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponentAdmin } from '../header/header.component';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { HttpClient } from '@angular/common/http';
import { reviewModel } from '../../Models/reviewModel';
import { CommonModule } from '@angular/common';
import { UserReviewModel } from '../../Models/userReviewModel';

@Component({
  selector: 'app-reviews',
  imports: [HeaderComponentAdmin, NavigationbarComponentAdmin, CommonModule],
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
  http = inject(HttpClient);

  allReviews: reviewModel[] = [];
  allUserReviews: UserReviewModel[] = [];
  
  // State variables for container visibility
  showRatings: boolean = true;
  showReviews: boolean = false;

  ngOnInit(): void {
    this.http.get('https://localhost:7273/api/Car/get-all-cars-with-rating').subscribe({
      next: (result: any) => {
        this.allReviews = result;
      },
      error: (error) => {
        console.log(error);
        alert("Error fetching car reviews.");
      }
    });
  }

  getCarRatings(){
    this.http.get('https://localhost:7273/api/Car/get-all-cars-with-rating').subscribe({
      next: (result: any) => {
        this.allReviews = result;
         // Toggle visibility
         this.showRatings = true; 
         this.showReviews = false;  
      },
      error: (error) => {
        console.log(error);
        alert("Error fetching car reviews.");
      }
    });
  }

  getUserReviews() {
    this.http.get('https://localhost:7273/api/Review/get-all-user-Reviews').subscribe({
      next: (result: any) => {
        this.allUserReviews = result;

        // Toggle visibility
        this.showRatings = false; // Hide rating container
        this.showReviews = true;  // Show review container
      },
      error: (error) => {
        console.log(error);
        alert("Error fetching user reviews.");
      }
    });
  }
}
