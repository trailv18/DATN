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
import { UpdateStatusComponent } from './update-status/update-status.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-borrow-detail',
  templateUrl: './borrow-detail.component.html',
  styles: [
  ],
  animations: [appModuleAnimation()]
})
export class BorrowDetailComponent extends AppComponentBase implements OnInit {

  borrowBooks: GetBorrowBookDto[];
  borrowBookDetails: GetBorrowBookDetailDto[];
  names = '';
  advancedFiltersVisible = false;
  id: string;
  name: string;

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  borrowBook: UpdateStatusDto = new UpdateStatusDto();

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

  updateStatus(borrowBook: UpdateStatusDto): void {
    this.showCreateOrEditTenantDialog(this.id);
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
