import { Component, Input } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-program-pdf',
  templateUrl: './program-pdf.component.html',
  styleUrl: './program-pdf.component.css',
})
export class ProgramPdfComponent {
  @Input() pdfUrl?: string;
}
