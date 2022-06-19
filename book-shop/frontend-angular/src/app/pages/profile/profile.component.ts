import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { Order } from '../../shared/order';

@Component({
    selector: 'app-profile',
    templateUrl: "profile.component.html",
    styles: [
    ]
})
export class ProfileComponent implements OnInit {

    constructor(public data: StoreService) {
    }

    public orders!: Order[];

    ngOnInit(): void {
        this.data.loadOrders()
            .subscribe(() => {
                this.orders = this.data.orders
            });
    }
}
