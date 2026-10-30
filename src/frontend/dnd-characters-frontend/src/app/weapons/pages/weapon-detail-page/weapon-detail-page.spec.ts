import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WeaponDetailPage } from './weapon-detail-page';

describe('WeaponDetailPage', () => {
  let component: WeaponDetailPage;
  let fixture: ComponentFixture<WeaponDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeaponDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(WeaponDetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
