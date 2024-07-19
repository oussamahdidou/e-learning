import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatecoursComponent } from './updatecours.component';

describe('UpdatecoursComponent', () => {
  let component: UpdatecoursComponent;
  let fixture: ComponentFixture<UpdatecoursComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdatecoursComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatecoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
