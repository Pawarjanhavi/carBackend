import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { filterModel } from '../../Models/filterModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-filter-bar',
  imports: [FormsModule,CommonModule],
  templateUrl: './filter-bar.component.html',
  styleUrl: './filter-bar.component.css'
})
export class FilterBarComponent {

  states = ["Gujarat","Karnataka","Madhya Pradesh","Maharashtra","Punjab","Rajasthan","Tamil Nadu","Telangana","Uttar Pradesh","Delhi"];
  cities = ["Agra", "Ahmedabad", "Amritsar", "Bangalore", "Bhopal", "Chennai", "Coimbatore", "Gandhinagar", "Hyderabad", "Indore", "Jaipur", "Jodhpur", "Kota", "Lucknow", "Mumbai", "Nagpur", "Nashik", "New Delhi", "Pune", "Surat", "Thane", "Udaipur", "Vadodara"];
  carColours = ["White","Black","Gray","Red","Blue","Silver"];
  seats = ["5","6","7"];
  availability=["Available","Not Available"];
  modelYears: string[] = [];

    SelectedCity:string='';
    SelectedState:string='';
    SelectedColour:string='';
    SelectedYear:string='';
    SelectedSeats:string='';
    SelectedAvailablestatus:string='';
    SelectedMinPrice:string = '';
    SelectedMaxPrice:string='';

router = inject(Router);

  constructor() {
    this.initializeModelYears();
  }

  initializeModelYears() {
    for (let i = 2000; i <= 2024; i++) {
      this.modelYears.push(i.toString());
    }
  }

  applyFilters() {
    const finalSelectValues = {
      // city: this.SelectedCity,
      // state: this.SelectedState,
      colour: this.SelectedColour,
      year: this.SelectedYear ? parseInt(this.SelectedYear) : null,
      seats: this.SelectedSeats ? parseInt(this.SelectedSeats) : null,
      status: this.SelectedAvailablestatus === 'Available',
      // minPrice: this.SelectedMinPrice ? parseFloat(this.SelectedMinPrice) : null,
      // maxPrice: this.SelectedMaxPrice ? parseFloat(this.SelectedMaxPrice) : null,
    };

    this.router.navigate(['\app-filter-page'],{queryParams : finalSelectValues});
    console.log("Selected Filters:", finalSelectValues);
  }
}
