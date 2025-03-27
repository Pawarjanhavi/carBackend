import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { Routes } from '@angular/router';
import { LoginComponent } from './HomePageComponents/login/login.component';
import { HomeComponent } from './HomePageComponents/home/home.component';
import { RegisterComponent } from './HomePageComponents/register/register.component';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './auth.interceptor';


// const routes : Routes =[
  
// ];

export const appConfig: ApplicationConfig = {
  providers: [provideHttpClient(withInterceptors([authInterceptor])),provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)]
};
