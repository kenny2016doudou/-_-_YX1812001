using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.DataAccess.Interface.SchemeModule;
using TestSystem.DataAccess;
using TestSystem.Model.Entity.SchemeModule;
using TestSystem.Common.Constant;

namespace TestSystem.FormManage
{
    public partial class frm_Loading : Form
    {
        ISchemeInfo bll_SchemeInfo = BLL_Reference<ISchemeInfo>.CreateObj("SchemeInfo");
        public frm_Loading()
        {
            InitializeComponent();
            SchemeInfo _SchemeInfo = bll_SchemeInfo.SelectSchemeInfo(StaticModule.Scheme_ID, "", "");
            string schemeName = _SchemeInfo.Scheme_Name;
            this.lbl_title.Text = schemeName;
        }

       
    }
}
