import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface InventoryItem {
  id: number;
  name: string;
  quantity: number;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private apiUrl = 'https://localhost:7135/api/inventory'; // Update with your actual API URL

  constructor(private http: HttpClient) {}

  getInventoryItems(): Observable<InventoryItem[]> {
    return this.http.get<InventoryItem[]>(this.apiUrl);
  }

  // Add a new inventory item
  addInventoryItem(item: InventoryItem): Observable<InventoryItem> {
    return this.http.post<InventoryItem>(this.apiUrl, item);
  }

  // Update an existing inventory item (e.g., after selling or modifying item)
  updateInventoryItem(item: InventoryItem): Observable<InventoryItem> {
    return this.http.put<InventoryItem>(`${this.apiUrl}/${item.id}`, item);  // PUT request to update an item
  }

  // Delete an inventory item by ID
  deleteInventoryItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);  // DELETE request to remove an item
  }

  // Sell an item (adjust its quantity after selling)
  sellItem(id: number, quantity: number): Observable<InventoryItem> {
    // We assume a PATCH request for partial updates (like selling an item)
    return this.http.patch<InventoryItem>(`${this.apiUrl}/sell/${id}`, { quantity });
  }

  purchaseItem(itemId: number, quantity: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/purchase`, { itemId, quantity });
  }
  
}
