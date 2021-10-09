using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace frequentlyCtrlClass
{
    public partial class yxLedRoundtd : UserControl
    {
        private bool myState = false;

        public yxLedRoundtd()
        {
            InitializeComponent();
        }

        public bool Active
        {
            get
            {
                return myState;
            }
            set
            {
                myState = value;
                if (myState)
                {
                    BackColor = Color.Lime;
                }
                else
                {
                    BackColor = Color.Red;
                }
            }
        }

        private void yxLedRound_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, ((UserControl)sender).Width, ((UserControl)sender).Height);
            ((UserControl)sender).Region = new Region(path);
        }

        

    }


}
