using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace crucigramaBienLocote
{
    public partial class main_form : Form
    {

        pistasLocas vista_pistas = new pistasLocas();

        //Esto nos sirve para poder referencia a cada una de las celdas que vamos a crear
        List<id_cells> idc = new List<id_cells>();

        //Esta es la carga del archivo del crucigrama, se genera con una funcion
        public string pzl_file = ArchivoLocoteRandom();

        public static int colorDeFondo_num;
        // Esto se encarga de la carga aleatoria de los archivos

        static string ArchivoLocoteRandom()
        {
            //Se carga un archivo por defecto a pesar de que este será sobreescrito por otro, just in case
            string file = Application.StartupPath + "\\Pzls\\pzl_1.txt";
            //Se genera un numero aleatorio del 1 al 3, que corresponde a la cantidad actual de archivos
            int fileToOpen = NumeroRandom(1, 5);
            switch (fileToOpen)
            {
                case 1:
                    file = Application.StartupPath + "\\Pzls\\pzl_1.txt";
                    break;
                case 2:
                    file = Application.StartupPath + "\\Pzls\\pzl_2.txt";
                    break;
                case 3:
                    file = Application.StartupPath + "\\Pzls\\pzl_3.txt";
                    break;
                case 4:
                    file = Application.StartupPath + "\\Pzls\\pzl_4.txt";
                    break;
                case 5:
                    file = Application.StartupPath + "\\Pzls\\pzl_5.txt";
                    break;
                default:
                    file = Application.StartupPath + "\\Pzls\\pzl_5.txt";
                    break;
            }
            //Hacemos return del archivo a abrir para posteriormente generar las casillas y las pistas
            return file;
        }
        //Funcion principal
        public main_form()
        {
            Constructor_De_Palabras();
            InitializeComponent();
        }

        private void Constructor_De_Palabras()
        {
            string linea = "";
            using (StreamReader s = new StreamReader(pzl_file))
            {
                linea = s.ReadLine(); //Tenemos que ignorar la primera linea, ya que ella nos dice el formato del archivo en si, no contiene info util
                while ((linea = s.ReadLine()) != null)
                {
                    string[] l = linea.Split('/');//Esto lo que hace es transformar la linea de mi archivo a abrir en un arreglo, creando cada item a partir de cada / que encuentre
                    // las comillas sencillas son importantes en esta parte del código, de otra forma genera un error
                    idc.Add(new id_cells(Int32.Parse(l[0]),Int32.Parse(l[1]),l[2],l[3],l[4],l[5]));
                    //Se selecciona hasta el index 5, porque corresponde con el numero de propiedades que tienen nuestros archivos
                    vista_pistas.pistas_board.Rows.Add(new string[] {
                        l[3],//no de palabra
                        l[2],//direccion
                        l[5]//pista
                    });
                    //En este caso, agregamos el index 3,2,5, en ese orden porque, de nuevo, corresponden esos index con las propiedades que queremos mostrar en la tabla
                }
            }

        }
        //Esto cierra la app, es simple
        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Se cierra la aplicación al hacer clic en cerra, vaya, que impredecible, ¿no?
        }
        private void Main_form_Load(object sender, EventArgs e)
        {
            //Esta funcion es la que se encarga de la música hehe
            playSimpleSound();
            //Esta funcion le da formato a nuestra cuadricula
            Initialize_Board();
            //Con esto, lo que hacemos es generar la ventana de pistas seguido de nuestro main form
            vista_pistas.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            vista_pistas.Show();
            //Ajustamos el tamaño
            vista_pistas.pistas_board.AutoResizeColumns();
            //Cell_Formatter(5,5,"b");
        }
        //Esta función no tiene ciencia, solo se le asigna un color random de fondo cada que se carga el form
        public Color colorRandom()
        {
            Color colorDeFondo;
            colorDeFondo_num = NumeroRandom(1,5);
            switch (colorDeFondo_num)
            {
                case 1:
                    colorDeFondo = Color.Red;
                    break;
                case 2:
                    colorDeFondo = Color.DeepPink;
                    break;
                case 3:
                    colorDeFondo = Color.Cornsilk;
                    break;
                case 4:
                    colorDeFondo = Color.DarkViolet;
                    break;
                case 5:
                    colorDeFondo = Color.Azure;
                    break;
                default:
                    colorDeFondo = Color.AliceBlue;
                    break;
            }
            return colorDeFondo;
        }
        // Esta funcion le da formato al board
        private void Initialize_Board()
        {
            MessageBox.Show("Cuanto todas las letras esten de color verde, haz ganado.");
            //Este es el color de fondo del board, solo es visible en la parte de abajo y un poco a la derecha
            board.BackgroundColor = Color.White;
            //Este es el color de fondo de las casillas que son read only
            board.DefaultCellStyle.BackColor = colorRandom();
            for (int i = 0; i < 21; i++)
            {
                //Se agrega la misma cantidad de rows que de columns
                board.Rows.Add();
            }
            //Despues de agregar los rows, lo que hacemos es darles el tamaño que necesitamos con un foreach
            foreach (DataGridViewColumn column in board.Columns)
            {
                column.Width = board.Width / board.Columns.Count;
            }
            //Hacemos lo mismo con las rows, les tenemos que asignar una altura equitativa
            foreach (DataGridViewRow row in board.Rows)
            {
                row.Height = board.Height / board.Rows.Count;
            }
            //Ya se habían hecho todas los campos read only, en su generación, pero lo haremos manualmente de nuevo, solo para estar seguros
            for (int r = 0; r < board.Rows.Count; r++)
            {
                for (int c = 0; c < board.Columns.Count; c++)
                {
                    board[c, r].ReadOnly = true;
                }
            }

            foreach (id_cells i in idc)
            {
                int init_column = i.x;
                int init_row = i.y;
                char[] character = i.palabra.ToCharArray();
                int j = 0;
                for (j = 0; j < character.Length; j++)
                {
                    //Con estas condicionales vamos a verificar la direccion de la palabra, en funcion de su indicacion en el archivo, lo que nos dirá en que dirección tenemos que poner los campos a rellenar
                    if (i.dir.ToUpper() == "ACROSS")
                    {
                        //Esto se pasa a la funcion que hace que los campos sean seleccionables, es decir, que el usuario podrá contestar
                        Cell_Formatter(init_row, init_column + j, character[j].ToString());
                    }else if (i.dir.ToUpper() == "DOWN")
                    {
                        //Si nos damos cuenta, el cambio en los parametros viene en que parte sumamos la j, esto le dice a la funcion en que direccion tiene que ir, por la forma en la que esta programada la funcion
                        Cell_Formatter(init_row + j, init_column, character[j].ToString());
                    }
                }
            }

        }
        private void Cell_Formatter(int row, int col, string character)
        {
            DataGridViewCell c = board[col, row];
            //asignacion de las propiedades básicas de las cells
            c.Style.BackColor = Color.White;
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Cyan;
            c.Tag = character; //La propiedad tag nos va a servir para el matcheo de letras introducidas por el usuario
            //fin de la asignacio de propiedades basicas de las cells
        }
        // Esta función se encarga de mantener el segundo form pegado al primero en caso de que haya movimiento
        private void Main_form_LocationChanged(object sender, EventArgs e)
        {
            vista_pistas.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
        }
        // Esto se encarga de filtrar el valor de la celda, para decir si es correcto o incorrecto
        private void Board_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Por cuestiones de facilidad de filtrado, lo que vamos a hacer es, hacer todas las letras mayusculas
            //Para un filtrado eficiente, nos vamos a encargar de limitar las entradas de datos a una letra por celda
            try
            {
                board[e.ColumnIndex, e.RowIndex].Value = board[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                if (board[e.ColumnIndex, e.RowIndex].Value.ToString().Length > 1)
                {
                    //Lo que hace esto es siempre dejar solo la primera letra de la cadena que introduzca el usuario
                    board[e.ColumnIndex, e.RowIndex].Value = board[e.ColumnIndex, e.RowIndex].Value.ToString().Substring(0, 1);
                }
                //Ahora comprobamos que la letra introducida por el usuario, sea igual a la letra asignada en el Tag, que corresponde a la letra correcta de esa ubicacion
                if(board[e.ColumnIndex, e.RowIndex].Value.ToString().Equals(board[e.ColumnIndex, e.RowIndex].Tag.ToString().ToUpper()))
                {
                    //Si si es la letra esperada, ponemos la letra de color verde bajito
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.LightGreen;
                }
                else
                {
                    // Si no es la letra esperada ponemos la letra de color rojo, sencillo, ¿no?
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
        //Esta funcion sirve para cargar un nuevo archivo con un nuevo crucigrama
        private void AbrirCrucigramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creamos una ventana de seleccion de archivos
            OpenFileDialog ofd = new OpenFileDialog();
            //Para prevenir que algo explote, limitamos los tipos de archivos
            ofd.Filter = "Text files|*.txt";
            //Si no nos arroja ningun error, limpiamos todos los datos y lanzamos las funciones de nuevo, para darle formato al juego
            if (ofd.ShowDialog().Equals(DialogResult.OK))
            {
                //Tomamos el path del nuevo archivo abierto, que suplantara al archivo abierto por defecto
                pzl_file = ofd.FileName;
                //Limpiado de info
                board.Rows.Clear();
                vista_pistas.pistas_board.Rows.Clear();
                idc.Clear();
                //generacion del nuevo crucigrama
                Constructor_De_Palabras();
                Initialize_Board();
            }
        }
        //Esta función, lo que hace es poner el numero de pista que le corresponde
        private void Board_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Creamos una variable para almacenar el numero
            string numero = "";
            //Iteramos entre las celdas hasta encontrar un match 
            if (idc.Any(c => (numero = c.no) != "" && c.x == e.ColumnIndex && c.y == e.RowIndex))
            {
                //Creamos y le damos formato a un cuadrado donde "pintamos" el numero
                Rectangle rect = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                e.Graphics.FillRectangle(Brushes.White, rect);
                Font font = new Font(e.CellStyle.Font.FontFamily, 7);
                e.Graphics.DrawString(numero, font, Brushes.Black, rect);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
        }
        //Simplemente generamos un numero random entre X y N numero, en función de los parametros que se pasen
        static public int NumeroRandom (int max, int min)
        {
            Random numero = new Random();
            return numero.Next(max, min);
        }
        //En caso de que querramos un nuevo crucigrama, lo generamos aqui
        private void GenerarCrucigramaRandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int fileToOpen = NumeroRandom(1,3);
            switch (fileToOpen)
            {
                case 1:
                    pzl_file = Application.StartupPath + "\\Pzls\\pzl_1.txt";
                    break;
                case 2:
                    pzl_file = Application.StartupPath + "\\Pzls\\pzl_2.txt";
                    break;
                case 3:
                    pzl_file = Application.StartupPath + "\\Pzls\\pzl_3.txt";
                    break;
                case 4:
                    pzl_file = Application.StartupPath + "\\Pzls\\pzl_4.txt";
                    break;
                case 5:
                    pzl_file = Application.StartupPath + "\\Pzls\\pzl_5.txt";
                    break;
                default:
                    pzl_file = Application.StartupPath + "\\Pzls\\pzl_5.txt";
                    break;
            }

            board.Rows.Clear();
            vista_pistas.pistas_board.Rows.Clear();
            idc.Clear();
            //generacion del nuevo crucigrama
            Constructor_De_Palabras();
            Initialize_Board();
        }
        // Esto enseña mi info lol
        private void CreadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ángel Alberto Rivas Álvarez 3°E 18100242");
        }
        // Esta es una pequeña intro
        private void InstruccionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cuando se introduce una letra en cada espacio aparecerá de color VERDE si la letra es correcta, y de color ROJO si la letra es incorrecta.");
        }
        // Esto es algo de relleno tbh
        private void LicenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pues esto ni si quiera se vende, así que si llega a tus manos, puedes hacer lo que quieras con el haha.");
        }
        // Esto reproduce musica de fondo
        public void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Application.StartupPath + "\\musiquita\\LA_ROLA.wav");
            simpleSound.Play();
            simpleSound.PlayLooping();
        }
    }

    public class id_cells{
        public int x;
        public int y;
        public string dir;
        public string no;
        public string palabra;
        public string pista;
        // Esto es una especie de constructor, se parece mucho al __init__ de python haha, de hecho, creo que cumplen la misma funcion lol
        public id_cells(int x, int y, string d, string n, string p, string ps)
        {
            this.x = x;
            this.y = y;
            this.dir = d;
            this.no = n;
            this.palabra = p;
            this.pista = ps;
        }
    }//final de id_cells
}
