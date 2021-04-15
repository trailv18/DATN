import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';

import {
  BorrowBookServiceProxy, GetBorrowBookDetailDto, GetBorrowBookDto, UpdateStatusDto
} from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router'
import { appModuleAnimation } from '@shared/animations/routerTransition';
@Component({
  selector: 'app-reader-borrow-detail',
  templateUrl: './reader-borrow-detail.component.html',
  animations: [appModuleAnimation()]
})
export class ReaderBorrowDetailComponent extends AppComponentBase implements OnInit {

  borrowBooks: GetBorrowBookDto[];
  borrowBookDetails: GetBorrowBookDetailDto[];
  names = '';
  advancedFiltersVisible = false;
  id: string;
  name: string;

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  borrowBook: UpdateStatusDto = new UpdateStatusDto();
  total:number = 0;
  status: string;

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private _borrowBookDetailService: BorrowBookServiceProxy,
    private _borrowBookService: BorrowBookServiceProxy,

    private route: ActivatedRoute,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._borrowBookDetailService
      .getBorrowBookDetailById(this.route.snapshot.paramMap.get('id'))
      .subscribe(response => {
        this.id = response[0].id;
        this.borrowBooks = response;
        this.borrowBookDetails = response[0].borrowBookDetails;              
      });
  }

}
