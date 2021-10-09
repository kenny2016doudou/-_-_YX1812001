using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TestSystem.Common;
using System.Collections;
using TestSystem.BusinessRule.Interface.SchemeModule;
using TestSystem.BusinessRule.SchemeModule;
using TestSystem.Model.Entity.SchemeModule;

namespace TestSystem.FormManage
{
    public class UserControlManager
    {
        private static string xmlfile = Application.StartupPath + @"\UserControls.xml";
        static ISchemeUCBO  schemeUCBO = new SchemeUCBO();       

        /// <summary>
        /// 系统设置界面是否使用默认模板 是则返回true 否则返回false
        /// </summary>
        /// <returns></returns>
        public static bool IsDefautSystemSet()
        {
            SchemeUC uc = schemeUCBO.SelectSchemeUC(ControlType.用户管理.ToString());
            string Namespace = uc.Namespace; //命名空间
            string ClassName = uc.ClassName; //类名

            if (ClassName == "Default")
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 数据查询界面是否使用默认模板 是则返回true 否则返回false
        /// </summary>
        /// <returns></returns>
        public static bool IsDefautDataQuery()
        {
            SchemeUC uc = schemeUCBO.SelectSchemeUC(ControlType.数据查询.ToString());
            string Namespace = uc.Namespace; //命名空间
            string ClassName = uc.ClassName; //类名

            if (ClassName == "Default")
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 基本信息界面是否使用默认模板 是则返回true 否则返回false
        /// </summary>
        /// <returns></returns>
        public static bool IsDefautBaseInfo()
        {
            XmlNode txn = XMLHelper.GetNode(xmlfile, "BaseInfo");
            XmlNodeList nodelist = txn.ChildNodes[0].ChildNodes;
            if (nodelist.Count == 1)
            {
                if (nodelist[0].Attributes["value"].Value == "Default")
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 获得主测试界面用户控件
        /// </summary>
        /// <returns></returns>
        public static UserControl [] GetUCList(ControlType enumvalue)
        {
         
            string dllname = "";
            SchemeUC uc = schemeUCBO.SelectSchemeUC(enumvalue.ToString());
            string Namespace = uc.Namespace; //命名空间
            string ClassName = uc.ClassName; //类名

            dllname = Namespace;

            UserControl[] maintest = new UserControl[1];
            for (int i = 0; i < maintest.Length; i++)
            {
                maintest[i] = LoadUC.GetUC(dllname, ClassName);
            }
            return maintest;
        }


        public static bool IsHaveMenuBy(ControlType enumvalue)
        {

            SchemeUC uc = schemeUCBO.SelectSchemeUC(enumvalue.ToString());
            if (uc == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public enum ControlType
    {
        使用指南 = 1,//
        数据查询 = 2,//
        参数设置 = 3,//
        用户管理 = 4,//
        系统设置 = 5,//
        测试 = 6

    };


}
