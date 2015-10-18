namespace AerolineaFrba.Abm_Aeronave
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
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkReinicio = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtTime = new System.Windows.Forms.DateTimePicker();
            this.cboFabricante = new System.Windows.Forms.ComboBox();
            this.cboServicio = new System.Windows.Forms.ComboBox();
            this.txtButacas = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKilos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(37, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(78, 21);
            this.button6.TabIndex = 8;
            this.button6.Text = "Atras";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(36, 552);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 11;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 552);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 10;
            this.button2.Text = "Guardar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cboFabricante);
            this.groupBox1.Controls.Add(this.cboServicio);
            this.groupBox1.Controls.Add(this.txtButacas);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtKilos);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMatricula);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtModelo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(36, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 488);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Campos de la Aeronave";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkReinicio);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dtTime);
            this.groupBox2.Location = new System.Drawing.Point(25, 363);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(376, 102);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reinicio del servicio de la aeronave";
            // 
            // chkReinicio
            // 
            this.chkReinicio.AutoSize = true;
            this.chkReinicio.Location = new System.Drawing.Point(14, 25);
            this.chkReinicio.Name = "chkReinicio";
            this.chkReinicio.Size = new System.Drawing.Size(217, 17);
            this.chkReinicio.TabIndex = 37;
            this.chkReinicio.Text = "Modificar la fecha de reinicio del servicio";
            this.chkReinicio.UseVisualStyleBackColor = true;
            this.chkReinicio.CheckedChanged += new System.EventHandler(this.chkReinicio_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Fecha de reinicio de servicio";
            // 
            // dtTime
            // 
            this.dtTime.Location = new System.Drawing.Point(165, 58);
            this.dtTime.Name = "dtTime";
            this.dtTime.Size = new System.Drawing.Size(200, 20);
            this.dtTime.TabIndex = 35;
            // 
            // cboFabricante
            // 
            this.cboFabricante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFabricante.Enabled = false;
            this.cboFabricante.FormattingEnabled = true;
            this.cboFabricante.Location = new System.Drawing.Point(175, 146);
            this.cboFabricante.Name = "cboFabricante";
            this.cboFabricante.Size = new System.Drawing.Size(157, 21);
            this.cboFabricante.TabIndex = 31;
            // 
            // cboServicio
            // 
            this.cboServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServicio.Enabled = false;
            this.cboServicio.FormattingEnabled = true;
            this.cboServicio.Location = new System.Drawing.Point(175, 205);
            this.cboServicio.Name = "cboServicio";
            this.cboServicio.Size = new System.Drawing.Size(157, 21);
            this.cboServicio.TabIndex = 30;
            // 
            // txtButacas
            // 
            this.txtButacas.Enabled = false;
            this.txtButacas.Location = new System.Drawing.Point(175, 259);
            this.txtButacas.Name = "txtButacas";
            this.txtButacas.Size = new System.Drawing.Size(157, 20);
            this.txtButacas.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Cantidad de butacas";
            // 
            // txtKilos
            // 
            this.txtKilos.Enabled = false;
            this.txtKilos.Location = new System.Drawing.Point(175, 314);
            this.txtKilos.Name = "txtKilos";
            this.txtKilos.Size = new System.Drawing.Size(157, 20);
            this.txtKilos.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cantidad de KG que lleva";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo de servicio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fabricante";
            // 
            // txtMatricula
            // 
            this.txtMatricula.Enabled = false;
            this.txtMatricula.Location = new System.Drawing.Point(175, 94);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(157, 20);
            this.txtMatricula.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Matricula";
            // 
            // txtModelo
            // 
            this.txtModelo.Enabled = false;
            this.txtModelo.Location = new System.Drawing.Point(175, 46);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(157, 20);
            this.txtModelo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modelo";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(364, 12);
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
            this.ClientSize = new System.Drawing.Size(491, 630);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Name = "Modificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Modificacion";
            this.Load += new System.EventHandler(this.Modificacion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboFabricante;
        private System.Windows.Forms.ComboBox cboServicio;
        private System.Windows.Forms.TextBox txtButacas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKilos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkReinicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtTime;
    }
}