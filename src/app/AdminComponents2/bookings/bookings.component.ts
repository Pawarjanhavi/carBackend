import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponentAdmin } from '../header/header.component';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { HttpClient } from '@angular/common/http';
import { adminReservationDetailsModel } from '../../Models/adminReservationDetailsModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bookings',
  imports: [HeaderComponentAdmin,NavigationbarComponentAdmin,CommonModule],
  templateUrl: './bookings.component.html',
  styleUrl: './bookings.component.css'
})
export class BookingsComponent implements OnInit {

  http = inject(HttpClient);

  allReservations:adminReservationDetailsModel[]=[];

  ngOnInit(): void {
    this.http.get('https://localhost:7273/api/Reservation/get-all-reservation-details').subscribe((result:any)=>{
      this.allReservations = result;
      // console.log(result);
      
      // this.allReservations.forEach(element => {
      //   console.log(element);
        
      // });
    });
  }
}
