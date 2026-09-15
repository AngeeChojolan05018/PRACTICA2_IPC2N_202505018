using Practica2.Modelos;

namespace Practica2.Estructuras
{
    public class ArbolCancion
    {
        private NodoArbol? raiz;

        public void Insertar(Cancion cancion)
        {
            NodoArbol nuevo = new NodoArbol(cancion);

            if (raiz == null)
            {
                raiz = nuevo;
                return;
            }

            InsertarRecursivo(raiz, nuevo);
        }

        private void InsertarRecursivo(NodoArbol actual, NodoArbol nuevo)
        {
            int comparacion = string.Compare(
                nuevo.Cancion.Titulo,
                actual.Cancion.Titulo,
                StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
            {
                if (actual.Izquierdo == null)
                {
                    actual.Izquierdo = nuevo;
                }
                else
                {
                    InsertarRecursivo(actual.Izquierdo, nuevo);
                }
            }
            else
            {
                if (actual.Derecho == null)
                {
                    actual.Derecho = nuevo;
                }
                else
                {
                    InsertarRecursivo(actual.Derecho, nuevo);
                }
            }
        }

        public Cancion? Buscar(string titulo)
        {
            return BuscarRecursivo(raiz, titulo);
        }

        private Cancion? BuscarRecursivo(NodoArbol? actual, string titulo)
        {
            if (actual == null)
            {
                return null;
            }

            int comparacion = string.Compare(
                titulo,
                actual.Cancion.Titulo,
                StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
            {
                return actual.Cancion;
            }

            if (comparacion < 0)
            {
                return BuscarRecursivo(actual.Izquierdo, titulo);
            }

            return BuscarRecursivo(actual.Derecho, titulo);
        }
    }
}