import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ShopDetailPage } from './shop-detail-page';

describe('ShopDetailPage', () => {
  let component: ShopDetailPage;
  let fixture: ComponentFixture<ShopDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShopDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ShopDetailPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
