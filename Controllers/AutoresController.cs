using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers{
    [ApiController]/*ATRIBUTO que me permitira hacer validaciones automaticas respecto a la data recibida en nuestro controlador*/
    [Route("api/autores")] /*Ruta del controlador*/
    public class AutoresController: ControllerBase{
        [HttpGet]   
        public ActionResult<List<Autor>> GetActionResult(){
            return new List<Autor>(){
                /*Después se sacarán los datos de una Base de Datos*/
                new Autor(){ Id=1, Nombre="Felipe"},
                new Autor(){ Id=2, Nombre="Claudia"}
            };
        }

        
    }

}