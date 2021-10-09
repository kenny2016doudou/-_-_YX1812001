using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SchemeManage.Model.Entity.SchemeModule;
using TestSystem.Common.Constant;
using SchemeManage.DataAccess.Interface.SchemeModule;
using SchemeManage.DataAccess;
using SchemeManage.BusinessRule.Interface.SchemeModule;
using System.Threading;

namespace ZKZDLQ.SystemTest
{
    public partial class frmReportDataSave : CSharpWin.SkinForm
    {
        SchemeInfo _schemeInfo = null;
        private ISchemeTable bll_SchemeTable = BLL_Reference<ISchemeTable>.CreateObj("SchemeTable");
        private ISchemeInfo bll_SchemeInfo = BLL_Reference<ISchemeInfo>.CreateObj("SchemeInfo");
        private ISchemeColumn bll_SchemeColumn = BLL_Reference<ISchemeColumn>.CreateObj("SchemeColumn");
        private ISchemeRow bll_SchemeRow = BLL_Reference<ISchemeRow>.CreateObj("SchemeRow");

        private string str_parentid="";


        public frmReportDataSave()
        {
            InitializeComponent();
            _schemeInfo = bll_SchemeInfo.SelectSchemeInfo(StaticModule.Scheme_ID, "", "");
            Control.CheckForIllegalCrossThreadCalls = false;
            //GetData();
        }

        public frmReportDataSave(string id)
        {
            InitializeComponent();
            _schemeInfo = bll_SchemeInfo.SelectSchemeInfo(StaticModule.Scheme_ID, "", "");
            Control.CheckForIllegalCrossThreadCalls = false;
            str_parentid = id;
            //GetData();
        }

        private void frmReportDataSave_Load(object sender, EventArgs e)
        {

            Bind();

        }


