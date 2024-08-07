import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ModuleRequirementsDialogComponent } from '../module-requirements-dialog/module-requirements-dialog.component';
import { ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import Swal from 'sweetalert2';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrl: './module.component.css',
})
export class ModuleComponent implements OnInit {
  deleteexamfinal(id: number) {
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
        this.dashboardservice.deleteexamfinal(id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
            this.exam = null;
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
  editrequiredmodule(module: any) {
    Swal.fire({
      title: 'Edit seuil Name',
      input: 'text',
      inputLabel: 'seuill',
      inputValue: module.seuill,
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
          .editrequiredmodule(this.moduleId, module.id, result.value)
          .subscribe(
            (response) => {
              module.seuill = response.seuill;
              console.log(response);
              this.modulessource.data = [...this.modules]; // Refresh the table data
              Swal.fire('Saved!', 'Edit successfuly', 'success');
            },
            (error) => {
              Swal.fire(`error`, `${error.error}`, `error`);
            }
          );
      }
    });
  }
  deleterequiredmodule(id: number) {
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
        this.dashboardservice.deleterequiredmodule(this.moduleId, id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
            this.modules = this.modules.filter(
              (controle) => controle.id !== id
            );
            this.modulessource.data = [...this.modules];
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
  deletecontrole(id: number) {
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
        this.dashboardservice.deletecontrole(id).subscribe(
          (response) => {
            Swal.fire({
              title: 'Deleted!',
              text: 'Your file has been deleted.',
              icon: 'success',
            });
            this.controles = this.controles.filter(
              (controle) => controle.id !== id
            );
            this.controlessource.data = [...this.controles];
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
  refuser() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes !',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice.refuserexam(this.moduleId).subscribe(
          (response) => {
            console.log(response);
            this.exam.status = response.status;
            Swal.fire({
              title: 'Refuser!',
              text: 'Your file has been Refuser.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      }
    });
  }
  approuver() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        this.dashboardservice.approuverexam(this.moduleId).subscribe(
          (response) => {
            console.log(response);
            this.exam.status = response.status;
            Swal.fire({
              title: 'Accepter!',
              text: 'Your file has been Accepter.',
              icon: 'success',
            });
          },
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      }
    });
  }
  controleForm: FormGroup;
  host = environment.apiUrl;
  exam: any;

  displayedColumns: string[] = ['id', 'number', 'nom', 'module', 'plus'];
  controlesColumns: string[] = ['id', 'nom', 'action', 'plus'];
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

  chapters: any[] = [];
  controles: any[] = [];
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
                (error) => {
                  Swal.fire(`error`, `${error.error}`, `error`);
                }
              );
            },
            (error) => {
              Swal.fire(`error`, `${error.error}`, `error`);
            }
          );
      }
    });
  }
  moduleId: number = 0;
  enonceFile: File | null = null;
  solutionFile: File | null = null;
  onFileChange(event: any, type: string) {
    const file = event.target.files[0];
    if (file && file.type === 'application/pdf') {
      if (type === 'enonce') {
        this.enonceFile = file;
      } else if (type === 'solution') {
        this.solutionFile = file;
      }
    } else {
      Swal.fire({
        title: 'Warning',
        text: 'Veuillez sélectionner un fichier PDF',
        icon: 'warning',
      });
    }
  }
  SelectSolution(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.moduleId.toString());
      this.dashboardservice.updateexamsolution(formData).subscribe(
        (response) => {
          this.exam.solution = response.solution;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
    }
  }
  SelectEnnonce(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      const formData = new FormData();
      formData.append('File', file);
      formData.append('Id', this.moduleId.toString());
      this.dashboardservice.updateexamsolution(formData).subscribe(
        (response) => {
          this.exam.ennonce = response.ennonce;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
    }
  }
  onSubmit() {
    if (this.controleForm.valid) {
      const formData = new FormData();
      formData.append('Nom', this.controleForm.get('nomControle')?.value);
      if (this.enonceFile) {
        formData.append('Ennonce', this.enonceFile);
      }
      if (this.solutionFile) {
        formData.append('Solution', this.solutionFile);
      }
      formData.append('ModuleId', this.moduleId.toString());

      this.dashboardservice.createexam(formData).subscribe(
        (response) => {
          console.log(response);
          this.exam = response;
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
  constructor(
    private fb: FormBuilder,
    public dialog: MatDialog,
    private readonly route: ActivatedRoute,
    private readonly dashboardservice: DashboardService,
    public authservice: AuthService
  ) {
    this.controleForm = this.fb.group({
      nomControle: ['', Validators.required],
      enonce: [null],
      solution: [null],
    });
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
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.moduleId = params['id'];
      this.dashboardservice.getexambymodule(this.moduleId).subscribe(
        (response) => {
          this.exam = response;
          console.log(response);
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
      );
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
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
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
          (error) => {
            Swal.fire(`error`, `${error.error}`, `error`);
          }
        );
      this.dashboardservice.getcontrolesbymodule(this.moduleId).subscribe(
        (response) => {
          this.controles = response;
          this.controlessource = new MatTableDataSource(this.controles);
          this.chapitressource.sortingDataAccessor = (item, property) => {
            switch (property) {
              default:
                return item[property];
            }
          };
          this.controlessource.sort = this.sort;
        },
        (error) => {
          Swal.fire(`error`, `${error.error}`, `error`);
        }
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
