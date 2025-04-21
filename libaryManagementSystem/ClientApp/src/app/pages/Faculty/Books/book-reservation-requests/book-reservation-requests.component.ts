import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BookReservationStatusService } from '@services/book-reservation-status.service';
import { BookService } from '@services/book.service';
import { ReservationService } from '@services/reservation.service';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  selector: 'app-book-reservation-requests',
  templateUrl: './book-reservation-requests.component.html',
  styleUrls: ['./book-reservation-requests.component.scss'], // Corrected styleUrl to styleUrls for array syntax
  standalone: true
})
export class BookReservationRequestsComponent implements OnInit {
   bookForm: FormGroup;
  reservations$: Observable<any[]>;
  selectedReservation: any;
  reservationId:any | null = null; 
  action:any;
  imageUrl= environment.imageUploadUrl;
  studentId: any;
  statuses: any[] = [];
  updatedCopies;
  bookCopies;
  showModal: boolean = false;
  
  totalCount: number = 0;
  pageNumber: number = 1;
  pageSize: number = 5;


  constructor(private bookService: BookService,
    private fb: FormBuilder,
    private route : ActivatedRoute,
  private reservationService: ReservationService,
  private bookReservationService: BookReservationStatusService, 
  ) {}
  
  
    ngOnInit(): void {
      this.bookForm = this.fb.group({
        UserName: [''],
        bookTitle: [''],
        RequestedDate: [''],
        CreatedOn: [''],
        ReturnDate: [''],
        NumberOfCopies:[''],
         status: [''] 
      });

      
      this.getStatuses();
      
      this.getReservationRequests();
      this.getAllReservations();
      const reservationId = this.route.snapshot.params['id'];
      this.reservationId = reservationId;
      debugger;
      
   
  }
  getReservationRequests(): void {
    debugger;

    const searchReservation = {
      UserName: this.bookForm.value.UserName,
      bookTitle: this.bookForm.value.bookTitle,
      CreatedOn: this.bookForm.value.CreatedOn,
      reservationDate: this.bookForm.value.reservationDate,
      returnDate: this.bookForm.value.returnDate,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      status: this.bookForm.value.status.id
    };

    this.reservationService.GetFilteredReservations(searchReservation).subscribe((res: any) => {
      debugger;
      this.getReservationRequests = res.reservations;
      this.totalCount = res.totalCount;
      console.log("Filtered Reservations", searchReservation)
    });
  }

  applyFilters(): void {
    debugger;
    this.pageNumber = 1;
    this.getReservationRequests();
  }

  getTotalPages(): number {

    return Math.ceil(this.totalCount / this.pageSize);
  }

  onPageChange(newPage: number): void {
    if (newPage < 1 || newPage > this.getTotalPages()) return;
    this.pageNumber = newPage;
    this.getAllReservations();
  }

  getStatusIdByName(statusName: string): number | null {
    debugger;
    const status = this.statuses.find((status) => status.statusName === statusName);
    return status ? status.id : null;
  }
getAllReservations(): void {
  debugger;
  this.reservations$ = this.reservationService.GetAllReservations();
  

}

getStatuses(): void {
  this.reservationService.GetStatuses().subscribe(
    (statuses) => {
      this.statuses = statuses; // Set the statuses returned from the API
    },
    (error) => {
      console.error('Error fetching statuses:', error);
      Swal.fire('Error!', 'Failed to load reservation statuses.', 'error');
    }
  );
}
UpdateReservation(reservation: any): void {
  this.selectedReservation = reservation;
  this.reservationId = reservation.id;
  this.bookForm.patchValue({
    UserName: reservation.UserName,
    bookTitle: reservation.bookTitle,
    reservationDate: reservation.reservationDate,
    returnDate: reservation.returnDate,
    numberOfCopies: reservation.numberOfCopies,
    status: reservation.status,
    CreatedOn: reservation.CreatedOn
  });
  this.showModal = true;
}

updateReservation(): void {
  if (this.bookForm.valid) {
    const formValues = this.bookForm.value;

    const statusId = this.getStatusIdByName(formValues.status);
    if (!statusId) {
      Swal.fire('Error!', 'Invalid status selected.', 'error');
      return;
    }

    const updatedReservation = {
      ...this.selectedReservation,
      UserName: formValues.UserName,
      BookTitle: formValues.bookTitle,
      CreatedOn:formValues.CreatedOn,
      reservationDate: formValues.reservationDate,
      returnDate: formValues.returnDate,
      numberOfCopies: formValues.numberOfCopies,
      statusId: statusId

      
    };

    this.reservationService.UpdateReservationStatus(this.reservationId, updatedReservation).subscribe(
      (response) => {
        Swal.fire('Success!', 'Reservation updated successfully!', 'success');
        this.getAllReservations();
        this.showModal = false;
      },
      (error) => {
        Swal.fire('Error!', 'Failed to update reservation.', 'error');
        console.error(error);
      }
    );
  }
}
closeModal(): void {
  this.showModal = false;
}



  
updateReservationStatus(reservation: any, action: any): void {
  debugger;
  const updatedReservation = {
    ...this.selectedReservation,
    ...this.bookForm.value
  };
  this.reservationService.UpdateReservationStatus(this.reservationId, updatedReservation).subscribe(
    (response) => {}
  ); 
  debugger;
  console.log('Available statuses:', this.statuses); 
  console.log('Action:', action);
  const statusId = this.getStatusIdByName(action); // Use the helper method
  if (!statusId) {
      Swal.fire('Error!', `Status ID not found for the action: ${action}`, 'error');
      return;
    }
    console.log('Selected status:', statusId); 


  Swal.fire({
    title: 'Are you sure?',
    text: `You are about to ${action} this reservation!`,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: action === 'Accepted' ? 'Yes, approve it!' : 'Yes, decline it!',
    cancelButtonText: 'Cancel',
    reverseButtons: true
  }).then((result) => {
    debugger;
    if (result.isConfirmed) {
      console.log('Before update: Reservation Status:', reservation.status);
      let updatedCopies = reservation.numberOfCopies;
    if (action=='Accepted'){
      updatedCopies=-(reservation.numberOfCopies);}
    else if(action=='Declined')
    {
      updatedCopies=reservation.numberOfCopies
    }
    console.log("Number of Copies Available :-", updatedCopies)
    
      this.reservationService.UpdateReservationStatus(reservation.id, statusId).subscribe(
        (response) => {
          debugger
          Swal.fire(
            'Success!',
            `${action} request status updated successfully.`,
            'success'
          );
          this.getAllReservations(); 
        },
        (error) => {
          Swal.fire(
            'Error!',
            `${action} request status failed: ${error}`,
            'error'
          );
        }
      );
 }});}}