import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterService } from '@services/register.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-register',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterModule],
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    roles$: Observable<any[]>;

    constructor(
        private fb: FormBuilder,
        private registerService: RegisterService,
        private toastr: ToastrService
    ) {
        this.registerForm = this.fb.group(
            {
                firstName: ['', [Validators.required, Validators.minLength(3)]], 
                lastName: ['', [Validators.required, Validators.minLength(3)]], 
                role: ['', [Validators.required]], 
                phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]], 
                email: ['', [Validators.required, Validators.email]], 
                password: ['', [Validators.required, Validators.minLength(6)]],  
                confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
            },
            { validator: this.passwordMatchValidator }
        );
    }

    ngOnInit(): void {
        this.getRoles();
      }
    
      // Fetch roles from API
      getRoles(): void {
        this.roles$ = this.registerService.getRoles(); // ✅ Assign the observable directly
    }
    
   
    private passwordMatchValidator(form: AbstractControl): ValidationErrors | null {
        const password = form.get('password')?.value;
        const confirmPassword = form.get('confirmPassword')?.value;
        return password === confirmPassword ? null : { passwordMismatch: true };
    }
    onSubmit(): void {
        debugger;
        if (this.registerForm.invalid) {
            this.toastr.error("Please fill out the form correctly.", "Validation Error");
            return;
        }
        console.log("Form Data:", this.registerForm.value); 

        this.registerService.RegisterUser(this.registerForm.value).subscribe({
            next: (response) => {
                console.log("API Response:", response); 

                this.toastr.success("Registration successful!", "Success");
                console.log("User registered:", response);
                this.registerForm.reset();
            },
            error: (error) => {
                this.toastr.error("Registration failed. Please try again.", "Error");
                console.error("Registration Error:", error);
            }
            
        }
    );
    }
}
