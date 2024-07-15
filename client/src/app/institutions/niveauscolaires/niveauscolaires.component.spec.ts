import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NiveauscolairesComponent } from './niveauscolaires.component';

describe('NiveauscolairesComponent', () => {
  let component: NiveauscolairesComponent;
  let fixture: ComponentFixture<NiveauscolairesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NiveauscolairesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NiveauscolairesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
