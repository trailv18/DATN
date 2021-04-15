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
  BorrowBookDetailDto
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
  qty=1;
 
  itemObject: BorrowBookDetailDto = new BorrowBookDetailDto();

  cart: Array<BorrowBookDetailDto> = [];

  saving = false;
  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _libraryService: LibraryServiceProxy,
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

  plus(){
    this.qty=this.qty+1;
  }

  minus(){
    if(this.qty !=0){
      this.qty=this.qty-1;
    }
  }

  onClick(borrowBookDetail: GetBookLibraryDto): void {
    this.itemObject.bookId = borrowBookDetail.id;
    this.itemObject.priceBorrow = borrowBookDetail.priceBorrow;
    this.itemObject.qty = this.qty;
    this.itemObject.bookName = borrowBookDetail.name;
    this.itemObject.urlImage= borrowBookDetail.urlImage;

    if(JSON.parse(localStorage.getItem('object'))) {
      this.cart = JSON.parse(localStorage.getItem('object'))
      let existItemIndex = this.cart.findIndex((item) => item.bookId === this.itemObject.bookId)
      if(existItemIndex >= 0) {
        this.cart[existItemIndex].qty += this.itemObject.qty
      }
      else {
        this.cart.push(this.itemObject)
      }
    }
    else {
      this.cart.push(this.itemObject)
    }
    localStorage.setItem("object", JSON.stringify(this.cart));
    this.notify.info(this.l('Add successfully'));
  }
}
