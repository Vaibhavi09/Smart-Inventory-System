import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-customer-detail-dialog',
  imports: [FormsModule, CommonModule],
  templateUrl: './customer-detail-dialog.component.html',
  styleUrl: './customer-detail-dialog.component.css'
})
export class CustomerDetailDialogComponent {

  httpClient = inject(HttpClient);
  modal = inject(NgbActiveModal);
  CustomerDetails = {
    CustomerID: '',
    FirstName: '',
    LastName: '',
    Email: '',
    Mobile: '',
    RegistrationDate: ''
  }

  onSubmit() {
    let ApiUrl = 'https://localhost:7149/api/Customer';
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Aks-auth-token',
        'Content-Type': 'application/json'
      })
    }
    this.httpClient.post(ApiUrl, this.CustomerDetails, httpOptions).subscribe({
      next: (data) => {
        console.log('Customer Data:', data);
        alert('Customer Details Submitted Successfully');
      },
      error: (error) => {
        console.error('Error submitting customer data:', error);
        alert('Failed to submit customer data. Please try again later.');
      },
      complete: () => {
        console.log('POST request completed.');
        this.modal.close({event: 'close'}); // Close the modal after submission
      }
    });
  }

}
