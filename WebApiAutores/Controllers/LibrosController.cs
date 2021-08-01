using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [Route("api/libros")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public LibrosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await this.dbContext.Libros.Include(libro => libro.Autor).FirstOrDefaultAsync((libro) => libro.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await this.dbContext.Autores.AnyAsync((autor) => autor.Id == libro.AutorId);
            if(!existeAutor)
            {
                return BadRequest($"No existe el autor {libro.AutorId}");
            }
            this.dbContext.Add(libro);
            await this.dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
