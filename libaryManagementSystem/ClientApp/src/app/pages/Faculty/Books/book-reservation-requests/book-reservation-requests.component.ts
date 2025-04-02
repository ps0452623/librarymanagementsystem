import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BookService } from '@services/book.service';
import { ReservationService } from '@services/reservation.service';
import { StudentService } from '@services/student.service';
import { Observable } from 'rxjs';

@Component({
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  selector: 'app-book-reservation-requests',
  templateUrl: './book-reservation-requests.component.html',
  styleUrls: ['./book-reservation-requests.component.scss'], // Corrected styleUrl to styleUrls for array syntax
  standalone: true
})
export class BookReservationRequestsComponent implements OnInit {
  bookForm: FormGroup;
  students$: Observable<any[]>;
  books$: Observable<any>;
  studentId: any; 

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
            private route: ActivatedRoute,
            private router: Router,
  private bookService : BookService,
  private reservationService: ReservationService
  ) {}
  ngOnInit(): void {
this.studentId = this.route.snapshot.params['id'];
this.getStudentDetails(this.studentId);
 }

 getStudentDetails(studentId: number) {
  this.students$ = this.studentService.GetStudentById(studentId); // Fetch student details by studentId
}




    changeRequestStatus(bookId: any, status: any) {
      this.reservationService.updateBookStatus(bookId, status).subscribe(
        (response) => {
          // Handle success (show success message, reload data, etc.)
          console.log('Request status updated successfully:', response);
          this.getStudentDetails(this.studentId)
        },
        (error) => {
          // Handle error (show error message)
          console.error('Error updating request status:', error);
        }
      );
}}

