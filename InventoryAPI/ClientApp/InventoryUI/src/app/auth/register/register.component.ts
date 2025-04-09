import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { RegisterPayload, RegisterResponse } from 'src/app/models/register.model';  
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  registerForm: FormGroup; // Create a FormGroup for form control
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private router: Router,
    private http: HttpClient  // Inject HttpClient
  ) {
    // Initialize the form with FormControl and validators
    this.registerForm = new FormGroup({
      username: new FormControl('', [
        Validators.required,  // Username is required
        Validators.minLength(3),  // Minimum length of 3 characters
        Validators.maxLength(20)  // Maximum length of 20 characters
      ]),
      password: new FormControl('', [
        Validators.required,  // Password is required
        Validators.minLength(6)  // Minimum length of 6 characters
      ])
    });
  }

  // Register function to submit the form
  // register(): void {
  //   if (this.registerForm.invalid) {
  //     return;  // Don't proceed if form is invalid
  //   }

  //   const registerPayload = {
  //     username: this.registerForm.value.username,
  //     password: this.registerForm.value.password
  //   };

  //   this.http.post(`${environment.authUrl}/register`, registerPayload)
  //     .subscribe(
  //       (response: any) => {
  //         // On successful registration, show success message
  //         this.successMessage = 'Registration successful!';
  //         this.errorMessage = '';  // Clear any previous errors

  //         // Redirect to login page after successful registration
  //         setTimeout(() => {
  //           this.router.navigate(['/login']);
  //         }, 2000);
  //       },
  //       (error) => {
  //         // Handle error if registration fails
  //         this.successMessage = '';  // Clear any previous success messages
  //         this.errorMessage = 'Registration failed. Please try again.';
  //       }
  //     );
  // }

  register(): void {
    if (this.registerForm.invalid) {
      return;
    }
  
    const registerPayload = {
      username: this.registerForm.value.username,
      password: this.registerForm.value.password
    };
  
    this.http.post(`${environment.authUrl}/register`, registerPayload)
      .subscribe(
        (response: any) => {
          this.successMessage = 'Registration successful!';
          this.errorMessage = '';
  
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        (error) => {
          this.successMessage = '';
  
          // 👇 Extract backend error message if available
          if (error.error && typeof error.error === 'string') {
            this.errorMessage = error.error;  // e.g. "User already exists"
          } else {
            this.errorMessage = 'Registration failed. Please try again.';
          }
        }
      );
  }
  

  // Getter for username control
  get username() {
    return this.registerForm.get('username');
  }

  // Getter for password control
  get password() {
    return this.registerForm.get('password');
  }
}
