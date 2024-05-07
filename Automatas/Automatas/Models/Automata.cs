namespace Automatas.Models
{
    public class Automata
    {
        private Estado ini = null;
        public void Crear(List<String> listaTexto)
        {
            List<Estado> constructor = new List<Estado>();
            int inicial;
            int posicion;
            inicial = Convert.ToInt32(listaTexto[1]) - 1;
            for (int i = 0; i < Convert.ToInt32(listaTexto[0]); i++)
            {
                Estado nuevoEstado = new Estado(i + 1);
                constructor.Add(nuevoEstado);
            }
            for (int i = 0; i < listaTexto[2].Split(",").Count(); i++)
            {
                constructor[Convert.ToInt32(listaTexto[2].Split(",")[i]) - 1].final = true;
            }
            for (int i = 3; i < Convert.ToInt32(listaTexto.Count()); i++)
            {
                posicion = Convert.ToInt32(listaTexto[i].Split(",")[0]) - 1;
                constructor[posicion].alfabeto.Add(listaTexto[i].Split(",")[1]);
                constructor[posicion].transiciones.Add(constructor[Convert.ToInt32(listaTexto[i].Split(",")[2]) - 1]);
            }
            ini = constructor[inicial];
        }
        public bool entrada(string entrada)
        {
            return comprobar(entrada, ini, 0);
        }
        private bool comprobar(string entrada, Estado actual, int inicio)
        {
            if (entrada.Length == 0)
            {
                if (actual.final)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            int indice = -1;
            for (int i = 0; i < actual.alfabeto.Count(); i++)
            {
                if (actual.alfabeto[i] == Convert.ToString(entrada[inicio]))
                {
                    indice = i;
                    break;
                }
            }
            if (indice == -1)
            {
                return false;
            }
            actual = actual.transiciones[indice];
            if (entrada.Length == (inicio + 1))
            {
                if (actual.final)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return comprobar(entrada, actual, (inicio + 1));
        }
        public void borrar()
        {
            ini = null;
        }
        public String recorrer(string entrada, string resultado)
        {
            return recorrido(entrada, ini, 0, resultado);
        }
        private String recorrido(string entrada, Estado actual, int inicio, string resultado)
        {
            if (entrada.Length == 0)
            {
                return actual.nombre.ToString();
            }
            string estados = resultado;
            estados += actual.nombre.ToString() + "->";
            int indice = -1;
            for (int i = 0; i < actual.alfabeto.Count(); i++)
            {
                if (actual.alfabeto[i] == Convert.ToString(entrada[inicio]))
                {
                    indice = i;
                    break;
                }
            }
            if (indice == -1)
            {
                return estados + "desconocido";
            }
            actual = actual.transiciones[indice];
            if (entrada.Length == (inicio + 1))
            {
                return estados + actual.nombre.ToString();
            }
            return recorrido(entrada, actual, (inicio + 1), estados);
        }
    }
}