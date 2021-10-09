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
    public partial class yxLedRect_button : UserControl
    {

        public delegate void UserClickDelegate(string name,string value);
        public event  UserClickDelegate myUserClickDelegate;

        private bool myState = false;
        private bool _showColor = true;
        private string smStr = "注释说明";        
        private float _fontsize = 9;
        public bool _clickValue = false;

        public yxLedRect_button()
        {
            InitializeComponent();
        }


        public float fontsize
        {
            get
            {
                return _fontsize;
            }
            set
            {
                _fontsize = value;
                glassButton1.Font = new Font(glassButton1.Font.FontFamily, _fontsize);               
            }
        }

       


        public string str_注释说明
        {
            get
            {
                return glassButton1.Text;
            }
            set
            {
                glassButton1.Text = value.Trim();
                toolTip1.ToolTipTitle = glassButton1.Text;
            }
        }

        public bool Active
        {
            get
            {
                return _clickValue;
            }
            set
            {
                _clickValue = value;
                if (_clickValue)
                {
                    glassButton1.BackColor = Color.Lime;
                }
                else
                {
                    glassButton1.BackColor = Color.Gray;
                }
            }
        }

        public bool ShowColor
        {
            get
            {
                return _showColor;
            }
            set
            {
                _showColor = value; 
            }
        }

        private void yxLedRect_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            _clickValue = !_clickValue;
            if (_clickValue)
            {
                glassButton1.BackColor = Color.Lime;
            }
            else
            {
                glassButton1.BackColor = Color.Gray;
            }
            if (myUserClickDelegate != null)
            {
                myUserClickDelegate(str_注释说明, _clickValue.ToString());
            }
        }
    }

}
