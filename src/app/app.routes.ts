import { Routes } from '@angular/router';
import { LoginComponent } from './HomePageComponents/login/login.component';
import { RegisterComponent } from './HomePageComponents/register/register.component';
import { HomeComponent } from './HomePageComponents/home/home.component';
import { PrivacyPolicyComponent } from './HomePageComponents/privacy-policy/privacy-policy.component';
import { TearmsandConditionsComponent } from './HomePageComponents/terms-and-conditions/terms-and-conditions.component';
import { CarDetailsComponent } from './CustomerComponents/car-details/car-details.component';
import { ContactusComponent } from './HomePageComponents/contactus/contactus.component';
import { AboutUsComponent } from './HomePageComponents/about-us/about-us.component';
import { ReservationPageComponent } from './CustomerComponents/reservation-page/reservation-page.component';
import { CustomerHomePageComponent } from './CustomerComponents/customer-home-page/customer-home-page.component';
import { ProfilePageComponent } from './CustomerComponents/profile-page/profile-page.component';
import { BookingsPageComponent } from './CustomerComponents/bookings-page/bookings-page.component';
import { SearchPageComponent } from './CustomerComponents/search-page/search-page.component';
import { FilterPageComponent } from './CustomerComponents/filter-page/filter-page.component';
import { TokenGuard } from './Guards/token.guard';
import { AdminHomeComponent } from './AdminComponents2/admin-home/admin-home.component';
import { ProfilePageComponentAdmin } from './AdminComponents2/profile-page/profile-page.component';
import { CarsComponent } from './AdminComponents2/cars/cars.component';
import { LocationComponent } from './AdminComponents2/location/location.component';
import { BookingsComponent } from './AdminComponents2/bookings/bookings.component';
import { ReviewsComponent } from './AdminComponents2/reviews/reviews.component';
import { PriceRangeSearchComponent } from './CustomerComponents/price-range-search/price-range-search.component';
import { AgentNavBarComponent } from './AgentComponents/agent-nav-bar/agent-nav-bar.component';
import { AgentHomePageComponent } from './AgentComponents/agent-home-page/agent-home-page.component';
import { YourCarsComponent } from './AgentComponents/your-cars/your-cars.component';
import { YourBookingsComponent } from './AgentComponents/your-bookings/your-bookings.component';
import { AgentComponent } from './AgentComponents/agent/agent.component';
import { UpdateCarDetailsComponent } from './AgentComponents/update-car-details/update-car-details.component';
import { ProfilePageComponentAgent } from './AgentComponents/profile-page/profile-page.component';
import { AgentsComponent } from './AdminComponents2/agents/agents.component';
import { UserComponent } from './AdminComponents2/user/user.component';
import { AdminAddCarsComponent } from './AdminComponents2/admin-add-cars/admin-add-cars.component';
import { AddNewAgentComponent } from './AdminComponents2/add-new-agent/add-new-agent.component';
import { AdminUpdateCarComponent } from './AdminComponents2/admin-update-car/admin-update-car.component';

export const routes: Routes = [
      {
        path:'',
        component:HomeComponent
      },
      {
        path:'app-login',
        component:LoginComponent
      },
      {
        path:'app-register',
        component:RegisterComponent
      },
      {
        path:'app-home',
        component:HomeComponent
      },
      {
        path:'app-privacy-policy',
        component:PrivacyPolicyComponent
      },
      {
        path:'app-terms-and-conditions',
        component:TearmsandConditionsComponent
      },
      {
        path:'app-car-details',
        component:CarDetailsComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-agent-nav-bar',
        component:AgentNavBarComponent
      },
      {
        path:'app-contact-us',
        component:ContactusComponent
      },
      {
        path:'app-about-us',
        component:AboutUsComponent
      },
      {
        path:'app-agent-home-page',
        component:AgentHomePageComponent
      },
      {
        path:'app-reservation-page',
        component:ReservationPageComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-customer-home-page',
        component:CustomerHomePageComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-profile-page',
        component:ProfilePageComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-bookings-page',
        component:BookingsPageComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-search-page',
        component:SearchPageComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-price-range-search',
        component:PriceRangeSearchComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-filter-page',
        component:FilterPageComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-location',
        component:LocationComponent
      },
      {
        path:'app-admin-home',
        component:AdminHomeComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-profile-page-admin',
        component:ProfilePageComponentAdmin,
        canActivate:[TokenGuard]
      },
      {
        path:'app-cars',
        component:CarsComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-bookings',
        component:BookingsComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-reviews',
        component:ReviewsComponent,
        canActivate:[TokenGuard]
      },
      {
        path:'app-your-cars',
        component:YourCarsComponent
      },
      {
        path:'app-your-bookings',
        component:YourBookingsComponent
      },
      {
        path:'app-agent',
        component:AgentComponent

      },
      {
        path:'app-update-car-details',
        component:UpdateCarDetailsComponent
      },
      {
        path:'app-profile-page-agent',
        component:ProfilePageComponentAgent,
        // canActivate:[TokenGuard]
      },
      {
        path:'app-agents',
        component:AgentsComponent
      },
      {
        path:'app-user',
        component:UserComponent
      },
      {
        path:'app-admin-add-cars',
        component:AdminAddCarsComponent
      },
      {
        path:'app-add-new-agent',
        component:AddNewAgentComponent
      },
      {
        path:'app-admin-update-car',
        component:AdminUpdateCarComponent
      }

];
