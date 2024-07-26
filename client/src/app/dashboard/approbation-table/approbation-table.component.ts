import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-approbation-table',
  templateUrl: './approbation-table.component.html',
  styleUrl: './approbation-table.component.css',
})
export class ApprobationTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'type', 'name', 'plus'];
  dataSource: MatTableDataSource<any>;

  objects: any[] = [
    { id: 1, type: 'controle', name: 'math' },
    { id: 2, type: 'quiz', name: 'physics' },
    { id: 3, type: 'chapitre', name: 'informatique' },
    { id: 4, type: 'chapitre', name: 'finance ' },
    { id: 5, type: 'controle', name: 'comptabilite' },
  ];

  @ViewChild(MatSort) sort!: MatSort;

  constructor() {
    this.dataSource = new MatTableDataSource(this.objects);
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
    this.objects = this.objects.filter((institution) => institution.id !== id);
    this.dataSource.data = this.objects;
  }
}
