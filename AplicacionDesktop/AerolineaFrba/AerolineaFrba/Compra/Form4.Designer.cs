﻿namespace AerolineaFrba.Compra
{
    partial class Form4
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.dgPasajes = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.dgEncomiendas = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.labelPasajesRestantes = new System.Windows.Forms.Label();
            this.labelKGRestantes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPasajes)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomiendas)).BeginInit();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 521);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(89, 37);
            this.button6.TabIndex = 53;
            this.button6.Text = "Atrás";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(568, 521);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 37);
            this.button3.TabIndex = 56;
            this.button3.Text = "Efectuar compra";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelPasajesRestantes);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.dgPasajes);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(645, 241);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de pasajes seleccionados";
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(528, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 48);
            this.button4.TabIndex = 60;
            this.button4.Text = "Cancelar Pasaje";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // dgPasajes
            // 
            this.dgPasajes.AllowUserToAddRows = false;
            this.dgPasajes.AllowUserToDeleteRows = false;
            this.dgPasajes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgPasajes.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgPasajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPasajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPasajes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgPasajes.Location = new System.Drawing.Point(14, 99);
            this.dgPasajes.MultiSelect = false;
            this.dgPasajes.Name = "dgPasajes";
            this.dgPasajes.ReadOnly = true;
            this.dgPasajes.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgPasajes.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgPasajes.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgPasajes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPasajes.Size = new System.Drawing.Size(616, 117);
            this.dgPasajes.TabIndex = 59;
            this.dgPasajes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPasajes_CellContentClick);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(14, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 48);
            this.button1.TabIndex = 58;
            this.button1.Text = "Cargar Pasaje";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelKGRestantes);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.dgEncomiendas);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(645, 241);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de encomiendas seleccionadas";
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(528, 24);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 48);
            this.button5.TabIndex = 61;
            this.button5.Text = "Cancelar Encomienda";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // dgEncomiendas
            // 
            this.dgEncomiendas.AllowUserToAddRows = false;
            this.dgEncomiendas.AllowUserToDeleteRows = false;
            this.dgEncomiendas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgEncomiendas.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgEncomiendas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgEncomiendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEncomiendas.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgEncomiendas.Location = new System.Drawing.Point(14, 99);
            this.dgEncomiendas.MultiSelect = false;
            this.dgEncomiendas.Name = "dgEncomiendas";
            this.dgEncomiendas.ReadOnly = true;
            this.dgEncomiendas.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgEncomiendas.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgEncomiendas.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgEncomiendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEncomiendas.Size = new System.Drawing.Size(616, 117);
            this.dgEncomiendas.TabIndex = 60;
            this.dgEncomiendas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEncomiendas_CellContentClick);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(14, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 48);
            this.button2.TabIndex = 59;
            this.button2.Text = "Cargar Encomienda";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // labelPasajesRestantes
            // 
            this.labelPasajesRestantes.AutoSize = true;
            this.labelPasajesRestantes.ForeColor = System.Drawing.Color.White;
            this.labelPasajesRestantes.Location = new System.Drawing.Point(122, 42);
            this.labelPasajesRestantes.Name = "labelPasajesRestantes";
            this.labelPasajesRestantes.Size = new System.Drawing.Size(96, 13);
            this.labelPasajesRestantes.TabIndex = 87;
            this.labelPasajesRestantes.Text = "Pasajes restantes: ";
            // 
            // labelKGRestantes
            // 
            this.labelKGRestantes.AutoSize = true;
            this.labelKGRestantes.Location = new System.Drawing.Point(122, 42);
            this.labelKGRestantes.Name = "labelKGRestantes";
            this.labelKGRestantes.Size = new System.Drawing.Size(61, 13);
            this.labelKGRestantes.TabIndex = 111;
            this.labelKGRestantes.Text = "Restantes: ";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(680, 570);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button6);
            this.MaximizeBox = false;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga de datos";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPasajes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomiendas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dgPasajes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dgEncomiendas;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelPasajesRestantes;
        private System.Windows.Forms.Label labelKGRestantes;
    }
}