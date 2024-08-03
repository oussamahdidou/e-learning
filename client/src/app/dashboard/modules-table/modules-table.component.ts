import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { DashboardService } from '../../services/dashboard.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-modules-table',
  templateUrl: './modules-table.component.html',
  styleUrl: './modules-table.component.css',
})
export class ModulesTableComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'niveauscolaire',
    'nom',
    'action',
    'plus',
  ];
  dataSource: MatTableDataSource<any>;
  moduleid!: number;
  modules: any[] = [];
  niveauscolaire: string = '';
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
      this.moduleid = params['id'];
      this.dashboardservice.getModules(this.moduleid).subscribe(
        (response) => {
          this.modules = response.modules;
          this.dataSource = new MatTableDataSource(this.modules);
          this.niveauscolaire = response.nom;
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
  addmodule(): void {
    Swal.fire({
      title: 'Add New module',
      input: 'text',
      inputLabel: 'module Name',
      inputPlaceholder: 'Enter the name of the module',
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
          .createModule(result.value, this.moduleid)
          .subscribe(
            (response) => {
              this.modules.push(response);
              this.dataSource.data = [...this.modules];
              Swal.fire('Added!', 'New module has been added.', 'success');
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
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice.deletemodule(id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
            this.modules = this.modules.filter((module) => module.id !== id);
            this.dataSource.data = this.modules;
          },
          (error) => {
            Swal.fire({
              title: 'Deleted!',
              text: error.error,
              icon: 'error',
            });
          }
        );
      }
    });
  }

  edit(module: any): void {
    Swal.fire({
      title: 'Edit module Name',
      input: 'text',
      inputLabel: 'module Name',
      inputValue: module.nom,
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
        this.dashboardservice.updateModule(result.value, module.id).subscribe(
          (response) => {
            module.nom = response.nom;
            console.log(response);
            this.dataSource.data = [...this.modules]; // Refresh the table data
            Swal.fire('Saved!', 'module name has been updated.', 'success');
          },
          (error) => {}
        );
      }
    });
  }
}
