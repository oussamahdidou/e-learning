import {
  ChangeDetectorRef,
  Component,
  Input,
  OnInit,
  Renderer2,
} from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Chapitre, Module } from '../../interfaces/dashboard';
import { ErrorHandlingService } from '../../services/error-handling.service';
import { SharedDataService } from '../../services/shared-data.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  module: Module | undefined;
  progress: number = 0;
  private activeElement: HTMLElement | null = null;
  examFinal: any;
  part1: string = '';
  part2: number = 0;

  constructor(
    private courseService: CourseService,
    private renderer: Renderer2,
    private route: ActivatedRoute,
    private errorHandlingService: ErrorHandlingService,
    private router: Router,
    private shared: SharedDataService
  ) {}

  ngOnInit() {
    this.shared.activeDiv$.subscribe((value) => {
      const parts = value.split('/');
      this.part1 = parts[0];
      this.part2 = Number(parts[1]);
      console.log('now', value);
    });
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.courseService.getCourseById(id).subscribe(
      (module) => {
        this.module = module;
        this.calculateProgress();
        this.fetchExamFinal(id);
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error Fetching Module');
        this.router.navigate(['/']);
      }
    );
  }

  fetchExamFinal(moduleId: any) {
    const id: number = parseInt(moduleId);
    this.courseService.getExamFinal(moduleId).subscribe(
      (examFinal) => {
        this.examFinal = examFinal;
        console.log('Exam Final:', this.examFinal);
      },
      (error) => {
        console.warn('No exam final found for this module.', error);
      }
    );
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
    this.courseService.checkChapter(chapite.id).subscribe(
      (state) => {
        console.log(state);
        if (state && this.module) {
          const chapter = this.module.chapitres.find(
            (c) => c.id === chapite.id
          );
          if (chapter) {
            chapter.statue = true;
            this.setActive(event);
            this.calculateProgress();
          }
        }
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error checking chapitre');
      }
    );
  }
}
