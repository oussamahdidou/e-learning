import { Component } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-learning',
  templateUrl: './learning.component.html',
  styleUrl: './learning.component.css',
})
export class LearningComponent {
  modules: any[] = [ ];
  sortedModules: any[] = [];
  filteredModules: any = [];


  constructor(
    private profileService:ProfileService,
    private router: Router
  ){}

  ngOnInit(): void {
    this.profileService.getLearning().subscribe(
      (data) =>{
        this.modules = data
        this.sortedModules = this.sortModulesByName(this.modules);
        this.filteredModules = this.sortedModules;
      },
      (error) => {
        console.error('Error fetching learning modules:', error);
        this.router.navigate(['/']);
      }
    )

  }

  sortModulesByName(modules: any[]): any[] {
    return modules.sort((a, b) => a.nom.localeCompare(b.nom));
  }
  calculateProgress(moduleID: number) {
    const module = this.modules.find((x) => x.moduleID == moduleID);
    if (module) {
      return (module.checkCount * 100) / module.numberOfChapter;
    }
    return undefined;
  }
  filterResults(text: string) {
    if (!text) {
      this.filteredModules = this.modules;
      return;
    }

    this.filteredModules = this.modules.filter((module) =>
      module?.nom.toLowerCase().includes(text.toLowerCase())
    );
  }
  onInputChange(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    this.filterResults(inputElement.value);
  }
}
