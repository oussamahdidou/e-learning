import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NiveauscolairemComponent } from './niveauscolairem.component';

describe('NiveauscolairemComponent', () => {
  let component: NiveauscolairemComponent;
  let fixture: ComponentFixture<NiveauscolairemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NiveauscolairemComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NiveauscolairemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
