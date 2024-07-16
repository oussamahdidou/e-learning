import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-modules-table',
  templateUrl: './modules-table.component.html',
  styleUrl: './modules-table.component.css',
})
export class ModulesTableComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'niveauscolaire',
    'name',
    'action',
    'plus',
  ];
  dataSource: MatTableDataSource<any>;

  institutions: any[] = [
    { id: 1, niveauscolaire: 'semester1', name: 'math' },
    { id: 2, niveauscolaire: 'semester1', name: 'physics' },
    { id: 3, niveauscolaire: 'semester1', name: 'informatique' },
    { id: 4, niveauscolaire: 'semester1', name: 'finance ' },
    { id: 5, niveauscolaire: 'semester1', name: 'comptabilite' },
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
