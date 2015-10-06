namespace AerolineaFrba.Abm_Rol
{
    partial class Alta
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.lstFuncionalidadesTotales = new System.Windows.Forms.ListBox();
            this.lstFuncionalidadesNuevas = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "INGRESE EL NOMBRE DEL NUEVO ROL";
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(31, 53);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(156, 20);
            this.txtRol.TabIndex = 1;
            // 
            // lstFuncionalidadesTotales
            // 
            this.lstFuncionalidadesTotales.FormattingEnabled = true;
            this.lstFuncionalidadesTotales.Location = new System.Drawing.Point(31, 127);
            this.lstFuncionalidadesTotales.Name = "lstFuncionalidadesTotales";
            this.lstFuncionalidadesTotales.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesTotales.TabIndex = 2;
            // 
            // lstFuncionalidadesNuevas
            // 
            this.lstFuncionalidadesNuevas.FormattingEnabled = true;
            this.lstFuncionalidadesNuevas.Location = new System.Drawing.Point(442, 127);
            this.lstFuncionalidadesNuevas.Name = "lstFuncionalidadesNuevas";
            this.lstFuncionalidadesNuevas.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesNuevas.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "FUNCIONALIDADES TOTALES";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "NUEVAS FUNCIONALIDADES PARA EL ROL";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "AGREGAR A NUEAS FUNCIONALIDADES";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(209, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(214, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "QUITAR NUEVA FUNCIONALIDAD";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(209, 209);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(214, 52);
            this.button3.TabIndex = 8;
            this.button3.Text = "ALTA";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Alta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 423);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstFuncionalidadesNuevas);
            this.Controls.Add(this.lstFuncionalidadesTotales);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.label1);
            this.Name = "Alta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Alta";
            this.Load += new System.EventHandler(this.Alta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.ListBox lstFuncionalidadesTotales;
        private System.Windows.Forms.ListBox lstFuncionalidadesNuevas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}