import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateControleChaptersDialogComponent } from './update-controle-chapters-dialog.component';

describe('UpdateControleChaptersDialogComponent', () => {
  let component: UpdateControleChaptersDialogComponent;
  let fixture: ComponentFixture<UpdateControleChaptersDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateControleChaptersDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateControleChaptersDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
