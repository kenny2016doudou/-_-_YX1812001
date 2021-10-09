using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;

namespace frequentlyCtrlClass
{
    public class StaticSendClass
    {

        public static string UserName;
        public static bool ifAdmin;
    }

    /// <summary>
    /// 测试类型类
    /// </summary>
    [Serializable]
    public class testTypeClass : asbaseClass
    {
        public string myName;
        public bool ifTested = false;
        public ArrayList xmArrayList = new ArrayList();

        public testxmClass addXM(int p_Index, string p_Name, bool p_Must, string p_Desc)
        {
            testxmClass tt_testxmClass = new testxmClass();
            tt_testxmClass.myIndex = p_Index;
            tt_testxmClass.myName = p_Name;
            tt_testxmClass.ifMustTest = p_Must;
            tt_testxmClass.xmDescStr = p_Desc;
            xmArrayList.Add(tt_testxmClass);
            return tt_testxmClass;
        }
        public testxmClass updateXM(string p_Name, string p_Desc)
        {
            testxmClass tt_testxmClass = null;
            for (int i = 0; i < xmArrayList.Count; i++)
            {
                tt_testxmClass = (testxmClass)xmArrayList[i];
                if (tt_testxmClass.myName.CompareTo(p_Name) == 0)
                {
                    tt_testxmClass.xmDescStr = p_Desc;
                    return tt_testxmClass;
                }
            }
            return null;
        }
        public string GetSaveInfo()
        {
            string tempstr = "";
            string retStr = "";
            testxmClass tt_testxmClass = null;
            for (int i = 0; i < xmArrayList.Count; i++)
            {
                tt_testxmClass = (testxmClass)xmArrayList[i];
                tempstr = tt_testxmClass.GetSaveInfo();
                if (tempstr.Length > 0)
                    retStr += tempstr + ",;";
            }
            return retStr;
        }

        public void ClearSaveInfo()
        {          
            testxmClass tt_testxmClass = null;
            for (int i = 0; i < xmArrayList.Count; i++)
            {
                tt_testxmClass = (testxmClass)xmArrayList[i];
                tt_testxmClass.ClearSaveInfo(); 
            }            
        }
        public void reinit()
        {
            testxmClass tt_testxmClass = null;
            for (int i = 0; i < xmArrayList.Count; i++)
            {
                tt_testxmClass = (testxmClass)xmArrayList[i];
                tt_testxmClass.reinit();
            }    
        }
    }


    /// <summary>
    /// 测试项目类
    /// </summary>
    [Serializable]
    public class testxmClass : asbaseClass
    {
        public int myIndex;
        public string myName;
        public bool ifMustTest = true;
        public bool ifTested = false;
        public string pdModel = "自动";
        public string pdResult = "不合格";
        public string xmDescStr = "";
        

        public bool pdAll()
        {
            dzFolder tt_dzFolder;
           
            pdResult = "合格";
            return true;
        }

        public string pdAll_Str()
        {
            string retString = "";
            dzFolder tt_dzFolder;
          
            return retString;
        }
         

        public string GetSaveInfo()
        {
            string tempstr = "";
            string retStr = myName + ",," + pdResult + ",*";
            dzFolder tt_dzFolder = null;
           
            return retStr;
        }

        public void ClearSaveInfo()
        {
            dzFolder tt_dzFolder = null;
           
            pdResult = "不合格";
        }
        public void reinit()
        {
           
        }
    }

    /// <summary>
    /// 动作类列表
    /// </summary>
    [Serializable]
    public class dzFolder : asbaseClass
    {
        public string myName = "";
        public int dzType = 0;
        public ArrayList dzArrayList = new ArrayList();
        public string pdResult = "不合格";

