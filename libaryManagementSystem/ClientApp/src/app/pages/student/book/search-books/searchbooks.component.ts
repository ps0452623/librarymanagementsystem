import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookService } from '@services/book.service';
import { BranchService } from '@services/branch.service';
import { CourseService } from '@services/course.service';
import { ReservationService } from '@services/reservation.service';
import { environment } from 'environments/environment';
import { ToastRef, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-searchbooks',
  templateUrl: './searchbooks.component.html',
  styleUrl: './searchbooks.component.scss'
})
export class SearchBooksComponent implements OnInit {
  books$: any[];
  TotalCount: number = 0;
  PageNumber: number = 1;
  PageSize: number = 2;
  searchForm: FormGroup;
  courses$: Observable<any[]>;
  branches$: Observable<any[]> = new Observable();
  imageUrl = environment.imageUploadUrl;
  reserveBookForm!: FormGroup;
  selectedBook: any;
  modalInstance: any;


  constructor(private bookService: BookService, private toastr : ToastrService, private courseservice: CourseService,
    private branchservice: BranchService, private fb: FormBuilder, private reservationService: ReservationService) { }

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      Title: [''],
      Genre: [''],
      YearPublished: [''],
      Course: [''],  // Added course dropdown
      BranchName: ['']
    });
    this.loadCourses();
    this.GetBooks();
    this.loadBooks();


    this.reserveBookForm = this.fb.group({
      userId: ['0DB811F6-8B7A-4071-617C-08DD6A27CB1D', Validators.required],
      numberOfCopies: [1, [Validators.required, Validators.min(1)]],
      reservationDate: ['', Validators.required],
      returnDate: ['', Validators.required]
    });
  }
  selectBook(bookId: any) {
    this.selectedBook = bookId;
   
  }
  loadBooks() {
    this.bookService.GetBooks().subscribe((data) => {
      this.books$ = data;
    });
  }

  ReserveBook(): void {
    
    if (this.reserveBookForm.valid && this.selectedBook) {
      
      const reservationData = {
        bookId: this.selectedBook,
        userId: this.reserveBookForm.value.userId,
        noOfCopies: this.reserveBookForm.value.noOfCopies,
        reservationDate: this.reserveBookForm.value.reservationDate,
        returnDate: this.reserveBookForm.value.returnDate
      };

      console.log('Submitting reservation:', reservationData);
      this.reservationService.ReserveBook(reservationData).subscribe(
        (response: any) => {
          debugger;
          this.toastr.success('Book reserved successfully!');
          this.modalInstance?.hide();
          this.closemodal();

        },
        
      );


    } else {
      this.toastr.warning("Please Fill All The Required fields")
      console.log('Form is invalid:', this.reserveBookForm.errors);
    }
  }


  loadCourses(): void {
    this.courses$ = this.bookService.GetCourse(); // Fetch courses from API
  }

  loadBranches(): void {

    this.branches$ = this.branchservice.GetBranchesByCourse(this.searchForm.value.Course); // Fetch branches for the selected course
  }

  GetBooks(): void {
    const bookSearchRequest = {
      title: this.searchForm.get('Title').value, // Search by title
      genre: this.searchForm.get('Genre').value, // Filter by genre
      yearPublished: this.searchForm.get('YearPublished').value, // Filter by YearPublished
      courseId: this.searchForm.get('Course').value,
      branchName: this.searchForm.get('BranchName').value,
      sortBy: "Title", // Sorting Field
      isAscending: true, // Ascending/Descending Order
      pageNumber: this.PageNumber,
      pageSize: 2
    };


    this.bookService.GetFilteredBooks(bookSearchRequest).subscribe(response => {
      this.books$ = response.books;
      this.TotalCount = response.totalCount;
    });
  }

  getTotalPages(): number {
    return Math.ceil(this.TotalCount / this.PageSize);
  }

  onPageChange(newPage: number): void {
    if (newPage < 1 || newPage > this.getTotalPages()) return;
    this.PageNumber = newPage;
    this.GetBooks();
  }



  applyFilters(): void {
    this.PageNumber = 1; // Reset to first page when applying filters
    this.GetBooks();
  }
  closemodal(): void {
    const modalElement = document.getElementById('ReserveBooks');
    if (modalElement) {
      modalElement.classList.remove('show'); // Remove the 'show' class
      modalElement.setAttribute('aria-hidden', 'true'); // Set aria-hidden to true
      modalElement.style.display = 'none'; // Hide the modal
      const backdrop = document.querySelector('.modal-backdrop');
      if (backdrop) {
        backdrop.parentNode?.removeChild(backdrop);

      }
    }
  }
}