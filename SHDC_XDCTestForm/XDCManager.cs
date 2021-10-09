using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestSystem.Command.PLC.Omron.HostLink.Fins;
using System.IO.Ports;
using TestSystem.Command.ControlCenter;
using ManagementSpecificTools.PlcConnectivity;

namespace SHDC_XDCTestForm
{
    public class XDCManager
    {


        public IComControl Com = null;
        public SerialPort sp = new SerialPort();
        public Dictionary<string, int> dic_显示值 = new Dictionary<string, int>();
        public Dictionary<string, int> dic_1参数值 = new Dictionary<string, int>();

        /// <summary>
        /// 欧姆龙通讯命令集合
        /// </summary>
        public string[] Omron_Command = new string[200];
        private static XDCManager uniqueInstance;
        public SiemensCommManage Com_Siemens = null;

        public static XDCManager getInstance()
        {

            if (uniqueInstance == null)
            {

                uniqueInstance = new XDCManager();
            }


            return uniqueInstance;
        }

        private XDCManager()
        {
            Com_Siemens = SiemensCommManage.getInstance();
            Com_Siemens.Start();
        }

        private string DL_Convert(string value)
        {
            try
            {
                return (float.Parse(value) / 10).ToString("0.0");
            }
            catch
            {
                return "错误";
            }

        }
        private string DL_Convert2(string value)
        {
            try
            {
                return (float.Parse(value) / 100).ToString("0.0");
            }
            catch
            {
                return "错误";
            }

        }

        private string GetDicData(string dicname, string nodename)
        {
            object[] objzt = Com.GetReaderIData(dicname);
            if (objzt != null)
            {
                Dictionary<string, string> dics = objzt[0] as Dictionary<string, string>;
                if (dics[nodename] == "0")
                {
                    return "false";
                }
                else if (dics[nodename] == "1")
                {
                    return "true";
                }
                else
                {
                    return "false";
                }

            }
            else
            {
                return "false";
                //throw new Exception();
            }
        }

