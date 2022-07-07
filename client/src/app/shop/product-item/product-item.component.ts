import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent implements OnInit {
  @Input() productItem:IProduct;
  constructor() { }

  ngOnInit(): void {
   
    
  }

}
