import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { StoreService } from './services/store.service';
import { ProductListViewComponent } from './views/product-list-view/product-list-view.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductListViewComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [
        StoreService],
  bootstrap: [AppComponent]
})
export class AppModule { }
