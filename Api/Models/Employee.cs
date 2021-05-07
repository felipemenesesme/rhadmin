using System;

namespace Api.Models
{
    public class Employee
    {
        public Employee() { }
        public Employee(int id, string nome, DateTime dataDeNascimento, string mae, string pai, string endereco, int cep, string bairro, string cidade, string uf, string estadoCivil, string grauDeInstrucao, long telefone, string rg, string cpf, string pis, DateTime dataAdmissao, string funcao, string departamento, float custoDiario, string escalaDeTrabalho, bool ativo)
        {
            this.Id = id;
            this.Nome = nome;
            this.DataDeNascimento = dataDeNascimento;
            this.Mae = mae;
            this.Pai = pai;
            this.Endereco = endereco;
            this.Cep = cep;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Uf = uf;
            this.EstadoCivil = estadoCivil;
            this.GrauDeInstrucao = grauDeInstrucao;
            this.Telefone = telefone;
            this.Rg = rg;
            this.Cpf = cpf;
            this.Pis = pis;
            this.DataAdmissao = dataAdmissao;
            this.Funcao = funcao;
            this.Departamento = departamento;
            this.CustoDiario = custoDiario;
            this.EscalaDeTrabalho = escalaDeTrabalho;
            this.Ativo = ativo;

        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public DateTime DateIni { get; set; } = DateTime.Now;
        public DateTime? DateFim { get; set; } = null;
        public string Mae { get; set; }
        public string Pai { get; set; }
        public string Endereco { get; set; }
        public int Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string EstadoCivil { get; set; }
        public string GrauDeInstrucao { get; set; }
        public long Telefone { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Pis { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string Funcao { get; set; }
        public string Departamento { get; set; }
        public float CustoDiario { get; set; }
        public string EscalaDeTrabalho { get; set; }
        public bool Ativo { get; set; } = true;
    }
}