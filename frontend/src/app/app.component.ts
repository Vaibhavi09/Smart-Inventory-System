import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { InventoryComponent } from './pages/inventory/inventory.component';
import { CustomerComponent } from './pages/customer/customer.component';
import { BillComponent } from './pages/bill/bill.component';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';
}
