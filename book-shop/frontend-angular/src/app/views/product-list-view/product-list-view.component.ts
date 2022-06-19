import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { Product } from '../../shared/product';

@Component({
  selector: 'product-list-view',
  templateUrl: './product-list-view.component.html',
  styles: [
  ]
})
export class ProductListViewComponent implements OnInit {
    constructor(public store: StoreService) {
    }

    public products!: Product[];

    ngOnInit(): void {
        this.store.loadProducts()
            .subscribe(() => {
                this.products = this.store.products
            });
    }
    addProduct(product: Product) {
        this.store.AddToOrder(product);
    }

}
