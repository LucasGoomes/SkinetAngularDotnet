import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';
import { ShopService } from './core/services/shop.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  private shopService = inject(ShopService);
  title = 'client';
  products: Product[] = [];

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      // subscribe and next to handle the data when it arrives
      next: response => {
        this.products = response.data;
      },
      error: error => {
        console.error(error);
      }
    });
  }
}
