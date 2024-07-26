import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstitutionsTableComponent } from './institutions-table.component';

describe('InstitutionsTableComponent', () => {
  let component: InstitutionsTableComponent;
  let fixture: ComponentFixture<InstitutionsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InstitutionsTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InstitutionsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
