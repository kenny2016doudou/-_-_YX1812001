using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace usefulClass
{
    public class DirectPrintClass
    {
        public static bool ifDirect = false;
        public static string querytype = "";
        public static string queryStr = "";
    }
    [Serializable]
    public class xhdyClass
    {
        public string xhnames = "";
        public string xhtypes = "";
        public string xmcell01 = "合闸时间";
        public float xmcell01_maxValue = 60;


        public string xmcell02 = "合闸速度";
        public float xmcell02_minValue = 6.8f, xmcell02_maxValue = 8.0f;

        public string xmcell03 = "分闸时间";
        public float xmcell03_minValue = 0, xmcell03_maxValue = 60;


        public string xmcell04 = "分闸速度";
        public float xmcell04_minValue = 6.8f, xmcell04_maxValue = 8.0f;


        public string xmcell05 = "超程时间";
        public float xmcell05_maxValue = 15;


        public string xmcell06 = "弹跳时间";
        public float xmcell06_maxValue = 15;


        public string xmcell07 = "电磁得电时间";
        public float xmcell07_minValue = 575, xmcell07_maxValue = 650;

        public string xmcell08 = "辅助联锁反馈时间";
        public float xmcell08_maxValue = 200;


        public fzcdCellClass[] hzfzcd_checkArray = new fzcdCellClass[15];
        public fzcdCellClass[] hzfzcd_checkArray_String = new fzcdCellClass[15];
        public xhdyClass()
        {
            init();
        }
        public void init()
        {

           
            if (hzfzcd_checkArray_String == null || hzfzcd_checkArray_String.Length != 15)
                hzfzcd_checkArray_String = new fzcdCellClass[15];

            for (int i = 0; i < hzfzcd_checkArray_String.Length; i++)
            {
                if (hzfzcd_checkArray_String[i] == null)
                    hzfzcd_checkArray_String[i] = new fzcdCellClass();
            }


            /*if (hzfzcd_checkArray == null || hzfzcd_checkArray.Length != 15)
                hzfzcd_checkArray = new fzcdCellClass[15];

            for (int i = 0; i < hzfzcd_checkArray.Length; i++)
            {
                if (hzfzcd_checkArray[i] == null)
                  hzfzcd_checkArray[i] = new fzcdCellClass();
            }*/
        }
        public bool addfzcdCell(int index, string sIndex, string eIndex)
        {
            //if (sIndex + 1 != eIndex)
            //{
            //    MessageBox.Show("增加辅助触点单元失败: 序号不匹配!");
            //    return false;
            //}

          
            hzfzcd_checkArray[index].startzdIndex_Strion = sIndex;
            hzfzcd_checkArray[index].endzdIndex_Strion = eIndex;
            return true;
        }

        public bool ifwyTest = false;
        public bool ifttTest = false;
        public bool ifccTest = false;
        public bool ifddTest = false;
        public bool ifsdTest = false;

        public string reportName = "";
        public string dkway = "";
        public string dkInfo = "";

        public bool iffzxqTest = false;
        public bool ifhzxqTest = false;
        public bool ifzctTest = false;
        public bool ifcbfzztTest = false;
        public bool ifckfzztTest = false;
    }

    [Serializable]
    public class fzcdCellClass
    {
        public int startzdIndex = 0;
        public int endzdIndex = 0;
        public int testChannelIndex = 0;

        public string startzdIndex_Strion = "";
        public string endzdIndex_Strion = "";
        public int testChannelIndex_Strion = 0;

    }
}
