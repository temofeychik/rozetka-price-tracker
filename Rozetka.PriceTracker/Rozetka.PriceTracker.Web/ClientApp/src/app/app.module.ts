import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PriceTrackerComponent } from './components/dashboard/price-tracker/price-tracker.component';
import { ProductComponent } from './components/dashboard/product/product.component';
import { MatDialogModule } from '@angular/material/dialog'
import { MatInputModule } from '@angular/material/input';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { AddProductComponent } from './components/dashboard/add-product/add-product.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { ChartsModule } from 'ng2-charts';
import { ProductPriceHistoryComponent } from './components/dashboard/product-price-history/product-price-history.component';
import { ProductDetailsComponent } from './components/dashboard/product-details/product-details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    PriceTrackerComponent,
    ProductComponent,
    AddProductComponent,
    ProductPriceHistoryComponent ,
    ProductDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    MatInputModule,
    RouterModule.forRoot([
      { path: "", component: PriceTrackerComponent },
      { path: "info/:id", component: ProductPriceHistoryComponent }
    ]),
    BrowserAnimationsModule,
    MatButtonModule,
    ChartsModule

  ],
  providers: [{ provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
  ],
  bootstrap: [AppComponent],
  entryComponents: [AddProductComponent]
})
export class AppModule { }
