import {
  ChangeDetectorRef,
  Component,
  HostListener,
  Input,
  OnInit,
  Renderer2,
} from '@angular/core';
import { CourseService } from '../../services/course.service';
import { ActivatedRoute, Router } from '@angular/router';
import {
  Chapitre,
  CheckChapterRequest,
  Controle,
  Module,
} from '../../interfaces/dashboard';
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
  showModal: boolean = false;
  chapterId: number = 0;
  chapitreNum: number = 0;

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
    this.courseService.getExamFinalByModuleId(moduleId).subscribe(
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
      }) || null
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
  CheckChapter(Id: number, event: Event, avis: string, chapitreNum: number) {
    console.log('This is the module before checking ', this.module);
    console.log('chapitreid ', Id);
    console.log('chapitreNum ', chapitreNum);
    const controle: Controle[] | null = this.getControles(chapitreNum);
    console.log(controle);
    var checkRequest: CheckChapterRequest = {
      Id: Id,
      avis: avis,
      lastChapter: false,
      lastChapterExam: false,
      ControleId: 0,
      ExamId: 0,
    };
    if (controle?.length === 1) {
      checkRequest.ControleId = controle[0].id;
      checkRequest.lastChapter = true;
    }
    console.log(checkRequest)
    this.courseService.checkChapter(checkRequest).subscribe(
      (state) => {
        console.log(state);
        if (state && this.module) {
          const chapter = this.module.chapitres.find((c) => c.id === Id);
          if (chapter) {
            chapter.statue = true;
            this.setActive(event);
            this.calculateProgress();
          }
          this.closeModal();
        }
      },
      (error) => {
        this.errorHandlingService.handleError(error, 'Error checking chapitre');
      }
    );
  }
  openModel(chapiteId: number, chapitreNum: number) {
    this.showModal = true;
    this.chapterId = chapiteId;
    this.chapitreNum = chapitreNum;
  }
  closeModal(): void {
    this.showModal = false;
  }
}
