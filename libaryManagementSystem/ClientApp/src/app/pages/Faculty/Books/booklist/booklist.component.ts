import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Form } from '@profabric/angular-components';
import { BookService } from '@services/book.service';
import { environment } from 'environments/environment';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

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

  // Fetch books from API
  GetBooks(): void {
    this.books$ = this.bookService.GetBooks(); // ✅ Assign the observable directly
  }
  updateBook(bookId: any): void {
    console.log('Update book with ID:', bookId);
}
DeleteBook(bookId: any): void {
  debugger;
  
    const toastref = this.toastr.info('Are you sure you want to delete this book?','Delete Book',{
      closeButton : true,
      tapToDismiss: false,
      timeOut: 0,
       positionClass: 'toast-top-center',
      extendedTimeOut:0

    });
  
  setTimeout(() => {
    const confirmDelete = confirm('Are you sure you want to delete this book?'); // Native confirmation dialog
    if (confirmDelete) {
      this.bookService.DeleteBook(bookId).subscribe(
        (response: any) => {
          this.GetBooks();
          console.log("API Response:", response);
          this.toastr.success("Book Deleted Successfully", "Success");
        },
        (error) => {
          console.error('There was an error deleting the book!', error);
          this.toastr.error("There was an Error in Deleting Book");
        }
      );
    } else {
      this.toastr.info("Book deletion canceled", "Canceled");
    }
  }, 2000); // Wait for 2 seconds before showing the confirmation prompt
}}
  // }, timeout);){
    
      

  //   this.bookService.DeleteBook(bookId).subscribe( 
     
  //     (response: any) => {
  //        this.GetBooks(); 
  //        console.log("API Response:", response); 

  //        this.toastr.success("Book Deleted Successfully", "Success");
         
  //     },
  //   ( error) => {
  //     debugger;
  //     console.error('There was an error deleting the book!', error);
  //     this.toastr.error("There was an Error in Deleting Book")    }
  // );}}}