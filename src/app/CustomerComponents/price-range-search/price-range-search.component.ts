import { Component, inject } from '@angular/core';
import { carModel } from '../../Models/CarWithLocation';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HeaderComponent } from '../header/header.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-price-range-search',
  imports: [HeaderComponent,CommonModule],
  templateUrl: './price-range-search.component.html',
  styleUrl: './price-range-search.component.css'
})
export class PriceRangeSearchComponent {

 receivedSearchValue:any;
 carDetails:carModel[]=[];

  route=inject(ActivatedRoute);
  http = inject(HttpClient);
  router = inject(Router);

  ngOnInit(): void {
   // Get query parameters
   this.route.queryParams.subscribe(params => {
    this.receivedSearchValue = params;
    console.log("Received Filters:", this.receivedSearchValue,this.receivedSearchValue.maxPrice);

      // Construct the API URL dynamically and fetch data
      const apiUrl = `https://localhost:7273/api/CarSearch/get-cars-by-price-range?maxPrice=${this.receivedSearchValue.maxPrice}&minPrice=${this.receivedSearchValue.minPrice}`;
      console.log('Constructed API URL:', apiUrl);

      // Make the API call
      this.http.get<carModel[]>(apiUrl).subscribe({
        next: (result) => {
          this.carDetails = result;
          // console.log('Fetched Car Details:', this.carDetails);
        },
        error: (err) => {
          alert("Car not found");
          this.router.navigate(['\app-customer-home-page'])
          // console.error('Error fetching car details:', err);
        },
      });
    });
  }

  onBooKNow(selectedCar:carModel){
    this.router.navigate(['/app-reservation-page'],
      {state: { data:selectedCar}}
    );
  }
}
