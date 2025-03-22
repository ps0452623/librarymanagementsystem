import { Component, OnInit } from '@angular/core';
import { Book, BookService } from '@services/book.service';


@Component({
  selector: 'app-books-list',
  templateUrl:'./book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  loading = true;

  constructor(private booksService: BookService) {}

  ngOnInit(): void {
    this.fetchBooks();
  }

  fetchBooks(): void {
    this.booksService.getBooks().subscribe({
      next: (data) => {
        this.books = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching books:', err);
        this.loading = false;
      },
    });
  }
}