        public void addDZ(testdzClass p_testdzClass)
        {
            dzArrayList.Add(p_testdzClass);
        }
        public bool pdAll()
        {
            testdzClass tt_testdzClass;
            for (int i = 0; i < dzArrayList.Count; i++)
            {
                tt_testdzClass = (testdzClass)dzArrayList[i];
                if (!tt_testdzClass.pdAll())
                {
                    pdResult = "不合格";
                    return false;
                }
            }
            pdResult = "合格";
            return true;
        }
        public string pdAll_Str()
        {
            string retString = "";
            testdzClass tt_testdzClass;
            for (int i = 0; i < dzArrayList.Count; i++)
            {
                tt_testdzClass = (testdzClass)dzArrayList[i];
                retString += tt_testdzClass.pdAll_Str() + "\n";
            }
            return retString;
        }


        public testdzClass addDZ(int p_myIndex, int p_dzType, string p_remDoTypeString, string p_myName, string p_stepAir, string p_stepDesc, string p_ifSave)
        {
            testdzClass tt_testdzClass = new testdzClass();
            tt_testdzClass.myIndex = DateTime.Now.Millisecond;
            tt_testdzClass.dzTypeStr = p_remDoTypeString;
            tt_testdzClass.myName = p_myName;
            tt_testdzClass.dzType = p_dzType;

            tt_testdzClass.stepAir = p_stepAir;
            tt_testdzClass.stepDesc = p_stepDesc;

            tt_testdzClass.ifSave = bool.Parse(p_ifSave.ToString());
            dzArrayList.Add(tt_testdzClass);
            return tt_testdzClass;
        }

        public void EditDZ(int p_myIndex, int p_dzType, string p_remDoTypeString, string p_myName, string p_stepAir, string p_stepDesc, string p_ifSave, testdzClass p_testdzClass)
        {
           
            p_testdzClass.dzTypeStr = p_remDoTypeString;
            p_testdzClass.myName = p_myName;
            p_testdzClass.dzType = p_dzType;

            p_testdzClass.stepAir = p_stepAir;
            p_testdzClass.stepDesc = p_stepDesc;
            p_testdzClass.ifSave = bool.Parse(p_ifSave.ToString());
        }
        public string GetSaveInfo()
        {
            string tempstr = "";
            string retStr = myName + ",," + pdResult + ",*";
            testdzClass tt_testdzClass = null;
            for (int i = 0; i < dzArrayList.Count; i++)
            {
                tt_testdzClass = (testdzClass)dzArrayList[i];
                tempstr = tt_testdzClass.GetSaveInfo();
                if (tempstr.Length > 0)
                    retStr += tempstr + ",.";
            }
            return retStr;
        }

        public void ClearSaveInfo()
        {
            testdzClass tt_testdzClass = null;
            for (int i = 0; i < dzArrayList.Count; i++)
            {
                tt_testdzClass = (testdzClass)dzArrayList[i];
                tt_testdzClass.ClearSaveInfo();
            }
            pdResult = "不合格";
        }
        public void reinit()
        {
            testdzClass tt_testdzClass = null;
            for (int i = 0; i < dzArrayList.Count; i++)
            {
                tt_testdzClass = (testdzClass)dzArrayList[i];
                tt_testdzClass.reinit();
            }
        }
    }
    
    /// <summary>
    ///具体执行指令
    /// </summary>
    [Serializable]
    public class testdzClass : asbaseClass
    {
        public int myIndex;
        public string myName = "";
        public int dzType = 0;
        public string dzTypeStr = "";

        public string stepAir = "";
        public string stepDesc = "";
        public string performReuslt = "";

        float minValue = 0, maxValue = 0, procValue = 0;
        int jxIndex = 0;

        public object jcObject;

        public void IgnoreStep()
        {
            performReuslt = "忽略";
        }

