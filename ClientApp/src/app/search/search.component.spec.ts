import { ComponentFixture, TestBed } from '@angular/core/testing';

import { searchComponent } from './search.component';

describe('SearchComponent', () => {
  let component: searchComponent;
  let fixture: ComponentFixture<searchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ searchComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(searchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
