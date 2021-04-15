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
  CategoryServiceProxy,
  CategoryDto,  
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  animations: [appModuleAnimation()]

})
export class CreateCategoryComponent extends AppComponentBase implements OnInit {

  saving = false;
  category : CategoryDto = new CategoryDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _categoryService: CategoryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector); }

  ngOnInit(): void {
  }

  save(): void {
    this.saving = true;
    this._categoryService
      .addCategory(this.category)
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
