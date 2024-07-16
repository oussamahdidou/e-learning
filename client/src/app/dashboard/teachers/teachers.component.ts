import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrl: './teachers.component.css',
})
export class TeachersComponent implements OnInit {
  displayedColumns: string[] = ['userName', 'email', 'action'];
  @ViewChild(MatSort) sort!: MatSort;
  dataSource: MatTableDataSource<any>;
  teachers: any[] = [
    { userName: 'john_doe', email: 'john_doe@example.com', status: false },
    {
      userName: 'jane_smith',
      email: 'jane_smith@example.com',
      status: false,
    },
    { userName: 'sam_brown', email: 'sam_brown@example.com', status: true },
  ];

  constructor() {
    this.dataSource = new MatTableDataSource(this.teachers);
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
  }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {}

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  toggleApproval(teacher: any) {
    teacher.status = !teacher.status;
  }
}
