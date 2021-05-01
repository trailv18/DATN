import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  BorrowBookServiceProxy, UpdateStatusDto
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-update-status',
  templateUrl: './update-status.component.html',
  animations: [appModuleAnimation()]
})
export class UpdateStatusComponent extends AppComponentBase implements OnInit {

  status = [
    {name: 'Đang mượn'},
    {name: 'Đã trả'},
    {name: 'Quá hạn'},
  ];

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  borrowBook: UpdateStatusDto = new UpdateStatusDto();
  id:string;
  names:string;

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _borrowBookService: BorrowBookServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._borrowBookService.getById(this.id).subscribe((result: UpdateStatusDto) => {
      this.borrowBook = result;
    });
  }

  save(): void {
    this.saving = true;
    this._borrowBookService
      .updateStatus(this.borrowBook)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Updated Successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }

}

