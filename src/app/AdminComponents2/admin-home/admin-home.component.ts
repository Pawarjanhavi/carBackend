import { Component, inject, OnInit } from '@angular/core';
import Chart from 'chart.js/auto';
import { HeaderComponentAdmin } from '../header/header.component';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { carModel } from '../../Models/CarWithLocation';
import { Router } from '@angular/router';
import { CarService } from '../../Services/car.service';
import { carModels } from '../../Models/carModels';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-admin-home',
  imports: [HeaderComponentAdmin, NavigationbarComponentAdmin],
  templateUrl: './admin-home.component.html',
  styleUrl: './admin-home.component.css'
})
export class AdminHomeComponent implements OnInit {
  carList:carModel [] = [];

  carService = inject(CarService);
  router = inject(Router);
  http=inject(HttpClient);

  carCount:number = 0;
  usersCount:number = 0;
  agentsCount:number=0;
  locationsCount:number=0;

  ngOnInit(): void {
    this.loadCharts();
    this.carService.getCars().subscribe((result: any) => {
      this.carList = result;
      this.carCount = this.carList.length;
    });
    this.http.get('https://localhost:7273/api/User/get-users-by-role').subscribe((result: any) => {
      console.log(result);
      this.usersCount = result.length;
    });
    this.http.get('https://localhost:7273/api/User/get-agents-by-role').subscribe((result: any) => {
      console.log(result);
      this.agentsCount = result.length;
    });
    this.http.get('https://localhost:7273/api/Location/GetAll').subscribe((result:any)=>{
      // console.log(result);
      this.locationsCount = result.length;
  
     });
  }

  loadCharts(): void {
    const milesCanvas = document.getElementById('milesChart') as HTMLCanvasElement;
    const carCanvas = document.getElementById('carChart') as HTMLCanvasElement;

    if (milesCanvas && milesCanvas.getContext) {
      const milesCtx = milesCanvas.getContext('2d');
      if (milesCtx) {
        new Chart(milesCtx, {
          type: 'bar',
          data: {
            labels: ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
            datasets: [{
              label: 'of Miles',
              data: [12, 19, 3, 5, 2, 3, 10],
              backgroundColor: 'rgba(75, 196, 196, 0.2)',
              borderColor: 'rgba(75, 192, 192, 1)',
              borderWidth: 1
            }]
          },
          options: {
            scales: {
              y: {
                beginAtZero: true
              }
            }
          }
        });
      }
    }

    if (carCanvas && carCanvas.getContext) {
      const carCtx = carCanvas.getContext('2d');
      if (carCtx) {
        new Chart(carCtx, {
          type: 'line',
          data: {
            labels: ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
            datasets: [{
              label: '# of Cars',
              data: [30, 50, 40, 60, 70, 50, 90],
              backgroundColor: 'rgba(255, 99, 132, 0.2)',
              borderColor: 'rgba(255, 99, 132, 1)',
              borderWidth: 1,
              fill: true
            }]
          },
          options: {
            scales: {
              y: {
                beginAtZero: true
              }
            }
          }
        });
      }
    }
  }
}

