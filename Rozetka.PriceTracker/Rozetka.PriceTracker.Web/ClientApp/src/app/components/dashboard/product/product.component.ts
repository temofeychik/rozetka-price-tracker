import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddProductComponent } from '../add-product/add-product.component';
import { TrackProductResponse } from 'src/app/generated/src/app/protos/price_track_pb';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  @Input() product: TrackProductResponse.AsObject;

  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() {
    
  }

  getImageUrl(source : string){
    return this.sanitizer.bypassSecurityTrustStyle(`url('${source}')`);
  }

  get isAvailable(){
    return this.product.sellStatus === 'available' || this.product.sellStatus === 'limited';
  }
  
  get priceDiff(){
    return this.product.price - this.product.prevPrice;
  }

}
