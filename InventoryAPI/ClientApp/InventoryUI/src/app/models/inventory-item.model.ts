export interface InventoryItem {
    id: number;
    name: string;
    quantity: number;
    price:number;
    isEditing?: boolean;  // Add the isEditing property as optional

  }
  