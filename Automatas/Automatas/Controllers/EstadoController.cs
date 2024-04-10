using Microsoft.AspNetCore.Mvc;
using Automatas.Models;
using System.IO;
using System.Web;
namespace Automatas.Controllers
{
    public class EstadoController : Controller
    {
        public static List<String> listaTexto = new List<String>();
        public IActionResult Index()
        {
            return View("Index");
        }
        [Route("carga")]
        public IActionResult SubirDatos(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                ViewBag.Error = "Seleccione un archivo válido.";
                return View("SubirDatos", listaTexto);
            }

            try
            {
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
        public IActionResult Comprobar()
        {
            return View("Comprobar");
        }
    }
}
