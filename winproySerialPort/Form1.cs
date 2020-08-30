using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Threading;

namespace winproySerialPort
{
    public partial class Form1 : Form
    {
        classTransRecep instancia;
        delegate void MostrarOtroProceso(string mensaje);
        MostrarOtroProceso delegadoMostrar;
        MostrarOtroProceso delegadoMostrar1;
        MostrarOtroProceso delegadoMostrarArchivo;
        Thread enviaArchivo;
        String com = "COM1";
        int velocidad = 57600;
        String directorioDeGuardado;
        bool puertoIsOpen = true;
        ProgressBar barraDePrueba1;
        ProgressBar barraDePrueba2;
        private int location = 0;
        
        public Form1()
        {
            
            InitializeComponent();
            //inicializar el directorio de guardado 
            directorioDeGuardado = Directory.GetCurrentDirectory();
            //MessageBox.Show(directorioDeGuardado);
            directorioDeGuardado = directorioDeGuardado + "\\recibidos";

            System.IO.Directory.CreateDirectory(directorioDeGuardado);
            //MessageBox.Show(directorioDeGuardado);
            theToolTip.SetToolTip(this.btnRecibir, "Cantidad de Chars por Enviar");
            theToolTip.SetToolTip(this.btnResetCOM, "Cambiar de puerto COM");
            theToolTip.SetToolTip(this.btnEnviar, "Enviar");
            theToolTip.SetToolTip(this.btnVelocidad, "Cambiar velocidad");
            theToolTip.SetToolTip(this.button1, "Cerrar Puerto");
            folderChooser.SelectedPath = directorioDeGuardado;

            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            flowLayoutPanel1.AutoScrollPosition = new Point(0, 0);
            flowLayoutPanel1.VerticalScroll.Maximum = 375;
            
        }

       


        private void btnEnviar_Click(object sender, EventArgs e)
        {
            instancia.Enviar(rchMensajes.Text.Trim(), "Mensaje");
            nuevoMensajeSaliente(rchMensajes.Text.Trim());
            //guardarMensajeSaliente();
            rchMensajes.Text = "Escriba su mensaje aqui...";




        }
        public void guardarMensajeSaliente(string mensaje)
        {
            //rchConversacion.SelectionAlignment = HorizontalAlignment.Right;
            //rchConversacion.SelectionFont = new Font("Tahoma", 10);
            //rchConversacion.SelectionBackColor = Color.FromArgb(220, 248, 198);
            //rchConversacion.SelectedText = mensaje + "\n";
            //DateTime date2 = DateTime.Now;
            //rchConversacion.SelectionBackColor = Color.FromArgb(220, 248, 198);
            //rchConversacion.SelectionColor = Color.Gray;
            //rchConversacion.SelectedText = date2.ToString("hh:mm") + " \n";

        }
        public void guardarMensajeEntrante(string mensaje)

        {
            nuevoMensajeEntrante(mensaje);
            //rchConversacion.SelectionFont = new Font("Tahoma", 10);
            //rchConversacion.SelectionBackColor = Color.White;
            //rchConversacion.SelectionColor = Color.Black;
            //rchConversacion.SelectedText = mensaje + new string(' ', 80) + "\n";
            //
            //
            //rchConversacion.SelectionBackColor = Color.White;
            //rchConversacion.SelectionColor = Color.Gray;
            //rchConversacion.SelectionAlignment = HorizontalAlignment.Left;
            //DateTime date1 = DateTime.Now;
            //rchConversacion.SelectedText = date1.ToString("hh:mm") + new string(' ', 89) + " \n";
        }

