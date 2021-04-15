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
  AuthorServiceProxy,
  AuthorDto,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  animations: [appModuleAnimation()]

})
export class EditAuthorComponent extends AppComponentBase implements OnInit {
  saving = false;
  author: AuthorDto = new AuthorDto();
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _authorService: AuthorServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._authorService.getAuthorById(this.id).subscribe((result: AuthorDto) => {
      this.author = result;
    });
  }

  save(): void {
    this.saving = true;
    this._authorService
      .updateAuthor(this.author)
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
