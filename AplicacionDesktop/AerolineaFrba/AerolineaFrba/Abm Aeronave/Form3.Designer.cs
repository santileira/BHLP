﻿using System.Data;
using System.Data.SqlClient;
namespace AerolineaFrba.Abm_Aeronave
{
    partial class Baja
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
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cboCamposFiltro2 = new System.Windows.Forms.ComboBox();
            this.cboCamposFiltro1 = new System.Windows.Forms.ComboBox();
            this.txtFiltro1 = new System.Windows.Forms.TextBox();
            this.txtFiltro2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.btfueradeservicio = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btlogica = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblErrores = new System.Windows.Forms.Label();
            this.txtFiltros = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.cboCamposFiltro2);
            this.groupBox1.Controls.Add(this.cboCamposFiltro1);
            this.groupBox1.Controls.Add(this.txtFiltro1);
            this.groupBox1.Controls.Add(this.txtFiltro2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de Busqueda";
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(501, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 21);
            this.button4.TabIndex = 21;
            this.button4.Text = "Agregar filtro";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(501, 29);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 21);
            this.button5.TabIndex = 20;
            this.button5.Text = "Agregar filtro";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // cboCamposFiltro2
            // 
            this.cboCamposFiltro2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCamposFiltro2.FormattingEnabled = true;
            this.cboCamposFiltro2.Items.AddRange(new object[] {
            "Matrícula",
            "Modelo",
            "Fabricante",
            "Servicio"});
            this.cboCamposFiltro2.Location = new System.Drawing.Point(173, 63);
            this.cboCamposFiltro2.Name = "cboCamposFiltro2";
            this.cboCamposFiltro2.Size = new System.Drawing.Size(117, 21);
            this.cboCamposFiltro2.TabIndex = 18;
            this.cboCamposFiltro2.SelectedIndexChanged += new System.EventHandler(this.cboCamposFiltro2_SelectedIndexChanged);
            // 
            // cboCamposFiltro1
            // 
            this.cboCamposFiltro1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCamposFiltro1.FormattingEnabled = true;
            this.cboCamposFiltro1.Items.AddRange(new object[] {
            "Matrícula",
            "Modelo",
            "Fabricante",
            "Servicio"});
            this.cboCamposFiltro1.Location = new System.Drawing.Point(173, 28);
            this.cboCamposFiltro1.Name = "cboCamposFiltro1";
            this.cboCamposFiltro1.Size = new System.Drawing.Size(117, 21);
            this.cboCamposFiltro1.TabIndex = 17;
            this.cboCamposFiltro1.SelectedIndexChanged += new System.EventHandler(this.cboCamposFiltro1_SelectedIndexChanged);
            // 
            // txtFiltro1
            // 
            this.txtFiltro1.Location = new System.Drawing.Point(296, 29);
            this.txtFiltro1.MaxLength = 30;
            this.txtFiltro1.Name = "txtFiltro1";
            this.txtFiltro1.Size = new System.Drawing.Size(183, 20);
            this.txtFiltro1.TabIndex = 16;
            // 
            // txtFiltro2
            // 
            this.txtFiltro2.Location = new System.Drawing.Point(296, 65);
            this.txtFiltro2.MaxLength = 30;
            this.txtFiltro2.Name = "txtFiltro2";
            this.txtFiltro2.Size = new System.Drawing.Size(183, 20);
            this.txtFiltro2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filtro por igualdad de palabra";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtro que contenga la palabra";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 21);
            this.button2.TabIndex = 1;
            this.button2.Text = "Limpiar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(713, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 21);
            this.button3.TabIndex = 2;
            this.button3.Text = "Buscar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btfueradeservicio,
            this.btlogica});
            this.dg.EnableHeadersVisualStyles = false;
            this.dg.Location = new System.Drawing.Point(12, 187);
            this.dg.Name = "dg";
            this.dg.ReadOnly = true;
            this.dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dg.Size = new System.Drawing.Size(802, 252);
            this.dg.TabIndex = 3;
            this.dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            // 
            // btfueradeservicio
            // 
            this.btfueradeservicio.HeaderText = "";
            this.btfueradeservicio.Name = "btfueradeservicio";
            this.btfueradeservicio.ReadOnly = true;
            this.btfueradeservicio.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btfueradeservicio.Text = "Fuera de Servicio";
            this.btfueradeservicio.UseColumnTextForButtonValue = true;
            // 
            // btlogica
            // 
            this.btlogica.HeaderText = "";
            this.btlogica.Name = "btlogica";
            this.btlogica.ReadOnly = true;
            this.btlogica.Text = "Baja";
            this.btlogica.UseColumnTextForButtonValue = true;
            this.btlogica.Width = 50;
            // 
            // lblErrores
            // 
            this.lblErrores.AutoSize = true;
            this.lblErrores.Location = new System.Drawing.Point(643, 190);
            this.lblErrores.Name = "lblErrores";
            this.lblErrores.Size = new System.Drawing.Size(0, 13);
            this.lblErrores.TabIndex = 4;
            // 
            // txtFiltros
            // 
            this.txtFiltros.BackColor = System.Drawing.Color.SlateGray;
            this.txtFiltros.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtFiltros.Location = new System.Drawing.Point(848, 28);
            this.txtFiltros.Multiline = true;
            this.txtFiltros.Name = "txtFiltros";
            this.txtFiltros.Size = new System.Drawing.Size(385, 681);
            this.txtFiltros.TabIndex = 6;
            // 
            // Baja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(1245, 467);
            this.Controls.Add(this.txtFiltros);
            this.Controls.Add(this.lblErrores);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Baja";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baja de aeronave";
            this.Load += new System.EventHandler(this.Listado_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Label lblErrores;
        private System.Windows.Forms.ComboBox cboCamposFiltro2;
        private System.Windows.Forms.ComboBox cboCamposFiltro1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtFiltros;
        private System.Windows.Forms.DataGridViewButtonColumn btfueradeservicio;
        private System.Windows.Forms.DataGridViewButtonColumn btlogica;

    }
}