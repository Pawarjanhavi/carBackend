import { Component } from '@angular/core';
import { CarRentalComponent } from '../car-rental/car-rental.component';
import { ExploreBrandsComponent } from '../explore-brands/explore-brands.component';
import { FooterComponent } from '../footer/footer.component';
import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';
import { WhyChooseUSComponent } from '../why-choose-us/why-choose-us.component';
import { ReviewsComponent } from '../reviews/reviews.component';

@Component({
  selector: '',
  standalone:true,
  imports: [NavigationBarComponent,CarRentalComponent,ExploreBrandsComponent,WhyChooseUSComponent,ReviewsComponent,FooterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
// app-home