import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InventoryListComponent } from './components/components/inventory-list/inventory-list.component';
import { InventoryDetailsComponent } from './components/components/inventory-details/inventory-details.component';
import { AddInventoryItemComponent } from './components/components/add-inventory-item/add-inventory-item.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule } from './auth/auth.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HomeComponent } from './components/components/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    InventoryListComponent,
    InventoryDetailsComponent,
    AddInventoryItemComponent,
    DashboardComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AuthModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
