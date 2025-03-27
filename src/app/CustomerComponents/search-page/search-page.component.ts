import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { HttpClient } from '@angular/common/http';
import { carModel } from '../../Models/CarWithLocation';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-page',
  imports: [HeaderComponent,CommonModule,FormsModule],
  templateUrl: './search-page.component.html',
  styleUrl: './search-page.component.css'
})
export class SearchPageComponent implements OnInit {

  receivedSearchValue:string='';
  carDetails:carModel[]=[];

  route=inject(ActivatedRoute);
  http = inject(HttpClient);
  router = inject(Router);

  ngOnInit(): void {
    // Retrieve the query parameter
    this.route.queryParams.subscribe((params) => {
      this.receivedSearchValue = params['value'];
      console.log('Received Search Value:', this.receivedSearchValue);

      // Construct the API URL dynamically and fetch data
      const apiUrl = `https://localhost:7273/api/CarSearch/get-cars-by-MakeOrModel?searchValue=${this.receivedSearchValue}`;
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