        public bool pdStep(object p_obj, Dictionary<string, float> p_dic)
        {
            string[] spResult;
            string[] sppp = { "|" };
            string[] chal_data_sp = { ";" };
            jcObject = "";
            if (p_obj == null || p_obj.ToString().Length <= 0)
                return false;
            bool retValue = true;
            jcObject = p_obj;
            performReuslt = "不合格";
            float procmidValue = 0;
            switch (dzType)
            {
                case 1://测量值 
                    if (stepDesc.ToUpper().Contains("ADD"))
                    {
                        procValue = float.Parse(p_obj.ToString());
                        remObject = procValue.ToString("0.00");
                        spResult = stepDesc.Split(sppp, StringSplitOptions.None);
                        if (!ifgetMinValue)
                        {
                            startValue = procValue;
                            ifgetMinValue = true;
                        }
                        procmidValue = Math.Abs(procValue - startValue);
                        minValue = float.Parse(spResult[1]);
                        maxValue = float.Parse(spResult[2]);
                        if (procmidValue < minValue)
                            retValue = false;
                    }
                    else if (stepDesc.ToUpper().Contains("SUB"))
                    {
                        procValue = float.Parse(p_obj.ToString());
                        remObject = procValue.ToString("0.00");
                        spResult = stepDesc.Split(sppp, StringSplitOptions.None);
                        if (!ifgetMinValue)
                        {
                            startValue = procValue;
                            ifgetMinValue = true;
                        }
                        procmidValue = Math.Abs(startValue-procValue);
                        minValue = float.Parse(spResult[1]);
                        maxValue = float.Parse(spResult[2]);
                        if (procmidValue < minValue)
                            retValue = false;
                    }
                    else
                    {
                        procValue = float.Parse(p_obj.ToString());
                        remObject = procValue.ToString("0.00");
                        spResult = stepDesc.Split(sppp, StringSplitOptions.None);
                        if (spResult.Length > 0)
                        {
                            if (spResult[0].ToUpper().CompareTo("DATA") == 0)//与确切值比较
                            {
                                minValue = float.Parse(spResult[1]);
                                maxValue = float.Parse(spResult[2]);
                                if (procValue < minValue || procValue > maxValue)
                                    return false;
                            }
                            else if (spResult[0].ToUpper().CompareTo("VARI") == 0)//与变量值比较
                            {
                                string pdfh = spResult[1].Trim();
                                maxValue = p_dic[spResult[2]];
                                switch (pdfh)
                                {
                                    case ">":
                                        if (procValue <= maxValue)
                                            retValue = false;
                                        break;
                                    case ">=":
                                        if (procValue < maxValue)
                                            retValue = false;
                                        break;
                                    case "<":
                                        if (procValue >= maxValue)
                                            retValue = false;
                                        break;
                                    case "<=":
                                        if (procValue > maxValue)
                                            retValue = false;
                                        break;
                                    case "==":
                                        if (procValue != maxValue)
                                            retValue = false;
                                        break;
                                    default:
                                        retValue = false;
                                        break;
                                }
                            }
                        }
                    }
                    if (retValue)
                        performReuslt = "合格";
                    else
                        performReuslt = "不合格";
                    return retValue;
                case 6://测量值变化时间测定,以及时间优劣判断
                    //data|450|500
                    string[] chvl_data = stepDesc.Split(chal_data_sp, StringSplitOptions.None);
                    procValue = float.Parse(p_obj.ToString());
                    remObject = procValue.ToString("0.00");
                    spResult = chvl_data[1].Split(sppp, StringSplitOptions.None);
                    if (spResult.Length > 0)
                    {
                        if (spResult[0].ToUpper().CompareTo("TIME") == 0)//与确切值比较
                        {
                            minValue = float.Parse(spResult[1]);
                            maxValue = float.Parse(spResult[2]);
                            if (procValue < minValue || procValue > maxValue)
                                return false;
                        }
                        else if (spResult[0].ToUpper().CompareTo("VARI") == 0)//与变量值比较
                        {
                            string pdfh = spResult[1].Trim();//符号
                            maxValue = p_dic[spResult[2]];   //变量实时变化值
                            switch (pdfh)
                            {
                                case ">":
                                    if (procValue <= maxValue)
                                        retValue = false;
                                    break;
                                case ">=":
                                    if (procValue < maxValue)
                                        retValue = false;
                                    break;
                                case "<":
                                    if (procValue >= maxValue)
                                        retValue = false;
                                    break;
                                case "<=":
                                    if (procValue > maxValue)
                                        retValue = false;
                                    break;
                                case "==":
                                    if (procValue != maxValue)
                                        retValue = false;
                                    break;
                                default:
                                    retValue = false;
                                    break;
                            }
                        }
                    }
                    if (retValue)
                        performReuslt = "合格";
                    else
                        performReuslt = "不合格";
                    return retValue;
                case 7://设定时间段内测量值变化测定
                    //data|450|500
                    string[] chvl_data_bh = stepDesc.Split(chal_data_sp, StringSplitOptions.None);
                    procValue = float.Parse(p_obj.ToString());
                    remObject = procValue.ToString("0.00");
                    spResult = chvl_data_bh[1].Split(sppp, StringSplitOptions.None);
                    if (spResult.Length > 0)
                    {
                        if (spResult[0].ToUpper().CompareTo("DATA") == 0)//与确切值比较
                        {
                            minValue = float.Parse(spResult[1]);
                            maxValue = float.Parse(spResult[2]);
                            if (procValue < minValue || procValue > maxValue)
                                return false;
                        }
                        else if (spResult[0].ToUpper().CompareTo("VARI") == 0)//与变量值比较
                        {
                            string pdfh = spResult[1].Trim();//符号
                            maxValue = p_dic[spResult[2]];   //变量实时变化值
                            switch (pdfh)
                            {
                                case ">":
                                    if (procValue <= maxValue)
                                        retValue = false;
                                    break;
                                case ">=":
                                    if (procValue < maxValue)
                                        retValue = false;
                                    break;
                                case "<":
                                    if (procValue >= maxValue)
                                        retValue = false;
                                    break;
                                case "<=":
                                    if (procValue > maxValue)
                                        retValue = false;
                                    break;
                                case "==":
                                    if (procValue != maxValue)
                                        retValue = false;
                                    break;
                                default:
                                    retValue = false;
                                    break;
                            }
                        }
                    }
                    if (retValue)
                        performReuslt = "合格";
                    else
                        performReuslt = "不合格";
                    return retValue;
                case 2://测量状态
                    if (stepDesc.CompareTo("开") == 0 && p_obj.ToString().CompareTo("1") == 0)
                    {
                        remObject = "开";
                        performReuslt = "合格";
                        return true;
                    }
                    if (stepDesc.CompareTo("关") == 0 && p_obj.ToString().CompareTo("0") == 0)
                    {
                        remObject = "关";
                        performReuslt = "合格";
                        return true;
                    }
                    break;
                default:
                    performReuslt = "合格";
                    break;
            }
            return false;
        }

