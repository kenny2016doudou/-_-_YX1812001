using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.IO.Ports;
using TestSystem.Command.Interface;


namespace TestSystem.Command.ControlCenter
{
    internal class TestBetReader
    {
        Dictionary<string, IRead> Readers;
        Dictionary<string, IRead> StepReaders;
        Thread thread_StartRead;
        bool isRead = true;
        bool isSuppurse = false;
        SerialPort sp;
        public TestBetReader(ref SerialPort sp)
        {
            Readers = new Dictionary<string, IRead>();
            thread_StartRead = new Thread(Read);
            StepReaders = new Dictionary<string, IRead>();
            thread_StartRead.Priority = ThreadPriority.Lowest;
            this.sp = sp;
        }

        public void SetReader(string position, IRead read)
        {
            Readers.Add(position, read);
        }

        public void SetStepReader(string position, IRead read)
        {
            StepReaders.Add(position, read);
        }


        private void Read()
        {
            while (true)
            {
                Thread.Sleep(10);
                foreach (IRead val in Readers.Values)
                {
                    Thread.Sleep(10);
                    val.Read(ref sp);
                    if (isRead == false)
                    {
                        return;
                    }
                    while (true)
                    {
                        if (!isSuppurse)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            isRead = false;
            thread_StartRead.Join(60);
            if (thread_StartRead.ThreadState == ThreadState.Running)
            {
                thread_StartRead.Abort();
            }
        }

        internal void Stop_In()
        {
            isSuppurse = true;
            thread_StartRead.Join(60);
            isSuppurse = true;
        }

        internal void Start_In()
        {
            isSuppurse = false;
            thread_StartRead.Join(60);
        }

        public void Start()
        {
            if (thread_StartRead.ThreadState != ThreadState.Running)
            {
                isRead = true;
                thread_StartRead.Start();
            }
        }

        public object GetReader(string position)
        {
            return Readers[position].Data;

        }

        public object[] GetReaderIData(string position)
        {
            return Readers[position].DataMuster;

        }


        public object GetStepReader(string position)
        {
            this.Stop_In();
            StepReaders[position].Read(ref sp);
            this.Start_In();
            return StepReaders[position].Data;
        }


        public object[] GetStepReaderIData(string position)
        {
            this.Stop_In();
            StepReaders[position].Read(ref sp);
            this.Start_In();
            return StepReaders[position].DataMuster;
        }
    }
}
