using System;

namespace Api.V1.Dtos
{
    /// <summary>
    /// This is Employee's DTO register
    /// </summary>
    public class EmployeePatchDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
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
        public bool Ativo { get; set; }
    }
}