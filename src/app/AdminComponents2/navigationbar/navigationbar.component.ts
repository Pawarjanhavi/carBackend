import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-navigationbar-admin',
  imports:[RouterLink,RouterLinkActive],
  templateUrl: './navigationbar.component.html',
  styleUrls: ['./navigationbar.component.css']
})
export class NavigationbarComponentAdmin implements OnInit {
  isOpen = false;

  constructor(private router: Router) {
    // Detect route changes
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.checkIfHomePage(event.urlAfterRedirects);
      }
    });
  }

  ngOnInit() {
    this.checkIfHomePage(this.router.url);
  }

  private checkIfHomePage(url: string) {
    this.isOpen = url === '/' || url.startsWith('/home');
  }
}
