import { Component } from '@angular/core';

@Component({
  selector: 'app-learning',
  templateUrl: './learning.component.html',
  styleUrl: './learning.component.css',
})
export class LearningComponent {
  modules = [
    {
      moduleID: 1,
      nom: 'Mathematics',
      numberOfChapter: 4,
      checkCount: 2,
    },
    {
      moduleID: 3,
      nom: 'XML',
      numberOfChapter: 1,
      checkCount: 1,
    },
    {
      moduleID: 3,
      nom: 'AI',
      numberOfChapter: 1,
      checkCount: 1,
    },
    {
      moduleID: 3,
      nom: 'Programation Web',
      numberOfChapter: 1,
      checkCount: 1,
    },
    {
      moduleID: 3,
      nom: 'Programmation Orienté objet',
      numberOfChapter: 1,
      checkCount: 1,
    },
    {
      moduleID: 3,
      nom: 'Physics',
      numberOfChapter: 1,
      checkCount: 1,
    },
  ];
  sortedModules: any = [];
  filteredModules: any = [];

  ngOnInit(): void {
    this.sortedModules = this.sortModulesByName(this.modules);
    this.filteredModules = this.sortedModules;
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
