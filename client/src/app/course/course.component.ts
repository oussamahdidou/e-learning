import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseService } from '../services/course.service';

interface Option {
  id: number;
  nom: string;
  truth: boolean;
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
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
})
export class CourseComponent {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {}
  module: Module | undefined;

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.courseService.getCourseById(id).subscribe((module) => {
      console.log(module);
      this.module = module;
    });
  }
}
