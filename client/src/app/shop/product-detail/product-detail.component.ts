import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/Models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  product:IProduct;

  constructor(private shop :ShopService, private active:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    this.shop.getProduct(+this.active.snapshot.paramMap.get('id')).subscribe(res=>{
      this.product = res;
    }, err=>{
      console.log(err);

    });
  }

}
