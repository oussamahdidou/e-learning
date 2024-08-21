import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParagrapheComponent } from './paragraphe.component';

describe('ParagrapheComponent', () => {
  let component: ParagrapheComponent;
  let fixture: ComponentFixture<ParagrapheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ParagrapheComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParagrapheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
