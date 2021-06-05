import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import { ActivatedRoute } from '@angular/router'
import {
  LibraryServiceProxy,
  GetBookLibraryDto,
  BorrowBookDetailDto,
  BorrowBookDetaiServiceProxy
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css'],
  animations: [appModuleAnimation()]
})
export class BookDetailComponent extends AppComponentBase implements OnInit {

  book : GetBookLibraryDto= new GetBookLibraryDto();
  saving = false;
  @Output() onSave = new EventEmitter<any>();
  borrowBook: BorrowBookDetailDto = new BorrowBookDetailDto();

  constructor(
    injector: Injector,
    private _libraryService: LibraryServiceProxy,
    private _borrowBookService: BorrowBookDetaiServiceProxy,
    private _modalService: BsModalService,
    private route: ActivatedRoute,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getBookDetail();
  }

  getBookDetail(): void {
    this._libraryService
      .getBookDetail(this.route.snapshot.paramMap.get('bookId'))
      .subscribe(
        response => {
          this.book = response;          
        }
      );
  }

  save(): void {
    this.saving = true;
    this.borrowBook.bookId = this.route.snapshot.paramMap.get('bookId');
    this._borrowBookService
      .addBorrowBookDetail(this.borrowBook)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Successfully'));
        this.onSave.emit();
      });
  }
}
