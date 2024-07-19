import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatesyntheseComponent } from './updatesynthese.component';

describe('UpdatesyntheseComponent', () => {
  let component: UpdatesyntheseComponent;
  let fixture: ComponentFixture<UpdatesyntheseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdatesyntheseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatesyntheseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
