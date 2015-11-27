namespace AerolineaFrba
{
    partial class FormLogin
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
            this.gbAdministrador = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.botonIngresar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioInvitado = new System.Windows.Forms.RadioButton();
            this.radioAdministrador = new System.Windows.Forms.RadioButton();
            this.gbInvitado = new System.Windows.Forms.GroupBox();
            this.cboRoles = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbAdministrador.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbInvitado.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAdministrador
            // 
            this.gbAdministrador.Controls.Add(this.txtPassword);
            this.gbAdministrador.Controls.Add(this.label2);
            this.gbAdministrador.Controls.Add(this.txtUsuario);
            this.gbAdministrador.Controls.Add(this.label1);
            this.gbAdministrador.ForeColor = System.Drawing.Color.White;
            this.gbAdministrador.Location = new System.Drawing.Point(22, 12);
            this.gbAdministrador.Name = "gbAdministrador";
            this.gbAdministrador.Size = new System.Drawing.Size(248, 96);
            this.gbAdministrador.TabIndex = 0;
            this.gbAdministrador.TabStop = false;
            this.gbAdministrador.Text = "Login Administrador";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(101, 62);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(121, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(101, 29);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(121, 20);
            this.txtUsuario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // botonIngresar
            // 
            this.botonIngresar.Location = new System.Drawing.Point(422, 135);
            this.botonIngresar.Name = "botonIngresar";
            this.botonIngresar.Size = new System.Drawing.Size(75, 23);
            this.botonIngresar.TabIndex = 5;
            this.botonIngresar.Text = "Ingresar";
            this.botonIngresar.UseVisualStyleBackColor = true;
            this.botonIngresar.Click += new System.EventHandler(this.botonIngresar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioInvitado);
            this.groupBox2.Controls.Add(this.radioAdministrador);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(22, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 58);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ingresar como: ";
            // 
            // radioInvitado
            // 
            this.radioInvitado.AutoSize = true;
            this.radioInvitado.Location = new System.Drawing.Point(129, 27);
            this.radioInvitado.Name = "radioInvitado";
            this.radioInvitado.Size = new System.Drawing.Size(63, 17);
            this.radioInvitado.TabIndex = 1;
            this.radioInvitado.Text = "Invitado";
            this.radioInvitado.UseVisualStyleBackColor = true;
            // 
            // radioAdministrador
            // 
            this.radioAdministrador.AutoSize = true;
            this.radioAdministrador.Checked = true;
            this.radioAdministrador.Location = new System.Drawing.Point(16, 27);
            this.radioAdministrador.Name = "radioAdministrador";
            this.radioAdministrador.Size = new System.Drawing.Size(88, 17);
            this.radioAdministrador.TabIndex = 0;
            this.radioAdministrador.TabStop = true;
            this.radioAdministrador.Text = "Administrador";
            this.radioAdministrador.UseVisualStyleBackColor = true;
            this.radioAdministrador.CheckedChanged += new System.EventHandler(this.radioAdministrador_CheckedChanged);
            // 
            // gbInvitado
            // 
            this.gbInvitado.Controls.Add(this.cboRoles);
            this.gbInvitado.Controls.Add(this.label3);
            this.gbInvitado.ForeColor = System.Drawing.Color.White;
            this.gbInvitado.Location = new System.Drawing.Point(288, 12);
            this.gbInvitado.Name = "gbInvitado";
            this.gbInvitado.Size = new System.Drawing.Size(209, 96);
            this.gbInvitado.TabIndex = 4;
            this.gbInvitado.TabStop = false;
            this.gbInvitado.Text = "Login Invitado";
            // 
            // cboRoles
            // 
            this.cboRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoles.FormattingEnabled = true;
            this.cboRoles.Location = new System.Drawing.Point(61, 40);
            this.cboRoles.Name = "cboRoles";
            this.cboRoles.Size = new System.Drawing.Size(121, 21);
            this.cboRoles.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Rol";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(559, 193);
            this.Controls.Add(this.gbInvitado);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.botonIngresar);
            this.Controls.Add(this.gbAdministrador);
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.FormPrincipalAdministrador_Load);
            this.gbAdministrador.ResumeLayout(false);
            this.gbAdministrador.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbInvitado.ResumeLayout(false);
            this.gbInvitado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAdministrador;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button botonIngresar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioInvitado;
        private System.Windows.Forms.RadioButton radioAdministrador;
        private System.Windows.Forms.GroupBox gbInvitado;
        private System.Windows.Forms.ComboBox cboRoles;
        private System.Windows.Forms.Label label3;

    }
}