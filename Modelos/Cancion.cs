namespace Practica2.Modelos
{
    public class Cancion
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string Genero { get; set; }
        public int DuracionMinutos { get; set; }

        public Cancion(string titulo, string artista, string genero, int duracionMinutos)
        {
            Titulo = titulo.Trim();
            Artista = artista.Trim();
            Genero = genero.Trim();
            DuracionMinutos = duracionMinutos;
        }

        public override string ToString()
        {
            return $"{Titulo} - {Artista}";
        }
    }
}