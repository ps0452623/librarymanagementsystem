import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ApiServiceService } from './api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
apiUrl= environment.baseUrl;
endpoint= "Student"
  constructor(private apiService:ApiServiceService) {
  
}
GetStudentById(Id: any): Observable<any> {
  // Make the API call to fetch a course by its ID
  return this.apiService.get(`${this.endpoint}/${Id}`);
}

GetAllStudents():Observable<any[]>{
  return this.apiService.get(`${this.endpoint}/GetAll`)
   }
}
