import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { Product } from '../../shared/product';

@Component({
  selector: 'app-product-list-view',
  templateUrl: './product-list-view.component.html',
  styles: [
  ]
})
export class ProductListViewComponent implements OnInit {

    public products: Product[] = [];
    constructor(private store: StoreService) {
    }

    ngOnInit(): void {
        this.store.loadProducts()
            .subscribe();
  }

}
