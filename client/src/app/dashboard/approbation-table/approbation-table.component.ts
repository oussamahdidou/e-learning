import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-approbation-table',
  templateUrl: './approbation-table.component.html',
  styleUrl: './approbation-table.component.css',
})
export class ApprobationTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'type', 'name', 'plus'];
  dataSource: MatTableDataSource<any>;

  objects: any[] = [];

  @ViewChild(MatSort) sort!: MatSort;

  constructor(private readonly dashboardservice: DashboardService) {
    this.dataSource = new MatTableDataSource(this.objects);
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
  }

  ngOnInit(): void {
    this.dashboardservice.getobjectspourapprouver().subscribe(
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
  }
  ngAfterViewInit(): void {}
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  delete(id: number): void {
    this.objects = this.objects.filter((institution) => institution.id !== id);
    this.dataSource.data = this.objects;
  }
}
