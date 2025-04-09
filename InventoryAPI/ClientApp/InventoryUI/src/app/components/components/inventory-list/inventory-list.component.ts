import { Component, OnInit } from '@angular/core';
import { InventoryItem, InventoryService } from 'src/app/services/services/inventory.service';

@Component({
  selector: 'app-inventory-list',
  templateUrl: './inventory-list.component.html',
  styleUrls: ['./inventory-list.component.scss']
})
export class InventoryListComponent implements OnInit {
  inventoryItems: any[] = [];
  isModalOpen = false; // To toggle the modal
  newItem: InventoryItem = { id: 0, name: '', quantity: 0, price: 0 };

  constructor(private inventoryService: InventoryService) {}

  ngOnInit(): void {
    this.loadInventoryItems();
  }

  loadInventoryItems(): void {
    this.inventoryService.getInventoryItems().subscribe(items => {
      // Initialize 'isEditing' for each item to prevent errors
      this.inventoryItems = items.map(item => ({
        ...item,
        isEditing: false  // Ensure each item has the 'isEditing' property
      }));
    });
  }

  // Sell item
  sellItem(item: any): void {
    const quantityToSell = prompt('Enter quantity to sell:', '1');
    const quantity = parseInt(quantityToSell ?? '0', 10);
    
    if (quantity && quantity <= item.quantity) {
      item.quantity -= quantity;
      this.inventoryService.updateInventoryItem(item).subscribe(() => {
        this.loadInventoryItems(); // Reload inventory after update
      });
    } else {
      alert('Not enough stock to sell');
    }
  }

  // Purchase item
  purchaseItem(item: any): void {
    const quantityToPurchase = prompt('Enter quantity to purchase:', '1');
    const quantity = parseInt(quantityToPurchase ?? '0', 10);
  
    if (quantity && quantity > 0) {
      item.quantity += quantity;
      this.inventoryService.updateInventoryItem(item).subscribe(() => {
        this.loadInventoryItems(); // Reload inventory after update
      });
    } else {
      alert('Please enter a valid quantity greater than zero.');
    }
  }

  // Edit item in the same row
  editItem(item: any): void {
    item.isEditing = true; // Set the editing flag to true
  }

  saveItem(item: InventoryItem, index: number): void {
    if (item.name && item.quantity > 0 && item.price >= 0) {
      this.inventoryService.updateInventoryItem(item).subscribe({
        next: () => {
          this.loadInventoryItems(); // Fetch latest data after updating
        },
        error: (err) => {
          console.error('Error updating item:', err);
        }
      });
    }
  }
  
  
  
  
  
  

  // Cancel editing and revert to the original item
  cancelEdit(item: any): void {
    item.isEditing = false; // Exit editing mode
    this.loadInventoryItems(); // Reload the inventory to get the original data
  }

  // deleteItem(item: any): void {
  //   if (confirm(`Are you sure you want to delete ${item.name}?`)) {
  //     this.inventoryService.deleteInventoryItem(item.id).subscribe(() => {
  //       this.inventoryItems = this.inventoryItems.filter(i => i.id !== item.id);
  //     });
  //   }
  // }

  deleteItem(itemId: number): void {
    const isConfirmed = confirm('Are you sure you want to delete this item?');
  
    if (isConfirmed) {
      this.inventoryService.deleteInventoryItem(itemId).subscribe(() => {
        this.inventoryItems = this.inventoryItems.filter(item => item.id !== itemId);
      });
    }
  }


  // Open modal to add new item
  openModal(): void {
    this.isModalOpen = true;
  }

  // Close the modal
  closeModal(): void {
    this.isModalOpen = false;
  }

  // Add new inventory item to the list
  addNewItem(): void {
    this.inventoryService.addInventoryItem(this.newItem).subscribe(item => {
      this.loadInventoryItems(); // Refresh the list
      this.inventoryItems.push(item); // Add the new item to the list
      this.closeModal(); // Close the modal
      this.newItem = { id: 0, name: '', quantity: 0, price: 0 }; // Reset form data
    });
  }

  
}

