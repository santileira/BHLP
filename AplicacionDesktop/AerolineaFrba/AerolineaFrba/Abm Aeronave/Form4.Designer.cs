﻿namespace AerolineaFrba.Abm_Aeronave
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mkMatricula = new System.Windows.Forms.MaskedTextBox();
            this.txtButacasV = new System.Windows.Forms.TextBox();
            this.txtButacasVActual = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtServicioActual = new System.Windows.Forms.TextBox();
            this.cboFabricante = new System.Windows.Forms.ComboBox();
            this.txtFabricanteActual = new System.Windows.Forms.TextBox();
            this.cboServicio = new System.Windows.Forms.ComboBox();
            this.txtButacasP = new System.Windows.Forms.TextBox();
            this.txtButacasPActual = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKilosActual = new System.Windows.Forms.TextBox();
            this.txtKilos = new System.Windows.Forms.TextBox();
            this.txtMatriculaActual = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModeloActual = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 474);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 11;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(513, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 10;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mkMatricula);
            this.groupBox1.Controls.Add(this.txtButacasV);
            this.groupBox1.Controls.Add(this.txtButacasVActual);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtServicioActual);
            this.groupBox1.Controls.Add(this.cboFabricante);
            this.groupBox1.Controls.Add(this.txtFabricanteActual);
            this.groupBox1.Controls.Add(this.cboServicio);
            this.groupBox1.Controls.Add(this.txtButacasP);
            this.groupBox1.Controls.Add(this.txtButacasPActual);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtKilosActual);
            this.groupBox1.Controls.Add(this.txtKilos);
            this.groupBox1.Controls.Add(this.txtMatriculaActual);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtModeloActual);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtModelo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(11, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 420);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Campos de la Aeronave";
            // 
            // mkMatricula
            // 
            this.mkMatricula.Location = new System.Drawing.Point(348, 47);
            this.mkMatricula.Mask = "AAA-AAA";
            this.mkMatricula.Name = "mkMatricula";
            this.mkMatricula.Size = new System.Drawing.Size(70, 20);
            this.mkMatricula.TabIndex = 45;
            // 
            // txtButacasV
            // 
            this.txtButacasV.Enabled = false;
            this.txtButacasV.Location = new System.Drawing.Point(348, 319);
            this.txtButacasV.MaxLength = 9;
            this.txtButacasV.Name = "txtButacasV";
            this.txtButacasV.Size = new System.Drawing.Size(157, 20);
            this.txtButacasV.TabIndex = 43;
            // 
            // txtButacasVActual
            // 
            this.txtButacasVActual.Enabled = false;
            this.txtButacasVActual.Location = new System.Drawing.Point(165, 319);
            this.txtButacasVActual.Name = "txtButacasVActual";
            this.txtButacasVActual.ReadOnly = true;
            this.txtButacasVActual.Size = new System.Drawing.Size(157, 20);
            this.txtButacasVActual.TabIndex = 44;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Cantidad de butacas ventanilla";
            // 
            // txtServicioActual
            // 
            this.txtServicioActual.Enabled = false;
            this.txtServicioActual.Location = new System.Drawing.Point(165, 209);
            this.txtServicioActual.Name = "txtServicioActual";
            this.txtServicioActual.ReadOnly = true;
            this.txtServicioActual.Size = new System.Drawing.Size(157, 20);
            this.txtServicioActual.TabIndex = 41;
            // 
            // cboFabricante
            // 
            this.cboFabricante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFabricante.Enabled = false;
            this.cboFabricante.FormattingEnabled = true;
            this.cboFabricante.Location = new System.Drawing.Point(348, 150);
            this.cboFabricante.Name = "cboFabricante";
            this.cboFabricante.Size = new System.Drawing.Size(157, 21);
            this.cboFabricante.TabIndex = 31;
            // 
            // txtFabricanteActual
            // 
            this.txtFabricanteActual.Enabled = false;
            this.txtFabricanteActual.Location = new System.Drawing.Point(165, 150);
            this.txtFabricanteActual.Name = "txtFabricanteActual";
            this.txtFabricanteActual.ReadOnly = true;
            this.txtFabricanteActual.Size = new System.Drawing.Size(157, 20);
            this.txtFabricanteActual.TabIndex = 40;
            // 
            // cboServicio
            // 
            this.cboServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServicio.Enabled = false;
            this.cboServicio.FormattingEnabled = true;
            this.cboServicio.Location = new System.Drawing.Point(348, 209);
            this.cboServicio.Name = "cboServicio";
            this.cboServicio.Size = new System.Drawing.Size(157, 21);
            this.cboServicio.TabIndex = 30;
            // 
            // txtButacasP
            // 
            this.txtButacasP.Enabled = false;
            this.txtButacasP.Location = new System.Drawing.Point(348, 263);
            this.txtButacasP.MaxLength = 9;
            this.txtButacasP.Name = "txtButacasP";
            this.txtButacasP.Size = new System.Drawing.Size(157, 20);
            this.txtButacasP.TabIndex = 11;
            // 
            // txtButacasPActual
            // 
            this.txtButacasPActual.Enabled = false;
            this.txtButacasPActual.Location = new System.Drawing.Point(165, 263);
            this.txtButacasPActual.Name = "txtButacasPActual";
            this.txtButacasPActual.ReadOnly = true;
            this.txtButacasPActual.Size = new System.Drawing.Size(157, 20);
            this.txtButacasPActual.TabIndex = 39;
            this.txtButacasPActual.TextChanged += new System.EventHandler(this.txtButacasActual_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Cantidad de butacas pasillo";
            // 
            // txtKilosActual
            // 
            this.txtKilosActual.Enabled = false;
            this.txtKilosActual.Location = new System.Drawing.Point(165, 374);
            this.txtKilosActual.Name = "txtKilosActual";
            this.txtKilosActual.ReadOnly = true;
            this.txtKilosActual.Size = new System.Drawing.Size(157, 20);
            this.txtKilosActual.TabIndex = 38;
            // 
            // txtKilos
            // 
            this.txtKilos.Enabled = false;
            this.txtKilos.Location = new System.Drawing.Point(348, 374);
            this.txtKilos.MaxLength = 9;
            this.txtKilos.Name = "txtKilos";
            this.txtKilos.Size = new System.Drawing.Size(157, 20);
            this.txtKilos.TabIndex = 9;
            // 
            // txtMatriculaActual
            // 
            this.txtMatriculaActual.Enabled = false;
            this.txtMatriculaActual.Location = new System.Drawing.Point(165, 47);
            this.txtMatriculaActual.Name = "txtMatriculaActual";
            this.txtMatriculaActual.ReadOnly = true;
            this.txtMatriculaActual.Size = new System.Drawing.Size(157, 20);
            this.txtMatriculaActual.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cantidad de KG que lleva";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo de servicio";
            // 
            // txtModeloActual
            // 
            this.txtModeloActual.Enabled = false;
            this.txtModeloActual.Location = new System.Drawing.Point(165, 96);
            this.txtModeloActual.Name = "txtModeloActual";
            this.txtModeloActual.ReadOnly = true;
            this.txtModeloActual.Size = new System.Drawing.Size(157, 20);
            this.txtModeloActual.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fabricante";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Matricula";
            // 
            // txtModelo
            // 
            this.txtModelo.Enabled = false;
            this.txtModelo.Location = new System.Drawing.Point(348, 96);
            this.txtModelo.MaxLength = 30;
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(157, 20);
            this.txtModelo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modelo";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(547, 11);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(78, 22);
            this.button5.TabIndex = 43;
            this.button5.Text = "Seleccionar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Modificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(648, 528);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Modificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificación de aeronave";
            this.Load += new System.EventHandler(this.Modificacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboFabricante;
        private System.Windows.Forms.ComboBox cboServicio;
        private System.Windows.Forms.TextBox txtButacasP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKilos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtServicioActual;
        private System.Windows.Forms.TextBox txtFabricanteActual;
        private System.Windows.Forms.TextBox txtButacasPActual;
        private System.Windows.Forms.TextBox txtKilosActual;
        private System.Windows.Forms.TextBox txtMatriculaActual;
        private System.Windows.Forms.TextBox txtModeloActual;
        private System.Windows.Forms.TextBox txtButacasV;
        private System.Windows.Forms.TextBox txtButacasVActual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mkMatricula;
    }
}