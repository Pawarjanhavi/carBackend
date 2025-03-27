import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigationBarComponent } from './HomePageComponents/navigation-bar/navigation-bar.component';
import { FooterComponent } from './HomePageComponents/footer/footer.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Car2Go_App';
}
