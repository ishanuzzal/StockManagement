import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterOutlet,RouterLink,RouterLinkActive,CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  sidebarNotActive = true;
  ToogleSidebar(){
    this.sidebarNotActive = !this.sidebarNotActive
  }
}
