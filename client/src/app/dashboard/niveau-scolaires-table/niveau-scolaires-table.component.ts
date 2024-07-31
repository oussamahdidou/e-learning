import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DashboardService } from '../../services/dashboard.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-niveau-scolaires-table',
  templateUrl: './niveau-scolaires-table.component.html',
  styleUrl: './niveau-scolaires-table.component.css',
})
export class NiveauScolairesTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'institution', 'nom', 'action', 'plus'];
  dataSource: MatTableDataSource<any>;
  niveauscolaireid!: number;
  niveauscolaires: any[] = [];
  instutution: string = '';
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private readonly dashboardservice: DashboardService,
    private readonly route: ActivatedRoute,
    public authservice: AuthService
  ) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.niveauscolaireid = params['id'];
      this.dashboardservice.getniveauscolaires(this.niveauscolaireid).subscribe(
        (response) => {
          this.niveauscolaires = response.niveauScolaires;
          this.dataSource = new MatTableDataSource(this.niveauscolaires);
          this.instutution = response.nom;
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
  addNiveauScolaire(): void {
    Swal.fire({
      title: 'Add New niveauscolaire',
      input: 'text',
      inputLabel: 'niveauscolaire Name',
      inputPlaceholder: 'Enter the name of the niveauscolaire',
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
        this.dashboardservice
          .createniveauscolaire(result.value, this.niveauscolaireid)
          .subscribe(
            (response) => {
              this.niveauscolaires.push(response);
              this.dataSource.data = [...this.niveauscolaires];
              Swal.fire(
                'Added!',
                'New niveauscolaire has been added.',
                'success'
              );
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
    this.niveauscolaires = this.niveauscolaires.filter(
      (niveauscolaire) => niveauscolaire.id !== id
    );
    this.dataSource.data = this.niveauscolaires;
  }

  edit(niveauscolaire: any): void {
    Swal.fire({
      title: 'Edit niveauscolaire Name',
      input: 'text',
      inputLabel: 'niveauscolaire Name',
      inputValue: niveauscolaire.nom,
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
          .updateniveauscolaire(result.value, niveauscolaire.id)
          .subscribe(
            (response) => {
              niveauscolaire.nom = response.nom;
              console.log(response);
              this.dataSource.data = [...this.niveauscolaires]; // Refresh the table data
              Swal.fire(
                'Saved!',
                'niveauscolaire name has been updated.',
                'success'
              );
            },
            (error) => {}
          );
      }
    });
  }
}
