namespace Automatas.Models
{
    public class EstadoN
    {
        public EstadoN(string dato, List<Transition> trans, bool final)
        {
            nombre = dato;
            final = final;
            transiciones = trans;
        }
        public string nombre { get; set; }
        public bool final { get; set; }
        public List<Transition> transiciones;
    }
}
