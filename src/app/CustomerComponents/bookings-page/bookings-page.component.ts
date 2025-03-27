import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { HttpClient } from '@angular/common/http';
import { bookingDetailsModel } from '../../Models/BookingDetailsModel';
import { SharedService } from '../../Services/shared.service';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bookings-page',
  imports: [HeaderComponent,CommonModule,ReactiveFormsModule],
  templateUrl: './bookings-page.component.html',
  styleUrl: './bookings-page.component.css'
})
export class BookingsPageComponent implements OnInit {
  http=inject(HttpClient);
  router=inject(Router);
  sharedService = inject(SharedService);

  bookingInfo:bookingDetailsModel[]=[];
  userEmail:string | null = null; //store email - fetch from shared service

  // Track if review has been submitted for each booking
  isReviewSubmitted: boolean[] = [];
  // Track if each booking is canceled
  isCancelled: boolean[] = [];

  carIndex:number=0;

  reviewForm: FormGroup = new FormGroup({
    reviewText: new FormControl('', Validators.required),
    rating: new FormControl(0, [Validators.required, Validators.min(1), Validators.max(5)]),
});

updateReviewForm:FormGroup= new FormGroup({
  reviewText: new FormControl(''),
  rating: new FormControl(0, [Validators.min(1), Validators.max(5)]),
});

    

  ngOnInit(): void {
    // Subscribe to the currentEmail observable
    this.sharedService.currentEmail.subscribe((email) => {
      if (email) { // Ensure email is not null
        this.userEmail = email;
        console.log('User Email:', this.userEmail);
        // Call API after receiving a valid email
        this.fetchBookingDetails();
      } else {
        console.error('Email is null or invalid');
      }
    });
  }
  
  fetchBookingDetails(): void {
    this.http
      .get(`https://localhost:7273/api/Reservation/get-reservation-history-of-user?email=${this.userEmail}`)
      .subscribe({
        next: (result: any) => {
          if(result.length == 0){
            alert("No Bookings Found");
            this.router.navigate(['/app-customer-home-page']);
          }
          this.bookingInfo = result;
          console.log('Booking Info:', this.bookingInfo);                   
        },
        error:(error) => {
          console.error('Error fetching booking details:', error);
        }
      });
  }

  isCancelButtonDisabled(pickUpDate: any, index: number): boolean {
    const currentTime = new Date();
    const pickUpTime = new Date(pickUpDate); // Convert string or DateOnly to Date object
    
    const timeDifference = currentTime.getTime() - pickUpTime.getTime();
  
    // Disable button if more than 24 hours have passed or if the reservation is already canceled
    return timeDifference > 24 * 60 * 60 * 1000 || this.isCancelled[index] || this.bookingInfo[index].reservationStatus === 'Cancelled';
  }
  
  
  //Cancel Reservation-Update in Db, Calling API
  cancelReservation(index: number): void {
    const email = this.userEmail; // User email from the shared service
    const licensePlate = this.bookingInfo[index].carNumber; // Car number (license plate) from the booking details
  
    // Call the backend API to cancel the reservation
    this.http
      .post(`https://localhost:7273/api/Reservation/Cancel?email=${this.userEmail}&licensePlate=${licensePlate}`,null)
      .subscribe({
        next: (response: any) => {
          // If the response contains "successfully", update the status
          alert(`Reservation for ${this.bookingInfo[index].carModel} cancelled successfully!`);
          this.bookingInfo[index].reservationStatus = 'Cancelled'; // Update the status in the frontend
          this.isCancelled[index] = true; // Mark the reservation as cancelled
          this.fetchBookingDetails();
        },
        error: (error) => {
          console.error('Error canceling reservation:', error);
          alert('Failed to cancel the reservation. Please try again.');
        }
      });
  }
  
  //Open Review modal 
  onReview(index: number): void {
     // Ensure Bootstrap modal functionality is used for fade and backdrop
     const modalDiv = document.getElementById("reviewModal");
     if (modalDiv) {
       modalDiv.classList.add("show");
       modalDiv.style.display = "block";
       document.body.classList.add("modal-open");
   
       // Add backdrop
       const backdrop = document.createElement("div");
       backdrop.className = "modal-backdrop fade show";
       backdrop.id = "custom-backdrop";
       document.body.appendChild(backdrop);
     }
     this.carIndex = index;

    //  console.log(this.userEmail);
    // Simulate the submission of a review
    // console.log('Review submitted for booking index:', index);
    // console.log(this.bookingInfo[index].carNumber);
  

    // After successful submission, mark the review as submitted
    // this.isReviewSubmitted[index] = true;
    // console.log(this.isReviewSubmitted[0]);

    
  }
  
