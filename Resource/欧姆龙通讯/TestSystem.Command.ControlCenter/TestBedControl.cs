using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO.Ports;
using TestSystem.Command.Interface;

namespace TestSystem.Command.ControlCenter
{
    internal class TestBedControl
    {
        Dictionary<string,ICommand> OnCommand;
        Queue<ICommand> q;
        Thread Thread_Excute;
        bool isExcute = true;
        public bool isClose = false;
        SerialPort sp;
        TestBetReader tbr;

        public TestBedControl(ref SerialPort sp,ref TestBetReader tbr)
        {
            OnCommand = new Dictionary<string,ICommand>();
            q = new Queue<ICommand>();
            Thread_Excute = new Thread(Excutes);
            Thread_Excute.Priority = ThreadPriority.Highest;
            isExcute = true;
            this.sp = sp;
            this.tbr = tbr;
            //Thread_Excute.Start();
        }

        /// <summary>
        /// 设置 按钮命令
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="OnCommand"></param>
        /// <param name="OffCommand"></param>
        public void SetCommand(string slot, ICommand OnCommand)
        {
            this.OnCommand.Add(slot, OnCommand);
        }

        /// <summary>
        /// 按钮按下 置位
        /// </summary>
        /// <param name="slot"></param>
        public void ExcuteCommand(string slot)
        {
            if (OnCommand.ContainsKey(slot))
                this.q.Enqueue(OnCommand[slot]);
        }
        int i = 0;
        /// <summary>
        /// 执行
        /// </summary>
        private void Excutes()
        {
            ICommand ttic;
            while (true)
            {
                Thread.Sleep(10);
                if (q.Count > 0)
                {
                    tbr.Stop_In();
                    i = q.Count;
                    Thread.Sleep(100);
                    for (; i > 0; i--)
                    {
                        ttic = q.Dequeue();
                        ttic.Excute(ref sp);
                        ttic.Excute(ref sp);
                        Thread.Sleep(10);
                    }
                    tbr.Start_In();
                }
                if (isExcute == false)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
         
            isExcute = false;
            Thread_Excute.Join(300);
            if (Thread_Excute.ThreadState == ThreadState.Running)
            {
                Thread_Excute.Abort();
            }
            OnCommand.Clear();
        }

        /// <summary>
        /// 获得标记的命令
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ICommand GetCommand(string position)
        {
            return OnCommand[position];

        }

        /// <summary>
        /// 获得 标记的写命令
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ICommand_WriteData GetWriteDataCommand(string position)
        {
            return (ICommand_WriteData)OnCommand[position];
        }

        /// <summary>
        /// 开启线程
        /// </summary>
        public void Start()
        {
            if (Thread_Excute.ThreadState != ThreadState.Running)
            {
                Thread_Excute.Start();
            }
        }

       
        

    }
}
