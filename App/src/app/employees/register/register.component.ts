import { Component, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { NgbDateStruct, NgbTimeStruct } from "@ng-bootstrap/ng-bootstrap";
import { ToastrService } from "ngx-toastr";
import { Employee } from "src/app/models/Employee";

import { EmployeesService } from "../../services/employees.service";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
})
export class RegisterComponent implements OnInit {
  employee: {
    nome: string;
    dataDeNascimento: NgbDateStruct;
    mae: string;
    pai: string;
    endereco: string;
    cep: Number;
    bairro: string;
    cidade: string;
    uf: string;
    estadoCivil: string;
    grauDeInstrucao: string;
    telefone: Number;
    rg: string;
    cpf: string;
    pis: string;
    dataAdmissao: NgbDateStruct;
    funcao: string;
    departamento: string;
    custoDiario: Number;
    escalaDeTrabalho: string;
  };

  data: Date = new Date();
  date: { year: number; month: number };

  constructor(
    private employeesService: EmployeesService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.employee = {
      nome: "",
      dataDeNascimento: undefined,
      mae: "",
      pai: "",
      endereco: "",
      cep: 0,
      bairro: "",
      cidade: "",
      uf: "",
      estadoCivil: "",
      grauDeInstrucao: "",
      telefone: 0,
      rg: "",
      cpf: "",
      pis: "",
      dataAdmissao: undefined,
      funcao: "",
      departamento: "",
      custoDiario: 0,
      escalaDeTrabalho: "",
    };
  }

  isWeekend(date: NgbDateStruct) {
    const d = new Date(date.year, date.month - 1, date.day);
    return d.getDay() === 0 || d.getDay() === 6;
  }

  isDisabled(date: NgbDateStruct, current: { month: number }) {
    return date.month !== current.month;
  }

  ngOnInit() {}

  register(frm: FormGroup) {
    const _employee: Employee = {
      id: 0,
      nome: this.employee.nome,
      dataDeNascimento:
        this.employee.dataDeNascimento.day +
        "-" +
        this.employee.dataDeNascimento.month +
        "-" +
        this.employee.dataDeNascimento.year,
      mae: this.employee.mae,
      pai: this.employee.pai,
      endereco: this.employee.endereco,
      cep: this.employee.cep,
      bairro: this.employee.bairro,
      cidade: this.employee.cidade,
      uf: this.employee.uf,
      estadoCivil: this.employee.estadoCivil,
      grauDeInstrucao: this.employee.grauDeInstrucao,
      telefone: this.employee.telefone,
      rg: this.employee.rg,
      cpf: this.employee.cpf,
      pis: this.employee.pis,
      dataAdmissao:
        this.employee.dataAdmissao.day +
        "-" +
        this.employee.dataAdmissao.month +
        "-" +
        this.employee.dataAdmissao.year,
      funcao: this.employee.funcao,
      departamento: this.employee.departamento,
      custoDiario: this.employee.custoDiario,
      escalaDeTrabalho: this.employee.escalaDeTrabalho,
      ativo: true,
    };
    this.employeesService.post(_employee).subscribe(
      (x) => {
        frm.reset();
        this.toastr.success("Colaborador cadastrador com sucesso!");
        this.router.navigate(["/employees"]);
      },
      (error) => {
        this.toastr.error("Erro ao cadastrar colaborador!");
        console.error(error);
      }
    );
  }
}