  //save review in database
  onSaveReview(): void {
    console.log(this.reviewForm.value);
    console.log("car index", this.carIndex, this.bookingInfo[this.carIndex].carNumber);
  
    const reviewFormValue = this.reviewForm.value;
  
    this.http
      .post(
        `https://localhost:7273/api/Review/give-review?email=${this.userEmail}&licensePlate=${this.bookingInfo[this.carIndex].carNumber}`,
        reviewFormValue
      )
      .subscribe({
        next: (response: any) => {
          console.log('Review submitted successfully:', response);

           // If the response contains "successfully", update the status
           alert(`Review for ${this.bookingInfo[this.carIndex].carModel} submitted successfully!`);
           this.bookingInfo[this.carIndex].hasReview = false; // Update the status in the frontend

          // Mark the review as submitted and disable the button
          this.isReviewSubmitted[this.carIndex] = true;
  
          // Close the modal after submission
          this.closeReviewModal();
          this.ngOnInit();
        },
        error: (error) => {
          alert(`Review for ${this.bookingInfo[this.carIndex].carModel} not submitted`)
          console.error('Error submitting review:', error);
        }
      });
  }

  //close review modal popup
  closeReviewModal() {
      const modalDiv = document.getElementById("reviewModal");
      if (modalDiv) {
        modalDiv.classList.remove("show");
        modalDiv.style.display = "none";
        document.body.classList.remove("modal-open");
    
        // Remove backdrop
        const backdrop = document.getElementById("custom-backdrop");
        if (backdrop) {
          document.body.removeChild(backdrop);
        }
      }
  }

  //Open update review modal
  onUpdateReview(){
    // Ensure Bootstrap modal functionality is used for fade and backdrop
    const modalDiv = document.getElementById("updateReviewModal");
    if (modalDiv) {
      modalDiv.classList.add("show");
      modalDiv.style.display = "block";
      document.body.classList.add("modal-open");
  
      // Add backdrop
      const backdrop = document.createElement("div");
      backdrop.className = "modal-backdrop fade show";
      backdrop.id = "custom-backdrop";
      document.body.appendChild(backdrop);
    }
  }

  onSaveUpdateReview(){
    const updateReviewFormValue = this.updateReviewForm.value;
  
    this.http
      .put(
        `https://localhost:7273/api/Review/update-review?email=${this.userEmail}&licensePlate=${this.bookingInfo[this.carIndex].carNumber}`,
        updateReviewFormValue
      ).subscribe({
        next: (response: any) => {
          if(response.result){

            console.log('Review submitted successfully:', response);           
            // If the response contains "successfully", update the status
            alert(`Review for ${this.bookingInfo[this.carIndex].carModel} updated successfully!`);
            // Close the modal after submission
            this.closeUpdateReviewModal();
            this.ngOnInit();
          }
          else{
            alert(`Review for ${this.bookingInfo[this.carIndex].carModel} not updated`)

          }
        },
        error: (error) => {
          alert(`Review for ${this.bookingInfo[this.carIndex].carModel} not updated`)
          console.error('Error updating review:', error);
        }
      });
  }
  
  //close review modal popup
    closeUpdateReviewModal() {
      const modalDiv = document.getElementById("updateReviewModal");
      if (modalDiv) {
        modalDiv.classList.remove("show");
        modalDiv.style.display = "none";
        document.body.classList.remove("modal-open");
    
        // Remove backdrop
        const backdrop = document.getElementById("custom-backdrop");
        if (backdrop) {
          document.body.removeChild(backdrop);
        }
      }
  }

  
}//end class

  // ngOnInit(): void {
  //    // Subscribe to the currentEmail observable
  //    this.sharedService.currentEmail.subscribe((email) => {
  //     this.userEmail = email;
  //     console.log('User Email:', this.userEmail);
  //   });

  //   //Call API to get reservation details of particular user
  //   this.http.get(`https://localhost:7273/api/Reservation/get-reservation-history-of-user?email=${this.userEmail}`).subscribe((result:any)=>{
  //     this.bookingInfo = result;
  //   });
  //   // Iterate over bookingList in console to verify the booking details
  //     this.bookingInfo.forEach((booking: any) => {
  //       console.log(booking);
  //     });
  //   console.log(this.bookingInfo.length);
  // }  
