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
        public bool entrada(string entrada)
        {
            return comprobar(entrada, ini, 0);
        }
        private bool comprobar(string entrada, EstadoN actual, int inicio)
        {
            // Caso base: verificamos si hemos consumido toda la entrada
            if (inicio >= entrada.Length)
            {
                // Verificamos si el estado actual es final
                return actual.final;
            }

            // Buscamos el índice del símbolo actual en el alfabeto del estado actual
            int indice = -1;
            for (int i = 0; i < actual.alfabeto.Count(); i++)
            {
                if (actual.alfabeto[i] == Convert.ToString(entrada[inicio]))
                {
                    indice = i;
                    break;
                }
            }

            // Si no encontramos el símbolo en el alfabeto del estado actual,
            // intentamos buscar transiciones con cadena vacía ("")
            if (indice == -1)
            {
                // Buscamos el índice correspondiente a la cadena vacía ("")
                for (int i = 0; i < actual.alfabeto.Count(); i++)
                {
                    if (actual.alfabeto[i] == "")
                    {
                        indice = i;
                        break;
                    }
                }

                // Si encontramos una transición con cadena vacía, no avanzamos el índice de la entrada
                if (indice != -1)
                {
                    // Recorremos las transiciones desde el estado actual con cadena vacía ("")
                    foreach (EstadoN siguienteEstado in actual.transiciones[indice])
                    {
                        // Llamamos recursivamente a comprobar con el siguiente estado y el mismo índice de entrada
                        bool resultado = comprobar(entrada, siguienteEstado, inicio);

                        // Si encontramos un camino que lleva a un estado final, retornamos true
                        if (resultado)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                // Si encontramos un índice válido (símbolo en el alfabeto), avanzamos al siguiente carácter de la entrada
                // y recorremos las transiciones desde el estado actual con el símbolo actual
                foreach (EstadoN siguienteEstado in actual.transiciones[indice])
                {
                    // Llamamos recursivamente a comprobar con el siguiente estado y el siguiente índice de entrada
                    bool resultado = comprobar(entrada, siguienteEstado, inicio + 1);

                    // Si encontramos un camino que lleva a un estado final, retornamos true
                    if (resultado)
                    {
                        return true;
                    }
                }
            }

            // Si ninguna transición llevó a un estado final, retornamos false
            return false;
        }
    }
}
