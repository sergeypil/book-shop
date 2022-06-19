import { Component, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styles: [
  ]
})
export class NavbarComponent implements OnInit {

    constructor(public store: StoreService) { }

    ngOnInit(): void {
        this.isLoggedIn = JSON.parse(localStorage.getItem("token") || "{}");
    }
    public isLoggedIn!: boolean;
    logout() {
        localStorage.removeItem("token");
        location.reload();
    }
}
