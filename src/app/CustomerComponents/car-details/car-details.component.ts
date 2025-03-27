import { Component, inject, OnInit } from '@angular/core';
import { CarService } from '../../Services/car.service';
import { carModel } from '../../Models/CarWithLocation';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from '../header/header.component';
import { FilterBarComponent } from '../filter-bar/filter-bar.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-details',
  imports: [CommonModule,FormsModule,HeaderComponent,FilterBarComponent],
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.css'
})

export class CarDetailsComponent implements OnInit {

  carService = inject(CarService);
  router = inject(Router);

  SelectedMinPrice:string = '';
  SelectedMaxPrice:string='';

  carList:carModel [] = [];

  ngOnInit(): void {
    this.getCarDetails();
  }

  getCarDetails() {
    this.carService.getCars().subscribe((result: any) => {
      this.carList = result;

      // Iterate over carList in console to verify the car details
      // this.carList.forEach((car: any) => {
      //   console.log(car);
      // });
    });
  }
  onBooKNow(selectedCar:carModel){
    this.router.navigate(['/app-reservation-page'],
      {state: { data:selectedCar}}
    );
  }

  onPriceSearch(){
    const finalValues = {
      minPrice: this.SelectedMinPrice ? parseFloat(this.SelectedMinPrice) : null,
      maxPrice: this.SelectedMaxPrice ? parseFloat(this.SelectedMaxPrice) : null,
    };

    this.router.navigate(['\app-price-range-search'],{queryParams : finalValues});
    console.log("Selected Filters:", finalValues);
  }
  
}
