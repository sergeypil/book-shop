import { RouterModule } from "@angular/router";
import { CartPageComponent } from "../pages/cart-page/cart-page.component";
import { CheckoutPageComponent } from "../pages/checkout-page/checkout-page.component";
import { LoginPageComponent } from "../pages/login-page/login-page.component";
import { ProfileComponent } from "../pages/profile/profile.component";
import { ShopPageComponent } from "../pages/shop-page/shop-page.component";
import { AuthActivator } from "../services/authActivator.service";
import { OrderComponent } from "../views/order/order.component";


const routes = [
    { path: "", component: ShopPageComponent },
    { path: "checkout", component: CheckoutPageComponent, canActivate: [AuthActivator]},
    { path: "login", component: LoginPageComponent },
    { path: "cart", component: CartPageComponent },
    { path: "profile", component: ProfileComponent },
    { path: "order/:orderId", component: OrderComponent },
    {path:"**", redirectTo:""}
];

const router = RouterModule.forRoot(routes, {
    /*useHash: true*/
});

export default router;