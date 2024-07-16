import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-institutions-table',
  templateUrl: './institutions-table.component.html',
  styleUrls: ['./institutions-table.component.css'],
})
export class InstitutionsTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'action', 'plus'];
  dataSource: MatTableDataSource<any>;

  institutions: any[] = [
    { id: 1, name: 'Harvard University' },
    { id: 2, name: 'Stanford University' },
    { id: 3, name: 'MIT' },
    { id: 4, name: 'University of California, Berkeley' },
    { id: 5, name: 'University of Oxford' },
  ];

  @ViewChild(MatSort) sort!: MatSort;

  constructor() {
    this.dataSource = new MatTableDataSource(this.institutions);
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
  }

  ngOnInit(): void {}
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  delete(id: number): void {
    this.institutions = this.institutions.filter(
      (institution) => institution.id !== id
    );
    this.dataSource.data = this.institutions;
  }

  edit(id: number): void {
    // Implement edit functionality
  }
}
