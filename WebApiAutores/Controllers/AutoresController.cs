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
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AutoresController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("mock")]
        public ActionResult<List<Autor>> GetMockList()
        {
            return new List<Autor>(){
             new Autor() { Id= 1, Nombre= "Dennis"},
             new Autor() { Id= 2, Nombre= "Juan"}
            };
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await this.dbContext.Autores.ToListAsync();
        }

        [HttpGet("{id: int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await this.dbContext.Autores.FirstOrDefaultAsync((autor) => autor.Id == id);
            // Ok() es un actionResult pero no sabe que objeto retornar, es generico
            // return autor; retorna el objeto Autor, es más especifico.
            return autor != null ? Ok(autor) : NotFound();
        } 
        
        //byDefault nombre tiene el valor de persona
        [HttpGet("{id: int}/{nombre=persona}")]
        public async Task<ActionResult<Autor>> Get(int id, string nombre)
        {
            var autor = await this.dbContext.Autores.FirstOrDefaultAsync((autor) => autor.Id == id && autor.Nombre.Contains(nombre));
            // Ok() es un actionResult pero no sabe que objeto retornar, es generico
            // return autor; retorna el objeto Autor, es más especifico.
            return autor != null ? autor : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            this.dbContext.Add(autor);
            await this.dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El id no pertenece al autor");
            }

             this.dbContext.Update(autor);
            await this.dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await this.dbContext.Autores.AnyAsync((autor) => autor.Id == id);
            if(!exists)
            {
                return NotFound();
            }

            this.dbContext.Remove(new Autor() { Id = id });
            await this.dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
