import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { registerModel } from '../Models/registerUser';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  // apiUrl:string = "https://localhost:7273/api/";
  constructor(private http:HttpClient) { }

  register(obj:registerModel){
    // return this.http.post(this.apiUrl + "Auth/register",obj)
    return this.http.post("https://localhost:7273/api/Auth/register",obj)
  }
}
