namespace Automatas.Models
{
    public class Automata
    {
        private Estado inicial = null;
        public void Crear(Estado dato)
        {
            inicial = dato;
        }
        public bool entrada(string entrada)
        {
            return comprobar(entrada, inicial, 0);
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
            inicial = null;
        }
        public String recorrer(string entrada, string resultado)
        {
            return recorrido(entrada, inicial, 0, resultado);
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