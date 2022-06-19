import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { StoreService } from './services/store.service';
import { ProductListViewComponent } from './views/product-list-view/product-list-view.component';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { CheckoutPageComponent } from './pages/checkout-page/checkout-page.component';
import { ShopPageComponent } from './pages/shop-page/shop-page.component';
import router from './router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { AuthActivator } from './services/authActivator.service';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from './views/navbar/navbar.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { OrderComponent } from './views/order/order.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductListViewComponent,
    CartPageComponent,
    CheckoutPageComponent,
    ShopPageComponent,
    LoginPageComponent,
    NavbarComponent,
    ProfileComponent,
    OrderComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      router,
      FormsModule
  ],
    providers: [
        StoreService,
        AuthActivator
    ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
