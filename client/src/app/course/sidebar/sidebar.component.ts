import { Component, Input, OnInit, Renderer2 } from '@angular/core';
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
  nom: string;
  questions: Question[];
}

interface Chapitre {
  id: number;
  ChapitreNum: number;
  nom: string;
  Statue: string;
  CoursPdfPath: string | null;
  VideoPath: string | null;
  Synthese: string | null;
  Schema: string | null;
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
  private activeElement: HTMLElement | null = null;

  constructor(
    private courseService: CourseService,
    private renderer: Renderer2
  ) {}

  ngOnInit() {
    this.courseService.getCourse().subscribe((module) => {
      this.module = module;
      this.calculateProgress();
    });
  }

  calculateProgress() {
    if (this.module) {
      const completedChapters = this.module.chapitres.filter(
        (chapitre) => chapitre.Statue === 'checked'
      ).length;
      this.progress = (completedChapters / this.module.chapitres.length) * 100;
    }
  }

  getControles(chapitreNum: number) {
    return (
      this.module?.controles?.filter((controle) => {
        const maxNum = Math.max(...controle.ChapitreNum);
        return maxNum === chapitreNum;
      }) || []
    );
  }
  setActive(event: Event): void {
    const target = event.currentTarget as HTMLElement;
    if (this.activeElement) {
      this.renderer.removeClass(this.activeElement, 'onit');
    }
    this.renderer.addClass(target, 'onit');
    this.activeElement = target;
  }
}
