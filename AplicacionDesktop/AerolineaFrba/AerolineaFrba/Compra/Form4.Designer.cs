namespace AerolineaFrba.Compra
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
            this.button1 = new System.Windows.Forms.Button();
            this.dgPasajes = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.dgEncomiendas = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgPasajes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomiendas)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cargar Pasaje";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgPasajes
            // 
            this.dgPasajes.AllowUserToAddRows = false;
            this.dgPasajes.AllowUserToDeleteRows = false;
            this.dgPasajes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgPasajes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgPasajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPasajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPasajes.Location = new System.Drawing.Point(32, 129);
            this.dgPasajes.MultiSelect = false;
            this.dgPasajes.Name = "dgPasajes";
            this.dgPasajes.ReadOnly = true;
            this.dgPasajes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPasajes.Size = new System.Drawing.Size(616, 117);
            this.dgPasajes.TabIndex = 49;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(78, 21);
            this.button6.TabIndex = 53;
            this.button6.Text = "Atras";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dgEncomiendas
            // 
            this.dgEncomiendas.AllowUserToAddRows = false;
            this.dgEncomiendas.AllowUserToDeleteRows = false;
            this.dgEncomiendas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgEncomiendas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgEncomiendas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgEncomiendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncomiendas.Location = new System.Drawing.Point(32, 348);
            this.dgEncomiendas.MultiSelect = false;
            this.dgEncomiendas.Name = "dgEncomiendas";
            this.dgEncomiendas.ReadOnly = true;
            this.dgEncomiendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEncomiendas.Size = new System.Drawing.Size(616, 117);
            this.dgEncomiendas.TabIndex = 55;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(32, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 48);
            this.button2.TabIndex = 54;
            this.button2.Text = "Cargar Pasaje";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(546, 502);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 48);
            this.button3.TabIndex = 56;
            this.button3.Text = "Efectuar compra";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(546, 54);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 48);
            this.button4.TabIndex = 57;
            this.button4.Text = "Cancelar Pasaje";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 593);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dgEncomiendas);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dgPasajes);
            this.Controls.Add(this.button1);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga de datos";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPasajes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomiendas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgPasajes;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgEncomiendas;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}