import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PriceTrackerClient } from 'src/app/generated/src/app/protos/price_track_pb_service';
import { environment } from 'src/environments/environment';
import { ChartDataSets, ChartOptions } from 'chart.js';
import { Color, BaseChartDirective, Label } from 'ng2-charts';
import { BehaviorSubject } from 'rxjs';
import { ProductInfoResponse, GetProductInfoRequest, PorductPriceHistoryResponse } from 'src/app/generated/src/app/protos/price_track_pb';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-product-price-history',
  templateUrl: './product-price-history.component.html',
  styleUrls: ['./product-price-history.component.css']
})
export class ProductPriceHistoryComponent implements OnInit {

  private _client: PriceTrackerClient;

  private productInfoBehaviorSubject: BehaviorSubject<ProductInfoResponse.AsObject> = new BehaviorSubject<ProductInfoResponse.AsObject>(null);
  private productInfo$ = this.productInfoBehaviorSubject.asObservable();

  constructor(private route: ActivatedRoute) {
    this._client = new PriceTrackerClient(environment.serviceBaseUrl);
  }

  ngOnInit() {
    let request = new GetProductInfoRequest();
    request.setProductid(this.route.snapshot.params.id);
    this._client.getProductInfo(request, (error, data) => {
      this.productInfoBehaviorSubject.next(data.toObject());
      let product = data.toObject();

      this.lineChartData = [{
        data: data.toObject().pricesList.map((x: PorductPriceHistoryResponse.AsObject) => {
          return x.price;
        }),
        label: product.productinfo.title
      }];

      this.lineChartData[0].data.push(product.productinfo.price);

      this._lineChatrLabels = [...this.productInfoBehaviorSubject.value.pricesList.map(x => this.formatDate(new Date(0, 0, 0, 0, 0, x.date.seconds))), this.formatDate(new Date())];
    });
  }

  get product(){
    if (this.productInfoBehaviorSubject.value)
      return this.productInfoBehaviorSubject.value.productinfo;
  }

  public lineChartLegend = true;
  public lineChartType = 'line';

  public lineChartData: ChartDataSets[];

  private _lineChatrLabels;
  public get lineChartLabels() {
    return this._lineChatrLabels;
  }


  private formatDate(date: Date) {
    return date.toLocaleString();
  }

  public lineChartColors: Color[] = [
    // { // grey
    //   backgroundColor: 'rgba(148,159,177,0.2)',
    //   borderColor: 'rgba(148,159,177,1)',
    //   pointBackgroundColor: 'rgba(148,159,177,1)',
    //   pointBorderColor: '#fff',
    //   pointHoverBackgroundColor: '#fff',
    //   pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    // },
    // { // dark grey
    //   backgroundColor: 'rgba(77,83,96,0.2)',
    //   borderColor: 'rgba(77,83,96,1)',
    //   pointBackgroundColor: 'rgba(77,83,96,1)',
    //   pointBorderColor: '#fff',
    //   pointHoverBackgroundColor: '#fff',
    //   pointHoverBorderColor: 'rgba(77,83,96,1)'
    // },
    { // red
      backgroundColor: 'rgba(255,0,0,0.3)',
      borderColor: 'red',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];

  @ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;

}
