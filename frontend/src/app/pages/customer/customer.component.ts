import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerDetailDialogComponent } from '../customer-detail-dialog/customer-detail-dialog.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customer',
  imports: [CommonModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {

  customerData = {
    customerID: '',
    firstName: '',
    lastName: '',
    email: '',
    mobile: '',
    registrationDate: ''
  }

  private modalService = inject(NgbModal);
  httpClient = inject(HttpClient);
  customerDetails: any;

  openModal() {
    this.modalService.open(CustomerDetailDialogComponent).result.then(data => { 
      if(data.event === 'close') {
        this.getCustomerDetails();
      }
      //console.log(data.event);
});
}

  ngOnInit(){
    this.getCustomerDetails();
  }

  getCustomerDetails() {
    let ApiUrl = 'https://localhost:7149/api/Customer';
    this.httpClient.get(ApiUrl).subscribe(result => {
      this.customerDetails = result;
      console.log(result);
    });
  }

  onDelete(productId: any) {
    //alert('Delete' + productId);
    const isDelete = confirm('Are you sure you want to delete this item?');
    if (isDelete) {
      const apiUrl = `https://localhost:7149/api/Customer?customerId=${productId}`; 
      this.httpClient.delete(apiUrl).subscribe(data =>{
        console.log(data);
        this.customerDetails();
        })
  }
}
}

