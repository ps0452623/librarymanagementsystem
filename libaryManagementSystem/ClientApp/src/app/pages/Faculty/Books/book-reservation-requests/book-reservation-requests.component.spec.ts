import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookReservationRequestsComponent } from './book-reservation-requests.component';

describe('BookReservationRequestsComponent', () => {
  let component: BookReservationRequestsComponent;
  let fixture: ComponentFixture<BookReservationRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BookReservationRequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookReservationRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
