using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;//traer con el ctrl+. o manualmente

namespace WebAPIAutores{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }//Para hacer una consulta particular a los autores
        public DbSet<Libro> Libros { get; set; }//Para hacer una consulta particular a los libros
    }

}