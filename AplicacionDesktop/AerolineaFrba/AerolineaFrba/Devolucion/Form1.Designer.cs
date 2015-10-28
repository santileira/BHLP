namespace AerolineaFrba.Devolucion
{
    partial class dgEncomiendas
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
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btBuscar = new System.Windows.Forms.Button();
            this.dgPasaje = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dgEncomienda = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgPasaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomienda)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese código de compra";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(445, 41);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pasajes";
            // 
            // btBuscar
            // 
            this.btBuscar.Location = new System.Drawing.Point(569, 39);
            this.btBuscar.Name = "btBuscar";
            this.btBuscar.Size = new System.Drawing.Size(75, 23);
            this.btBuscar.TabIndex = 3;
            this.btBuscar.Text = "Buscar";
            this.btBuscar.UseVisualStyleBackColor = true;
            this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // dgPasaje
            // 
            this.dgPasaje.AllowUserToAddRows = false;
            this.dgPasaje.AllowUserToDeleteRows = false;
            this.dgPasaje.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgPasaje.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPasaje.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPasaje.EnableHeadersVisualStyles = false;
            this.dgPasaje.Location = new System.Drawing.Point(34, 118);
            this.dgPasaje.MultiSelect = false;
            this.dgPasaje.Name = "dgPasaje";
            this.dgPasaje.ReadOnly = true;
            this.dgPasaje.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPasaje.Size = new System.Drawing.Size(802, 144);
            this.dgPasaje.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Encomiendas";
            // 
            // dgEncomienda
            // 
            this.dgEncomienda.AllowUserToAddRows = false;
            this.dgEncomienda.AllowUserToDeleteRows = false;
            this.dgEncomienda.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgEncomienda.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgEncomienda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncomienda.EnableHeadersVisualStyles = false;
            this.dgEncomienda.Location = new System.Drawing.Point(34, 319);
            this.dgEncomienda.MultiSelect = false;
            this.dgEncomienda.Name = "dgEncomienda";
            this.dgEncomienda.ReadOnly = true;
            this.dgEncomienda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEncomienda.Size = new System.Drawing.Size(802, 144);
            this.dgEncomienda.TabIndex = 6;
            // 
            // dgEncomiendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 531);
            this.Controls.Add(this.dgEncomienda);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgPasaje);
            this.Controls.Add(this.btBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.Name = "dgEncomiendas";
            this.Text = "Cancelación";
            ((System.ComponentModel.ISupportInitialize)(this.dgPasaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomienda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btBuscar;
        private System.Windows.Forms.DataGridView dgPasaje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgEncomienda;
    }
}