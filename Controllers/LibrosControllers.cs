using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIAutores.Entidades;
using Microsoft.EntityFrameworkCore;

namespace WebAPIAutores.Controllers{
    [ApiController]/*ATRIBUTO que me permitira hacer validaciones automaticas respecto a la data recibida en nuestro controlador*/
    [Route("api/libros")] /*Ruta del controlador*/
    public class LibrosController: ControllerBase{

        private readonly ApplicationDbContext context;


        //CONSTRUCTOR
        public LibrosController(ApplicationDbContext context){
            
            this.context = context;
        }


        [HttpGet("{id:int}")]   
        public async Task<ActionResult<Libro>> Get(int id){
            //Retorno un libro en particular
            
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id==id); //firstordefaultasync trae el primer registro que coincida con la condicion entre parentesis
            //Libros porque en el dbcontext pusimos libros
            //Libros.Include(x => x.Autor) realizo esta acción para incluir el Autor, porque si no solamente trae el id y no el objeto autor
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(Libro libro){
          //Agregar Libro   
          //Primero me fijo si existe un autor en la base de datos que tenga el id que trae el libro
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.IdAutor);
            
            if(!existeAutor){

                return BadRequest($"No existe el autor de Id: {libro.IdAutor}"); // puede ser tambien ("No existe el autor de Id: "+libro.IdAutor);

            }
        
            context.Add(libro);//agregamos en la bd un nuevo libro, pero aun no se ha creado en la bd de datos, debo guardar los cambios
            await context.SaveChangesAsync(); //acá guardo los cambios de manera asincrona
            return Ok();//retorno algo (ahora ok, en el futuro otra cosa)
        }

/*
        [HttpPut("{id:int}")]//  api/autores/1 (1 como ejemplo)
        //Modificar Autor
        public async Task<ActionResult> Put(Autor autor, int id){
            if(autor.Id != id){
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if(!existe){
                return NotFound();
            }

            context.Update(autor); //actualizo directamente al autor porque se lo mande por parametro
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]// api/autores/2 (2 como ejemplo)
        public async Task<ActionResult> Delete(int id){

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if(!existe){
                return NotFound();
            }

            context.Remove(new Autor(){Id= id});//Le mando una instancia con el id requerido
            await context.SaveChangesAsync();
            return Ok();


        }
*/
        
    }

}