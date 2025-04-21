import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Form } from '@profabric/angular-components';
import { BookService } from '@services/book.service';
import { environment } from 'environments/environment';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-booklist',
  templateUrl: './booklist.component.html',
  styleUrl: './booklist.component.scss'
})
export class BookListComponent implements OnInit {
  books$: Observable<any[]>;
  imageForm : FormGroup;
  imageUrl= environment.imageUploadUrl;
 
 
  constructor(private bookService: BookService,
            private toastr: ToastrService
    
  ) {
    
    this.imageForm = new FormGroup({
      image: new FormControl(null)
    });

  }


  ngOnInit(): void {

    this.GetBooks();
  }

  GetBooks(): void {
    this.books$ = this.bookService.GetBooks();
  }
  updateBook(bookId: any): void {
    console.log('Update book with ID:', bookId);
}
DeleteBook(bookId: any): void {
  debugger;
  
  Swal.fire({
    title: 'Are you sure?',
    text: 'Do you really want to delete this book?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'Cancel',
    reverseButtons: true,
    allowOutsideClick: false
  }).then((result) => {
    if (result.isConfirmed) {
      this.bookService.DeleteBook(bookId).subscribe(
        (response: any) => {
          this.GetBooks();
          console.log("API Response:", response);

          Swal.fire({
            icon: 'success',
            title: 'Deleted!',
            text: 'The book has been deleted successfully.',
            showConfirmButton: false,
            timer: 2000
          });
        },
        (error) => {
          console.error('There was an error deleting the book!', error);

          Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: 'There was an error deleting the book.',
            showConfirmButton: false,
            timer: 1500
          });
        }
      );
    } else {
      Swal.fire({
        icon: 'info',
        title: 'Canceled',
        text: 'Book deletion has been canceled.',
        showConfirmButton: false,
        timer: 1500
      });
}})}}