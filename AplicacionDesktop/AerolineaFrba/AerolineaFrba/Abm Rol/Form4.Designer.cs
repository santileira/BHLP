namespace AerolineaFrba.Abm_Rol
{
    partial class Modificacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.modif = new System.Windows.Forms.Label();
            this.modifHabilitacion = new System.Windows.Forms.Label();
            this.modifFuncionalidades = new System.Windows.Forms.Label();
            this.modifNombre = new System.Windows.Forms.Label();
            this.checkHabilitado = new System.Windows.Forms.CheckBox();
            this.txtRolSeleccionado = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lstFuncionalidadesActuales = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstFuncionalidadesTotales = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modif);
            this.groupBox1.Controls.Add(this.modifHabilitacion);
            this.groupBox1.Controls.Add(this.modifFuncionalidades);
            this.groupBox1.Controls.Add(this.modifNombre);
            this.groupBox1.Controls.Add(this.checkHabilitado);
            this.groupBox1.Controls.Add(this.txtRolSeleccionado);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lstFuncionalidadesActuales);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lstFuncionalidadesTotales);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(18, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 354);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Campos del ROL";
            // 
            // modif
            // 
            this.modif.AutoSize = true;
            this.modif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.modif.Location = new System.Drawing.Point(32, 275);
            this.modif.Name = "modif";
            this.modif.Size = new System.Drawing.Size(128, 13);
            this.modif.TabIndex = 47;
            this.modif.Text = "(*) Elementos modificados";
            this.modif.Visible = false;
            // 
            // modifHabilitacion
            // 
            this.modifHabilitacion.AutoSize = true;
            this.modifHabilitacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.modifHabilitacion.Location = new System.Drawing.Point(139, 64);
            this.modifHabilitacion.Name = "modifHabilitacion";
            this.modifHabilitacion.Size = new System.Drawing.Size(11, 13);
            this.modifHabilitacion.TabIndex = 46;
            this.modifHabilitacion.Text = "*";
            this.modifHabilitacion.Visible = false;
            // 
            // modifFuncionalidades
            // 
            this.modifFuncionalidades.AutoSize = true;
            this.modifFuncionalidades.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.modifFuncionalidades.Location = new System.Drawing.Point(532, 93);
            this.modifFuncionalidades.Name = "modifFuncionalidades";
            this.modifFuncionalidades.Size = new System.Drawing.Size(11, 13);
            this.modifFuncionalidades.TabIndex = 45;
            this.modifFuncionalidades.Text = "*";
            this.modifFuncionalidades.Visible = false;
            // 
            // modifNombre
            // 
            this.modifNombre.AutoSize = true;
            this.modifNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.modifNombre.Location = new System.Drawing.Point(237, 32);
            this.modifNombre.Name = "modifNombre";
            this.modifNombre.Size = new System.Drawing.Size(11, 13);
            this.modifNombre.TabIndex = 44;
            this.modifNombre.Text = "*";
            this.modifNombre.Visible = false;
            // 
            // checkHabilitado
            // 
            this.checkHabilitado.AutoSize = true;
            this.checkHabilitado.Enabled = false;
            this.checkHabilitado.Location = new System.Drawing.Point(29, 63);
            this.checkHabilitado.Name = "checkHabilitado";
            this.checkHabilitado.Size = new System.Drawing.Size(104, 17);
            this.checkHabilitado.TabIndex = 43;
            this.checkHabilitado.Text = "Volver a habilitar";
            this.checkHabilitado.UseVisualStyleBackColor = true;
            this.checkHabilitado.CheckedChanged += new System.EventHandler(this.checkHabilitado_CheckedChanged);
            // 
            // txtRolSeleccionado
            // 
            this.txtRolSeleccionado.Location = new System.Drawing.Point(275, 29);
            this.txtRolSeleccionado.MaxLength = 60;
            this.txtRolSeleccionado.Name = "txtRolSeleccionado";
            this.txtRolSeleccionado.ReadOnly = true;
            this.txtRolSeleccionado.Size = new System.Drawing.Size(110, 20);
            this.txtRolSeleccionado.TabIndex = 42;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(391, 23);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 31);
            this.button5.TabIndex = 41;
            this.button5.Text = "Seleccionar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(191, 122);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(116, 31);
            this.button6.TabIndex = 40;
            this.button6.Text = "Agregar";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Funcionalidades a darle de alta al rol";
            // 
            // lstFuncionalidadesActuales
            // 
            this.lstFuncionalidadesActuales.FormattingEnabled = true;
            this.lstFuncionalidadesActuales.Location = new System.Drawing.Point(350, 122);
            this.lstFuncionalidadesActuales.Name = "lstFuncionalidadesActuales";
            this.lstFuncionalidadesActuales.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesActuales.TabIndex = 38;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(228, 225);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 31);
            this.button3.TabIndex = 37;
            this.button3.Text = "Quitar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Funcionalidades totales";
            // 
            // lstFuncionalidadesTotales
            // 
            this.lstFuncionalidadesTotales.Enabled = false;
            this.lstFuncionalidadesTotales.FormattingEnabled = true;
            this.lstFuncionalidadesTotales.Location = new System.Drawing.Point(29, 122);
            this.lstFuncionalidadesTotales.Name = "lstFuncionalidadesTotales";
            this.lstFuncionalidadesTotales.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesTotales.TabIndex = 35;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(392, 307);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 31);
            this.button2.TabIndex = 7;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 307);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "Limpiar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(127, 29);
            this.txtNombre.MaxLength = 60;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(110, 20);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre del Rol";
            // 
            // button4
            // 
            this.button4.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button4.Location = new System.Drawing.Point(2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(46, 22);
            this.button4.TabIndex = 10;
            this.button4.Text = "Atrás";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Modificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(625, 392);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox1);
            this.Name = "Modificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Modificacion";
            this.Load += new System.EventHandler(this.Alta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstFuncionalidadesActuales;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFuncionalidadesTotales;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtRolSeleccionado;
        private System.Windows.Forms.CheckBox checkHabilitado;
        private System.Windows.Forms.Label modif;
        private System.Windows.Forms.Label modifHabilitacion;
        private System.Windows.Forms.Label modifFuncionalidades;
        private System.Windows.Forms.Label modifNombre;

    }
}