        public void addStep(int p_dzType, string p_stepAir, string p_stepDesc)
        {
            if (dzType == 1)
            {
                if (!p_stepDesc.Contains("-"))
                {
                    MessageBox.Show("测量值检测时未发现范围检测标识!");
                    return;
                }
            }
            stepAir = p_stepAir;
            stepDesc = p_stepDesc;
        }

        public bool pdAll()
        {
            if (performReuslt.Contains("不合格"))
                return false;
            for (int i = 0; i < sub_dzArrayList.Count; i++)
            {
                testdzClass tt_testdzClass = (testdzClass)sub_dzArrayList[i];
                if (!tt_testdzClass.pdAll())
                {
                    return false;
                }
            }
            return true;
        }

        public string pdAll_Str()
        {
            string retString = "";
            if (jcObject == null)
                jcObject = "";
            retString += stepAir + "|" + stepDesc + "|" + performReuslt + "|" + jcObject.ToString();
            return retString;
        }

        public bool ifSave = false;
        public string remObject = "";

        public string GetSaveInfo()
        {
            string retStr = "";
            if (ifSave)
                retStr = myName + ",," + remObject;
            return retStr;
        }
        public void ClearSaveInfo()
        {
            startValue = 0;
            endValue = 0;
            ljChangeValue = 0;
            ifgetMaxValue = false;
            ifgetMinValue = false;
            ifRunned = false;
            performReuslt = "不合格";
            remObject = "";
            jcObject = "";
            testdzClass tt_testdzClass = null;
            if (sub_dzArrayList != null)
            {
                for (int i = 0; i < sub_dzArrayList.Count; i++)
                {
                    tt_testdzClass = (testdzClass)sub_dzArrayList[i];
                    tt_testdzClass.ClearSaveInfo();
                }
            }
        }
        public float startValue = 0;
        public float endValue = 0;
        public bool ifgetMinValue = false;
        public bool ifgetMaxValue = false;
        public float ljChangeValue = 0;
        public bool ifRunned = false;

