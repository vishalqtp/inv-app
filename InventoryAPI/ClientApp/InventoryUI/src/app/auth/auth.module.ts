import { NgModule } from "@angular/core";
import { LoginComponent } from "./login/login.component";
import { CommonModule } from "@angular/common";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AuthService } from "./auth.service";
import { AuthInterceptor } from "./auth.interceptor";
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent  // Ensure this component is defined correctly and imported
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  exports: []  // Ensure this is intentional or add components here if needed
})
export class AuthModule {}
