using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using TestSystem.Command.Interface;

namespace TestSystem.Command.ControlCenter
{
    internal class ComControl :IComControl
    {
        /// <summary>
        /// 试验台置位复位控制
        /// </summary>
        TestBedControl tbc;
        /// <summary>
        /// 试验台读取控制
        /// </summary>
        TestBetReader tbr;

        public ComControl(ref SerialPort sp)
        {
            tbr = new TestBetReader(ref sp);
            tbc = new TestBedControl(ref sp, ref tbr);
        }
        /// <summary>
        /// 设置 置位、复位 命令对象
        /// </summary>
        /// <param name="slot">命令对象自定义名称</param>
        /// <param name="OnCommand">命令对象</param>
        public void SetCommand(string slot, ICommand OnCommand)
        {
            tbc.SetCommand(slot, OnCommand);
        }

        public void ExcuteCommand(string slot)
        {
            tbc.ExcuteCommand(slot);
        }

        public ICommand GetCommand(string position)
        {
            return tbc.GetCommand(position);
        }

        public ICommand_WriteData GetWriteDataCommand(string position)
        {
            return tbc.GetWriteDataCommand(position);
        }

        /// <summary>
        /// 设置读取命令对象(多线程方式)
        /// </summary>
        /// <param name="position">命令对象自定义名称</param>
        /// <param name="read">命令对象</param>
        public void SetReader(string position, IRead read)
        {
            tbr.SetReader(position, read);
        }


        /// <summary>
        /// 设置读取命令对象(非线程方式)
        /// </summary>
        /// <param name="position">命令对象自定义名称</param>
        /// <param name="read">命令对象</param>
        public void SetStepReader(string position, IRead read)
        {
            tbr.SetStepReader(position, read);
        }

        /// <summary>
        /// 读取命令对象单个值(线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public object GetReader(string position)
        {
            return tbr.GetReader(position);
        }

        /// <summary>
        /// 读取命令对象多个值(线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public object[] GetReaderIData(string position)
        {
            return tbr.GetReaderIData(position);

        }

        /// <summary>
        /// 读取命令对象单个值(非线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public object GetStepReader(string position)
        {
            return tbr.GetStepReader(position);
        }

        /// <summary>
        /// 读取命令对象多个值(非线程)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public object[] GetStepReaderIData(string position)
        {
            return tbr.GetStepReaderIData(position);
        }


        /// <summary>
        /// 停止处理命令
        /// </summary>
        public void Stop()
        {
            tbr.Stop();
            tbc.Stop();
        }

        /// <summary>
        /// 开始处理命令
        /// </summary>
        public void Start()
        {
            tbr.Start();
            tbc.Start();
            
        }





    }
}
