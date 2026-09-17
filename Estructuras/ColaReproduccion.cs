using Practica2.Modelos;
using System.Text;

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

        public int Cantidad()
        {
            int cantidad = 0;

            NodoCola? actual = frente;

            while (actual != null)
            {
                cantidad++;
                actual = actual.Siguiente;
            }

            return cantidad;
        }

        public int TiempoTotal()
        {
            int total = 0;

            NodoCola? actual = frente;

            while (actual != null)
            {
                total += actual.Cancion.DuracionMinutos;
                actual = actual.Siguiente;
            }

            return total;
        }

        public void GenerarGraphviz(StringBuilder contenido)
        {
            if (EstaVacia())
            {
                contenido.AppendLine(
                    "vacio [label=\"Cola vacía\", shape=box, style=\"rounded,filled\"];");

                return;
            }

            NodoCola? actual = frente;

            int contador = 0;

            while (actual != null)
            {
                string titulo = actual.Cancion.Titulo
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"");

                contenido.AppendLine(
                    $"n{contador} [label=\"{titulo}\"];");

                if (actual.Siguiente != null)
                {
                    contenido.AppendLine(
                        $"n{contador} -> n{contador + 1};");
                }

                contador++;

                actual = actual.Siguiente;
            }
        }
    }
}