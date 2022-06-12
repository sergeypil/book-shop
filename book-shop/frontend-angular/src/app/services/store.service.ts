import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class StoreService {

    constructor(private http: HttpClient) {

    }

    public products = [];

    public loadProducts() {
        return this.http.get<[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
                return;
            }));
    }
}