namespace AerolineaFrba.Abm_Rol
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
            this.chkEstadoIgnorar = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optEstadoBaja = new System.Windows.Forms.RadioButton();
            this.optEstadoAlta = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFiltro1 = new System.Windows.Forms.TextBox();
            this.txtFiltro4 = new System.Windows.Forms.TextBox();
            this.cboFiltro3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFiltro2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.btlogica = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btfisica = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEstadoIgnorar);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtFiltro1);
            this.groupBox1.Controls.Add(this.txtFiltro4);
            this.groupBox1.Controls.Add(this.cboFiltro3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFiltro2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de Busqueda";
            // 
            // chkEstadoIgnorar
            // 
            this.chkEstadoIgnorar.AutoSize = true;
            this.chkEstadoIgnorar.Location = new System.Drawing.Point(173, 115);
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
            this.groupBox2.Location = new System.Drawing.Point(9, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 84);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrar por estado";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 20);
            this.button1.TabIndex = 8;
            this.button1.Text = "Seleccionar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFiltro1
            // 
            this.txtFiltro1.Location = new System.Drawing.Point(159, 20);
            this.txtFiltro1.Name = "txtFiltro1";
            this.txtFiltro1.Size = new System.Drawing.Size(90, 20);
            this.txtFiltro1.TabIndex = 16;
            // 
            // txtFiltro4
            // 
            this.txtFiltro4.Enabled = false;
            this.txtFiltro4.Location = new System.Drawing.Point(414, 59);
            this.txtFiltro4.Name = "txtFiltro4";
            this.txtFiltro4.Size = new System.Drawing.Size(87, 20);
            this.txtFiltro4.TabIndex = 7;
            // 
            // cboFiltro3
            // 
            this.cboFiltro3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro3.FormattingEnabled = true;
            this.cboFiltro3.Location = new System.Drawing.Point(414, 25);
            this.cboFiltro3.Name = "cboFiltro3";
            this.cboFiltro3.Size = new System.Drawing.Size(180, 21);
            this.cboFiltro3.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Filtro 4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filtro 3";
            // 
            // txtFiltro2
            // 
            this.txtFiltro2.Location = new System.Drawing.Point(160, 58);
            this.txtFiltro2.Name = "txtFiltro2";
            this.txtFiltro2.Size = new System.Drawing.Size(88, 20);
            this.txtFiltro2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
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
            // dg
            // 
            this.dg.AllowUserToAddRows = false;
            this.dg.AllowUserToDeleteRows = false;
            this.dg.AllowUserToResizeColumns = false;
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btlogica,
            this.btfisica});
            this.dg.Location = new System.Drawing.Point(26, 260);
            this.dg.Name = "dg";
            this.dg.Size = new System.Drawing.Size(596, 235);
            this.dg.TabIndex = 3;
            this.dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            // 
            // btlogica
            // 
            this.btlogica.FillWeight = 50F;
            this.btlogica.HeaderText = "Baja Lógica";
            this.btlogica.Name = "btlogica";
            this.btlogica.ReadOnly = true;
            this.btlogica.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btlogica.Text = "Baja Lógica";
            this.btlogica.UseColumnTextForButtonValue = true;
            // 
            // btfisica
            // 
            this.btfisica.FillWeight = 50F;
            this.btfisica.HeaderText = "Baja Física";
            this.btfisica.Name = "btfisica";
            this.btfisica.ReadOnly = true;
            this.btfisica.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btfisica.Text = "Baja Física";
            this.btfisica.UseColumnTextForButtonValue = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(22, 197);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 52);
            this.button4.TabIndex = 4;
            this.button4.Text = "Limpiar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(482, 197);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 52);
            this.button3.TabIndex = 5;
            this.button3.Text = "Buscar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // Baja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 539);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.groupBox1);
            this.Name = "Baja";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Baja";
            this.Load += new System.EventHandler(this.Listado_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFiltro2;
        private System.Windows.Forms.TextBox txtFiltro1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFiltro4;
        private System.Windows.Forms.ComboBox cboFiltro3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optEstadoBaja;
        private System.Windows.Forms.RadioButton optEstadoAlta;
        private System.Windows.Forms.CheckBox chkEstadoIgnorar;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.DataGridViewButtonColumn btlogica;
        private System.Windows.Forms.DataGridViewButtonColumn btfisica;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;

    }
}