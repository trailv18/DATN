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
  PublisherServiceProxy,
  PublisherDto,  
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-publisher',
  templateUrl: './create-publisher.component.html',
  animations: [appModuleAnimation()]

})
export class CreatePublisherComponent extends AppComponentBase implements OnInit {

    saving = false;
    publisher : PublisherDto = new PublisherDto();
  
    @Output() onSave = new EventEmitter<any>();
  
    constructor(injector: Injector,
      private _publisherService: PublisherServiceProxy,
      public bsModalRef: BsModalRef
    ) {
      super(injector); }
  
    ngOnInit(): void {
    }

    save(): void {
      this.saving = true;
      this._publisherService
        .addPublisher(this.publisher)
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
