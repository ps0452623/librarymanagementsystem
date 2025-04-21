import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
 endpoint : 'Branch'
 private apiurl = environment.baseUrl

  constructor(private httpclient : HttpClient, private apiservice : ApiServiceService) { }
  
  GetBranchesByCourse(courseId : any): Observable<any[]>{
    return this.apiservice.get(`Branch/GetByCourse/${courseId}`)  
  }
}
