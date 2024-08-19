import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../services/course.service';
import { InfoCard, moduleInfo, NomDescription } from '../interfaces/dashboard';

@Component({
  selector: 'app-module-info',
  templateUrl: './module-info.component.html',
  styleUrl: './module-info.component.css',
})
export class ModuleInfoComponent {
  moduleInfo?: moduleInfo;
  headerData: NomDescription = {
    nom: 'Default name',
    moduleDescription: 'Default description',
  };
  InfoData: InfoCard = {
    nom: 'module name',
    moduleImg: 'imginstutition.png',
    Id: 1,
    numberOfChapter: 5,
  };
  pdfUrl: string = 'pdfUrl';
  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService
  ) {}
  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.courseService.getCourseInformationById(id).subscribe({
      next: (data) => {
        console.log(data);
        this.moduleInfo = data;
        this.headerData = {
          nom: data.nom,
          moduleDescription: data.moduleDescription,
        };
        this.InfoData = {
          nom: data.nom,
          moduleImg: data.moduleImg,
          Id: data.moduleID,
          numberOfChapter: data.numberOfChapter,
        };
        this.pdfUrl = data.moduleProgram;
        console.log(this.pdfUrl);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
