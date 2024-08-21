import { Component, Input } from '@angular/core';
import { NomDescription } from '../../interfaces/dashboard';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  @Input() data: NomDescription = {
    nom: 'module name',
    moduleDescription: 'module description',
  };
}
