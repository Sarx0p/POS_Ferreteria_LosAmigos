namespace Proyecto_POSFerreteria.Presentacion
{
    partial class FrmUsuarios
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCrear = new System.Windows.Forms.Button();
            this.txtModificar = new System.Windows.Forms.Button();
            this.txtEliminar = new System.Windows.Forms.Button();
            this.txtVolver = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(36, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(98, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(566, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre Completo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuario:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(339, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Telefono:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(339, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Correo:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(339, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 22);
            this.label6.TabIndex = 5;
            this.label6.Text = "Contraseña:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtCrear
            // 
            this.txtCrear.Location = new System.Drawing.Point(658, 227);
            this.txtCrear.Name = "txtCrear";
            this.txtCrear.Size = new System.Drawing.Size(110, 31);
            this.txtCrear.TabIndex = 2;
            this.txtCrear.Text = "Crear";
            this.txtCrear.UseVisualStyleBackColor = true;
            // 
            // txtModificar
            // 
            this.txtModificar.Location = new System.Drawing.Point(658, 271);
            this.txtModificar.Name = "txtModificar";
            this.txtModificar.Size = new System.Drawing.Size(110, 31);
            this.txtModificar.TabIndex = 3;
            this.txtModificar.Text = "Modificar";
            this.txtModificar.UseVisualStyleBackColor = true;
            // 
            // txtEliminar
            // 
            this.txtEliminar.Location = new System.Drawing.Point(658, 313);
            this.txtEliminar.Name = "txtEliminar";
            this.txtEliminar.Size = new System.Drawing.Size(110, 31);
            this.txtEliminar.TabIndex = 4;
            this.txtEliminar.Text = "Eliminar";
            this.txtEliminar.UseVisualStyleBackColor = true;
            // 
            // txtVolver
            // 
            this.txtVolver.Location = new System.Drawing.Point(658, 353);
            this.txtVolver.Name = "txtVolver";
            this.txtVolver.Size = new System.Drawing.Size(110, 31);
            this.txtVolver.TabIndex = 5;
            this.txtVolver.Text = "Volver";
            this.txtVolver.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 20);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(147, 92);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(168, 20);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(147, 129);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(168, 20);
            this.textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(430, 128);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(134, 20);
            this.textBox4.TabIndex = 9;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(415, 86);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(168, 20);
            this.textBox5.TabIndex = 10;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(415, 50);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(168, 20);
            this.textBox6.TabIndex = 11;
            // 
            // FrmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtVolver);
            this.Controls.Add(this.txtEliminar);
            this.Controls.Add(this.txtModificar);
            this.Controls.Add(this.txtCrear);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmUsuarios";
            this.Text = "FrmUsuarios";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button txtCrear;
        private System.Windows.Forms.Button txtModificar;
        private System.Windows.Forms.Button txtEliminar;
        private System.Windows.Forms.Button txtVolver;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox6;
    }
}