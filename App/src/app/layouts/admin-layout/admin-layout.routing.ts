import { Routes } from "@angular/router";

import { EmployeesComponent } from "../../employees/employees.component";
import { DashboardComponent } from "../../dashboard/dashboard.component";
import { RegisterComponent } from "../../employees/register/register.component";

export const AdminLayoutRoutes: Routes = [
  { path: "dashboard", component: DashboardComponent },
  { path: "employees", component: EmployeesComponent },
  { path: "employees/register", component: RegisterComponent },
];
