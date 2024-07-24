import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ModuleRequirementsDialogComponent } from '../module-requirements-dialog/module-requirements-dialog.component';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrl: './module.component.css',
})
export class ModuleComponent implements OnInit {
  edit(_t283: any) {
    throw new Error('Method not implemented.');
  }
  delete(arg0: any) {
    throw new Error('Method not implemented.');
  }
  displayedColumns: string[] = ['id', 'number', 'nom', 'module', 'plus'];
  modulesColumns: string[] = [
    'id',
    'nom',
    'niveauScolaire',
    'institution',
    'seuil',
    'action',
    'plus',
  ];
  @ViewChild(MatSort) sort!: MatSort;
  chapitressource: MatTableDataSource<any>;
  controlessource: MatTableDataSource<any>;
  modulessource!: MatTableDataSource<any>;

  chapters: any[] = [
    { id: 1, number: 2, nom: 'chapter1', module: 'High School' },
    { id: 2, number: 2, nom: 'chapter2', module: 'Middle School' },
    { id: 3, number: 2, nom: 'chapter3', module: 'Elementary School' },
    { id: 4, number: 2, nom: 'chapter4', module: 'High School' },
    { id: 5, number: 2, nom: 'chapter5', module: 'Middle School' },
  ];
  controles: any[] = [
    { id: 1, number: 2, nom: 'controle1', module: 'High School' },
    { id: 2, number: 2, nom: 'controle2', module: 'Middle School' },
    { id: 3, number: 2, nom: 'controle3', module: 'Elementary School' },
    { id: 4, number: 2, nom: 'controle4', module: 'High School' },
    { id: 5, number: 2, nom: 'controle5', module: 'Middle School' },
  ];
  modules: any[] = [];
  openDialog(): void {
    const dialogRef = this.dialog.open(ModuleRequirementsDialogComponent, {
      width: '400px',
      data: { name: 'Angular' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');
      console.log(result);
      if (result) {
        this.dashboardservice
          .createmodulerequirements({
            targetModuleId: this.moduleId,
            requiredModuleId: result.moduleId,
            seuill: result.seuil,
          })
          .subscribe(
            (response) => {
              this.dashboardservice.getrequiredmodules(this.moduleId).subscribe(
                (response) => {
                  this.modules = response;
                  this.modulessource = new MatTableDataSource(this.modules);
                  this.modulessource.sortingDataAccessor = (item, property) => {
                    switch (property) {
                      default:
                        return item[property];
                    }
                  };
                  this.modulessource.sort = this.sort;
                },
                (error) => {}
              );
            },
            (error) => {}
          );
      }
    });
  }
  moduleId: number = 0;
  constructor(
    public dialog: MatDialog,
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService
  ) {
    this.chapitressource = new MatTableDataSource(this.chapters);
    this.chapitressource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
    this.controlessource = new MatTableDataSource(this.controles);
    this.controlessource.sortingDataAccessor = (item, property) => {
      switch (property) {
        default:
          return item[property];
      }
    };
  }
  ngAfterViewInit(): void {
    this.chapitressource.sort = this.sort;
    this.controlessource.sort = this.sort;
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.moduleId = params['id'];
      this.dashboardservice.getrequiredmodules(this.moduleId).subscribe(
        (response) => {
          this.modules = response;
          this.modulessource = new MatTableDataSource(this.modules);
          this.modulessource.sortingDataAccessor = (item, property) => {
            switch (property) {
              default:
                return item[property];
            }
          };
          this.modulessource.sort = this.sort;
        },
        (error) => {}
      );
      this.dashboardservice
        .GetDashboardChaptersByModule(this.moduleId)
        .subscribe(
          (response) => {
            this.chapters = response;
            this.chapitressource = new MatTableDataSource(this.chapters);
            this.chapitressource.sortingDataAccessor = (item, property) => {
              switch (property) {
                default:
                  return item[property];
              }
            };
            this.chapitressource.sort = this.sort;
          },
          (error) => {}
        );
    });
  }

  applyChapitresFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.chapitressource.filter = filterValue.trim().toLowerCase();
  }
  applyControlesFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.controlessource.filter = filterValue.trim().toLowerCase();
  }
  applyModulesFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.controlessource.filter = filterValue.trim().toLowerCase();
  }
}
