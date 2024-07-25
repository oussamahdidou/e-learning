import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprobationTableComponent } from './approbation-table.component';

describe('ApprobationTableComponent', () => {
  let component: ApprobationTableComponent;
  let fixture: ComponentFixture<ApprobationTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ApprobationTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApprobationTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
