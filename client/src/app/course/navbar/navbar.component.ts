import { Component, Input } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  @Input() courseName: string | undefined = 'Course Name';
  constructor(private readonly authservice: AuthService) {}
  logout() {
    this.authservice.logout();
    window.location.href = `/`;
  }
}
