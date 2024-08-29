import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.css',
})
export class PostListComponent implements OnInit {
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  description: string = `Passionate and versatile Fullstack Software Engineer with a knack for crafting efficient, scalable, and user-centric applications. With a strong background in Business Intelligence, I bring data to life, transforming raw information into actionable insights. My expertise in web design ensures that every project not only functions seamlessly but also captivates with visually compelling and intuitive interfaces. Dedicated to continuous learning and innovation, I thrive on challenges and consistently strive to push the boundaries of technology, delivering solutions that exceed expectations.`;
}
