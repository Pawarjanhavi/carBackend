import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';

@Component({
  selector: 'app-contact-us',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, RouterLinkActive,NavigationBarComponent],
  templateUrl: './contactus.component.html',
  styleUrls: ['./contactus.component.css']
})
export class ContactusComponent {
  submitted = false;
  contact = {
    name: '',
    email: '',
    message: ''
  };

  onSubmit() {
    console.log('Contact Details:', this.contact);
    this.submitted = true;
  }
}
