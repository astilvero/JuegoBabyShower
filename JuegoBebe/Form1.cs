using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoBebe
{
    public partial class Form1 : Form
    {
        int TamanioColumnasFilas = 4;
        int Movimientos = 0;
        int CantidadCartasVolteadas = 0;
        List<string> CartasEnumeradas;
        List<string> CartasRevueltas;
        ArrayList CartasSeleccionadas;
        PictureBox CartaT1;
        PictureBox CartaT2;
        int CartaActual=0;

        public Form1()
        {
            InitializeComponent();
            iniciarJuego();
        }

        public void iniciarJuego()
        {
            timer1.Enabled = false;
            timer1.Stop();
            lblRecord.Text = "0";
            CantidadCartasVolteadas = 0;
            Movimientos = 0;
            PanelJuego.Controls.Clear();
            CartasEnumeradas = new List<string>();
            CartasRevueltas = new List<string>();
            CartasSeleccionadas = new ArrayList();
            for (int i = 0; i < 8; i++)
            {
                CartasEnumeradas.Add(i.ToString());
                CartasEnumeradas.Add(i.ToString());
            }
            var Aleatorio = new Random();
            var Resultado = CartasEnumeradas.OrderBy(item => Aleatorio.Next());
            foreach(string ValorCarta in Resultado)
            {
                CartasRevueltas.Add(ValorCarta);
            }
            var tablaPanel = new TableLayoutPanel();
            tablaPanel.RowCount = TamanioColumnasFilas;
            tablaPanel.ColumnCount = TamanioColumnasFilas;
            for (int i = 0; i < TamanioColumnasFilas; i++)
            {
                var Porcentaje = 150f / (float)TamanioColumnasFilas - 10;
                tablaPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,Porcentaje));
                tablaPanel.RowStyles.Add(new RowStyle(SizeType.Percent,Porcentaje));
            }
            int contadorFichas = 1;

            for (int i = 0; i < TamanioColumnasFilas; i++)
            {
                for (int j = 0; j < TamanioColumnasFilas; j++)
                {
                    var CartasJuego = new PictureBox();
                    CartasJuego.Name = string.Format("{0}", contadorFichas);
                    CartasJuego.Dock = DockStyle.Fill;
                    CartasJuego.SizeMode = PictureBoxSizeMode.StretchImage;
                    CartasJuego.Image = Properties.Resources.Isham;
                    CartasJuego.Cursor = Cursors.Hand;
                    CartasJuego.Click += btnCarta_Click;
                    tablaPanel.Controls.Add(CartasJuego, j, i);
                    contadorFichas++;
                }
            }
            tablaPanel.Dock = DockStyle.Fill;
            PanelJuego.Controls.Add(tablaPanel);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnReinicio_Click(object sender, EventArgs e)
        {
            iniciarJuego();
        }
        private void btnCarta_Click(object sender, EventArgs e)
        {
            if (CartasSeleccionadas.Count < 2)
            {            
            Movimientos++;
            lblRecord.Text = Convert.ToString(Movimientos);
            var CartaSeleccionadaUsuario = (PictureBox)sender;

            CartaActual = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaSeleccionadaUsuario.Name) - 1]);
            CartaSeleccionadaUsuario.Image = RecuperarImagen(CartaActual);
            CartasSeleccionadas.Add(CartaSeleccionadaUsuario);

                if (CartasSeleccionadas.Count==2)
                {
                    CartaT1 = (PictureBox)CartasSeleccionadas[0];
                    CartaT2 = (PictureBox)CartasSeleccionadas[1];
                    int Carta1 = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaT1.Name)-1]);
                    int Carta2 = Convert.ToInt32(CartasRevueltas[Convert.ToInt32(CartaT2.Name) - 1]);
                    
                    if(Carta1 != Carta2)
                    {
                        timer1.Enabled = true;
                        timer1.Start();;
                    } else
                    {
                        CantidadCartasVolteadas++;
                        if (CantidadCartasVolteadas>7)
                        {
                            MessageBox.Show("El juego ha terminado");
                        }
                        CartaT1.Enabled = false; CartaT2.Enabled = false;
                        CartasSeleccionadas.Clear();
                    }
                }
            }
        }
        public Bitmap RecuperarImagen(int NumeroImagen)
        {
            Bitmap TmpImg = new Bitmap(200, 100);
            switch (NumeroImagen)
            {
                case 0: TmpImg = Properties.Resources.img8;
                    break;
                default: TmpImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("img" + NumeroImagen);
                    break;
            }
            return TmpImg;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int TiempoVirarCarta = 1;
            if (TiempoVirarCarta == 1)
            {
                CartaT1.Image=Properties.Resources.Isham;
                CartaT2.Image = Properties.Resources.Isham;
                CartasSeleccionadas.Clear();
                TiempoVirarCarta = 0;
                timer1.Stop();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
