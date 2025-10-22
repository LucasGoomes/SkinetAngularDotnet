import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../../core/services/shop.service';
import { MatCard } from '@angular/material/card';

@Component({
  selector: 'app-shop',
  imports: [
    MatCard
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  private shopService = inject(ShopService);
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
