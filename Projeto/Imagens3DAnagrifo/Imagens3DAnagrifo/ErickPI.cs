using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Reflection;
using Emgu.CV.GPU;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Imagens3DAnagrifo
{
    public delegate void AlterarVisorPrincipal(int id);


    class Camera
    {
        private Capture camera;
        private ImageViewer visualizador;

        public virtual ImageViewer Visualizador
        {
            get { return visualizador; }
            set { visualizador = value; }
        }
        private int id;
        private Boolean cameraAberta;
        private Boolean visualizadorAberto;

        private Thread cicloDeCapitura;
        private Thread AbrirVisor;

        private int cicloTempoIntervalo;
        private StringBuilder alertaErro;
        private Image<Bgr, Byte> imagem;



        public virtual Image<Bgr, Byte> Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        private Point visualizadorPosicaoInicial;

        private AlterarVisorPrincipal ModoVisorPrincipal;

        public Camera(int id, int cicloTempoCapitura = 33, int visualizadorPI = 0, int visualizadorPF = 0, AlterarVisorPrincipal ModoVisorPrincipal = null)
        {
            this.id = id;
            this.cicloTempoIntervalo = cicloTempoCapitura;
            this.cameraAberta = false;
            this.visualizadorAberto = false;
            this.alertaErro = new StringBuilder();
            this.visualizadorPosicaoInicial = new Point(visualizadorPI, visualizadorPF);
            this.ModoVisorPrincipal = ModoVisorPrincipal;

        }
        public void AbrirCamera()
        {
            this.cameraAberta = true;
            this.cicloDeCapitura = new Thread(CapiturarImagens);
            this.cicloDeCapitura.Start();
        }
        public void FecharCamera()
        {
            this.cameraAberta = false;
        }
        public void IniciarGravacao()
        {

        }
        public void FinalizarGravacao()
        {

        }
        public void AbrirVisualizador()
        {
            this.visualizadorAberto = true;
        }
        public void FecharVisualizador()
        {
            this.visualizadorAberto = false;
        }

        private void CapiturarImagens()
        {
            try
            {
                CvInvoke.cvCreateCameraCapture(this.id);

                if (this.camera == null)
                    this.camera = new Capture(this.id);
                else
                {
                    this.camera.Dispose();
                    this.camera = new Capture(this.id);
                }

            }
            catch (Exception ex)
            {
                this.alertaErro.Append("Erro CapituraImagens: Não foi possivel encontrar a camera " + this.id.ToString());
                this.alertaErro.Append("\nMensagem:" + ex.Message);
                this.cameraAberta = false;
                AlertaErro();
            }

            while (this.cameraAberta)
            {
                try
                {
                    Thread.Sleep(this.cicloTempoIntervalo);
                    this.imagem = this.camera.QueryFrame();
                    //this.imagem.Resize(800, 600, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                    if (this.visualizadorAberto)
                    {
                        if ((this.visualizador != null) && (this.AbrirVisor != null) && (this.AbrirVisor.ThreadState == ThreadState.Running))
                        {
                            if (this.visualizador.Visible)
                            {
                                this.visualizador.Image = new Image<Bgr, byte>(FiltarCanal());

                            }
                        }
                        else
                        {
                            if (this.AbrirVisor == null)
                            {
                                this.AbrirVisor = new Thread(delegate() { CarregarVisualizador(); });
                                this.AbrirVisor.Start();
                            }
                            else
                                if (this.AbrirVisor.ThreadState != ThreadState.Running)
                                {
                                    this.AbrirVisor = new Thread(delegate() { CarregarVisualizador(); });
                                    this.AbrirVisor.Start();
                                }
                        }
                    }
                    else
                    {
                        if ((this.AbrirVisor != null) && (this.AbrirVisor.ThreadState == ThreadState.Running))
                        {
                            this.AbrirVisor.Abort();
                        }
                        else
                            if ((this.AbrirVisor != null) && (this.AbrirVisor.ThreadState == ThreadState.AbortRequested))
                            {
                                this.AbrirVisor = null;
                            }
                    }
                }
                catch (Exception ex)
                {
                    this.alertaErro.Append("Erro CapituraImagens: A camera " + this.id.ToString() + " esta aberta mas houve uma falha na capitura da imagem.");
                    this.alertaErro.Append("\nMensagem: " + ex.Message);
                    // this.cameraAberta = false;
                    // AlertaErro();
                }

            }

        }
        private void CarregarVisualizador()
        {
            this.visualizador = new ImageViewer();
            this.visualizador.StartPosition = FormStartPosition.Manual;
            this.visualizador.Location = this.visualizadorPosicaoInicial;
            // this.visualizador.AutoSize = true;
            //this.visualizador.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.visualizador.Text = "Camera " + (this.id + 1).ToString();
            this.visualizador.ShowInTaskbar = false;
            this.visualizador.ShowIcon = false;
            this.visualizador.FormClosing += delegate { this.visualizadorPosicaoInicial = this.visualizador.Location; this.ModoVisorPrincipal(-1); };
            //this.visualizador.SizeGripStyle = SizeGripStyle.Show;


            //this.visualizador.ImageBox.BackgroundImageLayout = ImageLayout.Stretch;
            //this.visualizador.ImageBox.Size = new Size(800, 600);
            //this.visualizador.ImageBox.Dock = DockStyle.Fill;
            this.visualizador.Refresh();
            this.visualizador.Size = this.imagem.Size;
            this.visualizador.MaximumSize = this.imagem.Size;
            this.visualizador.MinimumSize = this.imagem.Size;
            this.visualizador.ImageBox.Enabled = false;
            this.visualizador.Click += delegate { this.ModoVisorPrincipal(this.id); };
            this.visualizador.TopMost = true;
            this.visualizador.ShowDialog();
            this.visualizador.Dispose();

        }
        private void AlertaErro()
        {
            if (this.alertaErro.ToString().Length > 0)
            {
                MessageBox.Show(this.alertaErro.ToString(), "Camera " + this.id.ToString() + " Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.alertaErro.Clear();
            }
        }
        public Point PosicaoVisualizador
        {
            get { return this.visualizadorPosicaoInicial; }
            set
            {
                this.visualizadorPosicaoInicial = value;
                if (this.visualizador != null)
                    this.visualizador.Location = this.visualizadorPosicaoInicial;
            }
        }
        public Image<Gray, byte>[] FiltarCanal()
        {
            try
            {
                if (this.imagem != null)
                {
                    if ((this.id % 2) == 0)
                    {
                        return new Image<Gray, byte>[] { new Image<Gray, byte>(this.imagem.Width, this.imagem.Height), new Image<Gray, byte>(this.imagem.Width, this.imagem.Height), this.imagem[2] };

                    }
                    else
                    {
                        return new Image<Gray, byte>[] { this.imagem[0], this.imagem[1], new Image<Gray, byte>(this.imagem.Width, this.imagem.Height) };
                    }
                }
                else
                    return new Image<Gray, byte>[] { new Image<Gray, byte>(800, 600), new Image<Gray, byte>(800, 600), new Image<Gray, byte>(800, 600) };
            }
            catch
            {
                return new Image<Gray, byte>[] { new Image<Gray, byte>(800, 600), new Image<Gray, byte>(800, 600), new Image<Gray, byte>(800, 600) };
            }
        }
    }

    class Camera3D
    {
        Camera camera1;

        private Boolean calibragem = false;
        public Boolean Calibragem
        {
            get { return this.calibragem; }
            set { this.calibragem = value; }
        }
        private Boolean gravar = false;
        public Boolean Gravar
        {
            get { return this.gravar; }
            set { this.gravar = value; }
        }
        private VideoWriter video;
        Image<Bgr, byte> imagem;
        public Camera Camera1
        {
            get { return camera1; }
            set { camera1 = value; }
        }
        Camera camera2;

        public Camera Camera2
        {
            get { return camera2; }
            set { camera2 = value; }
        }

        PictureBox visor;
        Point visorPosicao;

        Thread exibicao3D;

        private Boolean exibir3D;

        public Boolean Modo3D
        {
            get { return exibir3D; }
        }
        private Boolean exibirCamera1;
        private Boolean exibirCamera2;
        private Boolean transmitindo;
        
        public Camera3D(PictureBox visor, Point visorPosicao)
        {
            this.visor = visor;
            this.visorPosicao = visorPosicao;
            this.camera1 = new Camera(0, 33, this.visorPosicao.X, this.visorPosicao.Y, TransmitirModo);
            this.camera2 = new Camera(1, 33, this.visorPosicao.X, this.visorPosicao.Y, TransmitirModo);
            this.exibir3D = false;
            this.exibirCamera1 = false;
            this.exibirCamera2 = false;
            this.transmitindo = false;
        }
        public void IniciarTransmissao()
        {
            this.camera1.AbrirCamera();
            this.camera2.AbrirCamera();
            TransmitirModo();
            this.transmitindo = true;
            this.exibicao3D = new Thread(FundirImagens);
            this.exibicao3D.Start();

        }
        public void PararTransmissao()
        {
            this.transmitindo = false;
            this.exibir3D = false;
            this.exibirCamera1 = false;
            this.exibirCamera2 = false;
            this.transmitindo = false;
            this.camera1.FecharCamera();
            this.camera2.FecharCamera();
        }
        private void TransmitirModo(int id = -1)
        {
            if (id == -1)
            {
                this.visor.Tag = "EGITA - Câmera 3D";            
                this.exibir3D = true;
                exibirCamera1 = false;
                exibirCamera2 = false;
                
            }
            else
                if (id == 0)
                {
                    this.visor.Tag = "EGITA - Câmera 1";
                    this.exibir3D = false;
                    exibirCamera1 = true;
                    exibirCamera2 = false;
                    
                }
                else
                    if (id == 1)
                    {
                        this.visor.Tag = "EGITA - Câmera 2";
                        this.exibir3D = false;
                        exibirCamera1 = false;
                        exibirCamera2 = true;
                        
                    }
            
        }
        public void Transmitir3D()
        {
            TransmitirModo();
        }
        private void AlterarNomeForme()
        {

        }
        private void FundirImagens()
        {
            while (this.transmitindo)
            {
                try
                {
                    Thread.Sleep(33);

                    if (exibir3D)
                    {
                        Image<Gray, byte>[] camera1 = this.camera1.FiltarCanal();
                        Image<Gray, byte>[] camera2 = this.camera2.FiltarCanal();
                        this.imagem = new Image<Bgr, byte>(new Image<Gray, byte>[] { camera2[0], camera2[1], camera1[2] });
                        
                        if (this.gravar)
                        {
                            if (video == null)
                                this.video = new VideoWriter(Application.StartupPath + "\\Erick_video3D.avi",0, 30, this.imagem.Width, this.imagem.Height, true);
                            video.WriteFrame<Bgr, byte>(this.imagem);
                        }
                    }
                    if (exibirCamera1)
                    {
                        if (this.calibragem)
                        {
                            this.imagem = camera1.Imagem;
                        }
                        else
                        {
                            this.imagem = new Image<Bgr, byte>(this.camera1.FiltarCanal());
                        }
                    }
                    if (exibirCamera2)
                    {
                        if (this.calibragem)
                        {
                            this.imagem = camera2.Imagem;
                        }
                        else
                        {
                            this.imagem = new Image<Bgr, byte>(this.camera2.FiltarCanal());
                        }
                    }
                    this.visor.Image = this.imagem.Bitmap;
                }
                catch
                {
                    // FecharCamera();
                }
            }
        }
        public void FecharCamera()
        {
            PararTransmissao();
            this.camera1.FecharVisualizador();
            this.camera1.FecharCamera();
            this.camera2.FecharVisualizador();
            this.camera2.FecharCamera();
        }
    }
}
