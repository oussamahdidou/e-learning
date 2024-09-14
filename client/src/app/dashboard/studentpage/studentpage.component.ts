import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { DashboardService } from '../../services/dashboard.service';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-studentpage',
  templateUrl: './studentpage.component.html',
  styleUrl: './studentpage.component.css',
})
export class StudentpageComponent implements OnInit {
  studentid!: string;
  student: any;
  constructor(
    private readonly route: ActivatedRoute,
    public readonly authservice: AuthService,
    private readonly dashboardservice: DashboardService
  ) {}
  delete() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        // Show a loading spinner
        Swal.fire({
          title: 'Deleting...',
          text: 'Please wait while we delete the user.',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          },
        });

        // Perform the delete operation
        this.authservice.deleteuser(this.studentid).subscribe(
          (response) => {
            Swal.fire('Deleted!', 'The user has been deleted.', 'success').then(
              () => {
                window.location.href = `/dashboard/teacherstable`;
              }
            );
          },
          (error) => {
            Swal.fire('Error!', `${error.error}`, 'error');
          }
        );
      }
    });
  }
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.studentid = params['id'];
      this.dashboardservice.getStudentbyid(this.studentid).subscribe(
        (response) => {
          this.student = response;
          console.log(response);
        },
        (error) => {}
      );
    });
  }
  toggleApproval() {
    if (this.student.granted) {
      this.dashboardservice.removeaccessgrantstudent(this.student.id).subscribe(
        (response) => {
          this.student.granted = !this.student.granted;
        },
        (error) => {}
      );
    } else {
      this.dashboardservice.grantaccessstudent(this.student.id).subscribe(
        (response) => {
          this.student.granted = !this.student.granted;
        },
        (error) => {}
      );
    }
  }
}
