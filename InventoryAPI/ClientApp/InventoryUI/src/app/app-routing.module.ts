import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventoryListComponent } from './components/components/inventory-list/inventory-list.component';
import { InventoryDetailsComponent } from './components/components/inventory-details/inventory-details.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './components/components/home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { RegisterComponent } from './auth/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent }, // Home/Login route
  { path: 'login', component: LoginComponent }, // Login route
  { path: 'register', component: RegisterComponent }, // Register route

  // {
  //   path: 'dashboard',
  //   component: DashboardComponent,
  //   canActivate: [AuthGuard] // Protect this route with AuthGuard
  // },
  {
    path: 'inventory',
    component: InventoryListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'inventory/:id',
    component: InventoryDetailsComponent,
    canActivate: [AuthGuard]
  },
  
  {
    path: 'add-inventory',
    component: InventoryListComponent,
    canActivate: [AuthGuard] // Protect this route with AuthGuard
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