        void Bind()
        {
            DataTable dt_have = GetData();


            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("检测检修项目");
            dt.Columns.Add("检修标准");
            dt.Columns.Add("检修结果");
            dt.Columns.Add("处理意见");
            dt.Columns.Add("自检");
            dt.Columns.Add("互检员");
            dt.Columns.Add("测试数据ID");


            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("检查电气连接及其固定状态", "检查各紧固件应无松动现象,检查所有螺纹紧固件的防松标记应无错位现象。如有松动，必须按照拧紧力矩紧固。");

            dic.Add("外观检查1", "1.清洁瓷瓶，各绝缘子表面应无破损和裂纹。瓷绝缘子表面光洁，安装牢固，表面缺损须进行绝缘处理。");

            dic.Add("外观检查2", "2.金属法兰盘不许有裂纹、锈蚀，螺纹完好，与绝缘体浇铸牢固，不许有裂缝、掉块现象。高压连接铜排无变形，表面镀层无脱落现象，否则进行更新或重新进行表面处理。");

            dic.Add("外观检查3", "3.检测高压输入端与高压输出端之间的接触电阻应小于200µΩ。否则对主断路器上支持绝缘子更新。");

            dic.Add("气路系统", "检查气路系统的气管无老化现象，管路接头及气动元件无漏风现象，更换有缺陷的气管、接头等气动元件。");

            dic.Add("电路系统1", "1.检查电路系统的插座及辅助联锁主触头系统各部件表面清洁，不许有裂损、变形。");

            dic.Add("电路系统2", "2.电缆绝缘层无破损或老化，如有则更换，接线端子无松动，如有则重新拧紧。");

            dic.Add("电路系统3", "3.断路器控制顺序正确，不允许有误动作，联锁主触头接触良好，通断正确。");

            dic.Add("主触头磨损", "测量主触头的磨损量，如果磨损量大于2.5毫米，更换上绝缘子总成。");

            dic.Add("传动气缸", "检查传动气缸底部缓冲垫无破损，如有破损及缓冲失效，应更换，清洁传动风缸，补充润滑脂，传动气缸活塞动作灵活顺畅，不许有卡滞现象。");

            dic.Add("接地刀夹1", "1.检查接地夹无损坏，与接地开关配合良好。");

            dic.Add("接地刀夹2", "2.检查接地夹是否有结瘤出现（ 如果小于1毫米，用锉刀修平并润滑接地夹。 如果超过1毫米，更换并润滑接地夹）。");

            dic.Add("接地刀夹3", "3.如果刀夹两夹片之间距离大于9.2mm，更换接地刀夹。");

            dic.Add("干燥通风试验", "将1L储风缸空气压力调到3bar，并将它连接到断路器的干燥通风孔，关闭储风缸风源，储风缸压力应有明显下降，并能听到断路器下绝缘子内有气流声。");

            dic.Add("动作试验1", "输入空气压力调至1000kPa，供电电源分别调至直流110V、77V、137.5V，各操作断路器动作10周期，断路器动作正常。");

            dic.Add("动作试验2", "将输入空气压力调至450kPa，供电电源分别调至直流110V、77V、137.5V，各操作断路器动作10周期，断路器动作正常。");

            dic.Add("气密性能试验", "1.将节流阀出口端封闭，再连接上1L储风缸，空气压力调到900kPa，将输出电压调到直流77V，闭合主触头，关闭储风缸风源进行保压，要求10分钟储风缸内的空气压力下降不超过90kPa；2 断开主触头，将1L储风缸空气压力调到900kPa，关闭储风缸风源进行保压，要求10分钟储风缸内的空气压力下降不超过90kPa。");

            dic.Add("断路器开断及闭合时间试验", "在额定控制电压（110V DC）、额定工作气压（450～1000kPa）下，主断路器分闸时间：t1≤40ms；合闸时间：t1≤100ms；主触头弹跳时间：t4≤10ms 8%。");


            dic.Add("绝缘电阻测量1", "用1000V兆欧表测量低压回路和地（底板）之间的绝缘电阻，要求其值大于10MΩ。");

            dic.Add("绝缘电阻测量2", "用2500V兆欧表测量高压回路和地之间的绝缘电阻，要求其值大于500MΩ。");

            dic.Add("绝缘电阻测量3", "用1000V兆欧表测量两主触头之间的绝缘电阻，要求其值大于200MΩ。");

            dic.Add("工频耐压试验1", "断路器主触头极间及主回路对地应能承受工频试验电压56kV、1min，无击穿或闪络现象。");

            dic.Add("工频耐压试验2", "断路器的低压电路对地应能承受工频试验电压1.5kV、1min，无击穿或闪络现象。");


            dic.Add("接触电阻试验", "给500A电流源测试接触电阻。");
            
            int i = 0;
            foreach (var d in dic)
            {
                i++;
                DataRow dr = dt.NewRow();
                dr["序号"] = i;

                dr["检测检修项目"] = d.Key;

                dr["检修标准"] = d.Value;
                for (int k = 0; k < dt_have.Rows.Count; k++)
                {
                    if (dt_have.Rows[k]["检测检修项目"].ToString() == d.Key)
                    {
                        dr["检修结果"] = dt_have.Rows[k]["检修结果"].ToString();
                        dr["处理意见"] = dt_have.Rows[k]["处理意见"].ToString();
                        dr["自检"] = dt_have.Rows[k]["自检"].ToString();
                        dr["互检员"] = dt_have.Rows[k]["互检员"].ToString();
                        dr["测试数据ID"] = dt_have.Rows[k]["测试数据ID"].ToString();
                    }

                }

                dt.Rows.Add(dr);
            }
            //  this.dataGridView1.EndEdit();
            // this.dataGridView1.DataSource = null;


            this.dataGridView1.DataSource = dt;


        }







        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ISchemeRowBO schemeRowBO = new SchemeManage.BusinessRule.SchemeModule.SchemeRowBO();
            DataTable dt= schemeRowBO.SelectData("TestData");
            Bind();
        }


        private DataTable GetData()
        {


            BindColum();
            DataTable dt = dvtodt(this.dataGridView2);

            return dt;
        }


        private void BindColum()
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView2.Columns.Clear();
            SchemeTable _schemeTable = bll_SchemeTable.SelectSchemeTable(StaticModule.Scheme_ID, "", "", "TestData", "");
            DataSet dsSchemeCol = bll_SchemeColumn.SelectSchemeColumn(_schemeTable.Table_ID, "", "", "");

            //测试数据表名
            string TestDataTName = _schemeInfo.Scheme_Code + "_" + _schemeTable.Table_Code;

            if (dsSchemeCol.Tables[0].Rows.Count > 0)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "id";
                column.HeaderText = "id";
                column.Name = "id";
                column.DataPropertyName = "id";
                column.ReadOnly = true;
                column.Visible = false;
                column.Width = 150;
                this.dataGridView2.Columns.Add(column);

                DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                column2.DataPropertyName = "parent_id";
                column2.HeaderText = "parent_id";
                column2.Name = "parent_id";
                column2.DataPropertyName = "parent_id";
                column2.ReadOnly = true;
                column2.Visible = false;
                column2.Width = 150;
                this.dataGridView2.Columns.Add(column2);

                for (int i = 0; i < dsSchemeCol.Tables[0].Rows.Count; i++)
                {
                    DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                    column1.DataPropertyName = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString();
                    column1.HeaderText = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString();
                    column1.Name = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString();
                    column1.DataPropertyName = dsSchemeCol.Tables[0].Rows[i]["Col_Code"].ToString();
                    column1.ReadOnly = true;
                    column1.Width = 150;

                    this.dataGridView2.Columns.Add(column1);

                }


                string colCode = "Parent_ID";
                string sQuery = GetParentID();
                DataSet ds = bll_SchemeRow.SelectData(TestDataTName, colCode, sQuery, "", "");
                dataGridView2.DataSource = ds.Tables[0].DefaultView;


            }
        }

        private string GetParentID()
        {
            SchemeTable _schemeTable = bll_SchemeTable.SelectSchemeTable(StaticModule.Scheme_ID, "", "", "BaseInfo", "");
            string baseInfo_TableName = _schemeInfo.Scheme_Code + "_" + _schemeTable.Table_Code;
            DataTable dt = bll_SchemeRow.SelectData(baseInfo_TableName, "", "", "", "").Tables[0];

            DataSet dsSchemeCol = bll_SchemeColumn.SelectSchemeColumn(_schemeTable.Table_ID, "", "", "");
            DataTable dt_2 = new DataTable();
            for (int i = 0; i < dsSchemeCol.Tables[0].Rows.Count; i++)
            {
                dt_2.Columns.Add(dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString(), typeof(string));
                
            }
            dt_2.Columns.Add("id");

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow newDR = dt_2.NewRow();

                for (int i = 0; i < dsSchemeCol.Tables[0].Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {

                        if (dt.Columns[k].ToString() == dsSchemeCol.Tables[0].Rows[i]["Col_Code"].ToString())
                        {
                            newDR[dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString()] = dt.Rows[j][k].ToString();
                            break;
                        }
                    }

                }
                newDR["id"] = dt.Rows[j]["id"].ToString();
                dt_2.Rows.Add(newDR);
            }

            if (str_parentid == "")
            {
                str_parentid = (from r in dt.AsEnumerable() select r.Field<Int32>("id")).Max().ToString();

            }
            
          

            //this.lbl_cxch.Text = (from e in dt_2.AsEnumerable() where e.Field<string>("id") == str_parentid select e).First().Field<string>("拆下车号");
            //this.lbl_zdlq.Text = (from e in dt_2.AsEnumerable() where e.Field<string>("id") == str_parentid select e).First().Field<string>("主断路器编号");
            return str_parentid;
        }

        private DataTable dvtodt(DataGridView dv)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            for (int i = 0; i < dv.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = dv.Columns[i].HeaderText.ToString();
                dt.Columns.Add(dc);
            }
            for (int j = 0; j < dv.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                for (int x = 0; x < dv.Columns.Count; x++)
                {
                    if (dv.Rows[j].Cells[x].Value is DateTime)
                    {
                        dr[x] = DateTime.Parse(dv.Rows[j].Cells[x].Value.ToString()).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dr[x] = dv.Rows[j].Cells[x].Value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            SaveData();
            //try
            //{
            //    MessageBox.Show(this.dataGridView1.CurrentCell.Value.ToString());
            //}
            //catch
            //{
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "自检")
                {
                    if (dataGridView1.CurrentRow.Cells["检修结果"].Value.ToString() == "" || dataGridView1.CurrentRow.Cells["处理意见"].Value.ToString() == "")
                    {
                        MessageBox.Show("尚未填写检修结果和处理意见！");
                    }
                    else
                    {
                        this.dataGridView1.CurrentCell.Value = StaticModule.UserName;
                        SaveData();
                    }

                }


                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "互检员")
                {
                    if (dataGridView1.CurrentRow.Cells["检修结果"].Value.ToString() == "" || dataGridView1.CurrentRow.Cells["处理意见"].Value.ToString() == "")
                    {
                        MessageBox.Show("尚未填写检修结果和处理意见！");
                    }
                    else
                    {
                        if (dataGridView1.CurrentRow.Cells["自检"].Value.ToString() != "" && StaticModule.UserName != dataGridView1.CurrentRow.Cells["自检"].Value.ToString())
                        {
                            this.dataGridView1.CurrentCell.Value = StaticModule.UserName;
                            SaveData();
                        }
                        else
                        {
                            MessageBox.Show("无法录入互检人员！请检查自检是否已经存在！");
                        }
                    }

                }

            }
        }


        private void SaveData()
        {
            ISchemeRowBO schemeRowBO = new SchemeManage.BusinessRule.SchemeModule.SchemeRowBO();
            object id = this.dataGridView1.CurrentRow.Cells["测试数据ID"].Value;
            if (id.ToString() == "")
            {

                
                if (str_parentid != "")
                {
                    string[] argColName = new string[7];
                    object[] argCloContent = new object[7];
                    argColName[0] = "检测检修项目";
                    argCloContent[0] = this.dataGridView1.CurrentRow.Cells["检测检修项目"].Value;

                    argColName[1] = "检修结果";
                    argCloContent[1] = this.dataGridView1.CurrentRow.Cells["检修结果"].Value;


                    argColName[2] = "处理意见";
                    argCloContent[2] = this.dataGridView1.CurrentRow.Cells["处理意见"].Value;

                    argColName[3] = "自检";
                    argCloContent[3] = this.dataGridView1.CurrentRow.Cells["自检"].Value;

                    argColName[4] = "互检员";
                    argCloContent[4] = this.dataGridView1.CurrentRow.Cells["互检员"].Value;


                    argColName[5] = "测试数据ID";
                    argCloContent[5] = Guid.NewGuid().ToString();


                    argColName[6] = "parent_id";
                    argCloContent[6] = str_parentid;

                    this.dataGridView1.CurrentRow.Cells["测试数据ID"].Value = argCloContent[5];

                    schemeRowBO.SaveData("TestData", argColName, argCloContent, false);
                }
                else
                {
                    string[] argColName = new string[6];
                    object[] argCloContent = new object[6];
                    argColName[0] = "检测检修项目";
                    argCloContent[0] = this.dataGridView1.CurrentRow.Cells["检测检修项目"].Value;

                    argColName[1] = "检修结果";
                    argCloContent[1] = this.dataGridView1.CurrentRow.Cells["检修结果"].Value;


                    argColName[2] = "处理意见";
                    argCloContent[2] = this.dataGridView1.CurrentRow.Cells["处理意见"].Value;

                    argColName[3] = "自检";
                    argCloContent[3] = this.dataGridView1.CurrentRow.Cells["自检"].Value;

                    argColName[4] = "互检员";
                    argCloContent[4] = this.dataGridView1.CurrentRow.Cells["互检员"].Value;


                    argColName[5] = "测试数据ID";
                    argCloContent[5] = Guid.NewGuid().ToString();

                    this.dataGridView1.CurrentRow.Cells["测试数据ID"].Value = argCloContent[5];

                    schemeRowBO.SaveData("TestData", argColName, argCloContent, true);
                }

            }
            else
            {
                string[] argColName = new string[5];
                string[] argCloContent = new string[5];

                argColName[0] = "检修结果";
                argCloContent[0] = this.dataGridView1.CurrentRow.Cells["检修结果"].Value.ToString();

                argColName[1] = "处理意见";
                argCloContent[1] = this.dataGridView1.CurrentRow.Cells["处理意见"].Value.ToString();

                argColName[2] = "自检";
                argCloContent[2] = this.dataGridView1.CurrentRow.Cells["自检"].Value.ToString();

                argColName[3] = "互检员";
                argCloContent[3] = this.dataGridView1.CurrentRow.Cells["互检员"].Value.ToString();

                argColName[4] = "测试数据ID";
                argCloContent[4] = this.dataGridView1.CurrentRow.Cells["测试数据ID"].Value.ToString();

                schemeRowBO.ModifyData("testdata", "测试数据ID", argColName, argCloContent);

            }
        }




    }
}
