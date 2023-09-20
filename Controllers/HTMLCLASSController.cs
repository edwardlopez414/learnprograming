using learnprogramming.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace learnprogramming.Controllers
{
    [Route("api/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class HTMLCLASSController : ControllerBase
    {
        [HttpPost]
        [Route("HTMLCLASS")]
        public IActionResult Index(int numberlesson )
        {
            CLASHTML cLASHTML = new CLASHTML{
                lessonNumber = 0,
                lessonTitulo = "Bienvenida al curso",
                lesson = "¡Hola! ¡Bienvenido al estudio de HTML! Estoy aquí para ayudarte a aprender. Comencemos con los conceptos básicos y avancemos gradualmente. ¡No dudes en hacer cualquier pregunta que tengas a lo largo del camino! ¡Vamos a sumergirnos en el mundo del desarrollo web con HTML!",
                lessonliks = "www.lesson.com, www.telamuestro.en-hd.com.es"
            };
            return Ok(cLASHTML);
        }
    }
}