        private void LoadData()
        {
            Com = ComControl_Factory.getInstance().CreateComControl(ref sp);
            string dyString = "";
            Omron_ClickDown ttcd = null;
            Omron_ClickUp ttcu = null;
            Omron_WriteData ttwd = null;
            Omron_Reader ttreader = null;

            #region 1通道数据加载
            dyString = "W2.00";
            ttcd = new Omron_ClickDown(dyString);
            ttcu = new Omron_ClickUp(dyString);
            Com.SetCommand("1#充电操作按下", ttcd);
            Com.SetCommand("1#充电操作弹起", ttcu);

            dyString = "W2.01";
            ttcd = new Omron_ClickDown(dyString);
            ttcu = new Omron_ClickUp(dyString);
            Com.SetCommand("1#放电操作按下", ttcd);
            Com.SetCommand("1#放电操作弹起", ttcu);

            dyString = "W2.02";
            ttcd = new Omron_ClickDown(dyString);
            ttcu = new Omron_ClickUp(dyString);
            Com.SetCommand("1#停止操作按下", ttcd);
            Com.SetCommand("1#停止操作弹起", ttcu);

            dyString = "W408.00";
            ttcd = new Omron_ClickDown(dyString);
            ttcu = new Omron_ClickUp(dyString);
            Com.SetCommand("1#确认操作按下", ttcd);
            Com.SetCommand("1#确认操作弹起", ttcu);


            dyString = "W400.00";
            ttcd = new Omron_ClickDown(dyString);
            ttcu = new Omron_ClickUp(dyString);
            Com.SetCommand("1#返回操作按下", ttcd);
            Com.SetCommand("1#返回操作弹起", ttcu);

            Dictionary<string, string> One_Dic_故障点 = new Dictionary<string, string>();
            One_Dic_故障点.Add("三相不平衡", "W75.01");
            One_Dic_故障点.Add("温度检测异常", "W75.02");
            One_Dic_故障点.Add("电池开路", "W75.03");
            One_Dic_故障点.Add("小于充电起始电压", "W75.04");
            One_Dic_故障点.Add("大于充电终止电压", "W75.05");
            One_Dic_故障点.Add("小于放电终止电压", "W75.06");
            One_Dic_故障点.Add("电池极性接反", "W75.07");
            One_Dic_故障点.Add("IGBT超温", "W75.08");
            One_Dic_故障点.Add("IGBT过流", "W75.09");
            One_Dic_故障点.Add("充电电流过高", "W75.10");
            One_Dic_故障点.Add("输出电流调节故障", "W75.11");
            One_Dic_故障点.Add("电容电压过高", "W75.12");
            One_Dic_故障点.Add("变流箱预充电故障", "W75.13");
            One_Dic_故障点.Add("操作有误", "W75.14");
            One_Dic_故障点.Add("电池温度过高", "W75.15");
            Omron_BatchReadStatus One_故障点 = new Omron_BatchReadStatus("W75", DataType.Bit, 2, One_Dic_故障点);
            Com.SetReader("故障点", One_故障点);

            dyString = "D401";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#充电电流设置", ttwd);
            Com.SetReader("1#充电电流设置_读取", ttreader);

            dyString = "D402";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#充电时间设置", ttwd);
            Com.SetReader("1#充电时间设置_读取", ttreader);

            dyString = "D411";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#充电起始电压设置", ttwd);
            Com.SetReader("1#充电起始电压设置_读取", ttreader);

            dyString = "D412";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#充电终止电压设置", ttwd);
            Com.SetReader("1#充电终止电压设置_读取", ttreader);


            dyString = "D404";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#放电电流设置", ttwd);
            Com.SetReader("1#放电电流设置_读取", ttreader);

            dyString = "D405";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#放电时间设置", ttwd);
            Com.SetReader("1#放电时间设置_读取", ttreader);

            dyString = "D406";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("温度报警设置", ttwd);
            Com.SetReader("温度报警设置_读取", ttreader);

            dyString = "D413";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#放电终止电压设置", ttwd);
            Com.SetReader("1#放电终止电压设置_读取", ttreader);

            dyString = "D4";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("1#充电模式设置", ttwd);
            Com.SetReader("1#充电模式设置_读取", ttreader);

            //显示参数
            dyString = "D25";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#读取充电时间", ttreader);
            dyString = "D26";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#读取放电时间", ttreader);

            dyString = "D118";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#读取充电电流", ttreader);
            dyString = "D138";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#读取电池电压", ttreader);

            dyString = "D67";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#读取放电安时数", ttreader);
            dyString = "D118";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#读取放电电流", ttreader);


            dyString = "D2";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("1#运行状态", ttreader);

            dyString = "D100";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("电池温度", ttreader);

            dyString = "D148";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("单节电池电压显示", ttreader);

            dyString = "D128";
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetReader("单节充电电流显示", ttreader);

            dyString = "W50.00";
            ttreader = new Omron_Reader(dyString, DataType.Bit, 1);
            Com.SetReader("单节试验类型_读取", ttreader);

            dyString = "W50.01";
            ttreader = new Omron_Reader(dyString, DataType.Bit, 1);
            Com.SetReader("整组试验类型_读取", ttreader);



            dyString = "D501";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj充电电流设置", ttwd);
            Com.SetReader("Pdj充电电流设置_读取", ttreader);

            dyString = "D504";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj放电电流设置", ttwd);
            Com.SetReader("Pdj放电电流设置_读取", ttreader);

            dyString = "D511";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj充电起始电压设置", ttwd);
            Com.SetReader("Pdj充电起始电压设置_读取", ttreader);

            dyString = "D512";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj充电终止电压设置", ttwd);
            Com.SetReader("Pdj充电终止电压设置_读取", ttreader);

            dyString = "D513";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj放电终止电压设置", ttwd);
            Com.SetReader("Pdj放电终止电压设置_读取", ttreader);

            dyString = "D402";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj充电时间设置", ttwd);
            Com.SetReader("Pdj充电时间设置_读取", ttreader);


            dyString = "D403";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj循环次数设置", ttwd);
            Com.SetReader("Pdj循环次数设置_读取", ttreader);

            dyString = "D405";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj放电时间设置", ttwd);
            Com.SetReader("Pdj放电时间设置_读取", ttreader);

            dyString = "D406";
            ttwd = new Omron_WriteData(dyString, DataType.Word);
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);
            Com.SetCommand("Pdj温度报警设置", ttwd);
            Com.SetReader("Pdj温度报警设置_读取", ttreader);

