import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BookService } from '@services/book.service';
import { BranchService } from '@services/branch.service';
import { CourseService } from '@services/course.service';
import { environment } from 'environments/environment';
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs';

@Component({
  selector: 'app-update-book',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.scss']
})
export class UpdateBookComponent implements OnInit {
  bookForm: FormGroup; 
  filePreview: string | ArrayBuffer | null = null;  // Define filePreview variable
  imageUrl= environment.imageUploadUrl;

  selectedFile: File | null = null;
  courses$: Observable<any[]>;
  branches$: Observable<any[]>;
  CourseId: any;
  selectedCourseId: any;
  bookId:any | null = null; 
bookToEdit:any;
Picture;
  
  
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
        this.loadBookDetails();  
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
        
          this.bookservice.GetBookById(this.bookId).subscribe(
            (book) => {
              debugger
              this.bookToEdit = book;
            
                this.selectedCourseId = this.bookToEdit.courseId;
                this.bookForm.patchValue({
                
                  CourseId: this.bookToEdit.courseId,
                  BranchId: this.bookToEdit.branchId,
                  Title: this.bookToEdit.title,
                  Author: this.bookToEdit.author,
                  Publisher: this.bookToEdit.publisher,
                  Genre: this.bookToEdit.genre,
                  ISBN: this.bookToEdit.isbn,
                  YearPublished: this.bookToEdit.yearPublished,
                  CopiesAvailable: this.bookToEdit.copiesAvailable,
                  BookShelfNumber: this.bookToEdit.bookShelfNumber,
                  Picture: this.bookToEdit.Picture
                });
                
                if (this.bookToEdit.picture) {
                  debugger
                  this.filePreview = this.bookToEdit.picture;
                }
                this.loadBranches(this.selectedCourseId);
             
            },
            (error) => {
              this.toastr.error('Failed to load book details');
            }
          );
        
      }
    
      onFileSelected(event: any): void {
        const input = event.target as HTMLInputElement;
    if (input && input.files && input.files[0]) {
      this.selectedFile = input.files[0];

      // Preview the selected image
      const reader = new FileReader();
      reader.onload = (e) => this.filePreview = reader.result;
      reader.readAsDataURL(this.selectedFile);
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