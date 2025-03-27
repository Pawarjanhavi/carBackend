import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-explore-brands',
  imports: [CommonModule],
  templateUrl: './explore-brands.component.html',
  styleUrl: './explore-brands.component.css'
})
export class ExploreBrandsComponent {
  logos = [
    'images/Hyundai-logo.png',
    'images/maruti-logo.png',
    'images/honda-logo2.png',
    'images/ford-logo2.jpg',
    'images/kia-logo2.png',
    'images/renault-logo.png',
    'images/nissan-logo.jpg',
  ];
}
