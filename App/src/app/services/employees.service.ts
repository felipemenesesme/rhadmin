import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

import { Employee } from "../models/Employee";

import { environment } from "../../environments/environment";
import { PaginatedResult } from "../models/Pagination";
import { map, repeat } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class EmployeesService {
  baseURL = `${environment.mainUrlAPI}employee`;

  constructor(private http: HttpClient) {}

  getAll(
    page?: number,
    itemsPerPage?: number
  ): Observable<PaginatedResult<Employee[]>> {
    const paginatedResult: PaginatedResult<Employee[]> = new PaginatedResult<
      Employee[]
    >();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page.toString());
      params = params.append("pageSize", itemsPerPage.toString());
    }

    return this.http
      .get<Employee[]>(this.baseURL, { observe: "response", params })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get("Pagination") != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get("Pagination")
            );
          }
          return paginatedResult;
        })
      );
  }

  getById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.baseURL}/${id}`);
  }

  getEmployeeMonthlyCost(
    id: number,
    month: number,
    year: number
  ): Observable<number> {
    return this.http.get<number>(
      `${this.baseURL}/${id}/monthlycost/${month}/${year}`
    );
  }

  getEmployeeYearlyCost(id: number, year: number): Observable<number> {
    return this.http.get<number>(`${this.baseURL}/${id}/yearlycost/${year}`);
  }

  post(employee: Employee) {
    return this.http.post(this.baseURL, employee);
  }

  put(employee: Employee) {
    return this.http.put(`${this.baseURL}/${employee.id}`, employee);
  }

  changeState(employeeId: number, ativo: boolean) {
    return this.http.patch(`${this.baseURL}/${employeeId}/changeState`, {
      State: ativo,
    });
  }

  patch(employee: Employee) {
    return this.http.patch(`${this.baseURL}/${employee.id}`, employee);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
