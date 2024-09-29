import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { authGuard } from './Guards/auth.guard';
import { PartnersComponent } from './Components/partners/partners.component';
import { DashboardContentComponent } from './Components/dashboard-content/dashboard-content.component';
import { CategoriesComponent } from './Components/categories/categories.component';

export const routes: Routes = [
    {
        path: "",
        redirectTo: "login",
        pathMatch: 'full'
    },
    {
        path : "login",
        component: LoginComponent
    },
    {
        path: "register",
        component: RegisterComponent
    },
    {
        path: "dashboard",
        component: DashboardComponent,
        canActivate: [authGuard],
        children: [
            {
                path: "partners",
                component: PartnersComponent
            },
            {
                path: "categories",
                component: CategoriesComponent
            },
            {
                path: "",
                component: DashboardContentComponent
            }
        ]
    }
];
