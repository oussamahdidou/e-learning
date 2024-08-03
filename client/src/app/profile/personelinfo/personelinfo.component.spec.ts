import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonelinfoComponent } from './personelinfo.component';

describe('PersonelinfoComponent', () => {
  let component: PersonelinfoComponent;
  let fixture: ComponentFixture<PersonelinfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PersonelinfoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonelinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