        //cuando se carga el form
        private void Form1_Load(object sender, EventArgs e)
        {
            instancia = new classTransRecep(directorioDeGuardado);



            bool funciono = false;
            // pedismos q ingrese un puerto com hasta q el  puerto elegido sea uno valido
            while (!funciono)
            {
                try
                {
                    // pregunta si se presiono ok en el frame 
                    if (comSelector("Selecciona el COM", "Puerto COM que usaras:", ref com) == DialogResult.OK)
                    {
                        //myDocument.Name = value;
                        instancia.Inicializa(com);
                        //trigger la notificacion
                        funciono = true;
                        lbCom.Text = com;
                        lbVelocidad.Text = velocidad.ToString();
                        notificameInfo("Abrimos el puerto", "Puerto " + com + " abierto Exitosamente");
                    }
                }
                catch (Exception ex)
                {
                    notificameWarning("Error en puerto", ex.Message );
                }
            }


           
            instancia.LlegoMensaje += new classTransRecep.HandlerIO(instancia_LlegoMensaje);
            instancia.LlegoArchivo += new classTransRecep.HandlerIO(instancia_LlegoArchivo);
            delegadoMostrar = new MostrarOtroProceso(MostrarMensaje);
            delegadoMostrar1 = new MostrarOtroProceso(MostrarArchivo);

            //hanlders para placeholder en mensaje de envio
            rchMensajes.GotFocus += new EventHandler(RemoveText);
            rchMensajes.LostFocus += new EventHandler(AddText);
            //rchConversacion.TextChanged += new EventHandler(richTextBox_TextChanged);


            
            //this.rchConversacion.Controls.Add(b);
        }
        // bind this method to its TextChanged event handler:
        
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            //rchConversacion.SelectionStart = rchConversacion.Text.Length;
            // scroll it automatically
            //rchConversacion.ScrollToCaret();
        }
        //llamar notificacion de informacion
        public void notificameInfo(String titulo, String cuerpo)
        {
            notificacionError.Visible = false;
            notificacion.Visible= true;
            notificacion.BalloonTipText = titulo;
            notificacion.BalloonTipTitle = cuerpo;
            notificacion.ShowBalloonTip(0);
        }
        //llamar notificacion de alerta
        public void notificameWarning(String titulo, String cuerpo)
        {
            notificacionError.Visible = true;
            notificacion.Visible = false;
            notificacionError.BalloonTipText = titulo;
            notificacionError.BalloonTipTitle = cuerpo;
            notificacionError.ShowBalloonTip(0);
            
        }
        //implementacion del handler para placeholder en mensaje de envio
        public void RemoveText(object sender, EventArgs e)
        {
            if (rchMensajes.Text == "Escriba su mensaje aqui...")
            {
                rchMensajes.Text = "";
            }
        }
        //implementacion del handler para placeholder en mensaje de envio
        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rchMensajes.Text))
                rchMensajes.Text = "Escriba su mensaje aqui...";
        }


        private void instancia_LlegoMensaje(object oo , string mensaje)
        {
            //MessageBox.Show("se disparo el evento llego mensaje " + mensaje);
            Invoke(delegadoMostrar,mensaje);
        }
        private void instancia_LlegoArchivo(object oo,string path)
        {
            //MessageBox.Show("se disparo el evento llego mensaje " + mensaje);
            Invoke(delegadoMostrar1, path);
        }

        private void MostrarMensaje(string mensaje)
        {
            //rchConversacion.Text = '\n' + mensaje;
            guardarMensajeEntrante(mensaje);
        }
        private void MostrarArchivo(string path)
        {
            //rchConversacion.Text = '\n' + mensaje;
            string[] parr= path.Split('>');
            
            int avanze = Convert.ToInt32( parr[1]);
            int identificar= Convert.ToInt32(parr[2]);
            string tipoArchivo = parr[3];
            path = parr[0];
            switch (tipoArchivo)
            {
                
                case "11":
                    Console.WriteLine(identificar.ToString() +"  "+avanze.ToString() );
                    actualizaArchivoSaliente(identificar, avanze);
                    break;
                case "20":
                    
                    Console.WriteLine("llego contenido de archivo para crear" + identificar );
                    Console.WriteLine("directorio de archivo encontrado" );
                    
                    nuevoArchivoEntrante(path, identificar);
                    break;
                case "21":
                    Console.WriteLine("llego contenido de archivo para actualizar " + identificar + " " + avanze);
                    actualizaArchivoEntrante(identificar, avanze);
                    break;
                default:
                    break;
            }
            //guardarArchivoEntrante(Path.GetFileName(path));

        }
        private void guardarArchivoEntrante(string path) {
            
        }
        private void btnRecibir_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("faltan por salir a enviar  : " + instancia.bytesPorSalir().ToString());
            notificameInfo( instancia.bytesPorSalir().ToString(), "Bytes que faltan enviar");
        }

        private void rchMensajes_TextChanged(object sender, EventArgs e)
        {

        }

        //metodo static para seleccionar el com
        public static DialogResult comSelector(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            ComboBox myCombo = new ComboBox();
            Button buttonOk = new Button();

            for (int i = 0; i < 10; i++)
            {
                myCombo.Items.Add("COM"+(i+1).ToString());
            }
            form.Text = title;
            label.Text = promptText;
            
            myCombo.Text = value;
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            form.ControlBox = false;
            label.SetBounds(9, 20, 372, 13);
            myCombo.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(210, 72, 75, 23);

            label.AutoSize = true;
            myCombo.Anchor = myCombo.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, myCombo, buttonOk });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();
            value = myCombo.SelectedItem.ToString();
            return dialogResult;
        }
        //metodo static para seleccionar la velocidad
        public static DialogResult boudSelector(string title, string promptText, ref int value)
        {
            Form form = new Form();
            Label label = new Label();
            ComboBox myCombo = new ComboBox();
            Button buttonOk = new Button();

            myCombo.Items.Add("110");
            myCombo.Items.Add("300");
            myCombo.Items.Add("600");
            myCombo.Items.Add("1200");
            myCombo.Items.Add("2400");
            myCombo.Items.Add("4800");
            myCombo.Items.Add("9600");
            myCombo.Items.Add("14400");
            myCombo.Items.Add("19200");
            myCombo.Items.Add("38400");
            myCombo.Items.Add("57600");
            myCombo.Items.Add("115200");
            myCombo.Items.Add("128000"); 
            myCombo.Items.Add("256000");




            form.Text = title;
            label.Text = promptText;

            myCombo.Text = value.ToString();
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            form.ControlBox = false;
            label.SetBounds(9, 20, 372, 13);
            myCombo.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(210, 72, 75, 23);

            label.AutoSize = true;
            myCombo.Anchor = myCombo.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, myCombo, buttonOk });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();
            value = Convert.ToInt32(myCombo.SelectedItem.ToString());
            return dialogResult;
        }

        private void btnResetCOM_Click(object sender, EventArgs e)
        {

            bool funciono = false;
            while (!funciono)
            {
                try
                {
                    if (comSelector("Selecciona nuevo el COM", "Nuevo puerto COM que usaras:", ref com) == DialogResult.OK)
                    {
                        //cerrar el puerto com abierto
                        
                        instancia.termina();
                        //abriri otro puerto com
                        instancia.Inicializa(com);
                        puertoIsOpen = true;
                        lbCom.Text = com;
                        funciono = true;
                        notificameInfo("Abrimos el puerto", "Puerto " + com + " abierto Exitosamente");
                        imagenParaApagar();


                    }
                }
                catch (Exception ex)
                {
                    notificameWarning("Error en puerto", ex.Message);
                }
            }

           
        }

        public void imagenParaApagar()
        {
            button1.BackgroundImage = Properties.Resources.baseline_power_off_white_18dp;
            theToolTip.SetToolTip(this.button1, "Cerrar Puerto");
        }
        public void imagenParaPrender()
        {
            button1.BackgroundImage = Properties.Resources.baseline_power_white_18dp;
            theToolTip.SetToolTip(this.button1, "Abrir Puerto");
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool funciono = false;
            while (!funciono)
            {
                try
                {
                    if (boudSelector("Selecciona la nueva velocidad", "Nueva velocidad en baudios :", ref velocidad) == DialogResult.OK)
                    {
                        //cerrar el puerto com abierto
                        notificacionError.Visible = false;
                        
                        
                        lbVelocidad.Text = velocidad.ToString();
                        instancia.cambiaVelocidad(velocidad);
                        funciono = true;
                        notificameInfo("Cambiamos la velocidad", "Nueva velocidad : " + velocidad.ToString());
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    notificameWarning("Error en el cambio de velocidad", ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (puertoIsOpen)
            {
                try
                {
                    instancia.termina();
                    puertoIsOpen = false;
                    imagenParaPrender();
                    notificameInfo("Cerramos el puerto", "Puerto : " + com+" cerrado");
                }
                catch (Exception ex)
                {
                    notificameWarning("Error en cerrado de puerto", ex.Message);
                }
            }
            else
            {
                try
                {
                    instancia.Inicializa(com);
                    puertoIsOpen = true;
                    imagenParaApagar();
                    notificameInfo("Abrimos el puerto", "Puerto : " + com+ " abierto");
                }
                catch (Exception ex)
                {
                    notificameWarning("Error en abrir puerto", ex.Message);
                }
            }
           
            

            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        bool isCollapsed = false;
        int maxHeightCollapse = 128;
        int minHeightCollapse = 51;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panelFileCollapse.Height -= 10;
                if(panelFileCollapse.Height <= minHeightCollapse)
                {
                    timer1.Stop();
                    isCollapsed = false;

                }
            }
            else
            {
                panelFileCollapse.Height += 10;
                if (panelFileCollapse.Height >= maxHeightCollapse)
                {
                    timer1.Stop();
                    isCollapsed = true;

                }
            }
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("cklic");
            timer1.Start();
        }
        private int contadorArchivos = 0;
        private void btnImagen_Click(object sender, EventArgs e)
        {
            seleccionaArchivo.Filter = "Imagenes(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            if (seleccionaArchivo.ShowDialog() == DialogResult.OK)
            {
                if (seleccionaArchivo.FileNames.Length <= 16)
                {
                    foreach (String nombreArchivo in seleccionaArchivo.FileNames)
                    {
                        instancia.Enviar(nombreArchivo, "Archivo");
                        nuevoArchivoSaliente(nombreArchivo, contadorArchivos);
                        contadorArchivos += 1;
                        if (contadorArchivos == 16)
                        {
                            contadorArchivos = 0;
                        }
                    }
                   
                }
                else
                {
                    notificameWarning("Muchos archivos Seleccionados", "solo puedes enviar 16 archivos a la vez");
                }


            }
        }
        

        private void btnArchivo_Click(object sender, EventArgs e)
        {
            seleccionaArchivo.Filter = "Todos los archivos(*.*)|*.*";
            if (seleccionaArchivo.ShowDialog() == DialogResult.OK)
            {
                if(seleccionaArchivo.FileNames.Length <= 16)
                {
                    foreach (String nombreArchivo in seleccionaArchivo.FileNames)
                    {
                        Console.WriteLine("comenzando nuevo arhcivo");
                        instancia.Enviar(nombreArchivo, "Archivo");
                        nuevoArchivoSaliente(nombreArchivo, contadorArchivos);
                        contadorArchivos += 1;
                        if (contadorArchivos == 16)
                        {
                            contadorArchivos = 0;
                        }
                    }
                }
                else
                {
                    notificameWarning("Muchos archivos Seleccionados", "solo puedes enviar 16 archivos a la vez");
                }
                
            }
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            if (folderChooser.ShowDialog() == DialogResult.OK)
            {
                directorioDeGuardado = folderChooser.SelectedPath;
                instancia.directorioDeGuardado = directorioDeGuardado;
            }
        }
        public void nuevoMensajeEntrante(string text)
        {
            FlowLayoutPanel p1 = new FlowLayoutPanel();
            p1.BackColor = Color.White;
            Label l1 = new Label();
            Label l2 = new Label();
            p1.Width = 500;
            l1.Text = text;
            l1.MinimumSize = new Size(480, 0);
            l1.Margin = new Padding(10, 10, 3, 0);
            DateTime date2 = DateTime.Now;
            l1.AutoSize = true;
            l2.MinimumSize = new Size(480, 0);
            l2.TextAlign = ContentAlignment.MiddleRight;
            l2.ForeColor = Color.FromArgb(193, 193, 193);
            l2.Text = date2.ToString("hh:mm");
            l2.Margin = new Padding(10, 10, 3, 0);

            
            p1.Margin = new Padding(10, 5, 3, 5);
            p1.Controls.Add(l1);
            p1.Controls.Add(l2);
            Console.WriteLine("label height :"+ l1.Height.ToString());
            Console.WriteLine("panel height :" + p1.Height.ToString());
            p1.Height = 43 + l1.Height;
            Console.WriteLine("panel height 2:" + p1.Height.ToString());

            flowLayoutPanel1.Controls.Add(p1);
        }
        public void nuevoMensajeSaliente(string text)
        {
            FlowLayoutPanel p1 = new FlowLayoutPanel();
            p1.BackColor = Color.FromArgb(220, 248, 198);
            p1.Width = 500;
            Label l1 = new Label();
            Label l2 = new Label();
            
            l1.MinimumSize = new Size(480, 0);
            l1.Margin = new Padding(10, 10, 3, 0);
            DateTime date2 = DateTime.Now;
            l1.Text = text; 
            l1.AutoSize = true;
            l1.TextAlign = ContentAlignment.MiddleRight;
            l2.MinimumSize = new Size(480, 0);
            l2.TextAlign = ContentAlignment.MiddleRight;
            l2.ForeColor = Color.FromArgb(193, 193, 193);
            l2.Text = date2.ToString("hh:mm");
            l2.Margin = new Padding(10, 10, 3, 0);

            p1.Margin = new Padding(270, 5, 10, 5);
            
            p1.Controls.Add(l1);
            p1.Controls.Add(l2);
            p1.Height = 43 + l1.Height;
            flowLayoutPanel1.Controls.Add(p1);
        }
        public void nuevoArchivoEntrante(string text, int contador)
        {
            FlowLayoutPanel p1 = new FlowLayoutPanel();
            p1.BackColor = Color.White;
            LinkLabel l1 = new LinkLabel();
            ProgressBar pb1 = new ProgressBar();
            Label l2 = new Label();
            p1.Width = 500;

            pb1.Maximum = 16777215;

            
            pb1.Height = 10;
            pb1.MinimumSize = new Size(480, 0);
            pb1.Margin=new Padding(10, 10, 3, 0);

            Console.WriteLine("name en funcion q crea "+pb1.Name);

            l1.Text = text;
            l1.MinimumSize = new Size(480, 0);
            l1.Margin = new Padding(10, 10, 3, 0);
            DateTime date2 = DateTime.Now;
            l1.AutoSize = true;
            l2.MinimumSize = new Size(480, 0);
            l2.TextAlign = ContentAlignment.MiddleRight;
            l2.ForeColor = Color.FromArgb(193, 193, 193);
            l2.Text = date2.ToString("hh:mm");
            l2.Margin = new Padding(10, 10, 3, 0);

            p1.Name = "entrante"+contador.ToString();
            p1.Margin = new Padding(10, 5, 3, 5);
            p1.Controls.Add(l1);
            p1.Controls.Add(pb1);
            p1.Controls.Add(l2);
            Console.WriteLine("label height :" + l1.Height.ToString());
            Console.WriteLine("panel height :" + p1.Height.ToString());
            p1.Height = 63 + l1.Height;
            Console.WriteLine("panel height 2:" + p1.Height.ToString());
            //evetos para q se abra el archivo compartido para verse cuando se hace click en el panel
            p1.Click += new EventHandler((object obj, EventArgs args) => {

                System.Diagnostics.Process.Start(l1.Text);
            });
            l1.Click += new EventHandler((object obj, EventArgs args) => {

                System.Diagnostics.Process.Start(l1.Text);
            });
            l2.Click += new EventHandler((object obj, EventArgs args) => {

                System.Diagnostics.Process.Start(l1.Text);
            });

            flowLayoutPanel1.Controls.Add(p1);
        }
        private void  nuevoArchivoSaliente(string text, int contador )
        {
            FlowLayoutPanel p1 = new FlowLayoutPanel();
            ProgressBar pb1 = new ProgressBar();
            p1.BackColor = Color.FromArgb(220, 248, 198);
            p1.Width = 500;
            LinkLabel l1 = new LinkLabel();
            Label l2 = new Label();


            p1.Name = "saliente"+contador.ToString();
            pb1.Maximum = 16777215;

            
            pb1.Height = 10;
            pb1.MinimumSize = new Size(480, 0);
            pb1.Margin = new Padding(10, 10, 3, 0);

            l1.MinimumSize = new Size(480, 0);
            l1.Margin = new Padding(10, 10, 3, 0);
            DateTime date2 = DateTime.Now;
            l1.Text = text;
            l1.AutoSize = true;
            l1.TextAlign = ContentAlignment.MiddleRight;
            l2.MinimumSize = new Size(480, 0);
            l2.TextAlign = ContentAlignment.MiddleRight;
            l2.ForeColor = Color.FromArgb(193, 193, 193);
            l2.Text = date2.ToString("hh:mm");
            l2.Margin = new Padding(10, 10, 3, 0);

            p1.Margin = new Padding(270, 5, 10, 5);

            p1.Controls.Add(l1);
            p1.Controls.Add(pb1);
            p1.Controls.Add(l2);
            p1.Height = 43 + l1.Height;

            //evetos para q se abra el archivo compartido para verse cuando se hace click en el panel
            p1.Click += new EventHandler((object obj,EventArgs args)=> {

                System.Diagnostics.Process.Start(l1.Text);
            });
            l1.Click += new EventHandler((object obj, EventArgs args) => {

                System.Diagnostics.Process.Start(l1.Text);
            });
            l2.Click += new EventHandler((object obj, EventArgs args) => {

                System.Diagnostics.Process.Start(l1.Text);
            });
            flowLayoutPanel1.Controls.Add(p1);
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void actualizaArchivoSaliente(int identificador , int avanze)
        {
            

            for (int i = 0; i < flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray().Length; i++)
            {
                //Console.WriteLine(i.ToString() + ":" + flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name);
                if(flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name == "saliente" + identificador.ToString())
                {
                    ref ProgressBar  wi= ref flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Controls.OfType<ProgressBar>().ToArray()[0];
                    if (wi.Visible)
                    {
                        wi.Value += avanze;

                        if (wi.Value >= wi.Maximum- 100000)
                        {
                            wi.Visible = false;
                            wi.Name = "noSeUsaraMasxx";
                        }
                    }

                }
            }
        }
        private void actualizaArchivoEntrante(int identificador, int avanze)
        {


            for (int i = 0; i < flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray().Length; i++)
            {
                //Console.WriteLine(i.ToString() + ":" + flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name);
                if (flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name == "entrante" + identificador.ToString())
                {
                    ref ProgressBar wi = ref flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Controls.OfType<ProgressBar>().ToArray()[0];
                    if (wi.Visible)
                    {
                        wi.Value += avanze;
                        Console.WriteLine("actualizando entrante" + identificador.ToString());
                        if (wi.Value >= wi.Maximum - 100000)
                        {
                            wi.Visible = false;
                            wi.Name = "noSeUsaraMasxx";
                        }
                    }
                    

                }
            }
        }
        private void btnCambiaTextr_Click(object sender, EventArgs e)
        {
            //ad salinte
            //labelTest.Text = inputText.Text;
            //flowPanelTest.Height = 43 + labelTest.Height;

            //nuevoMensajeSaliente(inputText.Text);

            
            //flowLayoutPanel1.VerticalScroll.Value += 53;
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //nuevoMensajeEntrante(inputText.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray().Length; i++)
            {
                Console.WriteLine(i.ToString() + ":" + flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name);
                Console.WriteLine("para condicion");
                if (flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name == "dude")
                {
                    Console.WriteLine("entro");
                    ref ProgressBar barraDePruebaDude = ref flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Controls.OfType<ProgressBar>().ToArray()[0];
                    Console.WriteLine("the value is ", barraDePruebaDude.Value.ToString());

                    barraDePruebaDude.Value += 10; Console.WriteLine("the value is " + barraDePruebaDude.Value.ToString());
                    

                }
            }
            ;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray().Length; i++)
            {
                Console.WriteLine(i.ToString() + ":" + flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name);
                Console.WriteLine("para condicion");
                if (flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Name == "dude")
                {
                    Console.WriteLine("entro");
                    ref ProgressBar barraDePruebaDude = ref flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>().ToArray()[i].Controls.OfType<ProgressBar>().ToArray()[0];
                    Console.WriteLine("the value is ", barraDePruebaDude.Value.ToString());

                    
                    barraDePruebaDude.Value -= 10; Console.WriteLine("the value is " + barraDePruebaDude.Value.ToString());
                    

                }
            }
            ;
        }
    }
}
