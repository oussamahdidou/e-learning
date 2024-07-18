import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatequizComponent } from './updatequiz.component';

describe('UpdatequizComponent', () => {
  let component: UpdatequizComponent;
  let fixture: ComponentFixture<UpdatequizComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdatequizComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatequizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
