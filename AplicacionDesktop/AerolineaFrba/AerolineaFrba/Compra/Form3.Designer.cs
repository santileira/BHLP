namespace AerolineaFrba.Compra
{
    partial class Form3
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
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPasajes = new System.Windows.Forms.CheckBox();
            this.txtButacas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkEncomiendas = new System.Windows.Forms.CheckBox();
            this.txtKilos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 246);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(85, 38);
            this.button6.TabIndex = 52;
            this.button6.Text = "Atras";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(230, 246);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 38);
            this.button5.TabIndex = 53;
            this.button5.Text = "Siguiente";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPasajes);
            this.groupBox1.Controls.Add(this.txtButacas);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 99);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comprar Pasajes";
            // 
            // chkPasajes
            // 
            this.chkPasajes.AutoSize = true;
            this.chkPasajes.Location = new System.Drawing.Point(9, 30);
            this.chkPasajes.Name = "chkPasajes";
            this.chkPasajes.Size = new System.Drawing.Size(64, 17);
            this.chkPasajes.TabIndex = 57;
            this.chkPasajes.Text = "Habilitar";
            this.chkPasajes.UseVisualStyleBackColor = true;
            this.chkPasajes.CheckedChanged += new System.EventHandler(this.chkPasajes_CheckedChanged);
            // 
            // txtButacas
            // 
            this.txtButacas.Enabled = false;
            this.txtButacas.Location = new System.Drawing.Point(183, 59);
            this.txtButacas.Name = "txtButacas";
            this.txtButacas.Size = new System.Drawing.Size(100, 20);
            this.txtButacas.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Ingrese la cantidad de Pasajes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkEncomiendas);
            this.groupBox2.Controls.Add(this.txtKilos);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 99);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solicitar envio de encomienda";
            // 
            // chkEncomiendas
            // 
            this.chkEncomiendas.AutoSize = true;
            this.chkEncomiendas.Location = new System.Drawing.Point(9, 33);
            this.chkEncomiendas.Name = "chkEncomiendas";
            this.chkEncomiendas.Size = new System.Drawing.Size(64, 17);
            this.chkEncomiendas.TabIndex = 58;
            this.chkEncomiendas.Text = "Habilitar";
            this.chkEncomiendas.UseVisualStyleBackColor = true;
            this.chkEncomiendas.CheckedChanged += new System.EventHandler(this.chkEncomiendas_CheckedChanged);
            // 
            // txtKilos
            // 
            this.txtKilos.Enabled = false;
            this.txtKilos.Location = new System.Drawing.Point(181, 59);
            this.txtKilos.Name = "txtKilos";
            this.txtKilos.Size = new System.Drawing.Size(100, 20);
            this.txtKilos.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Ingrese la cantidad de Kilos:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(123, 246);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 38);
            this.button4.TabIndex = 56;
            this.button4.Text = "Limpiar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(331, 316);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.MaximizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cantidad de Pasajes/Kilos requeridos";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPasajes;
        private System.Windows.Forms.TextBox txtButacas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkEncomiendas;
        private System.Windows.Forms.TextBox txtKilos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
    }
}