using Practica2.Servicios;
using Practica2.Modelos;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Practica2
{
    public partial class Form1 : Form
    {
        private SistemaMusica sistema;

        private Panel panelBiblioteca = new Panel();
        private Panel panelCola = new Panel();
        private Panel panelArbol = new Panel();
        private Panel panelReproduccion = new Panel();

        private Label lblTitulo = new Label();
        private Label lblEnCola = new Label();
        private Label lblTiempoTotal = new Label();
        private DataGridView tablaBiblioteca = new DataGridView();

        private PictureBox imagenCola = new PictureBox();
        private PictureBox imagenArbol = new PictureBox();

        private TextBox txtBuscar = new TextBox();

        private Button btnBuscar = new Button();
        private Button btnAgregar = new Button();
        private Button btnReproducir = new Button();

        private Label lblCancionActual = new Label();

        public Form1()
        {
            InitializeComponent();

            sistema = new SistemaMusica();

            ConfigurarVentana();
            CrearInterfaz();

            CargarCanciones();
            CargarBiblioteca();

            ActualizarInformacionCola();
            GenerarReportes();
        }

        private void ConfigurarVentana()
        {
            Text = "Reproductor de Música";
            Width = 1400;
            Height = 850;
            StartPosition = FormStartPosition.CenterScreen;

            BackColor = Color.FromArgb(7, 20, 35);
        }

        private void CrearInterfaz()
        {
            CrearPaneles();
            CrearBiblioteca();
            CrearReporteCola();
            CrearReporteArbol();
            CrearPanelReproduccion();
        }

        private void CrearPaneles()
        {
            panelBiblioteca.Location = new Point(20, 20);
            panelBiblioteca.Size = new Size(400, 450);
            panelBiblioteca.BackColor = Color.FromArgb(10, 30, 48);
            panelBiblioteca.BorderStyle = BorderStyle.FixedSingle;

            panelCola.Location = new Point(435, 20);
            panelCola.Size = new Size(440, 450);
            panelCola.BackColor = Color.FromArgb(10, 30, 48);
            panelCola.BorderStyle = BorderStyle.FixedSingle;

            panelArbol.Location = new Point(890, 20);
            panelArbol.Size = new Size(470, 450);
            panelArbol.BackColor = Color.FromArgb(10, 30, 48);
            panelArbol.BorderStyle = BorderStyle.FixedSingle;

            panelReproduccion.Location = new Point(20, 490);
            panelReproduccion.Size = new Size(1340, 290);
            panelReproduccion.BackColor = Color.FromArgb(10, 30, 48);
            panelReproduccion.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(panelBiblioteca);
            Controls.Add(panelCola);
            Controls.Add(panelArbol);
            Controls.Add(panelReproduccion);
        }

        private void CrearBiblioteca()
        {
            Label titulo = new Label();

            titulo.Text = "Biblioteca / Buscar";
            titulo.Location = new Point(15, 15);
            titulo.AutoSize = true;
            titulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titulo.ForeColor = Color.White;

            txtBuscar.Location = new Point(15, 55);
            txtBuscar.Size = new Size(300, 30);
            txtBuscar.Text = "Título exacto...";

            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(320, 54);
            btnBuscar.Size = new Size(65, 32);

            btnBuscar.Click += BtnBuscar_Click;

            tablaBiblioteca.Location = new Point(15, 100);
            tablaBiblioteca.Size = new Size(370, 320);

            tablaBiblioteca.BackgroundColor =
                Color.FromArgb(10, 30, 48);

            tablaBiblioteca.ForeColor = Color.Black;

            tablaBiblioteca.AllowUserToAddRows = false;
            tablaBiblioteca.AllowUserToDeleteRows = false;
            tablaBiblioteca.ReadOnly = true;

            tablaBiblioteca.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tablaBiblioteca.Columns.Add("Titulo", "Título");
            tablaBiblioteca.Columns.Add("Artista", "Artista");
            tablaBiblioteca.Columns.Add("Genero", "Género");
            tablaBiblioteca.Columns.Add("Duracion", "Duración");

            panelBiblioteca.Controls.Add(titulo);
            panelBiblioteca.Controls.Add(txtBuscar);
            panelBiblioteca.Controls.Add(btnBuscar);
            panelBiblioteca.Controls.Add(tablaBiblioteca);
        }

        private void CrearReporteCola()
        {
            Label titulo = new Label();

            titulo.Text = "Reporte de cola";
            titulo.Location = new Point(15, 15);
            titulo.AutoSize = true;
            titulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titulo.ForeColor = Color.White;

            imagenCola = new PictureBox();

            imagenCola.Location = new Point(15, 55);
            imagenCola.Size = new Size(405, 370);
            imagenCola.SizeMode = PictureBoxSizeMode.Zoom;
            imagenCola.BackColor = Color.FromArgb(5, 18, 30);

            panelCola.Controls.Add(titulo);
            panelCola.Controls.Add(imagenCola);
        }

        private void CrearReporteArbol()
        {
            Label titulo = new Label();

            titulo.Text = "Reporte del árbol (por título)";
            titulo.Location = new Point(15, 15);
            titulo.AutoSize = true;
            titulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titulo.ForeColor = Color.White;

            imagenArbol = new PictureBox();

            imagenArbol.Location = new Point(15, 55);
            imagenArbol.Size = new Size(435, 370);
            imagenArbol.SizeMode = PictureBoxSizeMode.Zoom;
            imagenArbol.BackColor = Color.FromArgb(5, 18, 30);

            panelArbol.Controls.Add(titulo);
            panelArbol.Controls.Add(imagenArbol);
        }

        private void CrearPanelReproduccion()
        {
            lblTitulo.Text = "Reproduciendo ahora";
            lblTitulo.Location = new Point(25, 25);
            lblTitulo.AutoSize = true;
            lblTitulo.Font =
                new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;

            lblCancionActual.Text = "Ninguna canción";
            lblCancionActual.Location = new Point(25, 70);
            lblCancionActual.AutoSize = true;
            lblCancionActual.Font =
                new Font("Segoe UI", 20, FontStyle.Bold);
            lblCancionActual.ForeColor = Color.DeepPink;

            lblEnCola.Text = "En cola: 0";
            lblEnCola.Location = new Point(850, 40);
            lblEnCola.AutoSize = true;
            lblEnCola.Font =
                new Font("Segoe UI", 16, FontStyle.Bold);
            lblEnCola.ForeColor = Color.White;

            lblTiempoTotal.Text = "Tiempo total: 0 min";
            lblTiempoTotal.Location = new Point(1050, 40);
            lblTiempoTotal.AutoSize = true;
            lblTiempoTotal.Font =
                new Font("Segoe UI", 16, FontStyle.Bold);
            lblTiempoTotal.ForeColor = Color.White;

            btnAgregar.Text = "Agregar a la cola";
            btnAgregar.Location = new Point(850, 170);
            btnAgregar.Size = new Size(200, 50);

            btnAgregar.Click += BtnAgregar_Click;

            btnReproducir.Text = "Reproducir siguiente";
            btnReproducir.Location = new Point(1070, 170);
            btnReproducir.Size = new Size(220, 50);

            btnReproducir.Click += BtnReproducir_Click;

            panelReproduccion.Controls.Add(lblTitulo);
            panelReproduccion.Controls.Add(lblCancionActual);
            panelReproduccion.Controls.Add(lblEnCola);
            panelReproduccion.Controls.Add(lblTiempoTotal);
            panelReproduccion.Controls.Add(btnAgregar);
            panelReproduccion.Controls.Add(btnReproducir);
        }

        private void CargarCanciones()
        {
            string ruta = Path.Combine(
                Application.StartupPath,
                "Datos",
                "Canciones.json");

            sistema.CargarCanciones(ruta);
        }

        private void CargarBiblioteca()
        {
            tablaBiblioteca.Rows.Clear();

            sistema.Biblioteca.RecorrerEnOrden(
                cancion =>
                {
                    tablaBiblioteca.Rows.Add(
                        cancion.Titulo,
                        cancion.Artista,
                        cancion.Genero,
                        cancion.DuracionMinutos + " min");
                });
        }

        private void BtnBuscar_Click(
            object? sender,
            EventArgs e)
        {
            string titulo = txtBuscar.Text.Trim();

            if (titulo == "" ||
                titulo == "Título exacto...")
            {
                MessageBox.Show(
                    "Escribe el título de una canción.");

                return;
            }

            Cancion? cancion =
                sistema.Biblioteca.Buscar(titulo);

            if (cancion == null)
            {
                MessageBox.Show(
                    "La canción no fue encontrada.");

                return;
            }

            tablaBiblioteca.ClearSelection();

            foreach (DataGridViewRow fila
                in tablaBiblioteca.Rows)
            {
                if (fila.Cells["Titulo"].Value?.ToString()
                    == cancion.Titulo)
                {
                    fila.Selected = true;

                    tablaBiblioteca.CurrentCell =
                        fila.Cells["Titulo"];

                    break;
                }
            }
        }

        private void BtnAgregar_Click(
            object? sender,
            EventArgs e)
        {
            if (tablaBiblioteca.CurrentRow == null)
            {
                MessageBox.Show(
                    "Selecciona una canción de la biblioteca.");

                return;
            }

            string titulo =
                tablaBiblioteca.CurrentRow
                .Cells["Titulo"]
                .Value?
                .ToString() ?? "";

            if (titulo == "")
            {
                return;
            }

            Cancion? cancion =
                sistema.Biblioteca.Buscar(titulo);

            if (cancion == null)
            {
                MessageBox.Show(
                    "No se encontró la canción.");

                return;
            }

            sistema.Cola.Encolar(cancion);

            ActualizarInformacionCola();

            // Actualizar reporte de Graphviz
            GenerarReportes();

            MessageBox.Show(
                "Canción agregada a la cola.");
        }

        private void BtnReproducir_Click(
            object? sender,
            EventArgs e)
        {
            Cancion? cancion =
                sistema.Cola.Desencolar();

            if (cancion == null)
            {
                lblCancionActual.Text =
                    "Ninguna canción";

                MessageBox.Show(
                    "La cola está vacía.");

                ActualizarInformacionCola();

                GenerarReportes();

                return;
            }

            lblCancionActual.Text =
                cancion.Titulo + " - " + cancion.Artista;

            ActualizarInformacionCola();

            // Actualizar reporte de Graphviz
            GenerarReportes();
        }

        private void ActualizarInformacionCola()
        {
            lblEnCola.Text =
                "En cola: " + sistema.Cola.Cantidad();

            lblTiempoTotal.Text =
                "Tiempo total: "
                + sistema.Cola.TiempoTotal()
                + " min";
        }

        private void GenerarReportes()
        {
            string carpeta = Path.Combine(
                Application.StartupPath,
                "Reportes");

            Directory.CreateDirectory(carpeta);

            string rutaColaDot = Path.Combine(
                carpeta,
                "Cola.dot");

            string rutaColaPng = Path.Combine(
                carpeta,
                "Cola.png");

            string rutaArbolDot = Path.Combine(
                carpeta,
                "Arbol.dot");

            string rutaArbolPng = Path.Combine(
                carpeta,
                "Arbol.png");

            GraphvizServicio.GenerarReporteCola(
                sistema.Cola,
                rutaColaDot,
                rutaColaPng);

            GraphvizServicio.GenerarReporteArbol(
                sistema.Biblioteca,
                rutaArbolDot,
                rutaArbolPng);

            CargarImagenCola(rutaColaPng);
            CargarImagenArbol(rutaArbolPng);
        }

        private void CargarImagenCola(string ruta)
        {
            if (!File.Exists(ruta))
            {
                imagenCola.Image = null;
                return;
            }

            using (Image imagenTemporal =
                Image.FromFile(ruta))
            {
                imagenCola.Image =
                    new Bitmap(imagenTemporal);
            }
        }

        private void CargarImagenArbol(string ruta)
        {
            if (!File.Exists(ruta))
            {
                imagenArbol.Image = null;
                return;
            }

            using (Image imagenTemporal =
                Image.FromFile(ruta))
            {
                imagenArbol.Image =
                    new Bitmap(imagenTemporal);
            }
        }
    }
}