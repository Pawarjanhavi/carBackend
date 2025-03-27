import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
 
@Injectable({
  providedIn: 'root'
})

export class TokenGuard implements CanActivate {
 
  constructor(private router: Router) { }
 
  canActivate(
    //ActivatedRouteSnapshot ==> It represents the state of a route at a particular moment of time. It allows access to route params
    //RouterStateSnapshot ==> Represents the state of the entire router at a specific point in time and it will allow access to the
    //entire route tree
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');
    if (token) {
      return true;
    } else {
      this.router.navigate(['/app-login']);
      return false;
    }
  }
}
 