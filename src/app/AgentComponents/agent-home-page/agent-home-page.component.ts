import { Component } from '@angular/core';
import { AgentNavBarComponent } from '../agent-nav-bar/agent-nav-bar.component';
import { RegisterCarComponent } from '../register-car/register-car.component';

@Component({
  selector: 'app-agent-home-page',
  imports: [AgentNavBarComponent,RegisterCarComponent],
  templateUrl: './agent-home-page.component.html',
  styleUrl: './agent-home-page.component.css'
})
export class AgentHomePageComponent {

}
