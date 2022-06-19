import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { StoreService } from "../../services/store.service";

@Component({
    selector: "checkout",
    templateUrl: "checkout-page.component.html"
})
export class CheckoutPageComponent {

    constructor(public data: StoreService, private router: Router) {
    }

    errorMessage: string = "";

    onCheckout() {
        this.data.checkout()
            .subscribe(() => {
                    this.router.navigate(["/"]);
            }, err => this.errorMessage = `Failed to checkout: ${err}`);
    }
}