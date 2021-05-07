import { Component, OnInit, OnDestroy, TemplateRef } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { PaginatedResult, Pagination } from "../models/Pagination";

import { Employee } from "../models/Employee";

import { EmployeesService } from "../services/employees.service";

@Component({
  selector: "app-employees",
  templateUrl: "./employees.component.html",
})
export class EmployeesComponent implements OnInit, OnDestroy {
  public selectedEmployee: Employee;
  public employees: Employee[];
  public employee: Employee;
  public modeSave = "post";
  public msnDeleteEmployee: string;
  public employeeMonthlyCost: string;
  public employeeYearlyCost: string;
  pagination: Pagination;

  private unsubscriber = new Subject();

  constructor(
    private employeesService: EmployeesService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {
    //this.createForm();
  }

  ngOnInit(): void {
    this.pagination = { currentPage: 1, itemsPerPage: 4 } as Pagination;
    this.loadEmployees();
  }

  changeState(employee: Employee) {
    this.employeesService
      .changeState(employee.id, !employee.ativo)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe(
        (resp) => {
          console.log(resp);
          this.loadEmployees();
          this.toastr.success("Employee salvo com sucesso!");
        },
        (error: any) => {
          this.toastr.error(`Erro: Employee não pode ser salvo!`);
          console.error(error);
        }
      );
  }

  loadEmployees(): void {
    const employeeId = +this.route.snapshot.paramMap.get("id");

    this.employeesService
      .getAll(this.pagination.currentPage, this.pagination.itemsPerPage)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe(
        (employees: PaginatedResult<Employee[]>) => {
          this.employees = employees.result;
          this.pagination = employees.pagination;

          if (employeeId > 0) {
            this.employeeSelect(employeeId);
          }

          this.toastr.success("Employees foram carregado com Sucesso!");
        },
        (error: any) => {
          this.toastr.error("Employees não carregados!");
          console.error(error);
        }
      );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadEmployees();
  }

  employeeSelect(employeeId: number): void {
    this.modeSave = "patch";
    this.employeesService.getById(employeeId).subscribe(
      (employeeReturn) => {
        this.selectedEmployee = employeeReturn;
      },
      (error) => {
        this.toastr.error("Employees não carregados!");
        console.error(error);
      }
    );
    this.employeesService
      .getEmployeeMonthlyCost(employeeId, 5, 2021)
      .subscribe((employeeMonthlyCost) => {
        this.employeeMonthlyCost = employeeMonthlyCost.toLocaleString("pt-BR", {
          style: "currency",
          currency: "BRL",
        });
      });
    this.employeesService
      .getEmployeeYearlyCost(employeeId, 2021)
      .subscribe((employeeYearlyCost) => {
        this.employeeYearlyCost = employeeYearlyCost.toLocaleString("pt-BR", {
          style: "currency",
          currency: "BRL",
        });
      });
  }

  voltar(): void {
    this.selectedEmployee = null;
    this.employeeMonthlyCost = null;
    this.employeeYearlyCost = null;
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }
}
