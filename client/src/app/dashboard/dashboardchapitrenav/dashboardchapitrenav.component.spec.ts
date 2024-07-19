import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardchapitrenavComponent } from './dashboardchapitrenav.component';

describe('DashboardchapitrenavComponent', () => {
  let component: DashboardchapitrenavComponent;
  let fixture: ComponentFixture<DashboardchapitrenavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardchapitrenavComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardchapitrenavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
