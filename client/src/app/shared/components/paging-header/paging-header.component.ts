import { Component, Input, OnInit } from '@angular/core';
import { ShopParams } from '../../Models/shopParams';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.css']
})
export class PagingHeaderComponent implements OnInit {

  @Input() pageNumber:number;
  @Input() pageSize:number;
  @Input() totalCount:number;

  constructor() { }

  ngOnInit(): void {
  }

}
