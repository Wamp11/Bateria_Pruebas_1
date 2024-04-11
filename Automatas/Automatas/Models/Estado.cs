namespace Automatas.Models
{
    public class Estado
    {
        public Estado(int dato) 
        {
            nombre = dato;
            final = false;
            alfabeto = new List<String>();
            transiciones = new List<Estado>();
        }
        public int nombre { get; set; }
        public bool final { get; set; }
        public List<String> alfabeto { get; set; }
        public List<Estado> transiciones { get; set; }
    }
}
