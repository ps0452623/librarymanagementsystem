import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { BookService } from '@services/book.service';
import { CourseService } from '@services/course.service';
import { BranchService } from '@services/branch.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-bookadd',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterModule],

  templateUrl: './bookadd.component.html',
  styleUrls: ['./bookadd.component.scss']
})
export class BookAddComponent implements OnInit {
  bookForm: FormGroup; 
  selectedFile: File | null = null;
  courses$: Observable<any[]>;
  branches$: Observable<any[]>;
  selectedCourseId: any;
  
  
  constructor(private fb: FormBuilder,
     private bookservice: BookService,
     private courseservice: CourseService,
     private branchservice : BranchService,
  
        private toastr: ToastrService,
        private router: Router // Inject Router

      ) {
 
    this.bookForm = this.fb.group({
      CourseId: [null, Validators.required],
      BranchId: [null, Validators.required] ,
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
    this.loadCourses();
   
  }
  

  loadCourses(): void {
    this.courses$ = this.courseservice.GetCourse();
    
  }

  

    onCourseChange(event: Event): void {
     
      const selectElement = event.target as HTMLSelectElement;
      const selectedValue = selectElement.value;
      console.log('Selected value from dropdown:', selectedValue);

      this.selectedCourseId = selectedValue;
      
      console.log('Converted selected course ID:', this.selectedCourseId);

    
    this.bookForm.patchValue({ branchId: null });
 
      this.loadBranches(this.selectedCourseId);
    
      console.log('Valid selected course ID:', this.selectedCourseId);

    }
  
    loadBranches(courseId: string): void {
    
      this.branches$ = this.branchservice.GetBranchesByCourse(courseId)}

         
    onFileSelected(event: any): void {
   
      const file: File = event.target.files[0];
      if (file) {
        this.selectedFile = file;
    }
  }
  onSubmit(): void {
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
     else {
        this.addBook(formData);
      }
      

      this.bookservice.AddBooks(formData).subscribe(
        (response) => {
         
          this.toastr.success('Book added successfully');
          // Optionally navigate or reset the form
          this.bookForm.reset();
        },
        (error) => {
        
          this.toastr.error('Error adding book'+ (error.message || 'Unknown error'));
        }
      );

    } else {
      this.toastr.warning('Please fill out all required fields.');
    }
    
    
  }
  

addBook(formData: FormData): void {
    this.bookservice.AddBooks(formData).subscribe(
      (response) => {
        this.toastr.success('Book added successfully');
        this.bookForm.reset();
        // Navigate back to the book list or another appropriate page
        this.router.navigate(['/book-list']);
      },
      (error) => {
        this.toastr.error('Error adding book: ' + (error.message || 'Unknown error'));
      }
    );
  }
}