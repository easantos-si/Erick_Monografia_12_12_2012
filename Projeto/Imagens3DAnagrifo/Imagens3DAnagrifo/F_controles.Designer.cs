namespace Imagens3DAnagrifo
{
    partial class F_controles
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">verdade se for necessário descartar os recursos gerenciados; caso contrário, falso.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.B_transmissor = new System.Windows.Forms.Button();
            this.b_camera1 = new System.Windows.Forms.Button();
            this.b_camera2 = new System.Windows.Forms.Button();
            this.B_calibragem = new System.Windows.Forms.Button();
            this.B_gravar = new System.Windows.Forms.Button();
            this.B_Fechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // B_transmissor
            // 
            this.B_transmissor.Dock = System.Windows.Forms.DockStyle.Left;
            this.B_transmissor.Location = new System.Drawing.Point(0, 0);
            this.B_transmissor.Name = "B_transmissor";
            this.B_transmissor.Size = new System.Drawing.Size(75, 53);
            this.B_transmissor.TabIndex = 0;
            this.B_transmissor.TabStop = false;
            this.B_transmissor.Text = "Transmitir";
            this.B_transmissor.UseVisualStyleBackColor = true;
            this.B_transmissor.Click += new System.EventHandler(this.B_transmissor_Click);
            // 
            // b_camera1
            // 
            this.b_camera1.Dock = System.Windows.Forms.DockStyle.Left;
            this.b_camera1.Enabled = false;
            this.b_camera1.Location = new System.Drawing.Point(75, 0);
            this.b_camera1.Name = "b_camera1";
            this.b_camera1.Size = new System.Drawing.Size(99, 53);
            this.b_camera1.TabIndex = 6;
            this.b_camera1.TabStop = false;
            this.b_camera1.Text = "Mostrar câmera 1";
            this.b_camera1.UseVisualStyleBackColor = true;
            this.b_camera1.Click += new System.EventHandler(this.b_camera1_Click);
            // 
            // b_camera2
            // 
            this.b_camera2.Dock = System.Windows.Forms.DockStyle.Left;
            this.b_camera2.Enabled = false;
            this.b_camera2.Location = new System.Drawing.Point(174, 0);
            this.b_camera2.Name = "b_camera2";
            this.b_camera2.Size = new System.Drawing.Size(99, 53);
            this.b_camera2.TabIndex = 7;
            this.b_camera2.TabStop = false;
            this.b_camera2.Text = "Mostrar cêmera 2";
            this.b_camera2.UseVisualStyleBackColor = true;
            this.b_camera2.Click += new System.EventHandler(this.b_camera2_Click);
            // 
            // B_calibragem
            // 
            this.B_calibragem.Dock = System.Windows.Forms.DockStyle.Left;
            this.B_calibragem.Enabled = false;
            this.B_calibragem.Location = new System.Drawing.Point(273, 0);
            this.B_calibragem.Name = "B_calibragem";
            this.B_calibragem.Size = new System.Drawing.Size(99, 53);
            this.B_calibragem.TabIndex = 9;
            this.B_calibragem.TabStop = false;
            this.B_calibragem.Text = "Modo 3D";
            this.B_calibragem.UseVisualStyleBackColor = true;
            this.B_calibragem.Click += new System.EventHandler(this.B_calibragem_Click);
            // 
            // B_gravar
            // 
            this.B_gravar.Dock = System.Windows.Forms.DockStyle.Left;
            this.B_gravar.Enabled = false;
            this.B_gravar.Location = new System.Drawing.Point(372, 0);
            this.B_gravar.Name = "B_gravar";
            this.B_gravar.Size = new System.Drawing.Size(99, 53);
            this.B_gravar.TabIndex = 11;
            this.B_gravar.TabStop = false;
            this.B_gravar.Text = "Iniciar Gravação";
            this.B_gravar.UseVisualStyleBackColor = true;
            this.B_gravar.Click += new System.EventHandler(this.B_gravar_Click);
            // 
            // B_Fechar
            // 
            this.B_Fechar.Dock = System.Windows.Forms.DockStyle.Left;
            this.B_Fechar.Location = new System.Drawing.Point(471, 0);
            this.B_Fechar.Name = "B_Fechar";
            this.B_Fechar.Size = new System.Drawing.Size(99, 53);
            this.B_Fechar.TabIndex = 12;
            this.B_Fechar.TabStop = false;
            this.B_Fechar.Text = "Fechar";
            this.B_Fechar.UseVisualStyleBackColor = true;
            this.B_Fechar.Click += new System.EventHandler(this.B_Fechar_Click);
            // 
            // F_controles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(571, 53);
            this.Controls.Add(this.B_Fechar);
            this.Controls.Add(this.B_gravar);
            this.Controls.Add(this.B_calibragem);
            this.Controls.Add(this.b_camera2);
            this.Controls.Add(this.b_camera1);
            this.Controls.Add(this.B_transmissor);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_controles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EGITA - Erick Andrade dos Santos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Enter += new System.EventHandler(this.F_controles_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button B_transmissor;
        private System.Windows.Forms.Button b_camera1;
        private System.Windows.Forms.Button b_camera2;
        private System.Windows.Forms.Button B_calibragem;
        private System.Windows.Forms.Button B_gravar;
        private System.Windows.Forms.Button B_Fechar;
    }
}

