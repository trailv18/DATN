import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowMComponent } from './borrow-m.component';

describe('BorrowMComponent', () => {
  let component: BorrowMComponent;
  let fixture: ComponentFixture<BorrowMComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BorrowMComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BorrowMComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
