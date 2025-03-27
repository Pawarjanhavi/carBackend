import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SharedService } from './shared.service';
import { catchError, forkJoin, Observable, of, switchMap, throwError } from 'rxjs';
import { carModel } from '../Models/CarWithLocation';
import { carModels } from '../Models/carModels';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  apiUrl:string ="https://localhost:7273/api/Car/";

  // https://localhost:7273/api/Car/get-all-cars-with-rating
  agentApiUrl: string = "https://localhost:7273/api/Agent/agent-Create-Car";
  apigetcar: string = "https://localhost:7273/api/Agent/agent-get-all-cars";
  apideleteCar: string = "https://localhost:7273/api/Agent/agent-delete-car";
  apiUpdateCar: string = "https://localhost:7273/api/Agent/agent-update-car";

  
  
  AdminAddCarUrl: string = "https://localhost:7273/api/Car/Create-Car";
  AdminDeleteCarUrl: string = "https://localhost:7273/api/Car/delete-car";
  AdminUpdateCarUrl: string = "https://localhost:7273/api/Car/update-car";
  getAllCarsUrl:string = 'https://localhost:7273/api/Car/get-all-cars';


  constructor(private http: HttpClient, private sharedService: SharedService) { }

  getCars(){
    // return this.http.get(this.apiUrl+"get-all-cars");
    return this.http.get(this.apiUrl+"get-all-cars-with-rating");
  }
  
  getAllCars():Observable<any>{
    return this.http.get(this.getAllCarsUrl);
    
  }

  // Create Car (for Admin)
  createCarForAdmin(formData: FormData): Observable<any> {
    return this.http.post<any>(this.AdminAddCarUrl, formData, {
      headers: new HttpHeaders(),
    }).pipe(
      catchError(error => {
        console.error('Error in admin API create car:', error);
        return throwError(error);
      })
    );
  }

  // Delete Car (for Admin)
  deleteCarForAdmin(licensePlate: string): Observable<any> {
    const params = new HttpParams().set('licensePlate', licensePlate);

    return this.http.delete<any>(this.AdminDeleteCarUrl, { params }).pipe(
      catchError(error => {
        console.error('Error in admin API delete car:', error);
        return throwError(error);
      })
    );
  }

  updateCarForAdmin(updatedCar: carModels): Observable<any> {
    const params = new HttpParams().set('licensePlate', updatedCar.licensePlate);

    // First, update Admin system
    return this.http.put<any>(this.AdminUpdateCarUrl, updatedCar, { params }).pipe(
      switchMap(() => {
        // After Admin Update, try to update Agent system
        return this.http.put<any>(this.apiUpdateCar, updatedCar, { params }).pipe(
          catchError(agentError => {
            // If the agent update fails (e.g., car doesn't exist), just log the error
            console.error('Car does not exist in agent system. Admin system updated only.');
            return of('Car updated in Admin system only.');  // Return a message stating only Admin was updated
          })
        );
      }),
      catchError(error => {
        console.error('Error in updating car:', error);
        return throwError(error);  // Propagate the error to the caller
      })
    );
  }
  
  

  // Method to get all cars of Agent
  GetAgentCars(): Observable<any> {
    return this.http.get(this.apigetcar);
    
  }

  // Method to create a car (for Agent)
  createCar(formData: FormData): Observable<any> {
    return this.sharedService.currentEmail.pipe(
      switchMap((email) => {
        if (!email) {
          console.error('Email is not found');
          return throwError(() => new Error('Email is not found'));
        }

        const params = new HttpParams().set('email', email);

        // Call to agent API
        const agentRequest = this.http.post<any>(this.agentApiUrl, formData, {
          headers: new HttpHeaders(),
          params: params,
        }).pipe(
          catchError(error => {
            console.error('Error in agent API call:', error);
            return throwError(error);
          })
        );

        // Call to admin API
        const adminRequest = this.http.post<any>(this.AdminAddCarUrl, formData, {
          headers: new HttpHeaders(),
        }).pipe(
          catchError(error => {
            console.error('Error in admin API call:', error);
            return throwError(error);
          })
        );

        // Combine both API calls
        return forkJoin([agentRequest, adminRequest]);
      })
    );
  }

  // Update Car (for Agent)
 
  
  updateCar(updatedCar: carModels): Observable<any> {
    const params = new HttpParams().set('licensePlate', updatedCar.licensePlate);
  
    // Admin Update
    return this.http.put<any>(this.AdminUpdateCarUrl, updatedCar, { params }).pipe(
      switchMap(() => {
        // After Admin Update, Update Agent
        return this.http.put<any>(this.apiUpdateCar, updatedCar, { params });
      }),
      catchError(error => {
        console.error('Error in updating car:', error);
        return throwError(error);  // Propagate the error to the caller
      })
    );
  }


  // Delete Car (for Agent)
  DeleteCar(licensePlate: string): Observable<any> {
    const params = new HttpParams().set('licensePlate', licensePlate);

    // Call to agent API for deleting the car
    const agentDeleteRequest = this.http.delete<any>(this.apideleteCar, { params }).pipe(
      catchError(error => {
        console.error('Error in agent API delete call:', error);
        return throwError(error);
      })
    );

    // Call to admin API for deleting the car
    const adminDeleteRequest = this.http.delete<any>(this.AdminDeleteCarUrl, { params }).pipe(
      catchError(error => {
        console.error('Error in admin API delete call:', error);
        return throwError(error);
      })
    );

    // Combine both API calls
    return forkJoin([agentDeleteRequest, adminDeleteRequest]);
  }

}
