using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //property of navigation to libro
        public List<Libro> Libros { get; set; }


    }
}
