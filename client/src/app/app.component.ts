import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './Models/pagination';
import { IProduct } from './Models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'E-Commerce';
  products:IProduct[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void{
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe((response:IPagination) =>{
      this.products = response.data;
    },
    err =>{
      console.log(err);
        
    });

  }
}
