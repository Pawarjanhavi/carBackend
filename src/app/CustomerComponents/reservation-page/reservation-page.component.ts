import { CommonModule, JsonPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { carModel } from '../../Models/CarWithLocation';
import { HeaderComponent } from '../header/header.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { reservationDetailsModel } from '../../Models/reservationDetailsModel';
import { SharedService } from '../../Services/shared.service';
import { PaymentModel } from '../../Models/PaymentModel';

@Component({
  selector: 'app-reservation-page',
  imports: [CommonModule,HeaderComponent,FormsModule,ReactiveFormsModule],
  templateUrl: './reservation-page.component.html',
  styleUrl: './reservation-page.component.css'
})
export class ReservationPageComponent implements OnInit {

  paymentDetails:PaymentModel = new PaymentModel();

  receivedCarDetails: carModel = new carModel();
  dropOffDate: any;
  pickUpDate: any;
  calculateTotalAmount: any;
  userEmail: string | null = null; // To store the user's email

  http = inject(HttpClient);
  sharedService = inject(SharedService);
  router = inject(Router);

  ngOnInit(): void {
    // Subscribe to the currentEmail observable
    this.sharedService.currentEmail.subscribe((email) => {
      this.userEmail = email;
      console.log('User Email:', this.userEmail);
    });

    // Access the selected car details from the router state
    const navigationState = history.state?.data;
    if (navigationState) {
      this.receivedCarDetails = navigationState;
      console.log('Received selected car details:', this.receivedCarDetails);
    } else {
      console.log('No car details received.');
    }
  }

  payNow() {
    if (!this.userEmail) {
        alert('No email found. Please log in.');
        return;
    }

    if (!this.paymentDetails.paymentType) {
        alert('Please select a payment mode.');
        return;
    }

    // Prepare reservation details
    const reservationDetails: reservationDetailsModel = {
        Email: this.userEmail,
        LicensePlate: this.receivedCarDetails.licensePlate,
        PickUpDate: this.pickUpDate,
        DropOffDate: this.dropOffDate
    };

    console.log('Reservation Details:', reservationDetails);

    // Call Reservation API
    this.http.post('https://localhost:7273/api/Reservation/reserve-car', reservationDetails)
        .subscribe({
            next: (response: any) => {
                console.log('Reservation successful:', response);
                const reservationId = response; // Assuming reservationId is returned from the API.

                if (reservationId) {
                    // Set payment details
                    this.paymentDetails.paymentAmount = this.calculateTotalAmount;

                    const paymentPayload = {
                        PaymentType: this.paymentDetails.paymentType,
                        PaymentAmount: this.paymentDetails.paymentAmount
                    };

                    // Call Payment API
                    this.http.post(`https://localhost:7273/api/Payment/add-payment?reservationId=${reservationId}`, paymentPayload)
                        .subscribe({
                            next: (paymentResponse: any) => {
                                console.log('Payment successful:', paymentResponse);
                                alert('Payment Successful!');
                                this.closePaymentModal();
                                this.router.navigate(['/app-customer-home-page']);
                            },
                            error: (paymentError) => {
                                console.error('Payment Error:', paymentError);
                                alert('Payment failed. Please try again.');
                            }
                        });
                }
            },
            error: (error) => {
                console.error('Reservation Error:', error);
                alert('Reservation failed. Please try again.');
            }
        });
}


  calculateAmount() {
    // if((this.pickUpDate && this.dropOffDate == null) || (this.pickUpDate == null)|| (this.dropOffDate == null)){
    //   alert(" Select Pickup Date and DropOff Date");
    // }
    // else
     if(this.pickUpDate < this.receivedCarDetails.availableDate || this.pickUpDate > this.receivedCarDetails.availableDate){
      alert("Pickup Date must be same to available date");
    }
    else if(this.dropOffDate <= this.pickUpDate){
      alert("DropOff Date must be after Pickup Date");
    }
    else if((this.pickUpDate < this.receivedCarDetails.availableDate) && (this.dropOffDate <= this.pickUpDate)){
      alert("Pickup Date must be same to available date and DropOff Date must be after Pickup Date")
    }
    else{
      const pickup = new Date(this.pickUpDate);
      const dropoff = new Date(this.dropOffDate);
      const diffTime = Math.abs(dropoff.getTime() - pickup.getTime());
      const diffDays = Math.ceil(diffTime / (1000 * 3600 * 24)); // Convert milliseconds to days
      
      const pricePerDay = this.receivedCarDetails.pricePerDay || 0;
      this.calculateTotalAmount = pricePerDay * diffDays;
      console.log(this.calculateTotalAmount);
    }
  }


  openPaymentModal() {
    if (!this.pickUpDate || !this.dropOffDate) {
      alert('Please select both Pickup Date and Drop-Off Date before proceeding.');
      return;
    }
  
    // Ensure Bootstrap modal functionality is used for fade and backdrop
    const modalDiv = document.getElementById("paymentModal");
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
  
    console.log('Pickup Date:', this.pickUpDate, 'Drop-Off Date:', this.dropOffDate);
  }
  
  closePaymentModal() {
    const modalDiv = document.getElementById("paymentModal");
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
  
  onCancel(){
    this.router.navigate(['\app-customer-home-page']);
  }


  // payNow(){
  //   console.log("Click Successful");
  //   //call reserve-car api
  //   this.http.post('https://localhost:7273/api/Reservation/reserve-car',this.reservationDetails)
  // }


  // openPaymentModal(){
  //   const modalDiv = document.getElementById("paymentModal");
  //   if(modalDiv != null){
  //     modalDiv.style.display = 'block';
  //   }
  // }

  // closePaymentModal(){
  //   const modalDiv = document.getElementById("paymentModal");
  //   if(modalDiv != null){
  //     modalDiv.style.display = 'none';
  //   }
  // }

  
}
