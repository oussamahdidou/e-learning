import { Component, Input, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';


interface Option {
  id: number;
  nom: string;
  truth: string;
}

interface Question {
  id: number;
  nom: string;
  options: Option[];
}

interface Quiz {
  id: number;
  questions: Question[];
}

interface Chapitre {
  id: number;
  ChapitreNum: number;
  nom: string;
  Statue: string;
  CoursPdfPath: string;
  VideoPath: string;
  Synthese: string;
  Schema: string;
  Premium: boolean;
  quizId: number;
  quiz: Quiz;
}

interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  ChapitreNum: number[];
}

interface Module {
  id: number;
  nom: string;
  chapitres: Chapitre[];
  controles?: Controle[];
}
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})

export class SidebarComponent implements OnInit {

  module: Module | undefined;
  progress: number = 0;

  constructor(private courseService: CourseService) { }

  ngOnInit() {
    this.courseService.getCourse().subscribe(module => {
      this.module = module;
      this.calculateProgress();
    });
  }

  calculateProgress() {
    if (this.module) {
      const completedChapters = this.module.chapitres.filter(
        chapitre => chapitre.Statue === 'checked'
      ).length;
      this.progress = (completedChapters / this.module.chapitres.length) * 100;
    }
  }

  getControles(chapitreNum: number) {
    return this.module?.controles?.filter(controle =>
      controle.ChapitreNum.includes(chapitreNum)
    ) || [];
  }
}
