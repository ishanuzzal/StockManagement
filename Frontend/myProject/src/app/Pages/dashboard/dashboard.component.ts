import { Component } from '@angular/core';
import { SidebarComponent } from '../../Shared/sidebar/sidebar.component';
import { DashboardContentComponent } from "../../Components/dashboard-content/dashboard-content.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [SidebarComponent, DashboardContentComponent,RouterOutlet],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
