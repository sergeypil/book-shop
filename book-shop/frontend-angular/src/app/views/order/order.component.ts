import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StoreService } from '../../services/store.service';
import { Order } from '../../shared/order';

@Component({
  selector: 'app-order',
  templateUrl: 'order.component.html',
  styles: [
  ]
})
export class OrderComponent implements OnInit {

    constructor(public data: StoreService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.data.loadOrderById(this.route.snapshot.params['orderId']).
            subscribe(data => {
                this.order = data;
            })
    }
    public order!: Order;
}
