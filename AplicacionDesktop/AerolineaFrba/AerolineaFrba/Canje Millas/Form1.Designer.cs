namespace AerolineaFrba.Canje_Millas
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.puntosDisp = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgListadoProductos = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.txtProdSeleccionado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCantSeleccionada = new System.Windows.Forms.TextBox();
            this.listaPremiosSelec = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.listCantSelec = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListadoProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.puntosDisp);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgListadoProductos);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(6, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 198);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Productos";
            // 
            // puntosDisp
            // 
            this.puntosDisp.AutoSize = true;
            this.puntosDisp.Location = new System.Drawing.Point(323, 16);
            this.puntosDisp.Name = "puntosDisp";
            this.puntosDisp.Size = new System.Drawing.Size(0, 13);
            this.puntosDisp.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Puntos Restantes :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione:";
            // 
            // dgListadoProductos
            // 
            this.dgListadoProductos.AllowUserToAddRows = false;
            this.dgListadoProductos.AllowUserToDeleteRows = false;
            this.dgListadoProductos.AllowUserToResizeColumns = false;
            this.dgListadoProductos.AllowUserToResizeRows = false;
            this.dgListadoProductos.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgListadoProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgListadoProductos.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgListadoProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ScrollBar;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgListadoProductos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgListadoProductos.Location = new System.Drawing.Point(6, 60);
            this.dgListadoProductos.MultiSelect = false;
            this.dgListadoProductos.Name = "dgListadoProductos";
            this.dgListadoProductos.ReadOnly = true;
            this.dgListadoProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgListadoProductos.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgListadoProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgListadoProductos.Size = new System.Drawing.Size(388, 127);
            this.dgListadoProductos.TabIndex = 0;
            this.dgListadoProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListadoProductos_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Agregar Premio";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtProdSeleccionado
            // 
            this.txtProdSeleccionado.Enabled = false;
            this.txtProdSeleccionado.Location = new System.Drawing.Point(173, 232);
            this.txtProdSeleccionado.Name = "txtProdSeleccionado";
            this.txtProdSeleccionado.Size = new System.Drawing.Size(192, 20);
            this.txtProdSeleccionado.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(48, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Producto seleccionado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(48, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ingrese cantidad:";
            // 
            // txtCantSeleccionada
            // 
            this.txtCantSeleccionada.Location = new System.Drawing.Point(173, 263);
            this.txtCantSeleccionada.Name = "txtCantSeleccionada";
            this.txtCantSeleccionada.Size = new System.Drawing.Size(41, 20);
            this.txtCantSeleccionada.TabIndex = 6;
            // 
            // listaPremiosSelec
            // 
            this.listaPremiosSelec.BackColor = System.Drawing.Color.SlateGray;
            this.listaPremiosSelec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listaPremiosSelec.ForeColor = System.Drawing.Color.White;
            this.listaPremiosSelec.FormattingEnabled = true;
            this.listaPremiosSelec.Location = new System.Drawing.Point(32, 303);
            this.listaPremiosSelec.Name = "listaPremiosSelec";
            this.listaPremiosSelec.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listaPremiosSelec.Size = new System.Drawing.Size(238, 65);
            this.listaPremiosSelec.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(321, 392);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 31);
            this.button2.TabIndex = 8;
            this.button2.Text = "Efectuar Canje";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listCantSelec
            // 
            this.listCantSelec.BackColor = System.Drawing.Color.SlateGray;
            this.listCantSelec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listCantSelec.ForeColor = System.Drawing.Color.White;
            this.listCantSelec.FormattingEnabled = true;
            this.listCantSelec.Location = new System.Drawing.Point(276, 303);
            this.listCantSelec.Name = "listCantSelec";
            this.listCantSelec.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listCantSelec.Size = new System.Drawing.Size(110, 65);
            this.listCantSelec.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 392);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 31);
            this.button4.TabIndex = 11;
            this.button4.Text = "Limpiar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 21);
            this.button3.TabIndex = 12;
            this.button3.Text = "Atrás";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(416, 426);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.listCantSelec);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listaPremiosSelec);
            this.Controls.Add(this.txtCantSeleccionada);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProdSeleccionado);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Canje de Millas";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListadoProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgListadoProductos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtProdSeleccionado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCantSeleccionada;
        private System.Windows.Forms.ListBox listaPremiosSelec;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listCantSelec;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label puntosDisp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}