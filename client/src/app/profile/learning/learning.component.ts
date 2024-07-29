import { Component } from '@angular/core';

@Component({
  selector: 'app-learning',
  templateUrl: './learning.component.html',
  styleUrl: './learning.component.css'
})
export class LearningComponent {
  modules = [
  {
    moduleID: 1,
    nom: "Mathematics",
    numberOfChapter: 4,
    checkCount: 2
  },
  {
    moduleID: 3,
    nom: "History",
    numberOfChapter: 1,
    checkCount: 1
  },
  {
    moduleID: 3,
    nom: "History",
    numberOfChapter: 1,
    checkCount: 1
  },
  {
    moduleID: 3,
    nom: "History",
    numberOfChapter: 1,
    checkCount: 1
  },
  {
    moduleID: 3,
    nom: "History",
    numberOfChapter: 1,
    checkCount: 1
  },
  {
    moduleID: 3,
    nom: "History",
    numberOfChapter: 1,
    checkCount: 1
  }
]
  calculateProgress(moduleID : number){
    const module = this.modules.find(x => x.moduleID == moduleID)
    if(module){
      return module.checkCount * 100 / module.numberOfChapter
    }
    return undefined
  }
}
