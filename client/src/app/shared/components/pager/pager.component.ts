import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent implements OnInit {

  @Input() totalPages:number;
  @Input() currentPage:number;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }
  counter(n){
    return Array(n).fill(0).map((x,i)=>i+1)
  }
  onPageChanged(n:number){
    this.pageChanged.emit(n)
  }
}
