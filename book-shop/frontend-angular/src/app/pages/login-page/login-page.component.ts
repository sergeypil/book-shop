import { Component } from "@angular/core";

import { Router } from "@angular/router";
import { StoreService } from "../../services/store.service";



@Component({
    selector: "login-page",
    templateUrl: "login-page.component.html"
})
export class LoginPageComponent {

    constructor(private data: StoreService, private router: Router) {

    }

    errorMessage: string = "";
    public creds = {
        username: "",
        password: ""
    };

    onLogin() {
        this.data.login(this.creds)
            .subscribe(() => {
                if (this.data.order.items.length > 0) {
                    this.router.navigate(["checkout"]).then(() => location.reload());
                } else {
                    this.router.navigate([""]).then(() => location.reload());
                }
            }, err => this.errorMessage = "Failed to login")

    }
}