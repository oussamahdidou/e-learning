import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstutitionmmComponent } from './instutitionmm.component';

describe('InstutitionmmComponent', () => {
  let component: InstutitionmmComponent;
  let fixture: ComponentFixture<InstutitionmmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InstutitionmmComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstutitionmmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
