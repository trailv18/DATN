import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/storage';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  BookDto,
  BookServiceProxy,
  CategoryServiceProxy,
  GetAllCategoryDto,
  AuthorServiceProxy,
  GetAllAuthorDto,
  PublisherServiceProxy,
  GetAllPublisherDto
} from '@shared/service-proxies/service-proxies';

import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';
@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styles: [
  ]
})
export class CreateBookComponent extends AppComponentBase implements OnInit {

  saving = false;
  book: BookDto = new BookDto();

  urlImage: string;
  downloadURL: Observable<string>;

  @Output() onSave = new EventEmitter<any>();

  categories: GetAllCategoryDto[];
  authors: GetAllAuthorDto[];
  publishers: GetAllPublisherDto[];

  constructor(
    private storage: AngularFireStorage,
    private _bookService: BookServiceProxy,
    public bsModalRef: BsModalRef,
    private _categoryService: CategoryServiceProxy,
    private _authorService : AuthorServiceProxy,
    private _publisherService: PublisherServiceProxy,
    injector: Injector,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getCategories();
    this.getAuthors();
    this.getPublishers();
  }


  getCategories() {
    this._categoryService
      .getAllCategory()
      .subscribe(response => {
        this.categories = response;
      });

  }

  getAuthors() {
    this._authorService
      .getAllAuthor()
      .subscribe(response => {
        this.authors = response;
      });

  }

  getPublishers() {
    this._publisherService
      .getAllPublisher()
      .subscribe(response => {
        this.publishers = response;
      });

  }

  onFileSelected(event) {
    var n = Date.now();
    const file = event.target.files[0];
    const filePath = `Images/${n}`;
    const fileRef = this.storage.ref(filePath);
    const task = this.storage.upload(`Images/${n}`, file);
    task
      .snapshotChanges()
      .pipe(
        finalize(() => {
          this.downloadURL = fileRef.getDownloadURL();
          this.downloadURL.subscribe(url => {
            if (url) {
              this.book.urlImage = url;
            }
          });
        })
      )
      .subscribe();
  }

  save(): void {
    this.saving = true;
    this._bookService
      .addBook(this.book)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Saved successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }
}
