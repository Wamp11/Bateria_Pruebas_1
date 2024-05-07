namespace Automatas.Models
{
    public class EstadoN
    {
        public EstadoN(int dato)
        {
            nombre = dato;
            final = false;
            alfabeto = new List<String>();
            transiciones = new List<List<EstadoN>>();
        }
        public int nombre { get; set; }
        public bool final { get; set; }
        public List<String> alfabeto { get; set; }
        public List<List<EstadoN>> transiciones { get; set; }
    }
}
