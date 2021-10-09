using S7.Net;
using S7NetWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementSpecificTools.PlcConnectivity
{
    class Plc
    {
        public static bool ifConnected = false;
        public TagGroup TagGroups = null;
        private Queue<List<Tag>> queue_Writer=new Queue<List<Tag>>();
        private Dictionary<string, EmbedTag> list_Reader=new Dictionary<string, EmbedTag>();
        private bool StopFlag;
        public int PLCStatue;
        private Thread thread;
        string rem_ipAddress;
        CpuType rem_cpuType;

        #region Singleton

        // For implementation refer to: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx        
        private static readonly Lazy<Plc> _instance = new Lazy<Plc>(() => new Plc());

        public static Plc Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion

        #region Public properties

        public ConnectionStates ConnectionState { get { return plcDriver != null ? plcDriver.ConnectionState : ConnectionStates.Offline; } }

        public DB1 Db1 { get; set; }

        public TimeSpan CycleReadTime { get; private set; }

        #endregion

        #region Private fields

        IPlcSyncDriver plcDriver;

        #endregion

        #region Constructor

        private Plc()
        {
            Db1 = new DB1(); 
        }

        #endregion

        #region Add Reader  Remove Reader
        public void AddReader(String pName, EmbedTag pEmbedTag)
        {
            if (list_Reader.ContainsKey(pName))
                return;
            pEmbedTag.tagCell.Effective = true;
            list_Reader.Add(pName, pEmbedTag);
        }

        public void RemoveReader(String pName)
        {
            if (list_Reader.ContainsKey(pName))
                list_Reader.Remove(pName);
        }
        #endregion

        #region Public methods

        public bool Connect(string ipAddress, CpuType cpuType)
        {
            if (PLCStatue != 0)
                return true;
            if (!IsValidIp(ipAddress))
            {
                throw new ArgumentException("Ip address is not valid");
            }
            switch (cpuType)
            {
                case CpuType.S7200smart://ok
                    plcDriver = new S7NetPlcDriver(CpuType.S7200smart, ipAddress, 0, 1);
                    break;
                case CpuType.S7200:
                    plcDriver = new S7NetPlcDriver(CpuType.S7200, ipAddress, 0, 1);
                    break;
                case CpuType.S7300:
                    plcDriver = new S7NetPlcDriver(CpuType.S7300, ipAddress, 0, 2);
                    break;
                case CpuType.S7400:
                    plcDriver = new S7NetPlcDriver(CpuType.S7400, ipAddress, 0, 3);
                    break;
                case CpuType.S71200:
                    plcDriver = new S7NetPlcDriver(CpuType.S71200, ipAddress, 0, 1);
                    break;
                case CpuType.S71500:
                    plcDriver = new S7NetPlcDriver(CpuType.S71500, ipAddress, 0, 1);
                    break;
            }
            rem_ipAddress = ipAddress;
            rem_cpuType = cpuType;
            GloaleErrorCount_Connect = 0;
            GloaleErrorCount_Read = 0;
            thread = new Thread(new ParameterizedThreadStart(this.GetStatue));
            thread.IsBackground = true;
            thread.Start(plcDriver);
            return true;
        }

        private void GetStatue(object object_0)
        {
            S7NetPlcDriver plcDriver = (S7NetPlcDriver)object_0;
            bool flag = false;
            while (!this.StopFlag)
            {
                #region  while
                if (!flag)
                {
                    plcDriver.Connect();
                    Thread.Sleep(1000);
                    if (plcDriver.ConnectionState == ConnectionStates.Offline)
                    {
                        flag = false;
                        this.PLCStatue = 5;
                        Thread.Sleep(20);
                        continue;
                    }
                    flag = true;
                    this.PLCStatue = 3;
                }
                this.ProcessPLCData();
                if (GloaleErrorCount_Read <= 4)
                {
                    this.PLCStatue = 4;
                }
                else
                {
                    this.PLCStatue = 6;
                    while (!this.StopFlag && GloaleErrorCount_Connect <= 5)
                    {
                        if (StopFlag)
                        {
                            break;
                        }
                        plcDriver.Disconnect();
                        plcDriver.Connect();
                        Thread.Sleep(2000);
                        if (plcDriver.ConnectionState == ConnectionStates.Offline)
                        {
                            GloaleErrorCount_Connect++;
                            flag = false;
                        }
                    }
                }
                Thread.Sleep(20);
                #endregion
            }
            this.PLCStatue = 0;
        }

        private int GloaleErrorCount_Connect = 0;
        private int GloaleErrorCount_Read = 0;

        public void ProcessPLCData()
        {
            if (plcDriver == null || plcDriver.ConnectionState != ConnectionStates.Online)
            {
                Plc.ifConnected = false;
                GloaleErrorCount_Read++;
                return;
            }
            try
            {
                Plc.ifConnected = true;
                //RefreshTags_Class();
                if (list_Reader != null && list_Reader.Count > 0)
                    foreach (KeyValuePair<string, EmbedTag> GloaleReader in list_Reader)
                    {
                        if (this.IsNeedToWrite())
                        {
                            while (queue_Writer.Count > 0)
                            {
                                List<Tag> tagList = queue_Writer.Dequeue();
                                this.Write(tagList); 
                                if (StopFlag)
                                {
                                    break;
                                }
                            }
                           Thread.Sleep(10);
                        }
                        if (StopFlag)
                        {
                            break;
                        }
                        if (GloaleReader.Value.tagCell != null && GloaleReader.Value.tagCell.Effective)
                        {
                            //Thread.Sleep(10);
                            this.Read(GloaleReader.Value.tagList);
                        }
                        
                    }
                if (this.IsNeedToWrite())
                {
                    while (queue_Writer.Count > 0)
                    {
                        List<Tag> tagList = queue_Writer.Dequeue();
                        this.Write(tagList);
                        if (StopFlag)
                        {
                            break;
                        }
                    }
                    //Thread.Sleep(10);
                }
            }
#pragma warning disable CS0168 // 声明了变量“ee”，但从未使用过
            catch(Exception ee)
#pragma warning restore CS0168 // 声明了变量“ee”，但从未使用过
            {
                GloaleErrorCount_Read++;
            }
        }

        public bool IsNeedToWrite()
        {
            if (queue_Writer.Count > 0)
                return true;
            return false;
        }
         
        public void Disconnect()
        {
            if (plcDriver == null || this.ConnectionState == ConnectionStates.Offline)
            {
                return;
            }
            if (!StopFlag)
            {
                ifConnected = false;
                StopFlag = true;
                plcDriver.Disconnect();
            }
        }

        public void Effective()
        {
            foreach (KeyValuePair<string, EmbedTag> GloaleReader in list_Reader)
            {
                if (GloaleReader.Value.tagCell != null)
                    GloaleReader.Value.tagCell.Effective = true;
            }
        }
        public void Effective(String pName)
        {
            foreach (KeyValuePair<string, EmbedTag> GloaleReader in list_Reader)
            {
                if (GloaleReader.Value.tagCell != null && GloaleReader.Key.CompareTo(pName) == 0)
                    GloaleReader.Value.tagCell.Effective = true;
            }
        }

        public void DisEffective(String pName)
        {
            foreach (KeyValuePair<string, EmbedTag> GloaleReader in list_Reader)
            {
                if (GloaleReader.Value.tagCell != null && GloaleReader.Key.CompareTo(pName) == 0)
                    GloaleReader.Value.tagCell.Effective = false;
            }
        }
        public void Write(string name, object value)
        {
            if (plcDriver == null || plcDriver.ConnectionState != ConnectionStates.Online || TagGroups == null)
            {
                return;
            }
            if (!TagGroups.EmbedTags.ContainsKey(name))
                return;
            EmbedTag tt_EmbedTag = TagGroups.EmbedTags[name];
            Tag tag = new Tag(name, value, tt_EmbedTag.Accessaddress);
            List<Tag> tagList = new List<Tag>();
            tagList.Add(tag);
            queue_Writer.Enqueue(tagList);
        }

        public void Read(List<Tag> tags)
        {
            if (plcDriver == null || plcDriver.ConnectionState != ConnectionStates.Online)
            {
                return;
            }
            plcDriver.ReadItems(tags);
        }
       
        public void Write(List<Tag> tags)
        {
            if (plcDriver == null || plcDriver.ConnectionState != ConnectionStates.Online)
            {
                return;
            }
            plcDriver.WriteItems(tags); 
        }

        #endregion        

        #region Private methods

        private bool IsValidIp(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }

        /// <summary>
        /// 读取类
        /// </summary>
        private void RefreshTags_Class()
        { 
            plcDriver.ReadClass(Db1, 1);
        }

        #endregion 
    }
}
