import { Component, OnInit } from '@angular/core';
import { IPagination } from '../shared/Models/pagination';
import { IProduct } from '../shared/Models/product';
import { IBrand } from '../shared/Models/productBrand';
import { IType } from '../shared/Models/productType';
import { ShopParams } from '../shared/Models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  products:IProduct[];
  types:IType[];
  brands:IBrand[];
  shopParams = new ShopParams();
  paging={count:0, pageSize:0, totalPages:0}

  constructor(private servic: ShopService) {}

  ngOnInit(): void{
   this.getProducts();
   this.getTypes();
   this.getBrands();
  }

  getProducts(){
    this.servic.getProducts(this.shopParams).subscribe((response) =>{
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.paging = {count: response.count, pageSize:response.pageSize, totalPages: Math.ceil(response.count/response.pageSize)}
      
    }, err =>{
      console.log(err);
    });
  }

  getTypes(){
    this.servic.getTypes().subscribe(response =>{
      this.types = [{id:0, 'name':'All'}, ...response];
    }, err =>{
      console.log(err);
    });
  }

  getBrands(){
    this.servic.getBrands().subscribe(response =>{
      this.brands = [{id:0, 'name':'All'}, ...response];
    }, err =>{
      console.log(err);
    });
  }

  onBrandSelected(brandId:number){
      this.shopParams.brandId = brandId;
      this.shopParams.pageNumber = 1
      this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1
    this.getProducts();
}

onSort(sort:any){
  this.shopParams.sort = sort.value;
  this.getProducts()
  
}
search(search:string){
  this.shopParams.search = search
  this.shopParams.pageNumber = 1
  this.getProducts()
}
reset(res:any){
  res.value = ''
  this.shopParams = new ShopParams();
  this.search('')
  
}
getPage(n){
  this.shopParams.pageNumber = n;
  this.getProducts();
  
  
  
}
}
