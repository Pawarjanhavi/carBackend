import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from '../../Services/register.service';
import { registerModel } from '../../Models/registerUser';
import { CommonModule } from '@angular/common';
import { NavigationbarComponentAdmin } from '../navigationbar/navigationbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponentAdmin } from "../header/header.component";


@Component({
  selector: 'app-add-new-agent',
  imports: [CommonModule, NavigationbarComponentAdmin, ReactiveFormsModule, FormsModule, HeaderComponentAdmin],
  templateUrl: './add-new-agent.component.html',
  styleUrl: './add-new-agent.component.css'
})
export class AddNewAgentComponent {
registerUserObj:registerModel = new registerModel();

  constructor(private router:Router,private registerS:RegisterService){}

  // console.log(registerModel.firstName);
  
  onRegister(){
    console.log('Registered User:', this.registerUserObj);

    this.registerS.register(this.registerUserObj).subscribe((result:any)=>{
      console.log(result.firstName);
      
      if(result.email!=null){
        alert("Registered Successfully");
        this.router.navigate(['/app-agents']);
      }
      else{
        alert("Not Registered");
      }
      
    });
  }

  
  onSelectOption(role: any) {
    this.registerUserObj.roleType = [role.target.value];
    console.log(this.registerUserObj.roleType);
  }
}