        public ArrayList sub_dzArrayList = new ArrayList();
        public void insertDZ(testdzClass p_testdzClass, testdzClass insert_testdzClass,bool ifpre)
        {
            if (sub_dzArrayList != null)
            {
                string tempstr = "";
                for (int i = 0; i < sub_dzArrayList.Count; i++)
                {
                    testdzClass tt_testdzClass = (testdzClass)sub_dzArrayList[i];
                    tempstr = tt_testdzClass.myName + tt_testdzClass.myIndex.ToString();
                    if (tempstr.CompareTo(p_testdzClass.myName + p_testdzClass.myIndex.ToString()) == 0)
                    {
                        if (ifpre)
                        {
                            sub_dzArrayList.Insert(i, insert_testdzClass);
                        }
                        else
                        {
                            if (i >= sub_dzArrayList.Count - 1)
                                sub_dzArrayList.Add(insert_testdzClass);
                            else
                            {
                                sub_dzArrayList.Insert(i+1, insert_testdzClass);
                            }
                        }
                        break;
                    }                     
                }
            }   
        }
        public testdzClass addDZ(int p_myIndex, int p_dzType, string p_remDoTypeString, string p_myName, string p_stepAir, string p_stepDesc, string p_ifSave)
        {
            if (sub_dzArrayList == null)
                sub_dzArrayList = new ArrayList();
            testdzClass tt_testdzClass = new testdzClass();
            tt_testdzClass.myIndex = DateTime.Now.Millisecond;
            tt_testdzClass.dzTypeStr = p_remDoTypeString;
            tt_testdzClass.myName = p_myName;
            tt_testdzClass.dzType = p_dzType;

            tt_testdzClass.stepAir = p_stepAir;
            tt_testdzClass.stepDesc = p_stepDesc;

            tt_testdzClass.ifSave = bool.Parse(p_ifSave.ToString());
            sub_dzArrayList.Add(tt_testdzClass);
            return tt_testdzClass;
        }

        public void reinit()
        {
            if (sub_dzArrayList == null)
                sub_dzArrayList = new ArrayList();
            for (int i = 0; i < sub_dzArrayList.Count; i++)
            {
                testdzClass tt_testdzClass = (testdzClass)sub_dzArrayList[i];
                tt_testdzClass.reinit();
            }
        }
    }

    ///// <summary>
    ///// 测试项动作步进类
    ///// </summary>
    //[Serializable]
    //public class testdz_bjClass : asbaseClass
    //{
    //    public string stepAir = "";
    //    public string stepDesc = "";
    //    public string performReuslt = "";

    //    public void IgnoreStep()
    //    {
    //        performReuslt = "忽略";
    //    }
    //    public void RightStep()
    //    {
    //        performReuslt = "正常";
    //    }
    //    public void ErroStep()
    //    {
    //        performReuslt = "错误";
    //    }
    //}

    /// <summary>
    /// 测试项判断类
    /// </summary>
    [Serializable]
    public class testpdClass : asbaseClass
    {
        /// <summary>
        /// 0为最高等级
        /// </summary>
        public byte pdLevel = 0;
        public string pdObject = "";
        public string pdStandard = "";
        public string pdValue = "";
        public string pdResult = "";
    }


