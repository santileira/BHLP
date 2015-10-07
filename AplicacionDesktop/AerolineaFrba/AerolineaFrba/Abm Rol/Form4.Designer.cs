namespace AerolineaFrba.Abm_Rol
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
            this.label3 = new System.Windows.Forms.Label();
            this.lstFuncionalidadesAEliminar = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(653, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "FUNCIONALIDADES DADAS DE BAJA LOGICA";
            // 
            // lstFuncionalidadesAEliminar
            // 
            this.lstFuncionalidadesAEliminar.FormattingEnabled = true;
            this.lstFuncionalidadesAEliminar.Location = new System.Drawing.Point(656, 122);
            this.lstFuncionalidadesAEliminar.Name = "lstFuncionalidadesAEliminar";
            this.lstFuncionalidadesAEliminar.Size = new System.Drawing.Size(156, 134);
            this.lstFuncionalidadesAEliminar.TabIndex = 29;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(193, 40);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(214, 35);
            this.button4.TabIndex = 28;
            this.button4.Text = "BUSCAR ROL Y CARGAR FUNCIONALIDADES ACTUALES";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(353, 312);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(156, 52);
            this.button3.TabIndex = 27;
            this.button3.Text = "EFECTUAR MODIFICACION";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(528, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 25;
            this.button1.Text = "BAJA LOGICA";
            this.button1.UseVisualStyleBackColor = true;

            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(15, 48);
            this.txtRol.Name = "txtRol";
            this.txtRol.Size = new System.Drawing.Size(156, 20);
            this.txtRol.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "INGRESE EL NOMBRE DEL NUEVO ROL";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(656, 271);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(156, 35);
            this.button5.TabIndex = 33;
            this.button5.Text = "QUITAR SELECCIONADO";
            this.button5.UseVisualStyleBackColor = true;

            // 
            // Modificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 393);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstFuncionalidadesAEliminar);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRol);
            this.Controls.Add(this.label1);
            this.Name = "Modificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABM Modificacion";
            this.Load += new System.EventHandler(this.Modificacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstFuncionalidadesAEliminar;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
    }
}