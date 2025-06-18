import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-inventory',
  imports: [FormsModule, CommonModule],
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent {
  inventoryDto: any[] = []; // Assuming the response is an array of inventory items
  isUpdate: boolean = false; // Flag to check if it's an update operation

  inventoryData = {
    productId: "",
    productName: "",
    stockAvailable: 0,
    reorderStock: 0
  };

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.inventoryDetails(); // Fetch inventory details on component initialization
  }

inventoryDetails() {
  const apiUrl = "https://localhost:7149/api/Inventory";

    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Aks-auth-token' // Include this if your API requires authorization
      })
    };

    this.httpClient.get<any[]>(apiUrl, httpOptions).subscribe({
      next: (data) => {
        this.inventoryDto = data; // Assign the response to inventoryDto
        console.log('Inventory Data:', this.inventoryDto);
      },
      error: (error) => {
        console.error('Error fetching inventory data:', error);
        alert('Failed to fetch inventory data. Please try again later.');
      },
      complete: () => {
        console.log('GET request completed.');
      }
    });

    this.inventoryData = {
      productId: "",
      productName: "",
      stockAvailable: 0,
      reorderStock: 0
    }
    this.isUpdate = false; // Reset the update flag

}

  onDelete(productId: any) {
    //alert('Delete' + productId);
    const isDelete = confirm('Are you sure you want to delete this item?');
    if (isDelete) {
      const apiUrl = `https://localhost:7149/api/Inventory?ProductId=${productId}`; 
      this.httpClient.delete(apiUrl).subscribe(data =>{
        console.log(data);
        this.inventoryDetails();
        })
  }
}

  onEdit(inventory: any) {
    this.inventoryData.productId = inventory.ProductID;
    this.inventoryData.productName = inventory.ProductName;
    this.inventoryData.stockAvailable = inventory.StockAvailable;
    this.inventoryData.reorderStock = inventory.ReorderStock;

    this.isUpdate = true; // Set the flag to true for update operation
  }

  onSubmit(): void {
    // Client-side validation
    if (!this.inventoryData.productId || !this.inventoryData.productName || 
        this.inventoryData.stockAvailable <= 0 || this.inventoryData.reorderStock <= 0) {
      alert('Please fill all fields with valid values.');
      return;
    }

    const apiUrl = "https://localhost:7149/api/Inventory";
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Aks-auth-token',
        'Content-Type': 'application/json'
      })
    };
    if(!this.isUpdate) {

    this.httpClient.post(apiUrl, this.inventoryData, httpOptions).subscribe({
      next: (response: any) => {
        console.log('Response:', response);
        alert('Form Submitted Successfully!');
      },
      error: (error: any) => {
        console.error('Full Error Response:', error);
        if (error.error && error.error.errors) {
          console.error('Validation Errors:', error.error.errors);
          alert('Validation Errors: ' + JSON.stringify(error.error.errors));
        } else {
          alert('Error submitting the form. Please check the input values.');
        }
      },
      complete: () => {
        console.log('POST request completed.');
        this.inventoryDetails(); // Refresh inventory details after submission
      }
    });
  } else {
    this.httpClient.put(apiUrl, this.inventoryData, httpOptions).subscribe({
      next: (response: any) => {
        console.log('Response:', response);
        alert('Form Submitted Successfully!');
      },
      error: (error: any) => {
        console.error('Full Error Response:', error);
        if (error.error && error.error.errors) {
          console.error('Validation Errors:', error.error.errors);
          alert('Validation Errors: ' + JSON.stringify(error.error.errors));
        } else {
          alert('Error submitting the form. Please check the input values.');
        }
      },
      complete: () => {
        console.log('POST request completed.');
        this.inventoryDetails(); // Refresh inventory details after submission
      }
    });
  }
  }
}