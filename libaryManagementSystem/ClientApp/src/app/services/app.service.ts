import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class AppService {
    public user: any = null;
    private apiUrl = 'https://localhost:7028/api/auth'; 
    constructor(
        private http: HttpClient,
        private router: Router,
        private toastr: ToastrService
    ) {}

    async registerWithEmail(email: string, password: string) {
        try {
            const result = await this.http.post(`${this.apiUrl}/Register`, { email, password }).toPromise();
            this.user = result;
            this.router.navigate(['/']);
            return result;
        } catch (error: any) {
            this.toastr.error(error.error.message || 'Registration failed');
            throw error;
        }
    }

    async loginWithEmail(email: string, password: string) {
        try {
            const result: any = await this.http.post(`${this.apiUrl}/Login`, { email, password }).toPromise();
            this.user = result;
            localStorage.setItem('token', result.token); // Store JWT Token
            this.router.navigate(['/']);
            return result;
        } catch (error: any) {
            this.toastr.error(error.error.message || 'Login failed');
        }
    }

    async getProfile() {
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                this.logout();
                return;
            }

            const user = await this.http.get(`${this.apiUrl}/profile`, {
                headers: { Authorization: `Bearer ${token}` }
            }).toPromise();

            this.user = user;
        } catch (error: any) {
            this.logout();
            this.toastr.error(error.error.message || 'Failed to fetch profile');
        }
    }

    async logout() {
        localStorage.removeItem('token');
        this.user = null;
        this.router.navigate(['/login']);
    }
}
