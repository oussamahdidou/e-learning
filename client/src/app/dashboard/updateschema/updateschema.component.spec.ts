import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateschemaComponent } from './updateschema.component';

describe('UpdateschemaComponent', () => {
  let component: UpdateschemaComponent;
  let fixture: ComponentFixture<UpdateschemaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateschemaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateschemaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
