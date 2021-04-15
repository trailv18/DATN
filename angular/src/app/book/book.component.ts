import { Component, Injector, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import { DomSanitizer, SafeResourceUrl, SafeUrl} from '@angular/platform-browser';

import {
  AuthorDto,
  AuthorServiceProxy,
  BookDto,
  BookServiceProxy,
  CategoryDto,
  CategoryServiceProxy,
  GetAllBookDto,
  PublisherDto,
  PublisherServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { CreateBookComponent } from './create-book/create-book.component';
import { EditBookComponent } from './edit-book/edit-book.component';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  animations: [appModuleAnimation()]

})

export class BookComponent extends AppComponentBase implements OnInit{

  books: GetAllBookDto[];
  id:string;
  advancedFiltersVisible = false;

  pageNumber: number = 1;
  count: number;
  pageSize: number;

  name: string;
  categoryName: string = "";
  authorName: string = "";
  publisherName: string = "";

  publishers: PublisherDto[];
  authors: AuthorDto[];
  categories: CategoryDto[];

  constructor(
    injector: Injector,
    private _bookService: BookServiceProxy,
    private _modalService: BsModalService,
    public sanitizer: DomSanitizer,
    private _authorService: AuthorServiceProxy,
    private _publisherService: PublisherServiceProxy,
    private _categoryService: CategoryServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
    this.listPublisher();
    this.listAuthor();
    this.listCategory();
  }

  listCategory(): void {
    this._categoryService
      .getAllCategory()
      .subscribe(
        response => {
          this.categories = response;
        }
      );
  }


  listPublisher(): void {
    this._publisherService
      .getAllPublisher()
      .subscribe(
        response => {
          this.publishers = response;
        }
      );
  }

  listAuthor(): void {
    this._authorService
      .getAllAuthor()
      .subscribe(
        response => {
          this.authors = response;
        }
      );
  }

  list(): void {
    this._bookService
      .getPageBook(this.pageNumber, this.name, this.categoryName, this.authorName, this.publisherName)
      .subscribe(response => {
        this.books = response.items;
        this.count = response.count;
        this.pageNumber = response.pageIndex;
        this.pageSize = response.pageSize;
      }
      );
  }
  
  filter() {
    if (this.name === undefined && this.categoryName === "" && this.authorName === "" && this.publisherName === "") {
      this.list();
    } else {
      this.list();
    }
  }

  onChangePage(event) {
    this.pageNumber = event;
    this.list()
  }

  setup() {
    this.categoryName = "";
    this.authorName = "";
    this.publisherName = "";
    this.name = undefined;
    this.list();
  }

  refresh(){
    this.pageNumber = 1;
    this.list();
  }

  delete(author: BookDto): void {
    abp.message.confirm(
      this.l('Book delete warning message', author.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._bookService
            .deleteBook(author.id)
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

  createBook(): void {
    this.showCreateOrEditTenantDialog();
  }

  editBook(book: BookDto): void {
    this.showCreateOrEditTenantDialog(book.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateBookComponent,
        {
          class: 'modal-lg',
        }
      );

    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditBookComponent,
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
