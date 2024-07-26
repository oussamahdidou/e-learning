import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateChapterQuizComponent } from './create-chapter-quiz.component';

describe('CreateChapterQuizComponent', () => {
  let component: CreateChapterQuizComponent;
  let fixture: ComponentFixture<CreateChapterQuizComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateChapterQuizComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateChapterQuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
