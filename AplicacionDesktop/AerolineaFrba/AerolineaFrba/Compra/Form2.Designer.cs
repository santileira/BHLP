namespace AerolineaFrba.Compra
{
    partial class Form2
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
            this.dgPasillo = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.dgVentanilla = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgPasillo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgVentanilla)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPasillo
            // 
            this.dgPasillo.AllowUserToAddRows = false;
            this.dgPasillo.AllowUserToDeleteRows = false;
            this.dgPasillo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgPasillo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgPasillo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPasillo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPasillo.Location = new System.Drawing.Point(343, 129);
            this.dgPasillo.MultiSelect = false;
            this.dgPasillo.Name = "dgPasillo";
            this.dgPasillo.ReadOnly = true;
            this.dgPasillo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPasillo.Size = new System.Drawing.Size(251, 89);
            this.dgPasillo.TabIndex = 46;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(27, 23);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(78, 21);
            this.button6.TabIndex = 47;
            this.button6.Text = "Atras";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dgVentanilla
            // 
            this.dgVentanilla.AllowUserToAddRows = false;
            this.dgVentanilla.AllowUserToDeleteRows = false;
            this.dgVentanilla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgVentanilla.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgVentanilla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgVentanilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVentanilla.Location = new System.Drawing.Point(27, 129);
            this.dgVentanilla.MultiSelect = false;
            this.dgVentanilla.Name = "dgVentanilla";
            this.dgVentanilla.ReadOnly = true;
            this.dgVentanilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVentanilla.Size = new System.Drawing.Size(251, 89);
            this.dgVentanilla.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Butacas libres:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 354);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgVentanilla);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dgPasillo);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compra de Pasajes y Encomiendas";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPasillo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgVentanilla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPasillo;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgVentanilla;
        private System.Windows.Forms.Label label2;
    }
}