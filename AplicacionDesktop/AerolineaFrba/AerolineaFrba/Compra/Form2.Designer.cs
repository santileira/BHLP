﻿namespace AerolineaFrba.Compra
{
    partial class Form2
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
            this.button6 = new System.Windows.Forms.Button();
            this.dgButacas = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgKilos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.txtApe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDire = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgCliente = new System.Windows.Forms.DataGridView();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dp = new System.Windows.Forms.DateTimePicker();
            this.dgCliente2 = new System.Windows.Forms.DataGridView();
            this.dgImporte = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgButacas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgKilos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCliente2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgImporte)).BeginInit();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(27, 23);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(78, 21);
            this.button6.TabIndex = 47;
            this.button6.Text = "Atras";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dgButacas
            // 
            this.dgButacas.AllowUserToAddRows = false;
            this.dgButacas.AllowUserToDeleteRows = false;
            this.dgButacas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgButacas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgButacas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgButacas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgButacas.Location = new System.Drawing.Point(26, 326);
            this.dgButacas.MultiSelect = false;
            this.dgButacas.Name = "dgButacas";
            this.dgButacas.ReadOnly = true;
            this.dgButacas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgButacas.Size = new System.Drawing.Size(298, 98);
            this.dgButacas.TabIndex = 48;
            this.dgButacas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgButacas_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Butacas libres:";
            // 
            // dgKilos
            // 
            this.dgKilos.AllowUserToAddRows = false;
            this.dgKilos.AllowUserToDeleteRows = false;
            this.dgKilos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgKilos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgKilos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgKilos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKilos.Location = new System.Drawing.Point(6, 2);
            this.dgKilos.MultiSelect = false;
            this.dgKilos.Name = "dgKilos";
            this.dgKilos.ReadOnly = true;
            this.dgKilos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgKilos.Size = new System.Drawing.Size(12, 68);
            this.dgKilos.TabIndex = 51;
            this.dgKilos.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Numero de documento:";
            // 
            // txtDni
            // 
            this.txtDni.Location = new System.Drawing.Point(190, 58);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(134, 20);
            this.txtDni.TabIndex = 53;
            this.txtDni.TextChanged += new System.EventHandler(this.txtDni_TextChanged);
            // 
            // txtApe
            // 
            this.txtApe.Location = new System.Drawing.Point(190, 84);
            this.txtApe.Name = "txtApe";
            this.txtApe.Size = new System.Drawing.Size(134, 20);
            this.txtApe.TabIndex = 55;
            this.txtApe.TextChanged += new System.EventHandler(this.txtApe_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Apellido:";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(190, 154);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(134, 20);
            this.txtNom.TabIndex = 57;
            this.txtNom.TextChanged += new System.EventHandler(this.txtNom_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Nombre:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "Fecha de nacimiento:";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(190, 206);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(134, 20);
            this.txtTel.TabIndex = 61;
            this.txtTel.TextChanged += new System.EventHandler(this.txtTel_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Telefono:";
            // 
            // txtDire
            // 
            this.txtDire.Location = new System.Drawing.Point(190, 180);
            this.txtDire.Name = "txtDire";
            this.txtDire.Size = new System.Drawing.Size(134, 20);
            this.txtDire.TabIndex = 59;
            this.txtDire.TextChanged += new System.EventHandler(this.txtDire_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Direccion:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 38);
            this.button1.TabIndex = 64;
            this.button1.Text = "Buscar en la base de datos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgCliente
            // 
            this.dgCliente.AllowUserToAddRows = false;
            this.dgCliente.AllowUserToDeleteRows = false;
            this.dgCliente.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgCliente.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgCliente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCliente.Location = new System.Drawing.Point(5, 80);
            this.dgCliente.MultiSelect = false;
            this.dgCliente.Name = "dgCliente";
            this.dgCliente.ReadOnly = true;
            this.dgCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCliente.Size = new System.Drawing.Size(12, 68);
            this.dgCliente.TabIndex = 65;
            this.dgCliente.Visible = false;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(190, 258);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(134, 20);
            this.txtMail.TabIndex = 67;
            this.txtMail.TextChanged += new System.EventHandler(this.txtMail_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 261);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 13);
            this.label8.TabIndex = 66;
            this.label8.Text = "Direccion de correo electronico:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(212, 446);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 38);
            this.button2.TabIndex = 68;
            this.button2.Text = "Confirmar pasaje";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 446);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 38);
            this.button3.TabIndex = 69;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dp
            // 
            this.dp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dp.Location = new System.Drawing.Point(190, 232);
            this.dp.Name = "dp";
            this.dp.Size = new System.Drawing.Size(136, 20);
            this.dp.TabIndex = 70;
            this.dp.ValueChanged += new System.EventHandler(this.dp_ValueChanged);
            // 
            // dgCliente2
            // 
            this.dgCliente2.AllowUserToAddRows = false;
            this.dgCliente2.AllowUserToDeleteRows = false;
            this.dgCliente2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgCliente2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgCliente2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgCliente2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCliente2.Location = new System.Drawing.Point(5, 157);
            this.dgCliente2.MultiSelect = false;
            this.dgCliente2.Name = "dgCliente2";
            this.dgCliente2.ReadOnly = true;
            this.dgCliente2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCliente2.Size = new System.Drawing.Size(12, 68);
            this.dgCliente2.TabIndex = 95;
            this.dgCliente2.Visible = false;
            // 
            // dgImporte
            // 
            this.dgImporte.AllowUserToAddRows = false;
            this.dgImporte.AllowUserToDeleteRows = false;
            this.dgImporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgImporte.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgImporte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgImporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgImporte.Location = new System.Drawing.Point(6, 238);
            this.dgImporte.MultiSelect = false;
            this.dgImporte.Name = "dgImporte";
            this.dgImporte.ReadOnly = true;
            this.dgImporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgImporte.Size = new System.Drawing.Size(12, 68);
            this.dgImporte.TabIndex = 96;
            this.dgImporte.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 522);
            this.Controls.Add(this.dgImporte);
            this.Controls.Add(this.dgCliente2);
            this.Controls.Add(this.dp);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgCliente);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDire);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtApe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgKilos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgButacas);
            this.Controls.Add(this.button6);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccion de Butacas";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgButacas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgKilos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCliente2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgImporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgButacas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgKilos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.TextBox txtApe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDire;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgCliente;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dp;
        private System.Windows.Forms.DataGridView dgCliente2;
        private System.Windows.Forms.DataGridView dgImporte;
    }
}