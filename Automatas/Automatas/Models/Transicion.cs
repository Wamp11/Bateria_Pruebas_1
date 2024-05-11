namespace Automatas.Models
{
    public class Transition : IComparable<Transition>
    {
        public string inicio;
        public string entrada;
        public string final;

        public Transition(string inicio, string entrada, string final)
        {
            this.inicio = inicio;
            this.entrada = entrada;
            this.final = final;
        }
        public Transition() { }
        public string Recorrido()
        {
            return inicio + "->" + final;
        }
        public int CompareTo(Transition other)
        {
            return inicio.CompareTo(other.inicio);
        }

    }
}
