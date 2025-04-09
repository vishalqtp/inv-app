import { Component, EventEmitter, Output } from '@angular/core';
import { InventoryItem, InventoryService } from 'src/app/services/services/inventory.service';

@Component({
  selector: 'app-add-inventory-item',
  templateUrl: './add-inventory-item.component.html',
  styleUrls: ['./add-inventory-item.component.scss']
})
export class AddInventoryItemComponent {
  @Output() close = new EventEmitter<void>();
  @Output() itemAdded = new EventEmitter<InventoryItem>();

  newItem: InventoryItem = { id: 0, name: '', quantity: 0, price: 0 };

  constructor(private inventoryService: InventoryService) {}

  addItem(): void {
    this.inventoryService.addInventoryItem(this.newItem).subscribe((item) => {
      this.itemAdded.emit(item); // Notify parent
      this.close.emit(); // Close dialog
    });
  }
}