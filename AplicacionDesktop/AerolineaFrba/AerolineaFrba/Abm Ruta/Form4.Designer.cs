﻿namespace AerolineaFrba.Abm_Ruta
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
            this.botonLimpiar = new System.Windows.Forms.Button();
            this.botonGuardar = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPrecioEncomiendaNueva = new System.Windows.Forms.TextBox();
            this.txtPrecioPasajeNuevo = new System.Windows.Forms.TextBox();
            this.txtPrecioEncomienda = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecioPasaje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCiudadDestino = new System.Windows.Forms.TextBox();
            this.txtCiudadOrigen = new System.Windows.Forms.TextBox();
            this.botonSelDestino = new System.Windows.Forms.Button();
            this.botonSelOrigen = new System.Windows.Forms.Button();
            this.txtCiudadDestinoNueva = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCiudadOrigenNueva = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.botonSelServicios = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // botonLimpiar
            // 
            this.botonLimpiar.Location = new System.Drawing.Point(37, 357);
            this.botonLimpiar.Name = "botonLimpiar";
            this.botonLimpiar.Size = new System.Drawing.Size(112, 35);
            this.botonLimpiar.TabIndex = 11;
            this.botonLimpiar.Text = "Limpiar";
            this.botonLimpiar.UseVisualStyleBackColor = true;
            this.botonLimpiar.Click += new System.EventHandler(this.botonLimpiar_Click);
            // 
            // botonGuardar
            // 
            this.botonGuardar.Location = new System.Drawing.Point(564, 357);
            this.botonGuardar.Name = "botonGuardar";
            this.botonGuardar.Size = new System.Drawing.Size(112, 35);
            this.botonGuardar.TabIndex = 10;
            this.botonGuardar.Text = "Guardar";
            this.botonGuardar.UseVisualStyleBackColor = true;
            this.botonGuardar.Click += new System.EventHandler(this.botonGuardar_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(598, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(78, 22);
            this.button5.TabIndex = 43;
            this.button5.Text = "Seleccionar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtPrecioEncomiendaNueva);
            this.groupBox4.Controls.Add(this.txtPrecioPasajeNuevo);
            this.groupBox4.Controls.Add(this.txtPrecioEncomienda);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtPrecioPasaje);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(320, 57);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(356, 148);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Precios";
            // 
            // txtPrecioEncomiendaNueva
            // 
            this.txtPrecioEncomiendaNueva.Location = new System.Drawing.Point(232, 82);
            this.txtPrecioEncomiendaNueva.Name = "txtPrecioEncomiendaNueva";
            this.txtPrecioEncomiendaNueva.Size = new System.Drawing.Size(117, 20);
            this.txtPrecioEncomiendaNueva.TabIndex = 9;
            // 
            // txtPrecioPasajeNuevo
            // 
            this.txtPrecioPasajeNuevo.Location = new System.Drawing.Point(232, 35);
            this.txtPrecioPasajeNuevo.Name = "txtPrecioPasajeNuevo";
            this.txtPrecioPasajeNuevo.Size = new System.Drawing.Size(117, 20);
            this.txtPrecioPasajeNuevo.TabIndex = 8;
            // 
            // txtPrecioEncomienda
            // 
            this.txtPrecioEncomienda.Location = new System.Drawing.Point(94, 82);
            this.txtPrecioEncomienda.Name = "txtPrecioEncomienda";
            this.txtPrecioEncomienda.ReadOnly = true;
            this.txtPrecioEncomienda.Size = new System.Drawing.Size(117, 20);
            this.txtPrecioEncomienda.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Encomienda";
            // 
            // txtPrecioPasaje
            // 
            this.txtPrecioPasaje.Location = new System.Drawing.Point(94, 35);
            this.txtPrecioPasaje.Name = "txtPrecioPasaje";
            this.txtPrecioPasaje.ReadOnly = true;
            this.txtPrecioPasaje.Size = new System.Drawing.Size(117, 20);
            this.txtPrecioPasaje.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Pasaje";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCiudadDestino);
            this.groupBox3.Controls.Add(this.txtCiudadOrigen);
            this.groupBox3.Controls.Add(this.botonSelDestino);
            this.groupBox3.Controls.Add(this.botonSelOrigen);
            this.groupBox3.Controls.Add(this.txtCiudadDestinoNueva);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtCiudadOrigenNueva);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(37, 211);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(477, 107);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ciudades";
            // 
            // txtCiudadDestino
            // 
            this.txtCiudadDestino.Location = new System.Drawing.Point(95, 64);
            this.txtCiudadDestino.Name = "txtCiudadDestino";
            this.txtCiudadDestino.ReadOnly = true;
            this.txtCiudadDestino.Size = new System.Drawing.Size(117, 20);
            this.txtCiudadDestino.TabIndex = 10;
            // 
            // txtCiudadOrigen
            // 
            this.txtCiudadOrigen.Location = new System.Drawing.Point(95, 27);
            this.txtCiudadOrigen.Name = "txtCiudadOrigen";
            this.txtCiudadOrigen.ReadOnly = true;
            this.txtCiudadOrigen.Size = new System.Drawing.Size(117, 20);
            this.txtCiudadOrigen.TabIndex = 9;
            // 
            // botonSelDestino
            // 
            this.botonSelDestino.ForeColor = System.Drawing.Color.Black;
            this.botonSelDestino.Location = new System.Drawing.Point(383, 62);
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
            this.botonSelOrigen.Location = new System.Drawing.Point(383, 25);
            this.botonSelOrigen.Name = "botonSelOrigen";
            this.botonSelOrigen.Size = new System.Drawing.Size(88, 25);
            this.botonSelOrigen.TabIndex = 7;
            this.botonSelOrigen.Text = "Seleccionar";
            this.botonSelOrigen.UseVisualStyleBackColor = true;
            this.botonSelOrigen.Click += new System.EventHandler(this.botonSelOrigen_Click);
            // 
            // txtCiudadDestinoNueva
            // 
            this.txtCiudadDestinoNueva.Location = new System.Drawing.Point(237, 64);
            this.txtCiudadDestinoNueva.Name = "txtCiudadDestinoNueva";
            this.txtCiudadDestinoNueva.ReadOnly = true;
            this.txtCiudadDestinoNueva.Size = new System.Drawing.Size(117, 20);
            this.txtCiudadDestinoNueva.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Destino";
            // 
            // txtCiudadOrigenNueva
            // 
            this.txtCiudadOrigenNueva.Location = new System.Drawing.Point(237, 27);
            this.txtCiudadOrigenNueva.Name = "txtCiudadOrigenNueva";
            this.txtCiudadOrigenNueva.ReadOnly = true;
            this.txtCiudadOrigenNueva.Size = new System.Drawing.Size(117, 20);
            this.txtCiudadOrigenNueva.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Origen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.botonSelServicios);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(37, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 148);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos principales";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 13);
            this.label10.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Nuevos";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(77, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Actuales";
            // 
            // botonSelServicios
            // 
            this.botonSelServicios.ForeColor = System.Drawing.Color.Black;
            this.botonSelServicios.Location = new System.Drawing.Point(95, 63);
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
            this.label7.Location = new System.Drawing.Point(7, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tipo de servicio";
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
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Código";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Los campos en blanco no se tendrán en cuenta";
            // 
            // Modificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(716, 417);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.botonLimpiar);
            this.Controls.Add(this.botonGuardar);
            this.MaximizeBox = false;
            this.Name = "Modificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificación de ruta";
            this.Load += new System.EventHandler(this.Modificacion_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonLimpiar;
        private System.Windows.Forms.Button botonGuardar;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPrecioEncomienda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrecioPasaje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button botonSelDestino;
        private System.Windows.Forms.Button botonSelOrigen;
        private System.Windows.Forms.TextBox txtCiudadDestinoNueva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCiudadOrigenNueva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPrecioEncomiendaNueva;
        private System.Windows.Forms.TextBox txtPrecioPasajeNuevo;
        private System.Windows.Forms.TextBox txtCiudadDestino;
        private System.Windows.Forms.TextBox txtCiudadOrigen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button botonSelServicios;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
    }
}