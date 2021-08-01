using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Entities;

namespace WebApiAutores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        //esto permite generar una tabla con las propiedades del objeto Autor
        public DbSet<Autor> Autores { get; set; }
        //agregar el objeto libros con migrations el package manager: Add-Migration Libros 
        //despues actualizar la base de datos desde el package manager: update-database
        public DbSet<Libro> Libros { get; set; }
    }
}
