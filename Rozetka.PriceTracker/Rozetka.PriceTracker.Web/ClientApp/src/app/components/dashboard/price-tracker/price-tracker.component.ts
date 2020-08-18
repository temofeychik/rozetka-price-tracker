import { Component, OnInit, OnDestroy } from '@angular/core';
import { AddProductComponent } from '../add-product/add-product.component';
import { MatDialog } from '@angular/material/dialog';
import { PriceTrackerClient, ResponseStream } from 'src/app/generated/src/app/protos/price_track_pb_service';
import { environment } from 'src/environments/environment';
import { TrackProductRequest, TrackProductResponse, TrackProductPriceResponse } from 'src/app/generated/src/app/protos/price_track_pb';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';
import { Observable, interval, Subscription, timer, BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-price-tracker',
  templateUrl: './price-tracker.component.html',
  styleUrls: ['./price-tracker.component.css']
})
export class PriceTrackerComponent implements OnInit, OnDestroy {
  private _client: PriceTrackerClient;
  private _products: TrackProductResponse.AsObject[];

  private loadIntervalSubscription: Subscription;

  private productsBehaviorSubject: BehaviorSubject<TrackProductResponse.AsObject[]> = new BehaviorSubject<TrackProductResponse.AsObject[]>([]);
  private products$ = this.productsBehaviorSubject.asObservable();

  constructor(private dialog: MatDialog) {
    this._client = new PriceTrackerClient(environment.serviceBaseUrl);

  }

  ngOnInit() {
    // this.loadProducts();
    this.loadIntervalSubscription = timer(0, 60 * 10 * 1000,).subscribe(value => {

      let requestMessage = new Empty();

      this._client.trackPrices(requestMessage, (error, response: TrackProductPriceResponse) => {
        let responseObject = response.toObject();
        this.productsBehaviorSubject.next(responseObject.productsList);
      });

    });
  }

  get products() {
    return this.products$;
  }

  openDialog() {
    const dialogRef = this.dialog.open(AddProductComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.trackProduct(result);
    });


  }

  public trackProduct(url: string) {
    const request = new TrackProductRequest();
    request.setProducturl(url);
    let resp = this._client.trackProduct(request, (error, response: TrackProductResponse) => {
      if (!this._products)
        this._products = [];
      
      this._products.push(response.toObject());

      this.products$.pipe(take(1)).subscribe(value=>{
        let products = [...value,response.toObject()];
        this.productsBehaviorSubject.next(products);
      });
    });
  }

  // private loadProducts() {
  //   let requestMessage = new Empty();

  //   this._client.trackPrices(requestMessage, (error, response: TrackProductPriceResponse) => {
  //     let responseObject = response.toObject();
  //     this._products = responseObject.productsList;
  //   });
  // }

  ngOnDestroy() {
    this.loadIntervalSubscription.unsubscribe();
  }
}
