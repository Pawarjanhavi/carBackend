import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { CarDetailsComponent } from '../car-details/car-details.component';
import { FilterBarComponent } from '../filter-bar/filter-bar.component';
import { ActivatedRoute } from '@angular/router';
import { SharedService } from '../../Services/shared.service';

@Component({
  selector: 'app-customer-home-page',
  imports: [HeaderComponent,CarDetailsComponent,FilterBarComponent],
  templateUrl: './customer-home-page.component.html',
  styleUrl: './customer-home-page.component.css'
})
export class CustomerHomePageComponent {
  
  constructor(private route: ActivatedRoute, private sharedService: SharedService) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      const email = params['email'];
      console.log('Email:', email);
      if (email) {
        this.sharedService.setEmail(email); // Pass the email to the shared service
      }
    });
  }
  
}
