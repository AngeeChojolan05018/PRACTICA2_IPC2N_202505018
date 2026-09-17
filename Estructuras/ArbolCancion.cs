using Practica2.Modelos;
using System.Text;

namespace Practica2.Estructuras
{
    public class ArbolCancion
    {
        private NodoArbol? raiz;

        public void Insertar(Cancion cancion)
        {
            NodoArbol nuevo =
                new NodoArbol(cancion);

            if (raiz == null)
            {
                raiz = nuevo;
                return;
            }

            InsertarRecursivo(
                raiz,
                nuevo);
        }

        private void InsertarRecursivo(
            NodoArbol actual,
            NodoArbol nuevo)
        {
            int comparacion =
                string.Compare(
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
                    InsertarRecursivo(
                        actual.Izquierdo,
                        nuevo);
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
                    InsertarRecursivo(
                        actual.Derecho,
                        nuevo);
                }
            }
        }

        public Cancion? Buscar(string titulo)
        {
            return BuscarRecursivo(
                raiz,
                titulo);
        }

        private Cancion? BuscarRecursivo(
            NodoArbol? actual,
            string titulo)
        {
            if (actual == null)
            {
                return null;
            }

            int comparacion =
                string.Compare(
                    titulo,
                    actual.Cancion.Titulo,
                    StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
            {
                return actual.Cancion;
            }

            if (comparacion < 0)
            {
                return BuscarRecursivo(
                    actual.Izquierdo,
                    titulo);
            }

            return BuscarRecursivo(
                actual.Derecho,
                titulo);
        }

        public void RecorrerEnOrden(
            Action<Cancion> accion)
        {
            RecorrerEnOrdenRecursivo(
                raiz,
                accion);
        }

        private void RecorrerEnOrdenRecursivo(
            NodoArbol? actual,
            Action<Cancion> accion)
        {
            if (actual == null)
            {
                return;
            }

            RecorrerEnOrdenRecursivo(
                actual.Izquierdo,
                accion);

            accion(actual.Cancion);

            RecorrerEnOrdenRecursivo(
                actual.Derecho,
                accion);
        }

        public void GenerarGraphviz(
            StringBuilder contenido)
        {
            if (raiz == null)
            {
                return;
            }

            int contador = 0;

            GenerarGraphvizRecursivo(
                raiz,
                contenido,
                ref contador);
        }

        private string GenerarGraphvizRecursivo(
            NodoArbol actual,
            StringBuilder contenido,
            ref int contador)
        {
            string idActual =
                "n" + contador;

            contador++;

            string titulo =
                EscaparTexto(
                    actual.Cancion.Titulo);

            contenido.AppendLine(
                $"{idActual} [label=\"{titulo}\"];");

            if (actual.Izquierdo != null)
            {
                string idIzquierdo =
                    GenerarGraphvizRecursivo(
                        actual.Izquierdo,
                        contenido,
                        ref contador);

                contenido.AppendLine(
                    $"{idActual} -> {idIzquierdo};");
            }

            if (actual.Derecho != null)
            {
                string idDerecho =
                    GenerarGraphvizRecursivo(
                        actual.Derecho,
                        contenido,
                        ref contador);

                contenido.AppendLine(
                    $"{idActual} -> {idDerecho};");
            }

            return idActual;
        }

        private string EscaparTexto(
            string texto)
        {
            return texto
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
        }
    }
}