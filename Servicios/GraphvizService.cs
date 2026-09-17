using Practica2.Estructuras;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Practica2.Servicios
{
    public static class GraphvizServicio
    {
        public static void GenerarReporteCola(
            ColaReproduccion cola,
            string rutaDot,
            string rutaPng)
        {
            StringBuilder contenido =
                new StringBuilder();

            contenido.AppendLine("digraph Cola {");
            contenido.AppendLine("rankdir=LR;");
            contenido.AppendLine("bgcolor=\"transparent\";");
            contenido.AppendLine("node [shape=box, style=\"rounded,filled\", fillcolor=\"#16324A\", color=\"#5DADE2\", fontcolor=\"white\", fontname=\"Segoe UI\", fontsize=12];");
            contenido.AppendLine("edge [color=\"#5DADE2\", penwidth=2, arrowsize=0.8];");
            contenido.AppendLine("nodesep=0.5;");
            contenido.AppendLine("ranksep=0.8;");

            cola.GenerarGraphviz(contenido);

            contenido.AppendLine("}");

            File.WriteAllText(
                rutaDot,
                contenido.ToString());

            GenerarImagen(
                rutaDot,
                rutaPng);
        }

        public static void GenerarReporteArbol(
            ArbolCancion arbol,
            string rutaDot,
            string rutaPng)
        {
            StringBuilder contenido =
                new StringBuilder();

            contenido.AppendLine("digraph Arbol {");

            contenido.AppendLine("rankdir=TB;");
            contenido.AppendLine("bgcolor=\"transparent\";");

            contenido.AppendLine(
                "graph [pad=0.3, nodesep=0.7, ranksep=1.0];");

            contenido.AppendLine(
                "node [shape=box, style=\"rounded,filled\", fillcolor=\"#16324A\", color=\"#5DADE2\", penwidth=2, fontcolor=\"white\", fontname=\"Segoe UI\", fontsize=12, margin=\"0.18,0.10\"];");

            contenido.AppendLine(
                "edge [color=\"#5DADE2\", penwidth=2, arrowsize=0.8];");

            arbol.GenerarGraphviz(contenido);

            contenido.AppendLine("}");

            File.WriteAllText(
                rutaDot,
                contenido.ToString());

            GenerarImagen(
                rutaDot,
                rutaPng);
        }

        private static void GenerarImagen(
                string rutaDot,
                string rutaPng)
            {
                Process proceso = new Process();

                proceso.StartInfo.FileName = "dot";

                proceso.StartInfo.Arguments =
                    $"-Tpng \"{rutaDot}\" -o \"{rutaPng}\"";

                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.CreateNoWindow = true;

                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.StartInfo.RedirectStandardError = true;

                proceso.Start();

                string salida = proceso.StandardOutput.ReadToEnd();
                string error = proceso.StandardError.ReadToEnd();

                proceso.WaitForExit();

                if (proceso.ExitCode != 0)
                {
                    MessageBox.Show(
                        "Error al generar el reporte gráfico:\n\n" +
                        error,
                        "Graphviz",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
    }
}