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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btBuscar = new System.Windows.Forms.Button();
            this.dgPasaje = new System.Windows.Forms.DataGridView();
            this.Cancelar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.dgEncomienda = new System.Windows.Forms.DataGridView();
            this.Devolver = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btFinalizar = new System.Windows.Forms.Button();
            this.btLimpiar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgPasaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEncomienda)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(296, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese código de compra *";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(445, 41);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(31, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pasajes";
            // 
            // btBuscar
            // 
            this.btBuscar.ForeColor = System.Drawing.Color.Black;
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
            this.dgPasaje.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgPasaje.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPasaje.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPasaje.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cancelar});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPasaje.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgPasaje.EnableHeadersVisualStyles = false;
            this.dgPasaje.Location = new System.Drawing.Point(34, 118);
            this.dgPasaje.MultiSelect = false;
            this.dgPasaje.Name = "dgPasaje";
            this.dgPasaje.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPasaje.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgPasaje.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPasaje.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPasaje.Size = new System.Drawing.Size(802, 144);
            this.dgPasaje.TabIndex = 4;
            this.dgPasaje.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPasaje_CellContentClick);
            // 
            // Cancelar
            // 
            this.Cancelar.HeaderText = "Cancelar";
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.ReadOnly = true;
            this.Cancelar.Text = "Cancelar";
            this.Cancelar.UseColumnTextForButtonValue = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
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
            this.dgEncomienda.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dgEncomienda.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgEncomienda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEncomienda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Devolver});
            this.dgEncomienda.EnableHeadersVisualStyles = false;
            this.dgEncomienda.Location = new System.Drawing.Point(34, 319);
            this.dgEncomienda.MultiSelect = false;
            this.dgEncomienda.Name = "dgEncomienda";
            this.dgEncomienda.ReadOnly = true;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgEncomienda.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgEncomienda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEncomienda.Size = new System.Drawing.Size(802, 144);
            this.dgEncomienda.TabIndex = 6;
            this.dgEncomienda.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEncomienda_CellContentClick);
            // 
            // Devolver
            // 
            this.Devolver.HeaderText = "Cancelar";
            this.Devolver.Name = "Devolver";
            this.Devolver.ReadOnly = true;
            this.Devolver.Text = "Cancelar";
            this.Devolver.UseColumnTextForButtonValue = true;
            // 
            // btFinalizar
            // 
            this.btFinalizar.ForeColor = System.Drawing.Color.Black;
            this.btFinalizar.Location = new System.Drawing.Point(721, 487);
            this.btFinalizar.Name = "btFinalizar";
            this.btFinalizar.Size = new System.Drawing.Size(115, 32);
            this.btFinalizar.TabIndex = 8;
            this.btFinalizar.Text = "Finalizar";
            this.btFinalizar.UseVisualStyleBackColor = true;
            this.btFinalizar.Click += new System.EventHandler(this.btFinalizar_Click);
            // 
            // btLimpiar
            // 
            this.btLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btLimpiar.Location = new System.Drawing.Point(34, 487);
            this.btLimpiar.Name = "btLimpiar";
            this.btLimpiar.Size = new System.Drawing.Size(115, 32);
            this.btLimpiar.TabIndex = 9;
            this.btLimpiar.Text = "Limpiar";
            this.btLimpiar.UseVisualStyleBackColor = true;
            this.btLimpiar.Click += new System.EventHandler(this.btLimpiar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 537);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(339, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "(*) Las devoluciones se harán efectivas al momento de apretar finalizar";
            // 
            // dgEncomiendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(870, 566);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btLimpiar);
            this.Controls.Add(this.btFinalizar);
            this.Controls.Add(this.dgEncomienda);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgPasaje);
            this.Controls.Add(this.btBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.Name = "dgEncomiendas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.DataGridViewButtonColumn Cancelar;
        private System.Windows.Forms.DataGridViewButtonColumn Devolver;
        private System.Windows.Forms.Button btFinalizar;
        private System.Windows.Forms.Button btLimpiar;
        private System.Windows.Forms.Label label4;
    }
}