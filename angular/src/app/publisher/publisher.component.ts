import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  PublisherServiceProxy,
  Publisher,
  PublisherDto,
  GetAllPublisherDtoPageResult
} from '@shared/service-proxies/service-proxies';
import { CreatePublisherComponent } from './create-publisher/create-publisher.component';
import { EditPublisherComponent } from './edit-publisher/edit-publisher.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

class PageResult<T>
{
  count: number;
  pageIndex: number;
  pageSize: number;
  items: T[];
}

@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  animations: [appModuleAnimation()]

})
export class PublisherComponent extends AppComponentBase implements OnInit {

  publishers: PublisherDto[];
  name = '';
  advancedFiltersVisible = false;

  request: PageResult<GetAllPublisherDtoPageResult>;
  pageNumber: number = 1;;
  count: number;
  pageSize: number;

  constructor(
    injector: Injector,
    private _publisherService: PublisherServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._publisherService
      .getPagePublisher(this.pageNumber, this.name)
      .subscribe(response => {
        this.publishers = response.items;
        this.count = response.count;
        this.pageNumber = response.pageIndex;
        this.pageSize = response.pageSize;
      }
      );
  }

  filter() {
    if (this.name === undefined) {
      this.list();
    } else {
      this.list();
    }
  }

  setup() {
    this.name = undefined;
    this.list();
  }

  onChangePage(event) {
    this.pageNumber = event;
    this.list()
  }

  refresh(){
    this.pageNumber =1;
    this.list();
  }

  delete(publisher: Publisher): void {
    abp.message.confirm(

      this.l('Publisher delete warning message', publisher.name),
      undefined,
      (result: boolean) => {
        if (result) {
          console.log(publisher.id);
          this._publisherService
            .deletePublisher(publisher.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('Successfully deleted'));
                this.list();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createPublisher(): void {
    this.showCreateOrEditTenantDialog();
  }

  editPublisher(publisher: PublisherDto): void {
    console.log(publisher.id)
    this.showCreateOrEditTenantDialog(publisher.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreatePublisherComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditPublisherComponent,
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
