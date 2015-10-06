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
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstFuncionalidadesTotales = new System.Windows.Forms.ListBox();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lstFuncionalidadesAEliminar = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(203, 206);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(214, 60);
            this.button3.TabIndex = 17;
            this.button3.Text = "BAJA FISICA";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(203, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 60);
            this.button1.TabIndex = 15;
            this.button1.Text = "BAJA LOGICA";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "FUNCIONALIDADES DEL ROL";
            // 
            // lstFuncionalidadesTotales
            // 
            this.lstFuncionalidadesTotales.FormattingEnabled = true;
            this.lstFuncionalidadesTotales.Location = new System.Drawing.Point(25, 132);
            this.lstFuncionalidadesTotales.Name = "lstFuncionalidadesTotales";
            this.lstFuncionalidadesTotales.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesTotales.TabIndex = 11;
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(25, 58);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(156, 20);
            this.txtRol.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "INGRESE EL NOMBRE DEL NUEVO ROL";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(203, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(214, 35);
            this.button4.TabIndex = 18;
            this.button4.Text = "BUSCAR ROL Y CARGAR FUNCIONALIDADES ACTUALES";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "FUNCIONALIDADES DADAS DE BAJA LOGICA";
            // 
            // lstFuncionalidadesAEliminar
            // 
            this.lstFuncionalidadesAEliminar.FormattingEnabled = true;
            this.lstFuncionalidadesAEliminar.Location = new System.Drawing.Point(441, 132);
            this.lstFuncionalidadesAEliminar.Name = "lstFuncionalidadesAEliminar";
            this.lstFuncionalidadesAEliminar.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesAEliminar.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(441, 282);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 35);
            this.button2.TabIndex = 27;
            this.button2.Text = "QUITAR SELECCIONADO";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Baja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 329);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstFuncionalidadesAEliminar);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstFuncionalidadesTotales);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.label1);
            this.Name = "Baja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Baja";
            this.Load += new System.EventHandler(this.Baja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstFuncionalidadesTotales;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstFuncionalidadesAEliminar;
        private System.Windows.Forms.Button button2;
    }
}