import { Component, inject, OnInit } from '@angular/core';
import { SharedService } from '../../Services/shared.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { AgentNavBarComponent } from '../agent-nav-bar/agent-nav-bar.component';
AgentNavBarComponent

@Component({
  selector: 'app-profile-page-agent',
  standalone: true,
  imports: [AgentNavBarComponent, CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
})
export class ProfilePageComponentAgent implements OnInit {
  http = inject(HttpClient);
  sharedS = inject(SharedService);
  router = inject(Router);
  authS = inject(AuthService);

  isEditing: boolean = false; // State to toggle between view and edit modes

  user = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
  };

  passEmail:string ='';

  resetPassForm:FormGroup = new FormGroup({
    oldPassword:new FormControl(""),
    newPassword:new FormControl(""),
  });
  

  ngOnInit(): void {
    // Subscribe to email from SharedService
    this.sharedS.currentEmail.subscribe((email) => {
      if (email) {
        this.user.email = email;
        this.passEmail = email;
        this.fetchUserData(); // Fetch user data whenever the email is available
      } else {
        console.warn('No email found in SharedService.');
      }
    });
    
  }

  // Fetch user data from the server
  fetchUserData() {
    this.http
      .get(`https://localhost:7273/api/User/get-user?email=${this.user.email}`)
      .subscribe({
        next: (result: any) => {
          this.user.firstName = result.firstName;
          this.user.lastName = result.lastName;
          this.user.phoneNumber = result.phoneNumber;
        },
        error: (err) => {
          console.error('Error fetching user details:', err);
        },
      });
  }

  // Toggle to edit user information
  editUser() {
    this.isEditing = true;
  }

  // Save the updated user information
  saveUser() {
    this.http
      .put(`https://localhost:7273/api/User/update-user?email=${this.passEmail}`, this.user)
      .subscribe({
        next: (result) => {
          console.log('User information updated:', result);
          this.isEditing = false; // Toggle back to view mode
          this.sharedS.setEmail(this.user.email); // Update SharedService with the new email
        },
        error: (err) => {
          console.error('Error saving user details:', err);
          console.log('Error response body:', err.error); // Log server response
        },
      });
  }

  // Cancel edit and revert changes
  cancelEdit() {
    this.isEditing = false; // Just toggle back to the view mode without saving
  }

  // Delete user functionality
  deleteUser() {
    let userResponse = confirm("Do you want to delete account?");
    if(userResponse){
      this.http.delete(`https://localhost:7273/api/User/delete-user-account?email=${this.passEmail}`).subscribe({
        next: (result) => {
          console.log('User Account deleted:', result);
          this.isEditing = false; // Toggle back to view mode
          this.sharedS.clearEmail();
          this.authS.removeToken();
          this.router.navigate(['']);
        },
        error: (err) => {
          console.error('Error deleting user details:', err);
          console.log('Error response body:', err.error); // Log server response
        },
      });
    }
    else{
      this.router.navigate(['\app-profile-page']);
    }
  }

  //Reset modal popup

  onResetModal() {
    // Ensure Bootstrap modal functionality is used for fade and backdrop
    const modalDiv = document.getElementById("resetPassModal");
    if (modalDiv) {
      modalDiv.classList.add("show");
      modalDiv.style.display = "block";
      document.body.classList.add("modal-open");
  
      // Add backdrop
      const backdrop = document.createElement("div");
      backdrop.className = "modal-backdrop fade show";
      backdrop.id = "custom-backdrop";
      document.body.appendChild(backdrop);
    }
    console.log(this.user.email);
    // console.log(this.passEmail);  
  }

  //Update new password 
  updatePassword(){
    // console.log("update",this.oldPass,this.newPass);
    // const data ={
    //   oldPassword:any = this.oldPass;
    // }
    console.log(this.resetPassForm.value);

    const formValue = this.resetPassForm.value;

    this.http.put(`https://localhost:7273/api/User/reset-password?email=${this.user.email}`,formValue).subscribe({
      next:(response:any)=>{
        if(response.result){
          alert("Password Reset Successfully");
          this.closeResetModal();
          this.router.navigate(['\app-login']);
        }
        else{
          alert("Please enter correct old password");
        }
      },
      error:(err)=>{
        alert("Please enter correct old password");
        // console.log(err);
      }
    });
    
  }
  
  //close reset password modal popup
  closeResetModal() {
    const modalDiv = document.getElementById("resetPassModal");
    if (modalDiv) {
      modalDiv.classList.remove("show");
      modalDiv.style.display = "none";
      document.body.classList.remove("modal-open");
  
      // Remove backdrop
      const backdrop = document.getElementById("custom-backdrop");
      if (backdrop) {
        document.body.removeChild(backdrop);
      }
    }
  }
}//end class
