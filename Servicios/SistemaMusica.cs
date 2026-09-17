using Practica2.Estructuras;

namespace Practica2.Servicios
{
    public class SistemaMusica
    {
        public ColaReproduccion Cola { get; set; }

        public ArbolCancion Biblioteca { get; set; }

        public SistemaMusica()
        {
            Cola = new ColaReproduccion();
            Biblioteca = new ArbolCancion();
        }

        public void CargarCanciones(string ruta)
        {
            CargadorCanciones.Cargar(
                ruta,
                Biblioteca);
        }
    }
}