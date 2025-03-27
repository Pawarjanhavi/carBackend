import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { carModel } from '../../Models/CarWithLocation';
import { FilterBarComponent } from '../filter-bar/filter-bar.component';
import { HeaderComponent } from '../header/header.component';
import { filterModel } from '../../Models/filterModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-filter-page',
  imports: [HeaderComponent,FilterBarComponent,CommonModule],
  templateUrl: './filter-page.component.html',
  styleUrl: './filter-page.component.css'
})
export class FilterPageComponent implements OnInit {

  apiUrl = 'https://localhost:7273/api/CarSearch/get-cars-by-filters?';

  route = inject(ActivatedRoute);
  http = inject(HttpClient);
  router = inject(Router);

  filters: any;

  filterCarDetails: carModel[] = [];

  ngOnInit(): void {
    // Get query parameters
    this.route.queryParams.subscribe(params => {
      this.filters = params;
      console.log("Received Filters:", this.filters);
    });

    // Construct the API URL with proper interpolation
    const apiUrlWithParams = `${this.apiUrl}Colour=${encodeURIComponent(this.filters.colour || '')}&year=${encodeURIComponent(this.filters.year || '')}&TotalSeats=${encodeURIComponent(this.filters.seats || '')}&AvailableStatus=${encodeURIComponent(this.filters.status || '')}`;


    // const apiUrlWithParams = `${this.apiUrl}City=${encodeURIComponent(this.filters.city || '')}&State=${encodeURIComponent(this.filters.state || '')}&Colour=${encodeURIComponent(this.filters.colour || '')}&year=${encodeURIComponent(this.filters.year || '')}&TotalSeats=${encodeURIComponent(this.filters.seats || '')}&AvailableStatus=${encodeURIComponent(this.filters.status || '')}&minPrice=${encodeURIComponent(this.filters.minPrice || '')}&maxPrice=${encodeURIComponent(this.filters.maxPrice || '')}`;

    console.log('Constructed API URL:', apiUrlWithParams);

    // Send GET request
    this.http.get(apiUrlWithParams).subscribe({
      next: (result: any) => {
        // console.log("API Response:", result);
        this.filterCarDetails = result;
      },
      error: (error: any) => {
        console.error("API Error:", error);
        alert("Result not Found");
        this.router.navigate(['/app-customer-home-page']);
      }
    });
  }

  onBooKNow(selectedCar:carModel){
    this.router.navigate(['/app-reservation-page'],
      {state: { data:selectedCar}}
    );
  }
}
