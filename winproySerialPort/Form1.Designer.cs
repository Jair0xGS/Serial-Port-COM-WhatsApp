namespace winproySerialPort
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rchMensajes = new System.Windows.Forms.RichTextBox();
            this.theToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.notificacion = new System.Windows.Forms.NotifyIcon(this.components);
            this.notificacionError = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnVelocidad = new System.Windows.Forms.Button();
            this.btnRecibir = new System.Windows.Forms.Button();
            this.btnResetCOM = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCom = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbVelocidad = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.seleccionaArchivo = new System.Windows.Forms.OpenFileDialog();
            this.folderChooser = new System.Windows.Forms.FolderBrowserDialog();
            this.panelFileCollapse = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdjuntar = new System.Windows.Forms.Button();
            this.btnImagen = new System.Windows.Forms.Button();
            this.btnArchivo = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelFileCollapse.SuspendLayout();
            this.SuspendLayout();
            // 
            // rchMensajes
            // 
            this.rchMensajes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rchMensajes.BackColor = System.Drawing.Color.White;
            this.rchMensajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchMensajes.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rchMensajes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.rchMensajes.Location = new System.Drawing.Point(3, 4);
            this.rchMensajes.Name = "rchMensajes";
            this.rchMensajes.Size = new System.Drawing.Size(703, 40);
            this.rchMensajes.TabIndex = 1;
            this.rchMensajes.Text = "Escriba su mensaje aqui...";
            this.rchMensajes.TextChanged += new System.EventHandler(this.rchMensajes_TextChanged);
            // 
            // notificacion
            // 
            this.notificacion.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notificacion.BalloonTipText = "Puerto Abierto Exitosamente";
            this.notificacion.BalloonTipTitle = "Informacion del puerto";
            this.notificacion.Icon = ((System.Drawing.Icon)(resources.GetObject("notificacion.Icon")));
            this.notificacion.Text = "notifyIcon1";
            this.notificacion.Visible = true;
            this.notificacion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // notificacionError
            // 
            this.notificacionError.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.notificacionError.Icon = ((System.Drawing.Icon)(resources.GetObject("notificacionError.Icon")));
            this.notificacionError.Visible = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.panel1.Controls.Add(this.btnDirectorio);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnVelocidad);
            this.panel1.Controls.Add(this.btnRecibir);
            this.panel1.Controls.Add(this.btnResetCOM);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(66, 509);
            this.panel1.TabIndex = 6;
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.BackgroundImage = global::winproySerialPort.Properties.Resources.baseline_folder_open_white_18dp;
            this.btnDirectorio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDirectorio.FlatAppearance.BorderSize = 0;
            this.btnDirectorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectorio.Location = new System.Drawing.Point(12, 151);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(40, 40);
            this.btnDirectorio.TabIndex = 7;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackgroundImage = global::winproySerialPort.Properties.Resources.baseline_power_off_white_18dp;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnVelocidad
            // 
            this.btnVelocidad.BackgroundImage = global::winproySerialPort.Properties.Resources._159621834817650335;
            this.btnVelocidad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVelocidad.FlatAppearance.BorderSize = 0;
            this.btnVelocidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVelocidad.ForeColor = System.Drawing.SystemColors.Control;
            this.btnVelocidad.Location = new System.Drawing.Point(12, 13);
            this.btnVelocidad.Name = "btnVelocidad";
            this.btnVelocidad.Size = new System.Drawing.Size(40, 40);
            this.btnVelocidad.TabIndex = 5;
            this.btnVelocidad.UseVisualStyleBackColor = true;
            this.btnVelocidad.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRecibir
            // 
            this.btnRecibir.BackgroundImage = global::winproySerialPort.Properties.Resources._159621834817650335__1_;
            this.btnRecibir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRecibir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecibir.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnRecibir.FlatAppearance.BorderSize = 0;
            this.btnRecibir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecibir.Location = new System.Drawing.Point(12, 105);
            this.btnRecibir.Name = "btnRecibir";
            this.btnRecibir.Size = new System.Drawing.Size(40, 40);
            this.btnRecibir.TabIndex = 3;
            this.btnRecibir.UseVisualStyleBackColor = true;
            this.btnRecibir.Click += new System.EventHandler(this.btnRecibir_Click);
            // 
            // btnResetCOM
            // 
            this.btnResetCOM.BackgroundImage = global::winproySerialPort.Properties.Resources._159621834817650335__2_;
            this.btnResetCOM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnResetCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetCOM.FlatAppearance.BorderSize = 0;
            this.btnResetCOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetCOM.Location = new System.Drawing.Point(12, 59);
            this.btnResetCOM.Name = "btnResetCOM";
            this.btnResetCOM.Size = new System.Drawing.Size(40, 40);
            this.btnResetCOM.TabIndex = 4;
            this.btnResetCOM.UseVisualStyleBackColor = true;
            this.btnResetCOM.Click += new System.EventHandler(this.btnResetCOM_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Controls.Add(this.rchMensajes);
            this.panel2.Controls.Add(this.btnEnviar);
            this.panel2.Location = new System.Drawing.Point(81, 458);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(762, 50);
            this.panel2.TabIndex = 7;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviar.BackgroundImage = global::winproySerialPort.Properties.Resources.image;
            this.btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnviar.FlatAppearance.BorderSize = 0;
            this.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEnviar.Location = new System.Drawing.Point(719, 4);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(40, 40);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(190, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Velocidad :";
            // 
            // lbCom
            // 
            this.lbCom.AutoSize = true;
            this.lbCom.Location = new System.Drawing.Point(136, 36);
            this.lbCom.Name = "lbCom";
            this.lbCom.Size = new System.Drawing.Size(35, 13);
            this.lbCom.TabIndex = 9;
            this.lbCom.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Puerto :";
            // 
            // lbVelocidad
            // 
            this.lbVelocidad.AutoSize = true;
            this.lbVelocidad.Location = new System.Drawing.Point(267, 36);
            this.lbVelocidad.Name = "lbVelocidad";
            this.lbVelocidad.Size = new System.Drawing.Size(35, 13);
            this.lbVelocidad.TabIndex = 11;
            this.lbVelocidad.Text = "label4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "Convesacion";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // seleccionaArchivo
            // 
            this.seleccionaArchivo.Multiselect = true;
            this.seleccionaArchivo.Title = "Selecciona Un Archivo";
            // 
            // panelFileCollapse
            // 
            this.panelFileCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFileCollapse.Controls.Add(this.btnAdjuntar);
            this.panelFileCollapse.Controls.Add(this.btnImagen);
            this.panelFileCollapse.Controls.Add(this.btnArchivo);
            this.panelFileCollapse.Location = new System.Drawing.Point(774, 1);
            this.panelFileCollapse.Name = "panelFileCollapse";
            this.panelFileCollapse.Padding = new System.Windows.Forms.Padding(5);
            this.panelFileCollapse.Size = new System.Drawing.Size(51, 51);
            this.panelFileCollapse.TabIndex = 14;
            // 
            // btnAdjuntar
            // 
            this.btnAdjuntar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdjuntar.BackgroundImage = global::winproySerialPort.Properties.Resources.clip;
            this.btnAdjuntar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAdjuntar.FlatAppearance.BorderSize = 0;
            this.btnAdjuntar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdjuntar.Location = new System.Drawing.Point(8, 8);
            this.btnAdjuntar.Margin = new System.Windows.Forms.Padding(3, 3, 3, 4);
            this.btnAdjuntar.Name = "btnAdjuntar";
            this.btnAdjuntar.Size = new System.Drawing.Size(36, 36);
            this.btnAdjuntar.TabIndex = 12;
            this.btnAdjuntar.UseVisualStyleBackColor = true;
            this.btnAdjuntar.Click += new System.EventHandler(this.btnAdjuntar_Click);
            // 
            // btnImagen
            // 
            this.btnImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(68)))), ((int)(((byte)(207)))));
            this.btnImagen.BackgroundImage = global::winproySerialPort.Properties.Resources.baseline_photo_white_18dp;
            this.btnImagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagen.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnImagen.FlatAppearance.BorderSize = 0;
            this.btnImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImagen.Location = new System.Drawing.Point(8, 51);
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(36, 36);
            this.btnImagen.TabIndex = 7;
            this.btnImagen.UseVisualStyleBackColor = true;
            this.btnImagen.Click += new System.EventHandler(this.btnImagen_Click);
            // 
            // btnArchivo
            // 
            this.btnArchivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(102)))), ((int)(((byte)(205)))));
            this.btnArchivo.BackgroundImage = global::winproySerialPort.Properties.Resources.baseline_insert_drive_file_white_18dp;
            this.btnArchivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnArchivo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnArchivo.FlatAppearance.BorderSize = 0;
            this.btnArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArchivo.Location = new System.Drawing.Point(8, 93);
            this.btnArchivo.Name = "btnArchivo";
            this.btnArchivo.Size = new System.Drawing.Size(36, 36);
            this.btnArchivo.TabIndex = 13;
            this.btnArchivo.UseVisualStyleBackColor = false;
            this.btnArchivo.Click += new System.EventHandler(this.btnArchivo_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(221)))), ((int)(((byte)(213)))));
            this.flowLayoutPanel1.BackgroundImage = global::winproySerialPort.Properties.Resources.imageonline_co_brightnessadjusted__7_;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(81, 58);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(594, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(762, 394);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(219)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(849, 511);
            this.Controls.Add(this.panelFileCollapse);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbVelocidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbCom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(697, 0);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guevara Segura Jair";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelFileCollapse.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rchMensajes;
        private System.Windows.Forms.Button btnRecibir;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnResetCOM;
        private System.Windows.Forms.ToolTip theToolTip;
        private System.Windows.Forms.NotifyIcon notificacion;
        private System.Windows.Forms.NotifyIcon notificacionError;
        private System.Windows.Forms.Button btnVelocidad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbVelocidad;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAdjuntar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnArchivo;
        private System.Windows.Forms.Button btnImagen;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog seleccionaArchivo;
        private System.Windows.Forms.Button btnDirectorio;
        private System.Windows.Forms.FolderBrowserDialog folderChooser;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel panelFileCollapse;
    }
}

