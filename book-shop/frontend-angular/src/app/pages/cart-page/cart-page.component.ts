import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'cart-page',
  templateUrl: './cart-page.component.html',
  styles: [
  ]
})
export class CartPageComponent {

    constructor(public data: StoreService, private router: Router) {

    }

    onCheckout() {
        if (this.data.loginRequired) {
            this.router.navigate(["login"]);
        } else {
            this.router.navigate(["checkout"]);
        }
    }
}