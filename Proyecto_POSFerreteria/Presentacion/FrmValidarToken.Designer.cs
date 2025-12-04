namespace Proyecto_POSFerreteria.Presentacion
{
    partial class FrmValidarToken
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
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnValidar = new System.Windows.Forms.Button();
            this.cbxTipoRecuperacion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupCambio = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.lblUsuarioMostrado = new System.Windows.Forms.Label();
            this.groupCambio.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(204, 21);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(151, 27);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "VALIDACION";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(269, 138);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(202, 34);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(105, 201);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(18, 27);
            this.lblMensaje.TabIndex = 2;
            this.lblMensaje.Text = "/";
            // 
            // btnValidar
            // 
            this.btnValidar.Location = new System.Drawing.Point(47, 196);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(177, 36);
            this.btnValidar.TabIndex = 3;
            this.btnValidar.Text = "Aceptar";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // cbxTipoRecuperacion
            // 
            this.cbxTipoRecuperacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(241)))), ((int)(((byte)(230)))));
            this.cbxTipoRecuperacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbxTipoRecuperacion.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoRecuperacion.FormattingEnabled = true;
            this.cbxTipoRecuperacion.Items.AddRange(new object[] {
            "OLVIDÉ MI CONTRASEÑA",
            "OLVIDÉ MI USUARIO Y CONTRASEÑA"});
            this.cbxTipoRecuperacion.Location = new System.Drawing.Point(247, 141);
            this.cbxTipoRecuperacion.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTipoRecuperacion.Name = "cbxTipoRecuperacion";
            this.cbxTipoRecuperacion.Size = new System.Drawing.Size(0, 35);
            this.cbxTipoRecuperacion.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 27);
            this.label2.TabIndex = 16;
            this.label2.Text = "Codigo";
            // 
            // groupCambio
            // 
            this.groupCambio.Controls.Add(this.label4);
            this.groupCambio.Controls.Add(this.label1);
            this.groupCambio.Controls.Add(this.btnCambiar);
            this.groupCambio.Controls.Add(this.txtConfirm);
            this.groupCambio.Controls.Add(this.txtNueva);
            this.groupCambio.Location = new System.Drawing.Point(59, 238);
            this.groupCambio.Name = "groupCambio";
            this.groupCambio.Size = new System.Drawing.Size(431, 185);
            this.groupCambio.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 27);
            this.label4.TabIndex = 22;
            this.label4.Text = "Confirmar Contrasena";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 27);
            this.label1.TabIndex = 21;
            this.label1.Text = "Nueva Contrasena";
            // 
            // btnCambiar
            // 
            this.btnCambiar.Location = new System.Drawing.Point(23, 137);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(177, 36);
            this.btnCambiar.TabIndex = 21;
            this.btnCambiar.Text = "Cambiar";
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(229, 76);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(155, 34);
            this.txtConfirm.TabIndex = 1;
            // 
            // txtNueva
            // 
            this.txtNueva.Location = new System.Drawing.Point(229, 23);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.Size = new System.Drawing.Size(155, 34);
            this.txtNueva.TabIndex = 0;
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Location = new System.Drawing.Point(30, 21);
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.ReadOnly = true;
            this.txtIdentificador.Size = new System.Drawing.Size(129, 34);
            this.txtIdentificador.TabIndex = 21;
            // 
            // lblUsuarioMostrado
            // 
            this.lblUsuarioMostrado.AutoSize = true;
            this.lblUsuarioMostrado.Location = new System.Drawing.Point(247, 75);
            this.lblUsuarioMostrado.Name = "lblUsuarioMostrado";
            this.lblUsuarioMostrado.Size = new System.Drawing.Size(0, 27);
            this.lblUsuarioMostrado.TabIndex = 22;
            // 
            // FrmValidarToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 435);
            this.Controls.Add(this.lblUsuarioMostrado);
            this.Controls.Add(this.txtIdentificador);
            this.Controls.Add(this.groupCambio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxTipoRecuperacion);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmValidarToken";
            this.Load += new System.EventHandler(this.FrmValidarToken_Load);
            this.groupCambio.ResumeLayout(false);
            this.groupCambio.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.ComboBox cbxTipoRecuperacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel groupCambio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCambiar;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.TextBox txtIdentificador;
        private System.Windows.Forms.Label lblUsuarioMostrado;
    }
}