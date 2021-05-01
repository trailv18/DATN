import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  BorrowBookServiceProxy,
  GetAllBorrowBookDto
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
@Component({
  selector: 'app-reader-borrow',
  templateUrl: './reader-borrow.component.html',
  animations: [appModuleAnimation()]
})
export class ReaderBorrowComponent extends AppComponentBase implements OnInit {

  borrowBooks: GetAllBorrowBookDto[];
  names = '';
  advancedFiltersVisible = false;

  pageNumber: number = 1;;
  count: number;
  pageSize: number;
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

  list(): void {
    this._borrowBookService
      .getBorrowBookPageByUserId(this.pageNumber)
      .subscribe(response => {
        this.borrowBooks = response.items;
        this.count = response.count;
        this.pageNumber = response.pageIndex;
        this.pageSize = response.pageSize;        
      }
      );
  }

  onChangePage(event) {
    this.pageNumber = event;
    this.list();
  }
}

 
