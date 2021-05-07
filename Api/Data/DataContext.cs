using System;
using System.Collections.Generic;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }        
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .HasData(new List<Employee>{
                    new Employee(
                        1,
                        "Felipe Meneses Maia Eleno",
                        DateTime.Parse("01/05/2001"),
                        "Fernanda de Cássia Edel Meneses",
                        "Fabiano Maia Eleno",
                        "R. Ciro Monteiro, 168",
                        09433250,
                        "Jardim Iramaia",
                        "Ribeirão Pires",
                        "SP",
                        "solteiro",
                        "superior incompleto",
                        11953038040,
                        "54.327.923-6",
                        "228.118.388-21",
                        "430.12797.65-4",
                        DateTime.Parse("04/05/2021"),
                        "programador",
                        "ti",
                        80.00f,
                        "6x2",
                        true
                    ),
                    new Employee(
                        2,
                        "Tomás Iago Rodrigo Drumond",
                        DateTime.Parse("04/07/2000"),
                        "Ester Silvana Beatriz",
                        "Kevin Augusto Drumond",
                        "Rua Paulo Antônio Cardoso, 334",
                        09320610,
                        "Jardim Zaira",
                        "Mauá",
                        "SP",
                        "solteiro",
                        "superior completo",
                        1128932718,
                        "18.845.119-5",
                        "703.911.008-31",
                        "955.42023.62-4",
                        DateTime.Parse("04/05/2021"),
                        "programador",
                        "ti",
                        86.00f,
                        "5x2",
                        true
                    ),
                });
        }
    }
}