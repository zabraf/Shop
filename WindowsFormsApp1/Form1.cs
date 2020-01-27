using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Store _store;
        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            _store = new Store(ClientRectangle.Width,ClientRectangle.Height);
            tmrGameTime.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.FromArgb(255,204,153);
            _store.Draw(sender, e.Graphics);
        }

        private void tmrGameTime_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void tmrSecond_Tick(object sender, EventArgs e)
        {

        }
    }
}
