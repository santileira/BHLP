namespace AerolineaFrba.Abm_Ruta
{
    partial class Alta
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
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPrecioEncomienda = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecioPasaje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.botonSelDestino = new System.Windows.Forms.Button();
            this.botonSelOrigen = new System.Windows.Forms.Button();
            this.txtCiudadDestino = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCiudadOrigen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.botonSelServicios = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(13, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 368);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Campos de la Ruta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "(*) Obligatorios";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtPrecioEncomienda);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtPrecioPasaje);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(294, 29);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 140);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Precios";
            // 
            // txtPrecioEncomienda
            // 
            this.txtPrecioEncomienda.Location = new System.Drawing.Point(95, 68);
            this.txtPrecioEncomienda.MaxLength = 6;
            this.txtPrecioEncomienda.Name = "txtPrecioEncomienda";
            this.txtPrecioEncomienda.Size = new System.Drawing.Size(117, 20);
            this.txtPrecioEncomienda.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Encomienda *";
            // 
            // txtPrecioPasaje
            // 
            this.txtPrecioPasaje.Location = new System.Drawing.Point(95, 31);
            this.txtPrecioPasaje.MaxLength = 6;
            this.txtPrecioPasaje.Name = "txtPrecioPasaje";
            this.txtPrecioPasaje.Size = new System.Drawing.Size(117, 20);
            this.txtPrecioPasaje.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Pasaje *";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.botonSelDestino);
            this.groupBox3.Controls.Add(this.botonSelOrigen);
            this.groupBox3.Controls.Add(this.txtCiudadDestino);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtCiudadOrigen);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(15, 195);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 113);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ciudades";
            // 
            // botonSelDestino
            // 
            this.botonSelDestino.ForeColor = System.Drawing.Color.Black;
            this.botonSelDestino.Location = new System.Drawing.Point(237, 65);
            this.botonSelDestino.Name = "botonSelDestino";
            this.botonSelDestino.Size = new System.Drawing.Size(88, 25);
            this.botonSelDestino.TabIndex = 8;
            this.botonSelDestino.Text = "Seleccionar";
            this.botonSelDestino.UseVisualStyleBackColor = true;
            this.botonSelDestino.Click += new System.EventHandler(this.botonSelDestino_Click);
            // 
            // botonSelOrigen
            // 
            this.botonSelOrigen.ForeColor = System.Drawing.Color.Black;
            this.botonSelOrigen.Location = new System.Drawing.Point(237, 28);
            this.botonSelOrigen.Name = "botonSelOrigen";
            this.botonSelOrigen.Size = new System.Drawing.Size(88, 25);
            this.botonSelOrigen.TabIndex = 7;
            this.botonSelOrigen.Text = "Seleccionar";
            this.botonSelOrigen.UseVisualStyleBackColor = true;
            this.botonSelOrigen.Click += new System.EventHandler(this.botonSelOrigen_Click);
            // 
            // txtCiudadDestino
            // 
            this.txtCiudadDestino.Location = new System.Drawing.Point(95, 68);
            this.txtCiudadDestino.Name = "txtCiudadDestino";
            this.txtCiudadDestino.ReadOnly = true;
            this.txtCiudadDestino.Size = new System.Drawing.Size(117, 20);
            this.txtCiudadDestino.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Destino *";
            // 
            // txtCiudadOrigen
            // 
            this.txtCiudadOrigen.Location = new System.Drawing.Point(95, 31);
            this.txtCiudadOrigen.Name = "txtCiudadOrigen";
            this.txtCiudadOrigen.ReadOnly = true;
            this.txtCiudadOrigen.Size = new System.Drawing.Size(117, 20);
            this.txtCiudadOrigen.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Origen *";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.botonSelServicios);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(15, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 140);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos principales";
            // 
            // botonSelServicios
            // 
            this.botonSelServicios.ForeColor = System.Drawing.Color.Black;
            this.botonSelServicios.Location = new System.Drawing.Point(95, 65);
            this.botonSelServicios.Name = "botonSelServicios";
            this.botonSelServicios.Size = new System.Drawing.Size(88, 25);
            this.botonSelServicios.TabIndex = 8;
            this.botonSelServicios.Text = "Seleccionar";
            this.botonSelServicios.UseVisualStyleBackColor = true;
            this.botonSelServicios.Click += new System.EventHandler(this.botonSelServicios_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tipo de servicio *";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(95, 31);
            this.txtCodigo.MaxLength = 9;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(117, 20);
            this.txtCodigo.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Código *";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(486, 423);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 423);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 2;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "label6";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Alta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(632, 470);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Name = "Alta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta de ruta";
            this.Load += new System.EventHandler(this.Alta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPrecioEncomienda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrecioPasaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCiudadDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCiudadOrigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button botonSelDestino;
        private System.Windows.Forms.Button botonSelOrigen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button botonSelServicios;
        private System.Windows.Forms.Label label6;
    }
}