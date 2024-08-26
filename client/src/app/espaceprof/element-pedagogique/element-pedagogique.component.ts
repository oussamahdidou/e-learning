import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InstitutionService } from './../../services/institution.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-element-pedagogique',
  templateUrl: './element-pedagogique.component.html',
  styleUrl: './element-pedagogique.component.css'

})
export class ElementPedagogiqueComponent implements OnInit {
  elementForm!: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private institutionService: InstitutionService,
    private router: Router
  ) {}

  ngOnInit() {
    this.elementForm = this.fb.group({
      nom: ['', Validators.required],
      lien: ['', Validators.required],
      niveauScolaireId: [null, Validators.required],
    });
  }

  onSubmit() {
    if (this.elementForm.valid) {
      this.institutionService.createElementPedagogique(this.elementForm.value).subscribe({
        next: (response) => {
          console.log('Élément pédagogique créé avec succès', response);
          this.successMessage = 'Élément pédagogique créé avec succès';
          this.errorMessage = '';
          this.elementForm.reset();
          // Optionnel : rediriger après un délai
          // setTimeout(() => this.router.navigate(['/votre-chemin']), 2000);
        },
        error: (error) => {
          console.error('Erreur lors de la création de l\'élément pédagogique', error);
          this.errorMessage = 'Erreur lors de la création de l\'élément pédagogique';
          this.successMessage = '';
        },
      });
    }}
}