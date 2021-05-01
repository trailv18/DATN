import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  BorrowBookServiceProxy,
  GetAllBorrowBookDto
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import * as moment from 'moment';
@Component({
  selector: 'app-borrow-m',
  templateUrl: './borrow-m.component.html',
  styles: [
  ],
  animations: [appModuleAnimation()]
})
export class BorrowMComponent extends AppComponentBase implements OnInit {

  borrowBooks: GetAllBorrowBookDto[];
  advancedFiltersVisible = false;

  pageNumber: number = 1;;
  count: number;
  pageSize: number = 9;

  dateModel: Date = new Date();
  fromDate;
  toDate;
  month:number;

  listMonth =[
    {m: 1},{m: 2},{m:3},{m: 4},{m: 5},{m: 6},{m: 7},{m: 8},{m: 9},{m: 10},{m: 11},{m: 12}
  ]

  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  constructor(
    injector: Injector,
    private _borrowBookService: BorrowBookServiceProxy,
    private _modalService: BsModalService,
    
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  valueFromDate(value: Date): void {
    this.fromDate = moment(value, "YYYY-MM-DD");
  }

  valueToDate(value: Date): void {
    this.toDate = moment(value, "YYYY-MM-DD");
  }

  list(): void {
    this._borrowBookService
      .getPageBorrowBook(this.pageNumber, this.fromDate, this.toDate , this.month)
      .subscribe(response => {
        this.borrowBooks = response.items;
        this.count = response.count;
        this.pageNumber = response.pageIndex;
        this.pageSize = response.pageSize;
        console.log("ddddddd", this.borrowBooks, this.count);
        
      });
  }

  changePageSize(size: number){
    this.pageNumber = 1;
    this.pageSize = size;
    this.list();    
  }


  filter(){
    if(!this.fromDate && !this.toDate){
      this.list();      
    }
    else{
      this.list();
    }
  }

  setup(){
    this.fromDate = undefined;
    this.toDate = undefined;
    this.month = undefined;
    this.pageNumber = 1;
    this.pageSize = 9;
    this.list();
  }

  onChangePage(event) {
    // this.fromDate = undefined;
    // this.toDate = undefined;
    // this.month = undefined;
    this.pageNumber = event;
    this.list();
  }

  refresh() {
    this.fromDate = undefined;
    this.toDate = undefined;
    this.pageNumber = 1;
    this.pageSize = 9;
    this.list();
  }
}

