using Practica2.Modelos;

namespace Practica2.Estructuras
{
    public class ColaReproduccion
    {
        private NodoCola? frente;
        private NodoCola? final;

        public bool EstaVacia()
        {
            return frente == null;
        }

        public void Encolar(Cancion cancion)
        {
            NodoCola nuevo = new NodoCola(cancion);

            if (EstaVacia())
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final!.Siguiente = nuevo;
                final = nuevo;
            }
        }

        public Cancion? Desencolar()
        {
            if (EstaVacia())
            {
                return null;
            }

            Cancion cancion = frente!.Cancion;

            frente = frente.Siguiente;

            if (frente == null)
            {
                final = null;
            }

            return cancion;
        }

        public Cancion? VerFrente()
        {
            if (EstaVacia())
            {
                return null;
            }

            return frente!.Cancion;
        }
    }
}