import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { authGuard } from './Guards/auth.guard';
import { PartnersComponent } from './Components/partners/partners.component';
import { DashboardContentComponent } from './Components/dashboard-content/dashboard-content.component';
import { CategoriesComponent } from './Components/categories/categories.component';
import { AddProductComponent } from './Components/add-product/add-product.component';
import { ShowAllProductComponent } from './Components/show-all-product/show-all-product.component';
import { SellProductComponent } from './Components/sell-product/sell-product.component';

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
                path: "Addproducts",
                component: AddProductComponent
            },
            {
                path: "SellProduct",
                component: SellProductComponent
            },
            {
                path: "ShowProducts",
                component: ShowAllProductComponent
            },
            {
                path: "",
                component: DashboardContentComponent
            }
        ]
    }
];
