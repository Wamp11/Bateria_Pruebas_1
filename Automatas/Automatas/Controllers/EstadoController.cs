using Microsoft.AspNetCore.Mvc;
using Automatas.Models;
using System.IO;
using System.Web;
namespace Automatas.Controllers
{
    public class EstadoController : Controller
    {
        public static string resultado = "";
        public static string recorrido = "";
        public static Automata automata = new Automata();
        public static AutomataN automataN = new AutomataN();
        public static List<String> listaTexto = new List<String>();
        public IActionResult Index()
        {
            return View("Index");
        }
        [Route("carga")]
        public IActionResult SubirDatos(IFormFile archivo)
        {
            resultado = "";
            recorrido = "";
            automata.borrar();
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
                automata.Crear(listaTexto);
                listaTexto.Clear();
                List<String> salidas = new List<String>();
                salidas.Add("True");
                salidas.Add(resultado);
                salidas.Add(recorrido);
                return View("Comprobar", salidas);
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
            List<String> salidas = new List<String>();
            recorrido = "";
            resultado = "";
            if (automata.entrada(entrada))
            {
                resultado = "aceptado";
            }
            else
            {
                resultado = "no aceptado";
            }
            recorrido = automata.recorrer(entrada, recorrido);
            if (recorrido.Contains("desconocido"))
            {
                recorrido = recorrido.Substring(0, (recorrido.Length - 13));
                salidas.Add("False");
            }
            else
            {
                salidas.Add("True");
            }
            salidas.Add(resultado);
            salidas.Add(recorrido);
            return View("Comprobar", salidas);
        }
        [Route("carga2")]
        public IActionResult SubirDatos2(IFormFile archivo)
        {
            resultado = "";
            recorrido = "";
            automataN.borrar();
            if (archivo == null || archivo.Length == 0)
            {
                ViewBag.Error = "Seleccione un archivo válido.";
                return View("SubirDatos2", listaTexto);
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
                    return View("Error2");
                }
                using (var reader = new StreamReader(archivo.OpenReadStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        listaTexto.Add(linea);
                    }
                }
                return RedirectToAction("SubirDatos2", listaTexto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar el archivo: " + ex.Message;
                return View("Error2");
            }
        }
        [HttpPost]
        [Route("auto2")]
        public IActionResult construir2()
        {
            try
            {
                if (listaTexto.Count() == 0)
                {
                    return View("Error2");
                }
                automataN.Crear(listaTexto);
                listaTexto.Clear();
                List<String> salidas = new List<String>();
                salidas.Add("True");
                salidas.Add(resultado);
                salidas.Add(recorrido);
                return View("Comprobar2", salidas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al procesar el archivo: " + ex.Message;
                return View("Error2");
            }
        }
        public IActionResult comprobar2(String entrada)
        {
            List<String> salidas = new List<String>();
            recorrido = "";
            resultado = "";
            if (automataN.entrada(entrada))
            {
                resultado = "aceptado";
            }
            else
            {
                resultado = "no aceptado";
            }
            /*recorrido = automata.recorrer(entrada, recorrido);
            if (recorrido.Contains("desconocido"))
            {
                recorrido = recorrido.Substring(0, (recorrido.Length - 13));
                salidas.Add("False");
            }
            else
            {
                salidas.Add("True");
            }*/
            salidas.Add(resultado);
            salidas.Add("");
            return View("Comprobar2", salidas);
        }
    }
}