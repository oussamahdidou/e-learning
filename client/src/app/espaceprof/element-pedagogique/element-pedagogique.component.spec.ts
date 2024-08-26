import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ElementPedagogiqueComponent } from './element-pedagogique.component';

describe('ElementPedagogiqueComponent', () => {
  let component: ElementPedagogiqueComponent;
  let fixture: ComponentFixture<ElementPedagogiqueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ElementPedagogiqueComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ElementPedagogiqueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
