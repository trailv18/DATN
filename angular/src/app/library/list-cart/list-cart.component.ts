import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
  BorrowBookDetailDto,
  BorrowBookDetaiServiceProxy
} from '@shared/service-proxies/service-proxies';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
@Component({
  selector: 'app-list-cart',
  templateUrl: './list-cart.component.html',
  styleUrls: ['./list-cart.component.css'],
  animations: [appModuleAnimation()]
})
export class ListCartComponent extends AppComponentBase implements OnInit {
  itemObject: BorrowBookDetailDto = new BorrowBookDetailDto();
  cart: Array<BorrowBookDetailDto> = [];

  bookId: string;
  libraryId: string;

  saving = false;
  @Output() onSave = new EventEmitter<any>();

  total: number = 0;
  mess: string = "";

  constructor(
    injector: Injector,
    public _borrowBookDetaiService: BorrowBookDetaiServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.listAdded();
  }

  listAdded() {
    this.cart = JSON.parse(localStorage.getItem('object'));
    this.tinhtong();
  }

  tinhtong() {
    this.total = 0;
    this.cart = JSON.parse(localStorage.getItem('object'));
    if (!this.cart) {
      this.mess = "This cart is empty!";
    }
    else {
      for (let i = 0; i < this.cart.length; i++) {
        this.total += (this.cart[i].qty * this.cart[i].priceBorrow);
      }
      localStorage.setItem('total', String(this.total));
    }
  }

  update(item: string, qty: number) {
    this.cart = JSON.parse(localStorage.getItem('object'));
    for (let i = 0; i < this.cart.length; i++) {
      if (this.cart[i].bookId == item) {
        this.cart[i].qty = qty;
      }
    }
    localStorage.setItem("object", JSON.stringify(this.cart));
    this.tinhtong();
  }

  deleteCart(item) {
    this.cart = JSON.parse(localStorage.getItem('object'));
    for (let i = 0; i < this.cart.length; i++) {
      if (this.cart[i].bookId == item) {
        this.cart.splice(i, 1);
      }
    }
    localStorage.setItem('object', JSON.stringify(this.cart));
    this.tinhtong();
  }

  save() {
    this.saving = true;
    this.cart = JSON.parse(localStorage.getItem('object'))
    this._borrowBookDetaiService
      .addBorrowBookDetail(this.cart)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.listAdded();
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Successfully'));
        localStorage.removeItem('object');
      });
  }
}
