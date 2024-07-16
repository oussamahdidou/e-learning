import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-niveau-scolaires-table',
  templateUrl: './niveau-scolaires-table.component.html',
  styleUrl: './niveau-scolaires-table.component.css',
})
export class NiveauScolairesTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'institution', 'name', 'action', 'plus'];
  dataSource: MatTableDataSource<any>;

  institutions: any[] = [
    { id: 1, institution: 'MIT', name: 'semestre 1' },
    { id: 2, institution: 'MIT', name: 'semestre 2' },
    { id: 3, institution: 'MIT', name: 'semestre 3' },
    { id: 4, institution: '32', name: 'semestre 4' },
    { id: 5, institution: 'MIT', name: 'semestre 5' },
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
