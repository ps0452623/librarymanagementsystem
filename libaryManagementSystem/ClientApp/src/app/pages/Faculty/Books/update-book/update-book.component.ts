import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '@services/book.service';
import { BranchService } from '@services/branch.service';
import { CourseService } from '@services/course.service';
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs';

@Component({
  selector: 'app-update-book',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ],
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.scss']
})
export class UpdateBookComponent implements OnInit {
  bookForm: FormGroup; 
  selectedFile: File | null = null;
  courses$: Observable<any[]>;
  branches$: Observable<any[]>;
  selectedCourseId: any;
  bookId:string | null = null; 

  
  
  constructor(private fb: FormBuilder,
     private bookservice: BookService,
     private courseservice: CourseService,
     private branchservice : BranchService,
  
        private toastr: ToastrService,
        private router: Router,
        private route: ActivatedRoute

      ) {
        this.bookForm = this.fb.group({
          CourseId: [null, Validators.required],
          BranchId: [null, Validators.required],
          Title: ['', Validators.required],
          Author: ['', Validators.required],
          Publisher: ['', Validators.required],
          Genre: ['', Validators.required],
          ISBN: ['', Validators.required],
          YearPublished: [null, Validators.required],
          CopiesAvailable: [null, Validators.required],
          BookShelfNumber: [null, Validators.required],
          Picture: [null]
        });
      }
      ngOnInit(): void {
        debugger;
        this.loadCourses();
        this.bookId = this.route.snapshot.params['id']; // Get the book ID from the route
        if (this.bookId) {
          this.loadBookDetails();
        }else {
          this.toastr.error('Book ID is missing.');
        }
      }
      loadCourses(): void {
        this.courses$ = this.courseservice.GetCourse();
      }
      onCourseChange(event: Event): void {
        debugger;
        const selectElement = event.target as HTMLSelectElement;
        const selectedValue = selectElement.value;
        this.selectedCourseId = selectedValue;
        this.bookForm.patchValue({ BranchId: null });
        this.loadBranches(this.selectedCourseId);
      }
    
      loadBranches(courseId: string): void {
        this.branches$ = this.branchservice.GetBranchesByCourse(courseId);
      }
    
      loadBookDetails(): void {
        if (this.bookId !== null) {
          this.bookservice.GetBooks().subscribe(
            (books) => {
              const bookToEdit = books.find((book) => book.id === this.bookId);
              if (bookToEdit) {
                this.bookForm.patchValue({
                  CourseId: bookToEdit.courseId,
                  BranchId: bookToEdit.branchId,
                  Title: bookToEdit.title,
                  Author: bookToEdit.author,
                  Publisher: bookToEdit.publisher,
                  Genre: bookToEdit.genre,
                  ISBN: bookToEdit.isbn,
                  YearPublished: bookToEdit.yearPublished,
                  CopiesAvailable: bookToEdit.copiesAvailable,
                  BookShelfNumber: bookToEdit.bookShelfNumber
                });
              } else {
                this.toastr.error('Book not found');
              }
            },
            (error) => {
              this.toastr.error('Failed to load book details');
            }
          );
        }
      }
    
      onFileSelected(event: any): void {
        const file: File = event.target.files[0];
        if (file) {
          this.selectedFile = file;
        }
      }
    
      updateBook(formData: FormData): void {
        debugger;
        if (this.bookId !== null) {
          this.bookservice.UpdateBook(this.bookId, formData).subscribe(
            (response) => {
              this.toastr.success('Book updated successfully');
              this.router.navigate(['/book-list']);
            },
            (error) => {
              this.toastr.error('Error updating book: ' + (error.message || 'Unknown error'));
            }
          );
        } else {
          this.toastr.error('Book ID is missing for update');
        }
      }
    
      onSubmit(): void {
        debugger;
        if (this.bookForm.valid) {
          const formData = new FormData();
          for (const field in this.bookForm.value) {
            if (this.bookForm.value.hasOwnProperty(field)) {
              formData.append(field, this.bookForm.value[field]);
            }
          }
          if (this.selectedFile) {
            formData.append('Picture', this.selectedFile, this.selectedFile.name);
          }
          this.updateBook(formData);
        } else {
          this.toastr.warning('Please fill out all required fields.');
        }
      }
    }