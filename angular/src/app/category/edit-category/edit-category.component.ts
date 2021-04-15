import {
  Component,
  Injector,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CategoryServiceProxy,
  CategoryDto,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  animations: [appModuleAnimation()]

})
export class EditCategoryComponent extends AppComponentBase
implements OnInit {

  saving = false;
  category: CategoryDto = new CategoryDto();
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _categoryService: CategoryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._categoryService.getCategoryById(this.id).subscribe((result: CategoryDto) => {
      this.category = result;
    });
  }

  save(): void {
    this.saving = true;
    this._categoryService
      .updateCategory(this.category)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Updated successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }

}
