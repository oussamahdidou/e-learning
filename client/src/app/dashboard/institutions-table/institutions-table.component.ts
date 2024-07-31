import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { DashboardService } from '../../services/dashboard.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-institutions-table',
  templateUrl: './institutions-table.component.html',
  styleUrls: ['./institutions-table.component.css'],
})
export class InstitutionsTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'nom', 'action', 'plus'];
  dataSource!: MatTableDataSource<any>;
  institutions: any[] = [];

  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private readonly dashboardservice: DashboardService,
    public authservice: AuthService
  ) {}

  ngOnInit(): void {
    this.dashboardservice.getinstitutions().subscribe(
      (response) => {
        this.institutions = response;
        this.dataSource = new MatTableDataSource(this.institutions);
        this.dataSource.sort = this.sort;
        this.dataSource.sortingDataAccessor = (item, property) => {
          switch (property) {
            default:
              return item[property];
          }
        };
      },
      (error) => {}
    );
  }

  addInstitution(): void {
    Swal.fire({
      title: 'Add New Institution',
      input: 'text',
      inputLabel: 'Institution Name',
      inputPlaceholder: 'Enter the name of the institution',
      showCancelButton: true,
      confirmButtonText: 'Add',
      cancelButtonText: 'Cancel',
      preConfirm: (newName) => {
        if (!newName) {
          Swal.showValidationMessage('Please enter a valid name');
        }
        return newName;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice.createinstitution(result.value).subscribe(
          (response) => {
            this.institutions.push(response);
            this.dataSource.data = [...this.institutions];
            Swal.fire('Added!', 'New institution has been added.', 'success');
          },
          (error) => {}
        );
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  delete(id: number): void {
    this.institutions = this.institutions.filter(
      (institution) => institution.id !== id
    );
    this.dataSource.data = [...this.institutions];
  }

  edit(institution: any): void {
    Swal.fire({
      title: 'Edit Institution Name',
      input: 'text',
      inputLabel: 'Institution Name',
      inputValue: institution.nom,
      showCancelButton: true,
      confirmButtonText: 'Save',
      cancelButtonText: 'Cancel',
      preConfirm: (newName) => {
        if (!newName) {
          Swal.showValidationMessage('Please enter a valid name');
        }
        return newName;
      },
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice
          .updateinstitution(result.value, institution.id)
          .subscribe(
            (response) => {
              institution.nom = response.nom;
              console.log(response);
              this.dataSource.data = [...this.institutions]; // Refresh the table data
              Swal.fire(
                'Saved!',
                'Institution name has been updated.',
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }
}
