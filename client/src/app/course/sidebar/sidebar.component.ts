import {
  ChangeDetectorRef,
  Component,
  Input,
  OnInit,
  Renderer2,
} from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute } from '@angular/router';

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
  chapitreNum: number;
  nom: string;
  statue: boolean;
  coursPdfPath: string | null;
  videoPath: string | null;
  synthese: string | null;
  schema: string | null;
  premium: boolean;
  quizId: number;
  quiz: Quiz;
}

interface Controle {
  id: number;
  nom: string;
  ennonce: string;
  solution: string;
  chapitreNum: number[];
}

interface Module {
  id: number;
  nom: string;
  chapitres: Chapitre[];
  controles: Controle[];
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
    private renderer: Renderer2,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.courseService.getCourseById(id).subscribe((module) => {
      this.module = module;
      this.calculateProgress();
      console.log('Module ID from service:', this.module?.id);
    });
  }

  calculateProgress() {
    if (this.module) {
      const completedChapters = this.module.chapitres.filter(
        (chapitre) => chapitre.statue === true
      ).length;
      this.progress = parseFloat(
        ((completedChapters / this.module.chapitres.length) * 100).toFixed(2)
      );
    }
  }

  getControles(chapitreNum: number) {
    return (
      this.module?.controles?.filter((controle) => {
        const maxNum = Math.max(...controle.chapitreNum);
        return maxNum === chapitreNum;
      }) || []
    );
  }

  setActive(event: Event): void {
    const target = event.currentTarget as HTMLElement;
    if (target) {
      if (this.activeElement) {
        this.renderer.removeClass(this.activeElement, 'onit');
      }
      this.renderer.addClass(target, 'onit');
      this.activeElement = target;
    }
  }
  CheckChapter(chapite: Chapitre, event: Event) {
    console.log('This is the module before checking ', this.module);
    this.courseService.checkChapter(chapite.id).subscribe((state) => {
      console.log(state);
      if (state && this.module) {
        const chapter = this.module.chapitres.find((c) => c.id === chapite.id);
        if (chapter) {
          chapter.statue = true;
          this.setActive(event);
          this.calculateProgress();
          console.log('This is the module after checking ', this.module);
          console.log('This is the progress ', this.progress);
        }
      }
    });
  }
}
