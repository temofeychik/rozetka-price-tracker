import { Component, OnInit, Input } from '@angular/core';
import { TrackProductResponse } from 'src/app/generated/src/app/protos/price_track_pb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {


  @Input() product: TrackProductResponse.AsObject;

  constructor() { }

  ngOnInit() {
  }

}
