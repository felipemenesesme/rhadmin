import { Routes } from "@angular/router";

import { EmployeesComponent } from "../../employees/employees.component";
import { RegisterComponent } from "../../employees/register/register.component";

export const AdminLayoutRoutes: Routes = [
  { path: "employees", component: EmployeesComponent },
  { path: "employees/register", component: RegisterComponent },
];
