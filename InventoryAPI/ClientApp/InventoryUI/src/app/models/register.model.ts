// src/app/models/register.model.ts
export interface RegisterPayload {
    username: string;
    password: string;
  }
  
  export interface RegisterResponse {
    message: string;
    // Add any other properties you expect in the response
  }
  