import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { RegisterService } from '../../Services/register.service';
import { registerModel } from '../../Models/registerUser';
import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';

@Component({
  selector: 'app-register',
  standalone:true,
  imports: [FormsModule,CommonModule,RouterLink,RouterLinkActive,NavigationBarComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  registerUserObj:registerModel = new registerModel();

  constructor(private router:Router,private registerS:RegisterService){}

  // console.log(registerModel.firstName);
  
  onRegister(){
    console.log('Registered User:', this.registerUserObj);

    this.registerS.register(this.registerUserObj).subscribe((result:any)=>{
      console.log(result.firstName);
      
      if(result.email!=null){
        alert("Registered Successfully");
        this.router.navigate(['/app-login']);
      }
      else{
        alert("Not Registered");
      }
      
    });
  }

  // user = {
  //   firstName: '',
  //   lastName: '',
  //   email: '',
  //   password:'',
  //   phoneNumber:'',
  //   role:''
  // };

  // selectedRole:string = '';

  onSelectOption(role: any) {
    this.registerUserObj.roleType = [role.target.value];
    console.log(this.registerUserObj.roleType);
  }

  // Method to handle form submission
  // onSubmit() {
  //   console.log('User Details:', this.user);
  //     // Store the user data in localStorage as a JSON string
  //     localStorage.setItem('UserDetails', JSON.stringify(this.user));
      
  //     this.router.navigate(['/app-login']);
  //  }
  
}
