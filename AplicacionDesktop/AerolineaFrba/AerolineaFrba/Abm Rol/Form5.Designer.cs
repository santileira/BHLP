﻿namespace AerolineaFrba.Abm_Rol
{
    partial class Listado
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
            this.chkEstadoIgnorar = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optEstadoBaja = new System.Windows.Forms.RadioButton();
            this.optEstadoAlta = new System.Windows.Forms.RadioButton();
            this.txtFiltro1 = new System.Windows.Forms.TextBox();
            this.txtFiltro2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblErrores = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEstadoIgnorar);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtFiltro1);
            this.groupBox1.Controls.Add(this.txtFiltro2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(21, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de Busqueda";
            // 
            // chkEstadoIgnorar
            // 
            this.chkEstadoIgnorar.AutoSize = true;
            this.chkEstadoIgnorar.Location = new System.Drawing.Point(193, 80);
            this.chkEstadoIgnorar.Name = "chkEstadoIgnorar";
            this.chkEstadoIgnorar.Size = new System.Drawing.Size(154, 17);
            this.chkEstadoIgnorar.TabIndex = 15;
            this.chkEstadoIgnorar.Text = "Ignorar el estado para filtrar";
            this.chkEstadoIgnorar.UseVisualStyleBackColor = true;
            this.chkEstadoIgnorar.CheckedChanged += new System.EventHandler(this.chkEstadoIgnorar_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optEstadoBaja);
            this.groupBox2.Controls.Add(this.optEstadoAlta);
            this.groupBox2.Location = new System.Drawing.Point(9, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 84);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por estado";
            // 
            // optEstadoBaja
            // 
            this.optEstadoBaja.AutoSize = true;
            this.optEstadoBaja.Location = new System.Drawing.Point(6, 52);
            this.optEstadoBaja.Name = "optEstadoBaja";
            this.optEstadoBaja.Size = new System.Drawing.Size(87, 17);
            this.optEstadoBaja.TabIndex = 14;
            this.optEstadoBaja.TabStop = true;
            this.optEstadoBaja.Text = "Estado BAJA";
            this.optEstadoBaja.UseVisualStyleBackColor = true;
            // 
            // optEstadoAlta
            // 
            this.optEstadoAlta.AutoSize = true;
            this.optEstadoAlta.Location = new System.Drawing.Point(6, 29);
            this.optEstadoAlta.Name = "optEstadoAlta";
            this.optEstadoAlta.Size = new System.Drawing.Size(88, 17);
            this.optEstadoAlta.TabIndex = 13;
            this.optEstadoAlta.TabStop = true;
            this.optEstadoAlta.Text = "Estado ALTA";
            this.optEstadoAlta.UseVisualStyleBackColor = true;
            // 
            // txtFiltro1
            // 
            this.txtFiltro1.Location = new System.Drawing.Point(159, 20);
            this.txtFiltro1.MaxLength = 30;
            this.txtFiltro1.Name = "txtFiltro1";
            this.txtFiltro1.Size = new System.Drawing.Size(90, 20);
            this.txtFiltro1.TabIndex = 16;
            // 
            // txtFiltro2
            // 
            this.txtFiltro2.Location = new System.Drawing.Point(470, 19);
            this.txtFiltro2.MaxLength = 30;
            this.txtFiltro2.Name = "txtFiltro2";
            this.txtFiltro2.Size = new System.Drawing.Size(88, 20);
            this.txtFiltro2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filtro por igualdad de palabra";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtro que contenga la palabra";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 222);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 21);
            this.button2.TabIndex = 1;
            this.button2.Text = "Limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(360, 222);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 21);
            this.button3.TabIndex = 2;
            this.button3.Text = "Buscar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblErrores
            // 
            this.lblErrores.AutoSize = true;
            this.lblErrores.Location = new System.Drawing.Point(653, 197);
            this.lblErrores.Name = "lblErrores";
            this.lblErrores.Size = new System.Drawing.Size(0, 13);
            this.lblErrores.TabIndex = 4;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(491, 222);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 21);
            this.button4.TabIndex = 9;
            this.button4.Text = "Seleccionar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(25, 260);
            this.dg.MultiSelect = false;
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg.Size = new System.Drawing.Size(574, 235);
            this.dg.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 25);
            this.button1.TabIndex = 11;
            this.button1.Text = "Atrás";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Listado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(626, 517);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblErrores);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Listado";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de rol a modificar";
            this.Load += new System.EventHandler(this.Listado_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFiltro2;
        private System.Windows.Forms.TextBox txtFiltro1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblErrores;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optEstadoBaja;
        private System.Windows.Forms.RadioButton optEstadoAlta;
        private System.Windows.Forms.CheckBox chkEstadoIgnorar;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button button1;
    }
}