import { Component } from '@angular/core';
import { AuthService } from './auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'InventoryUI';
  constructor(public authService: AuthService, private router: Router) {}

  logout() {
    this.authService.removeToken(); // Remove token on logout
    this.router.navigate(['/login']); // Redirect to login
  }
}
