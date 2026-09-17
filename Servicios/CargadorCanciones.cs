using System.Text.Json;
using Practica2.Estructuras;
using Practica2.Modelos;

namespace Practica2.Servicios
{
    public sealed record CancionEntrada(
        string Titulo,
        string Artista,
        string Genero,
        int? Duracion);

    public static class CargadorCanciones
    {
        public static void Cargar(
            string ruta,
            ArbolCancion biblioteca)
        {
            string json = File.ReadAllText(ruta);

            CancionEntrada[] entradas =
                JsonSerializer.Deserialize<CancionEntrada[]>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    })
                ?? Array.Empty<CancionEntrada>();

            foreach (CancionEntrada entrada in entradas)
            {
                int minutos = entrada.Duracion is > 0
                    ? entrada.Duracion.Value
                    : DuracionPorGenero(entrada.Genero);

                Cancion cancion = new Cancion(
                    entrada.Titulo,
                    entrada.Artista,
                    entrada.Genero,
                    minutos);

                biblioteca.Insertar(cancion);
            }
        }

        private static int DuracionPorGenero(string genero)
        {
            return genero.Trim().ToLowerInvariant() switch
            {
                "pop" => 3,
                "rock" => 4,
                "jazz" => 5,
                "clásica" => 8,
                "clasica" => 8,

                _ => throw new InvalidDataException(
                    "Género no permitido")
            };
        }
    }
}