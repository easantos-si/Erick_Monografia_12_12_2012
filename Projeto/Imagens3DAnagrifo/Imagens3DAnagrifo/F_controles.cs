using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Imagens3DAnagrifo
{
    public partial class F_controles : Form
    {
        Camera3D camera3d;
        F_Imagem3D visorCentral;
        public F_controles(F_Imagem3D visorCentral)
        {
            InitializeComponent();
            // this.visorCentral = new F_Imagem3D();
            this.camera3d = new Camera3D(visorCentral.Visor, this.Location);
            this.visorCentral = visorCentral;
        }
        private void B_transmissor_Click(object sender, EventArgs e)
        {
            B_transmissor.Enabled = false;
            if (B_transmissor.Text == "Câmera 3D")
            {
                this.camera3d.Transmitir3D();
                B_transmissor.Text = "Parar transmissão";
                b_camera1.Enabled = true;
                b_camera2.Enabled = true;
            }
            else

                if (B_transmissor.Text == "Transmitir")
                {
                    this.camera3d.IniciarTransmissao();
                    B_transmissor.Text = "Parar transmissão";
                    b_camera1.Enabled = true;
                    b_camera2.Enabled = true;
                    B_calibragem.Enabled = true;
                    B_gravar.Enabled = true;
                }
                else
                {
                    B_transmissor.Text = "Transmitir";
                    this.camera3d.PararTransmissao();
                    b_camera1.Enabled = false;
                    b_camera2.Enabled = false;
                    B_calibragem.Enabled = false;
                    B_gravar.Enabled = false;

                }
            this.visorCentral.Visor.Refresh();
            this.visorCentral.Refresh();
            B_transmissor.Enabled = true;

        }

        private void b_camera1_Click(object sender, EventArgs e)
        {
            b_camera1.Enabled = false;
            if (b_camera1.Text == "Mostrar câmera 1")
            {
                this.camera3d.Camera1.AbrirVisualizador();
                b_camera1.Text = "Ocultar câmera 1";
                AlinharJanelaCamera1();
            }
            else
            {
                b_camera1.Text = "Mostrar câmera 1";
                this.camera3d.Camera1.FecharVisualizador();
                AlinharJanelaCamera1();
            }
            if (!this.camera3d.Modo3D)
            {
                B_transmissor.Text = "Câmera 3D";
            }
            b_camera1.Enabled = true;
        }

        private void b_camera2_Click(object sender, EventArgs e)
        {
            b_camera2.Enabled = false;
            if (b_camera2.Text == "Mostrar câmera 2")
            {
                this.camera3d.Camera2.AbrirVisualizador();
                b_camera2.Text = "Ocultar câmera 2";
                AlinharJanelaCamera2();
            }
            else
            {
                b_camera2.Text = "Mostrar câmera 2";
                this.camera3d.Camera2.FecharVisualizador();
                AlinharJanelaCamera2();
            }
            if (!this.camera3d.Modo3D)
            {
                B_transmissor.Text = "Câmera 3D";
            }
            b_camera2.Enabled = true;
        }
        private void B_calibragem_Click(object sender, EventArgs e)
        {
            B_calibragem.Enabled = false;
            if (B_calibragem.Text == "Modo 3D")
            {
                this.camera3d.Calibragem = true;
                B_calibragem.Text = "Modo Calibragem";
            }
            else
            {
                B_calibragem.Text = "Modo 3D";
                this.camera3d.Calibragem = false;
            }
            B_calibragem.Enabled = true;
        }
        private void B_gravar_Click(object sender, EventArgs e)
        {
            B_gravar.Enabled = false;
            if (B_gravar.Text == "Iniciar Gravação")
            {
                this.camera3d.Gravar = true;
                B_gravar.Text = "Parar Gravação";
            }
            else
            {
                B_gravar.Text = "Iniciar Gravação";
                this.camera3d.Gravar = false;
            }
            B_gravar.Enabled = true;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.camera3d.FecharCamera();
            Application.ExitThread();
            Application.Exit();
        }

        private void B_Fechar_Click(object sender, EventArgs e)
        {
            this.camera3d.FecharCamera();
            Application.ExitThread();
            //Application.Exit();
        }

        public void AlinharJanelaCamera1()
        {
            this.camera3d.Camera1.PosicaoVisualizador = new Point(this.Location.X - (camera3d.Camera1.Imagem.Width + 5), this.Location.Y - this.camera3d.Camera1.Imagem.Height + this.Height);
        }
        public void AlinharJanelaCamera2()
        {
            this.camera3d.Camera2.PosicaoVisualizador = new Point((this.Location.X + this.Width) + 5, this.Location.Y - this.camera3d.Camera2.Imagem.Height + this.Height);
        }

        private void F_controles_Enter(object sender, EventArgs e)
        {
            this.Focus();
        }

        

        
    }
}
