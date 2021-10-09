using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.BusinessRule.Interface.SchemeModule;
using TestSystem.Model.Entity.SchemeModule;
using TestSystem.BusinessRule.SchemeModule;

namespace TestSystem.FormManage
{
    public class LoadUC
    {
        public static UserControl GetUC(string dll_name, string control_name)
        {
            string asmfile = System.IO.Path.GetFullPath(dll_name+".dll");
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(asmfile);

            Type type = asm.GetType(dll_name + "." + control_name);
            UserControl uc = (UserControl)System.Activator.CreateInstance(type);
         
            return uc;
        }

        public static UserControl GetUC(string dll_name, string control_name,Form frm)
        {
            string asmfile = System.IO.Path.GetFullPath(dll_name + ".dll");
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(asmfile);

            Type type = asm.GetType(dll_name + "." + control_name);
            UserControl uc = (UserControl)System.Activator.CreateInstance(type,frm);
        
            return uc;
        }
    }
}