    [Serializable]
    public class dcfClass : asbaseClass
    {
        public string codeName;
        public string simpleName;
        public string dwValueString;
        public string descString;
        public string fkdw;
        public string dcfbh;
    }


    [Serializable]
    public class dwClass : asbaseClass
    {
        public string codeName;
        public string simpleName;
        public string dwValueString;
        public string descString;
    }


    [Serializable]
    public class cgq_clzClass : asbaseClass
    {
        public string codeName;
        public string simpleName;
        public string dwValueString;
        public string descString;
    }

    [Serializable]
    public class szzClass : asbaseClass
    {
        public string codeName;
        public string simpleName;
        public string dwValueString;
        public string descString;
    }


    /// <summary>
    /// 序列化,反序列化
    /// </summary>
    public static class SerializableClass
    {
        public static string eeMessage = "";
        public static void toSerialize(object myobj, string paths)
        {
            try
            {
                FileStream fs = new FileStream(paths, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, myobj);
                    fs.Close();
                    fs.Dispose();
                    return;
                }
                catch (Exception ee)
                {
                    eeMessage = ee.Message;
                    fs.Close();
                    fs.Dispose();
                    return;
                }
            }
            catch (Exception eeee)
            {
                eeMessage = eeee.Message;
                return;
            }
        }
        public static object toDeserialize(string paths)
        {
            object myobj = null;
            try
            {
                FileStream fs = new FileStream(paths, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    myobj = formatter.Deserialize(fs);
                }
                catch (Exception ee)
                {
                    eeMessage = ee.Message;
                    fs.Close();
                    fs.Dispose();
                    //MessageBox.Show(ee.Message);
                    return myobj;
                }
                fs.Close();
                fs.Dispose();
                return myobj;
            }
            catch (Exception eeee)
            {
                eeMessage = eeee.Message;
                return myobj;
            }
        }
    }


    [Serializable()]
    public abstract class asbaseClass : ICloneable
    {
        #region ICloneable 成员

        public object Clone()
        {
            object newObject = Activator.CreateInstance(this.GetType());
            //我们取得新的类型实例的字段数组。    
            FieldInfo[] fields = newObject.GetType().GetFields();
            int i = 0;
            foreach (FieldInfo fi in this.GetType().GetFields())
            {
                //我们判断字段是否支持ICloneable接口。      
                Type ICloneType = fi.FieldType.
                    GetInterface("ICloneable", true);
                if (ICloneType != null)
                {
                    //取得对象的Icloneable接口。      
                    ICloneable IClone = (ICloneable)fi.GetValue(this);
                    //我们使用克隆方法给字段设定新值。 
                    if (IClone != null)
                        fields[i].SetValue(newObject, IClone.Clone());
                }
                else
                {
                    // 如果该字段部支持Icloneable接口，直接设置即可。 
                    fields[i].SetValue(newObject, fi.GetValue(this));
                }             //现在我们检查该对象是否支持IEnumerable接口，如果支持，    
                //我们还需要枚举其所有项并检查他们是否支持IList 或 IDictionary 接口。      
                Type IEnumerableType = fi.FieldType.GetInterface("IEnumerable", true);
                if (IEnumerableType != null)
                {              //取得该字段的IEnumerable接口       
                    IEnumerable IEnum = (IEnumerable)fi.GetValue(this);
                    //这个版本支持IList 或 IDictionary 接口来迭代集合。        
                    Type IListType = fields[i].FieldType.GetInterface
                        ("IList", true);
                    Type IDicType = fields[i].FieldType.GetInterface
                        ("IDictionary", true);
                    int j = 0;
                    if (IListType != null && IEnum!=null)
                    {		  //取得IList接口。          
                        IList list = (IList)fields[i].GetValue(newObject);
                        foreach (object obj in IEnum)
                        {
                            //查看当前项是否支持支持ICloneable 接口。       
                            ICloneType = obj.GetType().GetInterface("ICloneable", true);
                            if (ICloneType != null)
                            {		          //如果支持ICloneable 接口，		
                                //我们用它李设置列表中的对象的克隆		
                                ICloneable clone = (ICloneable)obj;
                                list[j] = clone.Clone();
                            }
                            //注意：如果列表中的项不支持ICloneable接口，那么      
                            //在克隆列表的项将与原列表对应项相同                
                            //（只要该类型是引用类型）               
                            j++;
                        }
                    }
                    else if (IDicType != null)
                    {		  //取得IDictionary 接口    
                        IDictionary dic = (IDictionary)fields[i].GetValue(newObject);
                        j = 0;
                        foreach (DictionaryEntry de in IEnum)
                        {                     //查看当前项是否支持支持ICloneable 接口。     
                            ICloneType = de.Value.GetType().GetInterface("ICloneable", true);
                            if (ICloneType != null)
                            {
                                ICloneable clone = (ICloneable)de.Value;
                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }

        #endregion

        #region ICloneable 成员
        public object Clones(params object[] demoob)
        {
            object newObject = Activator.CreateInstance(this.GetType(), demoob);
            //我们取得新的类型实例的字段数组。    
            FieldInfo[] fields = newObject.GetType().GetFields();
            int i = 0;
            foreach (FieldInfo fi in this.GetType().GetFields())
            {
                //我们判断字段是否支持ICloneable接口。      
                Type ICloneType = fi.FieldType.
                    GetInterface("ICloneable", true);
                if (ICloneType != null)
                {
                    //取得对象的Icloneable接口。      
                    ICloneable IClone = (ICloneable)fi.GetValue(this);
                    //我们使用克隆方法给字段设定新值。        
                    fields[i].SetValue(newObject, IClone.Clone());
                }
                else
                {
                    // 如果该字段部支持Icloneable接口，直接设置即可。 
                    fields[i].SetValue(newObject, fi.GetValue(this));
                }             //现在我们检查该对象是否支持IEnumerable接口，如果支持，    
                //我们还需要枚举其所有项并检查他们是否支持IList 或 IDictionary 接口。      
                Type IEnumerableType = fi.FieldType.GetInterface("IEnumerable", true);
                if (IEnumerableType != null)
                {              //取得该字段的IEnumerable接口       
                    IEnumerable IEnum = (IEnumerable)fi.GetValue(this);
                    //这个版本支持IList 或 IDictionary 接口来迭代集合。        
                    Type IListType = fields[i].FieldType.GetInterface
                        ("IList", true);
                    Type IDicType = fields[i].FieldType.GetInterface
                        ("IDictionary", true);
                    int j = 0;
                    if (IListType != null)
                    {		  //取得IList接口。          
                        IList list = (IList)fields[i].GetValue(newObject);
                        foreach (object obj in IEnum)
                        {
                            //查看当前项是否支持支持ICloneable 接口。       
                            ICloneType = obj.GetType().GetInterface("ICloneable", true);
                            if (ICloneType != null)
                            {		          //如果支持ICloneable 接口，		
                                //我们用它李设置列表中的对象的克隆		
                                ICloneable clone = (ICloneable)obj;
                                list[j] = clone.Clone();
                            }
                            //注意：如果列表中的项不支持ICloneable接口，那么      
                            //在克隆列表的项将与原列表对应项相同                
                            //（只要该类型是引用类型）               
                            j++;
                        }
                    }
                    else if (IDicType != null)
                    {		  //取得IDictionary 接口    
                        IDictionary dic = (IDictionary)fields[i].GetValue(newObject);
                        j = 0;
                        foreach (DictionaryEntry de in IEnum)
                        {                     //查看当前项是否支持支持ICloneable 接口。     
                            ICloneType = de.Value.GetType().GetInterface("ICloneable", true);
                            if (ICloneType != null)
                            {
                                ICloneable clone = (ICloneable)de.Value;
                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }
        #endregion
    }

}
