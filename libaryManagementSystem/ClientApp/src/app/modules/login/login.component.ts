import {
    Component,
    OnInit,
    OnDestroy,
    Renderer2,
    HostBinding
} from '@angular/core';
import {UntypedFormGroup, UntypedFormControl, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {AppService} from '@services/app.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
    @HostBinding('class') class = 'login-box';
    public loginForm: UntypedFormGroup;
    public isAuthLoading = false;
   

    constructor(
        private renderer: Renderer2,
        private toastr: ToastrService,
        private appService: AppService
    ) {}

    ngOnInit() {
        this.renderer.addClass(document.body, 'login-page'); 

        this.loginForm = new UntypedFormGroup({
            email: new UntypedFormControl('sakshi@gmail.com', [
                Validators.required,
                Validators.email 
            ]),
            password: new UntypedFormControl('Sakshi@123', [
                Validators.required,
                Validators.minLength(6)
            ])
        });
    }

   async loginByAuth() {
    
    if (this.loginForm.valid) {
        this.isAuthLoading = true;
        console.log('Login form values:', this.loginForm.value);  // Log form data

        try {
            const response = await this.appService.loginWithEmail(
                this.loginForm.value.email,
                this.loginForm.value.password
            );
            console.log('Login response:', response);  // Log API response
            this.toastr.success('Login successful!');
        } catch (error) {
            console.error('Login error:', error);  // Log error details
            this.toastr.error('Invalid email or password.');
        }

        this.isAuthLoading = false;
    } else {
        console.warn('Form validation failed', this.loginForm.errors); // Log validation errors
        this.toastr.error('Please enter valid credentials.');
    }
}


    ngOnDestroy() {
        this.renderer.removeClass(
            document.querySelector('app-root'),
            'login-page'
        );
    }
}
