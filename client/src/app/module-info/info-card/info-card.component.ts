import { Component, Input } from '@angular/core';
import { InfoCard } from '../../interfaces/dashboard';

@Component({
  selector: 'app-info-card',
  templateUrl: './info-card.component.html',
  styleUrl: './info-card.component.css',
})
export class InfoCardComponent {
  @Input() data: InfoCard = {
    nom: 'module name',
    moduleImg: 'imginstutition.png',
    Id: 1,
    numberOfChapter: 5,
  };
}
