import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { SharedService } from '../../Services/shared.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-agent-nav-bar',
  imports: [RouterLink,RouterLinkActive,CommonModule],
  templateUrl: './agent-nav-bar.component.html',
  styleUrl: './agent-nav-bar.component.css'
})
export class AgentNavBarComponent {

  authS= inject(AuthService);
  router = inject(Router);
  sharedService = inject(SharedService);

  hoveredItem: string | null = null; // Tracks the item being hovered
  selectedItem: string | null = null; // Tracks the selected (clicked) item

  email: string | null = null;

  searchValue:string = '';

  // constructor(private sharedService: SharedService) {}

  ngOnInit(): void {
    this.sharedService.currentEmail.subscribe((email) => {
      this.email = email;
    });
    
    // Check localStorage directly if email is not available from the service
    if (!this.email) {
      this.email = localStorage.getItem('email');
    }
  }

  // Handle hover state
  onHover(item: string | null): void {
    this.hoveredItem = item;
  }

  // Handle selection (click) state
  onSelect(item: string): void {
    this.selectedItem = item;
    if(this.selectedItem == 'profile'){
      this.router.navigate(['\app-profile-page-agent'],{ queryParams: { userEmail: this.email } });
    }
    else{
      this.router.navigate(['\app-customer-home-page']);
    }
  }

  // onSearch(){
  //   this.router.navigate(['\app-search-page'],{queryParams:{
  //     value: this.searchValue
  //   }});
  //   console.log(this.searchValue);
      
  // }

  onLogout(){
    this.authS.removeToken(); 
  this.sharedService.clearEmail();
  this.router.navigate(['']);
  }

}
