import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

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
  synthèse: Content;
  quiz: Quiz;
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
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
})
export class CourseComponent {
  constructor(private router: Router, private route: ActivatedRoute) {}

  course: Course = {
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
        synthèse: {
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
        synthèse: {
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
}
