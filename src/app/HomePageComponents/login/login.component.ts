import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';
import { SharedService } from '../../Services/shared.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,NavigationBarComponent,RouterLinkActive,RouterLink],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'], // Fixed typo in styleUrls
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private authS: AuthService,
    private router: Router,
    private sharedService: SharedService
  ) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]), // Email validator
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6), // Minimum length 6
        Validators.maxLength(20), // Maximum length 20
      ]),
    });
  }

  onSubmit(): void {
    const email = this.loginForm?.get('email')?.value;
    const password = this.loginForm?.get('password')?.value;

    if (email && password) {
      this.authS.login(email, password).subscribe({
        next: (response: any) => {
          if (response?.token) {
            const token = response.token;
            const role = response.role;

            // Save token and email
            this.authS.setToken(token);
            this.sharedService.setEmail(email); // Update shared service with email

            // Decode and print the token
            this.decodeToken();
            console.log('Token Expiry:', this.authS.getTokenExpiry());
            console.log('Role:', role);

            // Navigate based on role
            switch (role[0]) {
              case 'Admin':
                this.router.navigate(['/app-admin-home'], { queryParams: { email } });
                break;
              case 'User':
                this.router.navigate(['/app-customer-home-page'], { queryParams: { email } });
                break;
              case 'Agent':
                this.router.navigate(['/app-agent-home-page'], { queryParams: { email } });
                break;
              default:
                this.router.navigate(['/']);
                break;
            }
          } else {
            console.error('Invalid response');
            alert('Invalid Credentials');
          }
        },
        error: (error: any) => {
          console.error('Error:', error);
          alert('Login failed. Please try again.');
        },
      });
    } else {
      alert('Please fill out all fields correctly.');
    }
  }

  decodeToken(): void {
    const decodedToken = this.authS.decodeToken();
    if (decodedToken) {
      console.log('Decoded Token:', decodedToken);
    } else {
      console.log('No token found or unable to decode.');
    }
  }
}



// import { CommonModule } from '@angular/common';
// import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
// import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
// import { Router, RouterLink, RouterLinkActive } from '@angular/router';
// import { AuthService } from '../../Services/auth.service';
// import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';
// import { SharedService } from '../../Services/shared.service';

// @Component({
//   selector: 'app-login',
//   standalone:true,
//   imports: [ReactiveFormsModule,CommonModule,RouterLink,RouterLinkActive,NavigationBarComponent],
//   templateUrl: './login.component.html',
//   styleUrl: './login.component.css',
// })

// export class LoginComponent implements OnInit{

//   loginForm!:FormGroup;

//   constructor(private authS:AuthService, private router: Router,private sharedService:SharedService){}
  
//   ngOnInit(): void {
//      this.loginForm = new FormGroup({
//       email:new FormControl('',Validators.required),
//       password:new FormControl('',Validators.required)
//      });
//    }

//    onSubmit(): void {
//     const email = this.loginForm?.get('email')?.value;
//     const password = this.loginForm?.get('password')?.value;
  
//     if (email && password) {
//       this.authS.login(email, password).subscribe({
//         next: (response: any) => {
//           if (response && response.token) {
//             const token = response.token;
//             const role: any = response.role;
  
//             // Save token and email
//             this.authS.setToken(token);
//             this.sharedService.setEmail(email); // Update shared service with email
  
//             // Decode and print the token
//             this.decodeToken();
//             console.log(this.authS.getTokenExpiry());
//             console.log(role);
  
//             // Navigate based on role
//             if (role[0] === 'Admin') {
//               this.router.navigate(['/app-admin-nav-bar'], { queryParams: { email } });
//             } else if (role[0] === 'User') {
//               this.router.navigate(['/app-customer-home-page'], { queryParams: { email } });
//             } else if (role[0] === 'Agent') {
//               this.router.navigate(['/app-agent-home-page'], { queryParams: { email } });
//             } else {
//               this.router.navigate(['/']);
//             }
//           } else {
//             console.error('Invalid response');
//             alert("Invalid Credentials");
//           }
//         },
//         error: (error: any) => {
//           console.error(error);
//         },
//         complete: () => {
//           console.log('Request completed');
//         }
//       });
//     } else {
//       alert('Invalid Form Values...');
//     }
//   }
  
//   decodeToken() {
//     const decodedToken = this.authS.decodeToken();
//     if (decodedToken) {
//       console.log('Decoded Token:', decodedToken);
//       // Now you can use the information in the token
//     } else {
//       console.log('No token found or unable to decode');
//     }
//   }

// }//End Class
