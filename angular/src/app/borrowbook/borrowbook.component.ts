import { Component, Injector, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { BorrowBookDetailDto, BorrowBookDetaiServiceProxy, GetAllBorrowBookDetailDto, UpdateStatusDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { UpdateStatusComponent } from './update-status/update-status.component';

@Component({
  selector: 'app-borrowbook',
  templateUrl: './borrowbook.component.html',
  styleUrls: ['./borrowbook.component.css'],
  animations: [appModuleAnimation()]

})
export class BorrowbookComponent extends AppComponentBase implements OnInit{

  borrows: GetAllBorrowBookDetailDto[];
  
  pageNumber: number = 1;
  count: number;
  pageSize: number;

  dateModel: Date = new Date();
  fromDate;
  toDate;
  month:number;

  listMonth =[
    {m: 1},{m: 2},{m:3},{m: 4},{m: 5},{m: 6},{m: 7},{m: 8},{m: 9},{m: 10},{m: 11},{m: 12}
  ]


  constructor(
    injector: Injector,
    public sanitizer: DomSanitizer,
    private _borrowService: BorrowBookDetaiServiceProxy,
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
    this._borrowService
      .getAllBorrowBookDetail(this.pageNumber, this.fromDate, this.toDate , this.month)
      .subscribe(response => {
        this.borrows = response.items;
        this.count = response.count;
        this.pageNumber = response.pageIndex;
        this.pageSize = response.pageSize;        
      }
      );
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

  delete(br: BorrowBookDetailDto): void {
    abp.message.confirm(
      this.l('Delete warning message'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._borrowService
            .delete(br.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('Successfully deleted'));
                this.list()
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  updateStatus(borrowBook: GetAllBorrowBookDetailDto): void {
    this.showCreateOrEditTenantDialog(borrowBook.id);
  }

  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      
    } else {
      createOrEditTenantDialog = this._modalService.show(
        UpdateStatusComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditTenantDialog.content.onSave.subscribe(() => {
      this.list();
    })
  }
}
