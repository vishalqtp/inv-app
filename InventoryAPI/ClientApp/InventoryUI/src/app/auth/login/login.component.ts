import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  username: string = '';
 password: string = '';
 errorMessage: string = '';


 constructor(
   private authService: AuthService,  // Inject AuthService
   private router: Router,
   private http: HttpClient  // Inject HttpClient
 ) {}


//  login(): void {
//    const loginPayload = { username: this.username, password: this.password };


//    // Call the backend API for authentication
//    this.http.post<any>(`${environment.authUrl}/login`, loginPayload)
//      .subscribe(
//        (response) => {
//          // Save the token received from the backend
//          const token = response.token;  // Assuming the token is in response.token
//          this.authService.saveToken(token);


//          // Redirect to the dashboard after successful login
//          this.router.navigate(['/add-inventory']);
//        },
//        (error) => {
//          // Handle error if login fails
//          this.errorMessage = 'Invalid credentials';
//        }
//      );
//  }

login(): void {
  const loginPayload = { username: this.username, password: this.password };

  // Call the backend API for authentication
  this.http.post<any>(`${environment.authUrl}/login`, loginPayload)
    .subscribe(
      (response) => {
        // Save the token received from the backend
        const token = response.token;  // Assuming the token is in response.token
        this.authService.saveToken(token);

        // Redirect to the dashboard after successful login
        this.router.navigate(['/add-inventory']);
      },
      (error) => {
        // Handle error if login fails
        this.errorMessage = 'Invalid credentials';
      }
    );
}


}
