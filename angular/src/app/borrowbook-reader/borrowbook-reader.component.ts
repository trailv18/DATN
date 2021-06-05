import { Component, Injector, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { BorrowBookDetaiServiceProxy, GetAllBorrowBookDetailDto, UpdateStatusDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-borrowbook-reader',
  templateUrl: './borrowbook-reader.component.html',
  styleUrls: ['./borrowbook-reader.component.css'],
  animations: [appModuleAnimation()]

})
export class BorrowbookReaderComponent extends AppComponentBase implements OnInit{

  borrows: GetAllBorrowBookDetailDto[];
  
  pageNumber: number = 1;
  count: number;
  pageSize: number;

  dateModel: Date = new Date();
 
  constructor(
    injector: Injector,
    public sanitizer: DomSanitizer,
    private _borrowService: BorrowBookDetaiServiceProxy,
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._borrowService
      .getBorrowBookPageByUserId(this.pageNumber)
      .subscribe(response => {
        this.borrows = response.items;
        this.count = response.count;
        this.pageNumber = response.pageIndex;
        this.pageSize = response.pageSize;
      }
      );
  }

  setup(){
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
    this.pageNumber = 1;
    this.pageSize = 9;
    this.list();
  }

}
