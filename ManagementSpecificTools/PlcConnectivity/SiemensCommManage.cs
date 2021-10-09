using ManagementSpecificTools.PlcConnectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ManagementSpecificTools.PlcConnectivity
{
    public class SiemensCommManage
    {

        private static SiemensCommManage uniqueInstance;
        private XmlDocument xmlDocument = new XmlDocument();
        public TagGroup TagGroups = null;
        public DB1 Db1 { get; set; }
        public static SiemensCommManage getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new SiemensCommManage();
            }
            return uniqueInstance;
        }

        private SiemensCommManage()
        {
            Db1 = Plc.Instance.Db1;
            if (File.Exists(Path.Combine(Application.StartupPath, "syscfg.xml")))
            {
                xmlDocument.Load(Path.Combine(Application.StartupPath, "syscfg.xml"));
                XmlNode documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
                XmlNodeList xmlNodeList = documentElement.SelectNodes("//TagGroup");
                string text = xmlNodeList[0].Attributes["name"].Value.ToUpper();
                TagGroups = new TagGroup(text);
                XmlNodeList xmlNodeList2 = xmlNodeList[0].SelectNodes("EmbedTag");
                for (int j = 0; j < xmlNodeList2.Count; j++)
                {
                    string text2 = xmlNodeList2[j].Attributes["name"].Value.ToUpper();
                    string value2 = xmlNodeList2[j].Attributes["accessAddress"].Value;
                    string value3 = xmlNodeList2[j].Attributes["accessType"].Value;
                    string value4 = xmlNodeList2[j].Attributes["dataType"].Value;
                    string value5 = xmlNodeList2[j].Attributes["desc"].Value;
                    EmbedTag pEmbedTag = new EmbedTag(text2, value3, value2, value4, value5);
                    TagGroups.EmbedTags.Add(text2, pEmbedTag);
                    if (value3.Contains("Read"))
                        Plc.Instance.AddReader(text2, pEmbedTag);
                }
                Plc.Instance.TagGroups = TagGroups;
            }
        }

        public void AddEmbedTag(String pName, EmbedTag pEmbedTag)
        {
            TagGroups.EmbedTags.Add(pName, pEmbedTag);
            if (pEmbedTag.AccessType.Contains("Read"))
            {
                Plc.Instance.AddReader(pName, pEmbedTag);
            }
        }

        public void RemoveEmbedTag(String pName)
        {
            if (TagGroups.EmbedTags.ContainsKey(pName))
                TagGroups.EmbedTags.Remove(pName);
            Plc.Instance.RemoveReader(pName);

            bool ifChanged = false;
            if (File.Exists(Path.Combine(Application.StartupPath, "syscfg.xml")))
            {
                xmlDocument.Load(Path.Combine(Application.StartupPath, "syscfg.xml"));
                XmlNode documentElement = xmlDocument.DocumentElement;
                XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
                XmlNodeList xmlNodeList = documentElement.ChildNodes;
                XmlNodeList xmlNodeList2 = xmlNodeList[0].ChildNodes;
                for (int j = 0; j < xmlNodeList2.Count; j++)
                {
                    string text2 = xmlNodeList2[j].Attributes["name"].Value.ToUpper();
                    if (text2.CompareTo(pName) == 0)
                    {
                        ifChanged = true;
                        xmlNodeList[0].RemoveChild(xmlNodeList2[j]);
                        break;
                    }
                }
                if (ifChanged)
                    xmlDocument.Save(Path.Combine(Application.StartupPath, "syscfg.xml"));
            }
        }

        public void Start()
        {
            try
            {
                if (File.Exists(Path.Combine(Application.StartupPath, "syscfg.xml")))
                {
                    XmlNode documentElement = xmlDocument.DocumentElement;
                    XmlNode xmlNode = documentElement.SelectSingleNode("/Device");
                    Plc.Instance.Effective();
                    Plc.Instance.Connect(xmlNode.Attributes["ip"].Value, (S7.Net.CpuType)Enum.Parse(typeof(S7.Net.CpuType), xmlNode.Attributes["name"].Value));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void Stop()
        {
            try
            {
                Plc.Instance.Disconnect();
                Plc.Instance.PLCStatue = 0;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        /// <summary>
        /// Pulse
        /// </summary>
        /// <param name="pOrder"></param>
        public void ExcuteCommand_Pulse(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            Plc.Instance.Write(pOrder, true);
            System.Threading.Thread.Sleep(1000);
            Plc.Instance.Write(pOrder, false);
        }

        /// <summary>
        /// Pulse置位
        /// </summary>
        /// <param name="pOrder"></param>
        public void ExcuteCommand_PulseZW(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            Plc.Instance.Write(pOrder, false);

        }

        /// <summary>
        /// Pulse复位
        /// </summary>
        /// <param name="pOrder"></param>
        public void ExcuteCommand_PulseFW(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            Plc.Instance.Write(pOrder, true);
           

        }



        /// <summary>
        /// Set
        /// </summary>
        /// <param name="pOrder"></param>
        public void ExcuteCommand_Set(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            Plc.Instance.Write(pOrder,true);
        }

        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="pOrder"></param>
        public void ExcuteCommand_Reset(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            Plc.Instance.Write(pOrder, false);
        }

        #region Write Value
        public void ExcuteCommand_Write(String pOrder, object pValue)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            String ValueStr = pValue.ToString().Trim();
            object _DataValue = null;
            switch (TagGroups.EmbedTags[pOrder].DataType)
            {
                case "Bit":
                    switch (ValueStr)
                    {
                        case "1":
                            _DataValue = true;
                            break;
                        case "0":
                            _DataValue = false;
                            break;
                        default:
                            bool tempbool = false;
                            if (bool.TryParse(ValueStr, out tempbool))
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
                    if (!isNumberic(ValueStr))
                    {
                        MessageBox.Show("请输入数值");
                        return;
                    }
                    switch (TagGroups.EmbedTags[pOrder].DataType)
                    {
                        case "Byte":
                            Byte tempByte = 0;
                            if (Byte.TryParse(ValueStr, out tempByte))
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
                            if (Int16.TryParse(ValueStr, out tempInt16))
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
                            if (float.TryParse(ValueStr, out tempfloat))
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
                            if (float.TryParse(ValueStr, out tempfloat2))
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
            if (_DataValue == null)
                return;
            Plc.Instance.Write(pOrder, _DataValue);
        }
        #endregion

        #region Number Check

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

        #endregion
       
        #region GetReader Value
        public object GetReader(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return null;
            return TagGroups.EmbedTags[pOrder].tagCell.ItemValue;
        }
        #endregion

        #region GetReader Value
        public object GetReaderB(String pOrder)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return null;
            object ss = false;
            if (TagGroups.EmbedTags[pOrder].tagCell.ItemValue.Equals(ss))
            {
                return "0";
            }
            else
            {
                return "1";
            }
           // return TagGroups.EmbedTags[pOrder].tagCell.ItemValue;
        }
        #endregion








        #region SetReaderState
        public void SetReaderState(String pOrder, bool effective)
        {
            if (!TagGroups.EmbedTags.ContainsKey(pOrder))
                return;
            TagGroups.EmbedTags[pOrder].tagCell.Effective = effective;
        }
        #endregion
    }
}
