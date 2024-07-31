import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardtableComponent } from './dashboardtable.component';

describe('DashboardtableComponent', () => {
  let component: DashboardtableComponent;
  let fixture: ComponentFixture<DashboardtableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardtableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardtableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
