using Microsoft.AspNetCore.Mvc;
using Automatas.Models;
using System.IO;
using System.Web;
namespace Automatas.Controllers
{
    public class EstadoController : Controller
    {
        public static string resultado = "";
        public static Automata automata = new Automata();
        public static List<String> listaTexto = new List<String>();
        public IActionResult Index()
        {
            return View("Index");
        }
        [Route("carga")]
        public IActionResult SubirDatos(IFormFile archivo)
        {
            resultado = "";
            automata.inicial = null;
            if (archivo == null || archivo.Length == 0)
            {
                ViewBag.Error = "Seleccione un archivo válido.";
                return View("SubirDatos", listaTexto);
            }
            try
            {
                if (listaTexto.Count() > 0)
                {
                    listaTexto.Clear();
                }
                if (Path.GetExtension(archivo.FileName).ToLower() != ".txt")
                {
                    ViewBag.Error = "Seleccione un archivo de texto (.txt).";
                    return View("Error");
                }
                using (var reader = new StreamReader(archivo.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        listaTexto.Add(linea);
                    }
                }
                return RedirectToAction("SubirDatos", listaTexto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar el archivo: " + ex.Message;
                return View("Error");
            }
        }
        [HttpPost]
        [Route("auto")]
        public IActionResult construir()
        {
            try
            {
                if (listaTexto.Count() == 0)
                {
                    return View("Error");
                }
                List<Estado> constructor = new List<Estado>();
                int inicial;
                int posicion;
                inicial = Convert.ToInt32(listaTexto[1]) - 1;
                for (int i = 0; i < Convert.ToInt32(listaTexto[0]); i++)
                {
                    Estado nuevoEstado = new Estado(i+1);
                    constructor.Add(nuevoEstado);
                }
                for (int i = 0; i < listaTexto[2].Split(",").Count(); i++)
                {
                    constructor[Convert.ToInt32(listaTexto[2].Split(",")[i])-1].final = true;
                }
                for (int i = 3; i < Convert.ToInt32(listaTexto.Count()); i++)
                {
                    posicion = Convert.ToInt32(listaTexto[i].Split(",")[0]) - 1; 
                    constructor[posicion].alfabeto.Add(listaTexto[i].Split(",")[1]);
                    constructor[posicion].transiciones.Add(constructor[Convert.ToInt32(listaTexto[i].Split(",")[2])-1]);
                }
                automata.Crear(constructor[inicial]);
                return View("Comprobar", resultado);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar el archivo: " + ex.Message;
                return View("Error");
            }
        }
        [HttpPost]
        [Route("Verificar")]
        public IActionResult comprobar(String entrada)
        {
            if (automata.entrada(entrada))
            {
                resultado = "aceptado";
            }
            else
            {
                resultado = "no aceptado";
            }
            return View("Comprobar", resultado);
        }
    }
}
