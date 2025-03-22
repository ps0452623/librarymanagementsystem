import { Component, OnInit } from '@angular/core';
import { BookService } from '@services/book.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-booklist',
  templateUrl: './booklist.component.html',
  styleUrl: './booklist.component.scss'
})
export class BookListComponent implements OnInit {
  books$: Observable<any[]>;
  constructor(private bookService: BookService) {

  }


  ngOnInit(): void {
    this.getRoles();
  }

  // Fetch roles from API
  getRoles(): void {
    //this.books$ = this.bookService.booklist(); // ✅ Assign the observable directly
  }

}
