import { Component, inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-pop-up',
  standalone: true,
  imports: [],
  templateUrl: './pop-up.component.html',
  styleUrl: './pop-up.component.css'
})
export class PopUpComponent {
  private dialogRef = inject(MatDialogRef<PopUpComponent>);

  generateReport(): void {
    this.dialogRef.close({ action: 'generate' });
  }

  cancel(): void {
    this.dialogRef.close({ action: 'cancel' });
  }
}
