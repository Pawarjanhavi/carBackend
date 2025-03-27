import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HeaderComponentAdmin } from '../header/header.component';
import { LocationModel } from '../../Models/LocationModel';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';

@Component({
  selector: 'app-location',
  imports: [CommonModule,FormsModule,HeaderComponentAdmin,NavigationbarComponentAdmin],
  templateUrl: './location.component.html',
  styleUrl: './location.component.css'
})
export class LocationComponent  implements OnInit{
 http = inject(HttpClient);

  receivedLocations:LocationModel[]=[];

  locationObj:LocationModel = new LocationModel();

  passAddressValue:string='';

 ngOnInit(): void {
  
   this.http.get('https://localhost:7273/api/Location/GetAll').subscribe((result:any)=>{
    // console.log(result);
    this.receivedLocations = result;

   });
  //  this.receivedLocations.forEach(element => {
  //   console.log(element);
    
  //  });
 }

 onEdit(data:LocationModel){
  this.locationObj = data;
  this.passAddressValue = data.address;
  //  console.log("Update SuccessFully");

 }
 onSaveLocation(){
  const payload = {
    address: this.locationObj.address,
    city: this.locationObj.city,
    state: this.locationObj.state, 
    country: this.locationObj.country,
    zipCode: this.locationObj.zipCode
  };

  this.http.post(
    'https://localhost:7273/api/Location/Create',
    payload,
    { headers: { 'Content-Type': 'application/json' } }
  ).subscribe({
    next :(result: any) => {
      console.log('Location created successfully:', result);
      if(result.address!=null){
        alert("Location Added SuccessFully");
        this.http.get('https://localhost:7273/api/Location/GetAll').subscribe((result:any)=>{
          console.log(result);
          this.receivedLocations = result;     
         });
      }
      else{
        alert("Location Not added");
      }
    },
    error:(error) => {
      alert("Location Not added");
      console.error('Error creating location:', error);
    }
});
  
 }
 onUpdateLocation(){
  const payload = {
    address: this.locationObj.address,
    city: this.locationObj.city,
    state: this.locationObj.state, 
    country: this.locationObj.country,
    zipCode: this.locationObj.zipCode
  };
    console.log('Payload for update:', payload);
  
    this.http.put(
      `https://localhost:7273/api/Location/Update?address=${this.passAddressValue}`,
      payload,
      { headers: { 'Content-Type': 'application/json' } }
    ).subscribe({
      next: (result: any) => {
        console.log('Location updated successfully:', result);
        if (result.result) {
          alert('Location updated successfully');
          this.http.get('https://localhost:7273/api/Location/GetAll').subscribe((result:any)=>{
            console.log(result);
            this.receivedLocations = result;     
           });
        } else {
          alert('Location update failed');
        }
      },
      error: (error) => {
        console.error('Error updating location:', error);
      }
    });
 }

 onDelete(address:string){
  this.http.delete(
    `https://localhost:7273/api/Location/delete?address=${address}`).subscribe({
    next: (result: any) => {
      console.log('Location deleted successfully:', result);
      if (result.result) {
        alert('Location deleted successfully');
        this.http.get('https://localhost:7273/api/Location/GetAll').subscribe((result:any)=>{
          console.log(result);
          this.receivedLocations = result;     
         });
      } else {
        alert('Location delete failed');
      }
    },
    error: (error) => {
      console.error('Error in deleting Location:', error);
    }
  });
  console.log("Delete SuccessFully");
  
 }

}//end class
