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
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(208, 23);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(151, 27);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "VALIDACION";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(243, 133);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(202, 34);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Location = new System.Drawing.Point(55, 73);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(18, 27);
            this.lblMensaje.TabIndex = 2;
            this.lblMensaje.Text = "/";
            // 
            // btnValidar
            // 
            this.btnValidar.Location = new System.Drawing.Point(182, 247);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(177, 36);
            this.btnValidar.TabIndex = 3;
            this.btnValidar.Text = "Aceptar";
            this.btnValidar.UseVisualStyleBackColor = true;
            
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
            this.cbxTipoRecuperacion.Location = new System.Drawing.Point(243, 193);
            this.cbxTipoRecuperacion.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTipoRecuperacion.Name = "cbxTipoRecuperacion";
            this.cbxTipoRecuperacion.Size = new System.Drawing.Size(237, 35);
            this.cbxTipoRecuperacion.TabIndex = 15;
            this.cbxTipoRecuperacion.SelectedIndexChanged += new System.EventHandler(this.cbxTipoRecuperacion_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 27);
            this.label2.TabIndex = 16;
            this.label2.Text = "Codigo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 27);
            this.label3.TabIndex = 17;
            this.label3.Text = "Opcion de recuperacion";
            // 
            // FrmValidarToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 317);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxTipoRecuperacion);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmValidarToken";
            this.Text = "Validacion de Token";
            this.Load += new System.EventHandler(this.FrmValidarToken_Load);
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
        private System.Windows.Forms.Label label3;
    }
}