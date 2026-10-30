import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WeaponsListPage } from './weapons-list-page';

describe('WeaponsListPage', () => {
  let component: WeaponsListPage;
  let fixture: ComponentFixture<WeaponsListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeaponsListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(WeaponsListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
