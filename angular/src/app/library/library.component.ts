import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';

import {
  LibraryServiceProxy,
  GetBookLibraryDto,
  PublisherDto,
  AuthorDto,
  CategoryDto,
  AuthorServiceProxy,
  PublisherServiceProxy,
  CategoryServiceProxy,
  BorrowBookDetaiServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css'],
  animations: [appModuleAnimation()]
})
export class LibraryComponent extends AppComponentBase implements OnInit {

  books: GetBookLibraryDto[];
  advancedFiltersVisible = false;
  pageNumber: number = 1;;
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
    private _libraryService: LibraryServiceProxy,
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

  list(): void {
    this._libraryService
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
    this.list();
  }

  setup() {
    this.categoryName = "";
    this.authorName = "";
    this.publisherName = "";
    this.name = undefined;
    this.pageNumber=1;
    this.list();
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


}
