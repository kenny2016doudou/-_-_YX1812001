using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using TestSystem.Command.Interface;
namespace TestSystem.Command.YB.TianChen
{
    public class Command_YBTianChen_WriteData : ICommand_WriteData
    {
        string point;
        string address;
        int sleep = 50;
        /// <summary>
        /// 格式 例：0000
        /// </summary>
        string strData;


        /// <summary>
        /// 数据  格式 例：0000
        /// </summary>
        public string StrData
        {
            set { strData = value; }
        }



        private MeterType mt;

        /// <summary>
        /// 仪表类型
        /// </summary>
        public MeterType MeterTypeEnum
        {
            get { return mt; }
            set { mt = value; }
        }

        /// <summary>
        /// point格式:00,address格式:01
        /// </summary>
        /// <param name="point"></param>
        /// <param name="address"></param>
        public Command_YBTianChen_WriteData(string point, string address)
        {
            this.point = point;
            this.address = address;
        }

        /// <summary>
        ///  point格式:00,address格式:01
        /// </summary>
        /// <param name="point"></param>
        /// <param name="address"></param>
        /// <param name="sleep"></param>
        public Command_YBTianChen_WriteData(string point,string address, int sleep)
        {
            this.point = point;
            this.sleep = sleep;
            this.address = address;
       
        }
        /// <summary>
        ///  point格式:00,address格式:01,strData格式:0000
        /// </summary>
        /// <param name="point"></param>
        /// <param name="address"></param>
        /// <param name="strData"></param>
        /// <param name="sleep"></param>
        public Command_YBTianChen_WriteData(string point, string address, string strData, int sleep)
        {
            this.point = point;
            this.sleep = sleep;
            this.strData = strData;
            this.address = address;
        }


        #region ICommand 成员
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Excute(ref SerialPort sp)
        {
            try
            {
                return YBTianChenControl.getInstance().ExcuteWrite(point, address, strData, mt,sp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
