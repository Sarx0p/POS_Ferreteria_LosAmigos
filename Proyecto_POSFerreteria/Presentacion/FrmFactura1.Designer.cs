namespace Proyecto_POSFerreteria.Presentacion
{
    partial class FrmFactura1
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFactura1));
            this.groupBoxCliente = new System.Windows.Forms.GroupBox();
            this.lblCliNombre = new System.Windows.Forms.Label();
            this.txtCliNombre = new System.Windows.Forms.TextBox();
            this.lblCliDireccion = new System.Windows.Forms.Label();
            this.txtCliDireccion = new System.Windows.Forms.TextBox();
            this.lblCliMail = new System.Windows.Forms.Label();
            this.txtCliMail = new System.Windows.Forms.TextBox();
            this.lblCliTelefono = new System.Windows.Forms.Label();
            this.txtCliTelefono = new System.Windows.Forms.TextBox();
            this.groupBoxEmpresa = new System.Windows.Forms.GroupBox();
            this.lblEmpNombre = new System.Windows.Forms.Label();
            this.txtEmpNombre = new System.Windows.Forms.TextBox();
            this.lblEmpDireccion = new System.Windows.Forms.Label();
            this.txtEmpDireccion = new System.Windows.Forms.TextBox();
            this.lblEmpMail = new System.Windows.Forms.Label();
            this.txtEmpMail = new System.Windows.Forms.TextBox();
            this.lblEmpTelefono = new System.Windows.Forms.Label();
            this.txtEmpTelefono = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.lblSubtotalText = new System.Windows.Forms.Label();
            this.lblIvaText = new System.Windows.Forms.Label();
            this.lblTotalText = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblSudTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCobro = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxCliente.SuspendLayout();
            this.groupBoxEmpresa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCliente
            // 
            this.groupBoxCliente.Controls.Add(this.lblCliNombre);
            this.groupBoxCliente.Controls.Add(this.txtCliNombre);
            this.groupBoxCliente.Controls.Add(this.lblCliDireccion);
            this.groupBoxCliente.Controls.Add(this.txtCliDireccion);
            this.groupBoxCliente.Controls.Add(this.lblCliMail);
            this.groupBoxCliente.Controls.Add(this.txtCliMail);
            this.groupBoxCliente.Controls.Add(this.lblCliTelefono);
            this.groupBoxCliente.Controls.Add(this.txtCliTelefono);
            this.groupBoxCliente.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCliente.Location = new System.Drawing.Point(33, 104);
            this.groupBoxCliente.Name = "groupBoxCliente";
            this.groupBoxCliente.Size = new System.Drawing.Size(440, 180);
            this.groupBoxCliente.TabIndex = 1;
            this.groupBoxCliente.TabStop = false;
            this.groupBoxCliente.Text = "DATOS DEL CLIENTE";
            // 
            // lblCliNombre
            // 
            this.lblCliNombre.AutoSize = true;
            this.lblCliNombre.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliNombre.Location = new System.Drawing.Point(15, 30);
            this.lblCliNombre.Name = "lblCliNombre";
            this.lblCliNombre.Size = new System.Drawing.Size(76, 22);
            this.lblCliNombre.TabIndex = 0;
            this.lblCliNombre.Text = "Nombre:";
            // 
            // txtCliNombre
            // 
            this.txtCliNombre.Location = new System.Drawing.Point(110, 26);
            this.txtCliNombre.Name = "txtCliNombre";
            this.txtCliNombre.Size = new System.Drawing.Size(310, 29);
            this.txtCliNombre.TabIndex = 1;
            // 
            // lblCliDireccion
            // 
            this.lblCliDireccion.AutoSize = true;
            this.lblCliDireccion.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliDireccion.Location = new System.Drawing.Point(15, 65);
            this.lblCliDireccion.Name = "lblCliDireccion";
            this.lblCliDireccion.Size = new System.Drawing.Size(85, 22);
            this.lblCliDireccion.TabIndex = 2;
            this.lblCliDireccion.Text = "Dirección:";
            // 
            // txtCliDireccion
            // 
            this.txtCliDireccion.Location = new System.Drawing.Point(110, 62);
            this.txtCliDireccion.Name = "txtCliDireccion";
            this.txtCliDireccion.Size = new System.Drawing.Size(310, 29);
            this.txtCliDireccion.TabIndex = 3;
            // 
            // lblCliMail
            // 
            this.lblCliMail.AutoSize = true;
            this.lblCliMail.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliMail.Location = new System.Drawing.Point(15, 100);
            this.lblCliMail.Name = "lblCliMail";
            this.lblCliMail.Size = new System.Drawing.Size(48, 22);
            this.lblCliMail.TabIndex = 4;
            this.lblCliMail.Text = "Mail:";
            // 
            // txtCliMail
            // 
            this.txtCliMail.Location = new System.Drawing.Point(110, 96);
            this.txtCliMail.Name = "txtCliMail";
            this.txtCliMail.Size = new System.Drawing.Size(310, 29);
            this.txtCliMail.TabIndex = 5;
            // 
            // lblCliTelefono
            // 
            this.lblCliTelefono.AutoSize = true;
            this.lblCliTelefono.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliTelefono.Location = new System.Drawing.Point(15, 135);
            this.lblCliTelefono.Name = "lblCliTelefono";
            this.lblCliTelefono.Size = new System.Drawing.Size(79, 22);
            this.lblCliTelefono.TabIndex = 6;
            this.lblCliTelefono.Text = "Teléfono:";
            // 
            // txtCliTelefono
            // 
            this.txtCliTelefono.Location = new System.Drawing.Point(110, 131);
            this.txtCliTelefono.Name = "txtCliTelefono";
            this.txtCliTelefono.Size = new System.Drawing.Size(180, 29);
            this.txtCliTelefono.TabIndex = 7;
            // 
            // groupBoxEmpresa
            // 
            this.groupBoxEmpresa.Controls.Add(this.lblEmpNombre);
            this.groupBoxEmpresa.Controls.Add(this.txtEmpNombre);
            this.groupBoxEmpresa.Controls.Add(this.lblEmpDireccion);
            this.groupBoxEmpresa.Controls.Add(this.txtEmpDireccion);
            this.groupBoxEmpresa.Controls.Add(this.lblEmpMail);
            this.groupBoxEmpresa.Controls.Add(this.txtEmpMail);
            this.groupBoxEmpresa.Controls.Add(this.lblEmpTelefono);
            this.groupBoxEmpresa.Controls.Add(this.txtEmpTelefono);
            this.groupBoxEmpresa.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEmpresa.Location = new System.Drawing.Point(533, 104);
            this.groupBoxEmpresa.Name = "groupBoxEmpresa";
            this.groupBoxEmpresa.Size = new System.Drawing.Size(440, 180);
            this.groupBoxEmpresa.TabIndex = 2;
            this.groupBoxEmpresa.TabStop = false;
            this.groupBoxEmpresa.Text = "DATOS DE LA EMPRESA";
            // 
            // lblEmpNombre
            // 
            this.lblEmpNombre.AutoSize = true;
            this.lblEmpNombre.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpNombre.Location = new System.Drawing.Point(15, 30);
            this.lblEmpNombre.Name = "lblEmpNombre";
            this.lblEmpNombre.Size = new System.Drawing.Size(76, 22);
            this.lblEmpNombre.TabIndex = 0;
            this.lblEmpNombre.Text = "Nombre:";
            // 
            // txtEmpNombre
            // 
            this.txtEmpNombre.Location = new System.Drawing.Point(110, 26);
            this.txtEmpNombre.Name = "txtEmpNombre";
            this.txtEmpNombre.Size = new System.Drawing.Size(310, 29);
            this.txtEmpNombre.TabIndex = 1;
            // 
            // lblEmpDireccion
            // 
            this.lblEmpDireccion.AutoSize = true;
            this.lblEmpDireccion.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpDireccion.Location = new System.Drawing.Point(15, 65);
            this.lblEmpDireccion.Name = "lblEmpDireccion";
            this.lblEmpDireccion.Size = new System.Drawing.Size(85, 22);
            this.lblEmpDireccion.TabIndex = 2;
            this.lblEmpDireccion.Text = "Dirección:";
            // 
            // txtEmpDireccion
            // 
            this.txtEmpDireccion.Location = new System.Drawing.Point(110, 62);
            this.txtEmpDireccion.Name = "txtEmpDireccion";
            this.txtEmpDireccion.Size = new System.Drawing.Size(310, 29);
            this.txtEmpDireccion.TabIndex = 3;
            // 
            // lblEmpMail
            // 
            this.lblEmpMail.AutoSize = true;
            this.lblEmpMail.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpMail.Location = new System.Drawing.Point(15, 100);
            this.lblEmpMail.Name = "lblEmpMail";
            this.lblEmpMail.Size = new System.Drawing.Size(48, 22);
            this.lblEmpMail.TabIndex = 4;
            this.lblEmpMail.Text = "Mail:";
            // 
            // txtEmpMail
            // 
            this.txtEmpMail.Location = new System.Drawing.Point(110, 96);
            this.txtEmpMail.Name = "txtEmpMail";
            this.txtEmpMail.Size = new System.Drawing.Size(310, 29);
            this.txtEmpMail.TabIndex = 5;
            // 
            // lblEmpTelefono
            // 
            this.lblEmpTelefono.AutoSize = true;
            this.lblEmpTelefono.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpTelefono.Location = new System.Drawing.Point(15, 135);
            this.lblEmpTelefono.Name = "lblEmpTelefono";
            this.lblEmpTelefono.Size = new System.Drawing.Size(79, 22);
            this.lblEmpTelefono.TabIndex = 6;
            this.lblEmpTelefono.Text = "Teléfono:";
            // 
            // txtEmpTelefono
            // 
            this.txtEmpTelefono.Location = new System.Drawing.Point(110, 131);
            this.txtEmpTelefono.Name = "txtEmpTelefono";
            this.txtEmpTelefono.Size = new System.Drawing.Size(180, 29);
            this.txtEmpTelefono.TabIndex = 7;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(36, 328);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(56, 22);
            this.lblFecha.TabIndex = 3;
            this.lblFecha.Text = "Fecha:";
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.AutoSize = true;
            this.lblFormaPago.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.Location = new System.Drawing.Point(360, 328);
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.Size = new System.Drawing.Size(124, 22);
            this.lblFormaPago.TabIndex = 6;
            this.lblFormaPago.Text = "Forma de pago:";
            // 
            // lblSubtotalText
            // 
            this.lblSubtotalText.AutoSize = true;
            this.lblSubtotalText.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalText.Location = new System.Drawing.Point(783, 463);
            this.lblSubtotalText.Name = "lblSubtotalText";
            this.lblSubtotalText.Size = new System.Drawing.Size(82, 22);
            this.lblSubtotalText.TabIndex = 9;
            this.lblSubtotalText.Text = "Subtotal :";
            // 
            // lblIvaText
            // 
            this.lblIvaText.AutoSize = true;
            this.lblIvaText.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIvaText.Location = new System.Drawing.Point(783, 506);
            this.lblIvaText.Name = "lblIvaText";
            this.lblIvaText.Size = new System.Drawing.Size(84, 22);
            this.lblIvaText.TabIndex = 11;
            this.lblIvaText.Text = "IVA 13 % :";
            // 
            // lblTotalText
            // 
            this.lblTotalText.AutoSize = true;
            this.lblTotalText.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalText.Location = new System.Drawing.Point(783, 550);
            this.lblTotalText.Name = "lblTotalText";
            this.lblTotalText.Size = new System.Drawing.Size(57, 22);
            this.lblTotalText.TabIndex = 15;
            this.lblTotalText.Text = "Total :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(40, 388);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(706, 279);
            this.dataGridView1.TabIndex = 17;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(494, 325);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 26);
            this.comboBox1.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(165)))), ((int)(((byte)(190)))));
            this.panel2.Controls.Add(this.btnCerrarSesion);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 48);
            this.panel2.TabIndex = 24;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCerrarSesion.Image = global::Proyecto_POSFerreteria.Properties.Resources.cerrar_sesion;
            this.btnCerrarSesion.Location = new System.Drawing.Point(957, 0);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(43, 48);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lblSudTotal
            // 
            this.lblSudTotal.AutoSize = true;
            this.lblSudTotal.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSudTotal.Location = new System.Drawing.Point(877, 463);
            this.lblSudTotal.Name = "lblSudTotal";
            this.lblSudTotal.Size = new System.Drawing.Size(49, 20);
            this.lblSudTotal.TabIndex = 25;
            this.lblSudTotal.Text = "$ 0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(430, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 26);
            this.label4.TabIndex = 28;
            this.label4.Text = "FACTURA";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(98, 326);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(236, 25);
            this.dateTimePicker1.TabIndex = 29;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(877, 552);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(49, 20);
            this.lblTotal.TabIndex = 30;
            this.lblTotal.Text = "$ 0.00";
            // 
            // lblCobro
            // 
            this.lblCobro.AutoSize = true;
            this.lblCobro.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCobro.Location = new System.Drawing.Point(877, 508);
            this.lblCobro.Name = "lblCobro";
            this.lblCobro.Size = new System.Drawing.Size(49, 20);
            this.lblCobro.TabIndex = 31;
            this.lblCobro.Text = "$ 0.00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(734, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 33);
            this.button1.TabIndex = 32;
            this.button1.Text = "IMPRIMIR";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(847, 320);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 33);
            this.button2.TabIndex = 33;
            this.button2.Text = "CANCELAR";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FrmFactura1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCobro);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSudTotal);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBoxCliente);
            this.Controls.Add(this.groupBoxEmpresa);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.lblSubtotalText);
            this.Controls.Add(this.lblIvaText);
            this.Controls.Add(this.lblTotalText);
            this.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmFactura1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFactura - Factura";
            this.groupBoxCliente.ResumeLayout(false);
            this.groupBoxCliente.PerformLayout();
            this.groupBoxEmpresa.ResumeLayout(false);
            this.groupBoxEmpresa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBoxCliente;
        private System.Windows.Forms.Label lblCliNombre;
        private System.Windows.Forms.TextBox txtCliNombre;
        private System.Windows.Forms.Label lblCliDireccion;
        private System.Windows.Forms.TextBox txtCliDireccion;
        private System.Windows.Forms.Label lblCliMail;
        private System.Windows.Forms.TextBox txtCliMail;
        private System.Windows.Forms.Label lblCliTelefono;
        private System.Windows.Forms.TextBox txtCliTelefono;

        private System.Windows.Forms.GroupBox groupBoxEmpresa;
        private System.Windows.Forms.Label lblEmpNombre;
        private System.Windows.Forms.TextBox txtEmpNombre;
        private System.Windows.Forms.Label lblEmpDireccion;
        private System.Windows.Forms.TextBox txtEmpDireccion;
        private System.Windows.Forms.Label lblEmpMail;
        private System.Windows.Forms.TextBox txtEmpMail;
        private System.Windows.Forms.Label lblEmpTelefono;
        private System.Windows.Forms.TextBox txtEmpTelefono;

        private System.Windows.Forms.Label lblFecha;

        private System.Windows.Forms.Label lblFormaPago;

        private System.Windows.Forms.Label lblSubtotalText;
        private System.Windows.Forms.Label lblIvaText;
        private System.Windows.Forms.Label lblTotalText;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblSudTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCobro;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
    
