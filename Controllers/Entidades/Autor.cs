using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPIAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }//me va a permitir cargar los libros de un autor (propiedad de navegaciçon List<Libro>)
    }
}