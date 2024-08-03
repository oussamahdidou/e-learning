import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {


  handleError(error: any, customMessage: string) {
    console.error(customMessage, error);
    Swal.fire({
      title: 'Error!',
      text: customMessage,
      icon: 'error',
      confirmButtonText: 'OK'
    });
  }
}
