import { Component, Input, OnInit } from '@angular/core';

interface Option {
  id: number;
  option: string;
}

interface Question {
  id: number;
  question: string;
  options: Option[];
}

interface Content {
  id: number;
  name: string;
  contentURL: string;
}

interface Quiz {
  questions: Question[];
}

interface Chapter {
  id: number;
  name: string;
  cour: Content;
  video: Content;
  schema: Content;
  synthese: Content;
  quiz: Quiz;
  Checked: boolean;
}

interface Controle {
  id: number;
  name: string;
  contentURL: string;
}

interface Course {
  id: number;
  name: string;
  description: string;
  chapters: Chapter[];
  controles?: Controle[];
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  @Input() course: Course = {
    id: 1,
    name: 'Introduction to Programming',
    description: 'This is a beginner-level course on programming.',
    chapters: [
      {
        id: 1,
        name: 'Chapter 1: Basics',
        cour: {
          id: 1,
          name: 'Introduction to Variables',
          contentURL: 'https://example.com/cour1',
        },
        video: {
          id: 1,
          name: 'Variables Explained',
          contentURL: 'https://example.com/video1',
        },
        schema: {
          id: 1,
          name: 'Variable Schema',
          contentURL: 'https://example.com/schema1',
        },
        synthese: {
          id: 1,
          name: 'Variable Summary',
          contentURL: 'https://example.com/synthese1',
        },
        quiz: {
          questions: [
            {
              id: 1,
              question: 'What is a variable used for?',
              options: [
                { id: 1, option: 'To store data' },
                { id: 2, option: 'To define a function' },
                { id: 3, option: 'To create a loop' },
              ],
            },
            {
              id: 2,
              question: 'Which of these is a correct variable name?',
              options: [
                { id: 1, option: '1variable' },
                { id: 2, option: 'variable1' },
                { id: 3, option: 'variable-1' },
              ],
            },
          ],
        },
        Checked: true,
      },
      {
        id: 2,
        name: 'Chapter 2: Advanced Topics',
        cour: {
          id: 2,
          name: 'Advanced Variables',
          contentURL: 'https://example.com/cour2',
        },
        video: {
          id: 2,
          name: 'Advanced Variables Explained',
          contentURL: 'https://example.com/video2',
        },
        schema: {
          id: 2,
          name: 'Advanced Variable Schema',
          contentURL: 'https://example.com/schema2',
        },
        synthese: {
          id: 2,
          name: 'Advanced Variable Summary',
          contentURL: 'https://example.com/synthese2',
        },
        quiz: {
          questions: [
            {
              id: 1,
              question: 'What is a pointer?',
              options: [
                { id: 1, option: 'A variable that stores a memory address' },
                { id: 2, option: 'A type of loop' },
                { id: 3, option: 'A function parameter' },
              ],
            },
            {
              id: 2,
              question: 'What is dynamic memory allocation?',
              options: [
                { id: 1, option: 'Allocating memory during runtime' },
                { id: 2, option: 'Allocating memory during compile time' },
                { id: 3, option: 'Allocating memory in the stack' },
              ],
            },
          ],
        },
        Checked: false,
      },
    ],
    controles: [
      {
        id: 1,
        name: 'Controle 1',
        contentURL: 'https://example.com/controle1',
      },
    ],
  };

  progress: number = 0;

  ngOnInit() {
    this.updateProgress();
  }

  updateProgress() {
    const totalChapters = this.course.chapters.length;
    const checkedChapters = this.course.chapters.filter(
      (chapter) => chapter.Checked
    ).length;
    this.progress = (checkedChapters / totalChapters) * 100;
  }
}
