using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIAutores.Entidades;
using Microsoft.EntityFrameworkCore;

namespace WebAPIAutores.Controllers{
    [ApiController]/*ATRIBUTO que me permitira hacer validaciones automaticas respecto a la data recibida en nuestro controlador*/
    [Route("api/autores")] /*Ruta del controlador*/
    public class AutoresController: ControllerBase{

        private readonly ApplicationDbContext context;


        //CONSTRUCTOR
        public AutoresController(ApplicationDbContext context){
            
            this.context = context;
        }


        [HttpGet]   
        public async Task<ActionResult<List<Autor>>> Get(){
            //Retorno listado de Autores
            
            return await context.Autores.ToListAsync();
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor){
          //Agregar Autor
            context.Add(autor);//agregamos en la bd un nuevo autor, pero aun no se ha creado en la bd de datos, debo guardar los cambios
            await context.SaveChangesAsync(); //acá guardo los cambios de manera asincrona
            return Ok();//retorno algo (ahora ok, en el futuro otra cosa)
        }


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

        
    }

}