import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { DashboardService } from '../../services/dashboard.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-teacher-profile',
  templateUrl: './teacher-profile.component.html',
  styleUrl: './teacher-profile.component.css',
})
export class TeacherProfileComponent implements OnInit {
  displayedColumns: string[] = ['id', 'type', 'name', 'plus'];
  dataSource: MatTableDataSource<any>;

  objects: any[] = [];

  @ViewChild(MatSort) sort!: MatSort;

  teacherid!: string;
  teacher: any;
  constructor(
    private readonly route: ActivatedRoute,
    public readonly authservice: AuthService,
    private readonly dashboardservice: DashboardService
  ) {
    this.dataSource = new MatTableDataSource(this.objects);
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
  }
  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.teacherid = params['id'];
      this.dashboardservice.getTeacherbyid(this.teacherid).subscribe(
        (response) => {
          this.teacher = response;
        },
        (error) => {}
      );
      this.dashboardservice.getteacherobjects(this.teacherid).subscribe(
        (response) => {
          console.log(response);
          this.dataSource = new MatTableDataSource(response);
          this.dataSource.sortingDataAccessor = (item, property) => {
            switch (property) {
              default:
                return item[property];
            }
          };
          this.dataSource.sort = this.sort;
        },
        (error) => {}
      );
    });
  }
  toggleApproval() {
    if (this.teacher.granted) {
      this.dashboardservice.removeaccessgrant(this.teacher.id).subscribe(
        (response) => {
          this.teacher.granted = !this.teacher.granted;
        },
        (error) => {}
      );
    } else {
      this.dashboardservice.grantaccess(this.teacher.id).subscribe(
        (response) => {
          this.teacher.granted = !this.teacher.granted;
        },
        (error) => {}
      );
    }
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
