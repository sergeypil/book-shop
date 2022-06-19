import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Product } from "../shared/product";
import { Observable } from "rxjs";
import { Order, OrderItem } from "../shared/order";
import { LoginRequest, LoginResults } from "../shared/LoginResults";

@Injectable({
    providedIn: 'root'
})
export class StoreService {

    constructor(private http: HttpClient) {
        this.inc = 0;
    }
    public inc: number;
    private token: string = "";
    private tokenExpiration!: Date;


    public get loginRequired(): boolean {
        return localStorage.getItem("token")===null;
    }

    public login(creds: LoginRequest) {
        return this.http.post<LoginResults>("https://localhost:5001/auth/createToken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.tokenExpiration = data.expiration;
                localStorage.setItem("token", this.token);
            }));
    }
    public products: Product[] = [];

    public loadProducts(): Observable<void> {
        return this.http.get<[]>("https://localhost:5001/api/products")
            .pipe(map(data => {
                this.products = data;
                return;
            }));
    }

    public order: Order = new Order();

    public AddToOrder(product: Product) {
        let containsProduct: boolean = false;
        let item: OrderItem = new OrderItem();
        for (let i = 0; i < this.order.items.length; i++) {
            if (this.order.items[i].productId == product.id) {
                containsProduct = true;
                item = this.order.items[i];
                break;
            }
        }
        if (containsProduct) {
            item.quantity++;
        } else {
            item.productId = product.id;
            item.productAuthor = product.author;
            item.productCategory = product.category;
            item.productTitle = product.title;
            item.unitPrice = product.price;
            item.quantity = 1;
            item.productIsbn = product.isbn;
            this.order.items.push(item);
        }
    }

    public checkout() {
        //order number is required on the api
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString()
                + this.order.orderDate.getTime().toString();
        }
        const headers = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("token")}`);
        return this.http.post("https://localhost:5001/api/orders", this.order, {
            headers: headers
        })
            .pipe(map(() => {
                this.order = new Order();
            }));
    }

    public orders: Order[] = [];
    public loadOrders() {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("token")}`);
        return this.http.get<[]>("https://localhost:5001/api/orders", {
            headers: headers
        })
            .pipe(map(data => {
                this.orders = data;
                return;
            }));
    }

    public loadOrderById(id:string) {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("token")}`);
        return this.http.get<Order>("https://localhost:5001/api/orders/" + id, {
            headers: headers
        });
    }
}