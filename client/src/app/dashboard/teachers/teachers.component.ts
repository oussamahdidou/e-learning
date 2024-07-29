import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrl: './teachers.component.css',
})
export class TeachersComponent implements OnInit {
  displayedColumns: string[] = ['userName', 'email', 'action'];
  @ViewChild(MatSort) sort!: MatSort;
  dataSource: MatTableDataSource<any>;
  teachers: any[] = [];

  constructor(private readonly dashboardservice: DashboardService) {
    this.dataSource = new MatTableDataSource(this.teachers);
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
  }
  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.dashboardservice.getteachers().subscribe(
      (response) => {
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
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  toggleApproval(teacher: any) {
    if (teacher.granted) {
      this.dashboardservice.removeaccessgrant(teacher.id).subscribe(
        (response) => {
          teacher.granted = !teacher.granted;
        },
        (error) => {}
      );
    } else {
      this.dashboardservice.grantaccess(teacher.id).subscribe(
        (response) => {
          teacher.granted = !teacher.granted;
        },
        (error) => {}
      );
    }
  }
}
