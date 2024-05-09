namespace Automatas.Models
{
    public class AutomataN
    {
        private EstadoN ini = null;
        public void Crear(List<String> listaTexto)
        {
            List<EstadoN> constructor = new List<EstadoN>();
            int inicial;
            int posicion;
            inicial = Convert.ToInt32(listaTexto[1]) - 1;
            for (int i = 0; i < Convert.ToInt32(listaTexto[0]); i++)
            {
                EstadoN nuevoEstado = new EstadoN(i + 1);
                constructor.Add(nuevoEstado);
            }
            for (int i = 0; i < listaTexto[2].Split(",").Count(); i++)
            {
                constructor[Convert.ToInt32(listaTexto[2].Split(",")[i]) - 1].final = true;
            }
            for (int i = 3; i < Convert.ToInt32(listaTexto.Count()); i++)
            {
                posicion = Convert.ToInt32(listaTexto[i].Split(",")[0]) - 1;
                if (!constructor[posicion].alfabeto.Contains(listaTexto[i].Split(",")[1].Trim()))
                {
                    constructor[posicion].alfabeto.Add(listaTexto[i].Split(",")[1].Trim());
                    List<EstadoN> nuevo = new List<EstadoN>();
                    constructor[posicion].transiciones.Add(nuevo);
                    constructor[posicion].transiciones[constructor[posicion].transiciones.Count() - 1].Add(constructor[Convert.ToInt32(listaTexto[i].Split(",")[2]) - 1]);
                }
                else
                {
                    constructor[posicion].transiciones[constructor[posicion].alfabeto.IndexOf(listaTexto[i].Split(",")[1].Trim())].Add(constructor[Convert.ToInt32(listaTexto[i].Split(",")[2]) - 1]);
                }
            }
            ini = constructor[inicial];
        }
        public void borrar()
        {
            ini = null;
        }
    }
}
