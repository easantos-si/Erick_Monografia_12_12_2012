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
    public partial class F_Imagem3D : Form
    {
        F_controles controles;
        public F_Imagem3D()
        {
            InitializeComponent();
            controles = new F_controles(this);
           
            
            //this.controles.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - (Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width / 2)+Convert.ToInt32(controles.Width/2)), Screen.PrimaryScreen.WorkingArea.Height-100);
          //  this.controles.Location = new Point(Screen.GetWorkingArea(this).Width - (Convert.ToInt32(Screen.GetWorkingArea(this).Width / 2) + Convert.ToInt32(controles.Width / 2)), Screen.GetWorkingArea(this).Height - 150);
            this.controles.Show(this);
            this.controles.Location = new Point(this.controles.Location.X, Screen.GetWorkingArea(this).Height - 80);
           // this.controles.AlinharJanelaCamera();
            this.pictureBox1.Click += delegate { this.Text = this.pictureBox1.Tag.ToString(); this.Focus(); };
        }
        public PictureBox Visor
        {
            get { return this.pictureBox1; }
            set {this.pictureBox1 = value; }
        }

        private void F_Imagem3D_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.controles.Close();
        }

        private void F_Imagem3D_Activated(object sender, EventArgs e)
        {

        }
    }
}
