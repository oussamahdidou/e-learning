import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NiveauScolairesTableComponent } from './niveau-scolaires-table.component';

describe('NiveauScolairesTableComponent', () => {
  let component: NiveauScolairesTableComponent;
  let fixture: ComponentFixture<NiveauScolairesTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NiveauScolairesTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NiveauScolairesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
