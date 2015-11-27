namespace AerolineaFrba.Abm_Ruta
{
    partial class Servicios
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
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lstServiciosActuales = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstServiciosTotales = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(398, 201);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 31);
            this.button2.TabIndex = 7;
            this.button2.Text = "Aceptar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(198, 52);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(116, 31);
            this.button6.TabIndex = 46;
            this.button6.Text = "Agregar";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(354, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Tipos de servicio a darle de alta a la ruta";
            // 
            // lstServiciosActuales
            // 
            this.lstServiciosActuales.BackColor = System.Drawing.Color.SlateGray;
            this.lstServiciosActuales.ForeColor = System.Drawing.Color.White;
            this.lstServiciosActuales.FormattingEnabled = true;
            this.lstServiciosActuales.Location = new System.Drawing.Point(357, 52);
            this.lstServiciosActuales.Name = "lstServiciosActuales";
            this.lstServiciosActuales.Size = new System.Drawing.Size(156, 134);
            this.lstServiciosActuales.TabIndex = 44;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(235, 155);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 31);
            this.button3.TabIndex = 43;
            this.button3.Text = "Quitar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(33, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Tipos de servicio totales";
            // 
            // lstServiciosTotales
            // 
            this.lstServiciosTotales.BackColor = System.Drawing.Color.SlateGray;
            this.lstServiciosTotales.ForeColor = System.Drawing.Color.White;
            this.lstServiciosTotales.FormattingEnabled = true;
            this.lstServiciosTotales.Location = new System.Drawing.Point(36, 52);
            this.lstServiciosTotales.Name = "lstServiciosTotales";
            this.lstServiciosTotales.Size = new System.Drawing.Size(156, 134);
            this.lstServiciosTotales.TabIndex = 41;
            // 
            // Servicios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(572, 260);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstServiciosActuales);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstServiciosTotales);
            this.Controls.Add(this.button2);
            this.MaximizeBox = false;
            this.Name = "Servicios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de tipos de servicios";
            this.Load += new System.EventHandler(this.Servicios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstServiciosActuales;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstServiciosTotales;

    }
}