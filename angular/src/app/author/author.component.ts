import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  AuthorServiceProxy,
  AuthorDto,
  Author,
  GetAllAuthorDtoPageResult
} from '@shared/service-proxies/service-proxies';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { EditAuthorComponent } from './edit-author/edit-author.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';


@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  animations: [appModuleAnimation()]
})
export class AuthorComponent extends AppComponentBase implements OnInit {

  authors: AuthorDto[];
  name = '';
  advancedFiltersVisible = false;

  pageNumber: number = 1;
  count: number;
  pageSize: number;

  constructor(
    injector: Injector,
    private _authorService: AuthorServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._authorService
      .getPageAuthor(this.pageNumber, this.name)
      .subscribe(response => {
        this.authors = response.items;
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

  delete(author: AuthorDto): void {
    abp.message.confirm(
      this.l('Author delete warning message', author.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._authorService
            .deleteAuthor(author.id)
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

  createAuthor(): void {
    this.showCreateOrEditTenantDialog();
  }

  editAuthor(author: AuthorDto): void {
    this.showCreateOrEditTenantDialog(author.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateAuthorComponent,
        {
          class: 'modal-lg',
        }
      );

    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditAuthorComponent,
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
