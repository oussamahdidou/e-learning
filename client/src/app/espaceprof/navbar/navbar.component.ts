import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  teacherId: string;
  constructor(public readonly authservice: AuthService) {
    this.teacherId = authservice.token.unique_name;
  }
  logout() {
    this.authservice.logout();
  }
}
