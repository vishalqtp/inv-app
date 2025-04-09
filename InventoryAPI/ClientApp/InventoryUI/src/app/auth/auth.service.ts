// auth.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth-token';

  // Save token to local storage
  saveToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  // Get token from local storage
  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  // Remove token from local storage
  removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }


  // Check if the user is logged in
  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }
}
