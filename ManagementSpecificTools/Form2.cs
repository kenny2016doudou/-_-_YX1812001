using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagementSpecificTools
{
    public partial class Form2 : Form
    {
        private object _DataValue;
        public object DataValue
        {
            get
            {
                return _DataValue;
            }
            set
            {
                _DataValue = value.ToString();
                textBox1.Text = value.ToString();
            }
        }

        public string DataType
        {
            get;
            set;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            switch (DataType)
            {
                case "Bit":
                    switch (textBox1.Text.Trim())
                    {
                        case "1":
                            _DataValue = true;
                            break;
                        case "0":
                            _DataValue = false;
                            break;
                        default:
                            bool tempbool = false;
                            if (bool.TryParse(textBox1.Text.Trim(), out tempbool))
                            {
                                _DataValue = tempbool;
                            }
                            else
                            {
                                _DataValue = false;
                            }
                            break;
                    }
                    break;
                default:
                    if (!isNumberic(textBox1.Text))
                    {
                        MessageBox.Show("请输入数值");
                        return;
                    }
                    switch (DataType)
                    {
                        case "Byte":
                            Byte tempByte = 0;
                            if (Byte.TryParse(textBox1.Text.Trim(), out tempByte))
                            {
                                _DataValue = tempByte;
                            }
                            else
                            {
                                _DataValue = 0;
                            }
                            break;
                        case "Word":
                            Int16 tempInt16 = 0;
                            if (Int16.TryParse(textBox1.Text.Trim(), out tempInt16))
                            {
                                _DataValue = tempInt16;
                            }
                            else
                            {
                                _DataValue = 0;
                            }
                            break;
                        case "DWord":
                            float tempfloat = 0;
                            if (float.TryParse(textBox1.Text.Trim(), out tempfloat))
                            {
                                _DataValue = tempfloat;
                            }
                            else
                            {
                                _DataValue = 0;
                            }
                            break;
                        case "Real":
                            float tempfloat2 = 0;
                            if (float.TryParse(textBox1.Text.Trim(), out tempfloat2))
                            {
                                _DataValue = tempfloat2;
                            }
                            else
                            {
                                _DataValue = 0;
                            }
                            break;
                    }
                    break;
            }
            this.DialogResult = DialogResult.OK;
        }


        private bool isNumberic(string message)
        {
            if (message == "")
            {
                return false;
            }
            else
            {
                System.Text.RegularExpressions.Regex m_regex = new System.Text.RegularExpressions.Regex("^(-?[0-9]*[.]*[0-9]{0,5})$");
                return m_regex.IsMatch(message);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
