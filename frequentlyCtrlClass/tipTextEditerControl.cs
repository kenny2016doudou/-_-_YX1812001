using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace frequentlyCtrlClass
{
    public partial class tipTextEditerControl : UserControl
    {
        private ArrayList allCtrlObjArray = new ArrayList();        
        private ListBox tipListBox = new ListBox();
        private string keyStr = "", tempProcStr = "";
        private bool ifMustReplaceStr = false;  

        public tipTextEditerControl(ArrayList tt_ctrlArray)
        {
            InitializeComponent();
            tipListBox.Visible = false;
            richTextBox2.Controls.Add(tipListBox);
            tipListBox.DoubleClick += new EventHandler(tipListBox_DoubleClick);
            tipListBox.VisibleChanged += new EventHandler(tipListBox_VisibleChanged);
            tipListBox.KeyPress += new KeyPressEventHandler(tipListBox_KeyPress);
            tipListBox.KeyDown += new KeyEventHandler(tipListBox_KeyDown);            
            allCtrlObjArray = tt_ctrlArray;
            tipListBox.Width = 180;
            tipListBox.Height = 98;
          
        }

        private void tipListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (keyStr.Length >= 1)
                {
                    keyStr = keyStr.Remove(keyStr.Length - 1, 1);
                    toFindPos();
                }
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {              
                if (tipListBox.SelectedIndex < 0 || tipListBox.SelectedIndex >= tipListBox.Items.Count)
                    return;
                Clipboard.SetDataObject(tipListBox.Items[tipListBox.SelectedIndex].ToString().Trim());
                tipListBox.Visible = false;
                toUpdateStr();
            }
       }

        private void tipListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                keyStr += e.KeyChar;
                toFindPos();
            } 
        }
        private void toFindPos()
        {
            bool ifgetit = false;
            for (int i = 0; i < tipListBox.Items.Count; i++)
            {
                tempProcStr= tipListBox.Items[i].ToString().ToLower();
                if (tempProcStr.Length >= keyStr.Length)
                {
                    tempProcStr = tempProcStr.Substring(0, keyStr.Length);
                    if (tempProcStr.Contains(keyStr.ToLower()))
                    {
                        if (tipListBox.SelectedIndex != i)
                            tipListBox.SelectedIndex = i;
                        ifgetit = true;
                        //tipListBox.SelectedItem = tipListBox.Items[i].ToString();
                        break;
                    }
                }
                else
                    continue;                
            }
            tipListBox.Visible = ifgetit;
        }

        private void tipListBox_VisibleChanged(object sender, EventArgs e)
        {
            if (tipListBox.Visible)
                tipListBox.Focus();
            else
            {
                keyStr = "";
                richTextBox2.Focus();
            }
        }
        private void toRefilltipListBox()
        {
            tipListBox.Items.Clear();
            if (preStr.Length <= 0)
                return;
            toFindObject();

        }
        private void toFindObject()
        {
            for (int i = 0; i < allCtrlObjArray.Count; i++)
            {
                if (((Control)allCtrlObjArray[i]).Name.CompareTo(preStr) == 0)
                {
                    PropertyInfo[] pis = ((Control)allCtrlObjArray[i]).GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);//获得对象的所有public属性 
                    if (pis != null)//如果获得了属性 
                    {
                        foreach (PropertyInfo pi in pis)//针对每一个属性进行循环 
                        {
                            tipListBox.Items.Add(pi.Name);
                        }
                    }
                    break;
                }
                else if (((Control)allCtrlObjArray[i]).Name.ToLower().Contains(preStr.ToLower()))
                {
                    tipListBox.Items.Add(((Control)allCtrlObjArray[i]).Name);
                }
            }
        }

        private void tipListBox_DoubleClick(object sender, EventArgs e)
        {
          
            if (tipListBox.SelectedIndex < 0)
                return;
            Clipboard.SetDataObject(tipListBox.Items[tipListBox.SelectedIndex].ToString().Trim());
            tipListBox.Visible = false;
            toUpdateStr();           
        }

        public void toUpdateStr()
        {
            if (preStr.Length > 0 && ifMustReplaceStr && !ifContainDot)
            {
                richTextBox2.Text = richTextBox2.Text.Remove(richTextBox2.Text.Length - preStr.Length, preStr.Length);
            }
            insert();
        }
        private void insert()
        {            
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                richTextBox2.Paste();
            }
        }

      
        public string Text
        {
            get
            {
                return richTextBox2.Text;
            }
            set
            {
                richTextBox2.Text = value;
                sysLineNo();
            }
        }

        public string[] Lines
        {
            get
            {
                return richTextBox2.Lines;
            }
            set
            {
                richTextBox2.Lines = value;
                sysLineNo();
            }
        }

        private void sysLineNo()
        {
            richTextBox1.Text = "";
            for (int i = 1; i <= richTextBox2.Lines.Length; i++)
                richTextBox1.Text += "行:" + i.ToString() + "\n";
            textBox2.Text = (1 + richTextBox2.GetLineFromCharIndex(richTextBox2.SelectionStart)).ToString();
            textBox1.Text = (1 + richTextBox2.SelectionStart - (richTextBox2.GetFirstCharIndexFromLine(1 + richTextBox2.GetLineFromCharIndex(richTextBox2.SelectionStart) - 1))).ToString();
        }

        private int lastLineCount = 0;
        private bool ifContainDot = false;
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
          /*  ifMustReplaceStr = true;
            if (lastLineCount != richTextBox2.Lines.Length)
            {
                sysLineNo();
            }
            lastLineCount = richTextBox2.Lines.Length;
            lineIndex = richTextBox2.GetLineFromCharIndex(richTextBox2.SelectionStart);
            if (richTextBox2.Lines.Length >= 1)
                lineStr = richTextBox2.Lines[lineIndex];
            textBox2.Text = (1 + lineIndex).ToString();
            textBox1.Text = (1 + richTextBox2.SelectionStart - (richTextBox2.GetFirstCharIndexFromLine(1 + richTextBox2.GetLineFromCharIndex(richTextBox2.SelectionStart) - 1))).ToString();
            ttpt = richTextBox2.GetPositionFromCharIndex(richTextBox2.SelectionStart);
            preStr = lineStr;
            ifContainDot = false;
            for (int i = lineStr.Length - 1; i > 0; i--)
            {
                if (lineStr[i] == '.')
                {
                    ifContainDot = true;
                    break;
                }
            }
            if (ifContainDot)
            {
                for (int i = lineStr.Length - 1; i > 0; i--)
                {
                    if (lineStr[lineStr.Length - 1] == '.')
                    {
                        preStr = lineStr.Substring(0, lineStr.Length - 1);
                        for (int ni = i - 1; ni > 0; ni--)
                        {
                            if (lineStr[ni] == ' ')
                            {
                                ifMustReplaceStr = false;
                                preStartIndex = ni;
                                preStr = lineStr.Substring(ni, lineStr.Length - ni);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                for (int i = lineStr.Length - 1; i > 0; i--)
                {
                    if (lineStr[i] == ' ')
                    {
                        preStartIndex = i;
                        preStr = lineStr.Substring(i, lineStr.Length - i);
                        break;
                    }
                }
            }
            if (preStr.Length > 0)
            {
                toRefilltipListBox();
                if (tipListBox.Items.Count > 0)
                {
                    //tipListBox.Items.Add(preStr);
                    ttpt.X -= 20;
                    if (ttpt.X < 0)
                        ttpt.X = 0;
                    ttpt.Y += 20;
                    tipListBox.Location = ttpt;
                    tipListBox.Visible = true;
                }
            }*/
        }

        private Point ttpt = new Point();
        private string lineStr="", preStr = "";
        private int lineIndex = 0, preStartIndex = 0;

        private void richTextBox2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void richTextBox2_Click(object sender, EventArgs e)
        {
            tipListBox.Visible = false;
        }

        private void 插入撒的发生的ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject("流口水的似懂非懂看来");
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                textEditorControl1.ActiveTextAreaControl.TextArea.InsertString("");
                textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
                string str = Clipboard.GetText();
                textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(str);
            }
        }

        private void 插入444ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(DateTime.Now.ToString());
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                textEditorControl1.ActiveTextAreaControl.TextArea.InsertString("");
                textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
                string str = Clipboard.GetText();
                textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(str);
            }
        }

    }
}


/*//复制选中文字
      string str = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText;
           Clipboard.SetText(str);
            //剪下选中文字
            string str = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText;
            //
            Clipboard.SetText(str);
            //要先插入空字串, 以定位,否则, 位置会跑掉
            textEditorControl1.ActiveTextAreaControl.TextArea.InsertString("");
            textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            //粘贴替换选中文字
            string str = Clipboard.GetText();
            textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(str);
            //
            if (textEditorControl1.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
                textEditorControl1.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            //
*/