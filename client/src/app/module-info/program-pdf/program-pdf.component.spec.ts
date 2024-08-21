import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramPdfComponent } from './program-pdf.component';

describe('ProgramPdfComponent', () => {
  let component: ProgramPdfComponent;
  let fixture: ComponentFixture<ProgramPdfComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProgramPdfComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProgramPdfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
