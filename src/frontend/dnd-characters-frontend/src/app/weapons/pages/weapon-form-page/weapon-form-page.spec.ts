import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WeaponFormPage } from './weapon-form-page';

describe('WeaponFormPage', () => {
  let component: WeaponFormPage;
  let fixture: ComponentFixture<WeaponFormPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeaponFormPage],
    }).compileComponents();

    fixture = TestBed.createComponent(WeaponFormPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