            dyString = "D167";           
            ttreader = new Omron_Reader(dyString, DataType.Word, 1);          
            Com.SetReader("Pdj放电安时数设置_读取", ttreader);

            #endregion
            Com.Start();
        }


        /// <summary>
        /// 1通道充电操作
        /// </summary>
        public void Set1充电操作()
        {
            Com_Siemens.ExcuteCommand_Pulse("充电启动");
            //Com.ExcuteCommand("1#充电操作按下");
            //Com.ExcuteCommand("1#充电操作弹起");
        }

        /// <summary>
        /// 1通道放电操作
        /// </summary>
        public void Set1放电操作()
        {
            Com_Siemens.ExcuteCommand_Pulse("放电启动");
            //Com.ExcuteCommand("1#放电操作按下");
            //Com.ExcuteCommand("1#放电操作弹起");
        }

        /// <summary>
        /// 1通道确认操作
        /// </summary>
        public void Set1确认操作()
        {
            //Com.ExcuteCommand("1#确认操作按下");
            //Com.ExcuteCommand("1#确认操作弹起");
        }

        /// <summary>
        /// 1通道停止操作
        /// </summary>
        public void Set1停止操作()
        {
            Com_Siemens.ExcuteCommand_Pulse("停止");
            //Com.ExcuteCommand("1#停止操作按下");
            //Com.ExcuteCommand("1#停止操作弹起");
        }

        /// <summary>
        /// 读取和设置 1通道充电电流设置值
        /// </summary>
        public string P1_充电电流设置值
        {
            get { return Com_Siemens.GetReader("设定充电电流").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("设定充电电流", value.ToString());
                //Com.GetWriteDataCommand("1#充电电流设置").StrData = (float.Parse(value) * 10).ToString("0");
                //Com.ExcuteCommand("1#充电电流设置");
            }
        }

        /// <summary>
        /// 读取和设置 1通道充电时间设置值
        /// </summary>
        public string P1_充电时间设置值
        {
            get { return Com_Siemens.GetReader("充电时间设置").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("充电时间设置", value.ToString());
                //Com.GetWriteDataCommand("1#充电时间设置").StrData = value;
                //Com.ExcuteCommand("1#充电时间设置");
            }
        }

        /// <summary>
        /// 读取和设置 1通道放电电流设置值
        /// </summary>
        public string P1_放电电流设置值
        {
            get { return Com_Siemens.GetReader("设定放电电流").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("设定放电电流", value.ToString());
                //Com.GetWriteDataCommand("1#放电电流设置").StrData = (float.Parse(value) * 10).ToString("0");
                //Com.ExcuteCommand("1#放电电流设置");
            }
        }

        /// <summary>
        /// 读取和设置 1通道放电时间设置值
        /// </summary>
        public string P1_放电时间设置值
        {
            get { return Com_Siemens.GetReader("放电时间设置").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("放电时间设置", value.ToString());
                //Com.GetWriteDataCommand("1#放电时间设置").StrData = value;
                //Com.ExcuteCommand("1#放电时间设置");
            }
        }

        /// <summary>
        /// 读取和设置 1通道起始电压设置值
        /// </summary>
        public string P1_起始电压设置值
        {
            get { return Com_Siemens.GetReader("充电起始电压").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("充电起始电压", value.ToString());
                //Com.GetWriteDataCommand("1#充电起始电压设置").StrData = (float.Parse(value) * 10).ToString("0");
                //Com.ExcuteCommand("1#充电起始电压设置");
            }
        }

        /// <summary>
        /// 读取和设置 1通道充电终止电压设置值
        /// </summary>
        public string P1_充电终止电压设置值
        {
            get { return Com_Siemens.GetReader("充电终止电压").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("充电终止电压", value.ToString());
                //Com.GetWriteDataCommand("1#充电终止电压设置").StrData = (float.Parse(value) * 10).ToString("0");
                //Com.ExcuteCommand("1#充电终止电压设置");
            }
        }

