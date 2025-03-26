import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
private endpoint = 'Course';
private  apiurl = environment.baseUrl;
  constructor(private Http: HttpClient, private apiService : ApiServiceService) { }

 GetCourse(): Observable<any[]> {
    return this.apiService.get(`Course/GetAll`);
  }
}

