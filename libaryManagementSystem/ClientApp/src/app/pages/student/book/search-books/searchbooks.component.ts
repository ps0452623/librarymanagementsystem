import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BookService } from '@services/book.service';
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
  PageSize: number =2;
  searchForm: FormGroup;

  constructor(private bookService: BookService, private fb: FormBuilder) { }

  


  ngOnInit(): void {
    this.searchForm = this.fb.group({
      Title: [''],
      Genre: [''],
      YearPublished: [''],
      BranchName :['']

    });

    this.GetBooks(); 
  }

 
  GetBooks(): void {
    const bookSearchRequest = {
      title: this.searchForm.get('Title').value, // Search by title
      genre: this.searchForm.get('Genre').value, // Filter by genre
      yearPublished: this.searchForm.get('YearPublished').value, // Filter by YearPublished
      branchName: this.searchForm.get('BranchName').value, // Filter by Branch
      branchId: "00000000-0000-0000-0000-000000000000", // Replace with an actual GUID
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
} 