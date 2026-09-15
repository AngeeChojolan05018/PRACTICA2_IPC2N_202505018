using Practica2.Modelos;

namespace Practica2.Estructuras
{
    public class NodoArbol
    {
        public Cancion Cancion { get; set; }

        public NodoArbol? Izquierdo { get; set; }

        public NodoArbol? Derecho { get; set; }

        public NodoArbol(Cancion cancion)
        {
            Cancion = cancion;
            Izquierdo = null;
            Derecho = null;
        }
    }
}