        /// <summary>
        /// 读取和设置 1通道过载电流设置值
        /// </summary>
        public string P1_已循环次数
        {
            get { return Com_Siemens.GetReader("已循环次数").ToString(); } 
        }

        public string P1_循环充电静置时间设置
        {
            get { return Com_Siemens.GetReader("循环充电静置时间设置").ToString(); }
        }


        public string P1_循环放电静置时间设置
        {
            get { return Com_Siemens.GetReader("循环放电静置时间设置").ToString(); }
        }


        public string P1_循环充电已静置时间
        {
            get { return Com_Siemens.GetReader("循环充电已静置时间").ToString(); }
        }


        public string P1_循环放电已静置时间
        {
            get { return Com_Siemens.GetReader("循环放电已静置时间").ToString(); }
        }



        /// <summary>
        /// 读取和设置 1通道放电终止电压设置值
        /// </summary>
        public string P1_放电终止电压设置值
        {
            get { return Com_Siemens.GetReader("放电终止电压").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("放电终止电压", value.ToString());
                //Com.GetWriteDataCommand("1#放电终止电压设置").StrData = (float.Parse(value) * 10).ToString("0");
                //Com.ExcuteCommand("1#放电终止电压设置");
            }
        }
        public string P1_循环次数设置值
        {
            get { return Com_Siemens.GetReader("循环次数设置").ToString(); }
            set
            {
                Com_Siemens.ExcuteCommand_Write("循环次数设置", value.ToString());
                //Com.GetWriteDataCommand("1#放电终止电压设置").StrData = (float.Parse(value) * 10).ToString("0");
                //Com.ExcuteCommand("1#放电终止电压设置");
            }
        }

        public string P1_充电模式设置值
        {
            get { return Com.GetReader("1#充电模式设置_读取").ToString(); }
            set
            {
                Com.GetWriteDataCommand("1#充电模式设置").StrData = value;
                Com.ExcuteCommand("1#充电模式设置");
            }
        }


        /// <summary>
        /// 读取1通道充电电流
        /// 
        /// </summary>
        public string P1_显示充电电流
        {

            get { return double.Parse(Com_Siemens.GetReader("电池电流").ToString()).ToString("0.0"); }
        }

        /// <summary>
        /// 读取1通道放电电流
        /// </summary>
        public string P1_显示放电电流
        {
            get { return double.Parse(Com_Siemens.GetReader("电池电流").ToString()).ToString("0.0"); }

        }



        /// <summary>
        /// 读取1通道电池电压
        /// </summary>
        public string P1_显示电池电压
        {
            get { return double.Parse(Com_Siemens.GetReader("电池电压").ToString()).ToString("0.0"); }
        }

        /// <summary>
        /// 读取1通道充电时间
        /// </summary>
        public string P1_显示充电时间
        {
            get { return Com_Siemens.GetReader("充电计时分").ToString(); }
        }
 

        /// <summary>
        /// 读取1通道放电时间
        /// </summary>
        public string P1_显示放电时间
        {
            get { return Com_Siemens.GetReader("放电计时分").ToString(); }
        }


        /// <summary>
        /// 读取1通道放电安时数
        /// </summary>
        public string P1_显示放电安时数
        {
            get { return Com_Siemens.GetReader("放电安时").ToString(); }
        }


        /// <summary>
        /// 读取1通道运行状态
        /// </summary>
        public string P1_显示运行状态
        {
            get { return Com_Siemens.GetReader("设置运行状态").ToString(); }
        }

        public string P1_显示循环次数
        {
            get { return Com_Siemens.GetReader("已循环次数").ToString(); }
        } 

        public bool getWarnningState(string p_Str)
        {
            object obj = Com_Siemens.GetReader(p_Str);
            if (obj == null)
                return false;
            return bool.Parse(obj.ToString());
        }
    }

    public class UserRights
    {
        public static string UserName;
        public static bool Manager;
    }
}
