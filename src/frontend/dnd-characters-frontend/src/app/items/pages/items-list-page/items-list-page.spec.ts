import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ItemsListPage } from './items-list-page';

describe('ItemsListPage', () => {
  let component: ItemsListPage;
  let fixture: ComponentFixture<ItemsListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ItemsListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ItemsListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
