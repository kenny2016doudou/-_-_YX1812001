using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSystem.DataAccess.Interface.SchemeModule;
using TestSystem.DataAccess;
using TestSystem.Common.Constant;
using TestSystem.Model.Entity.SchemeModule;
using TestSystem.Common;
using TestSystem.BusinessRule.Interface.SchemeModule;
using usefulClass;
using System.Collections;
using ZKZDLQ.SystemTest;
//using TestSystem.Command.PLC.Omron.HostLink.Fins

namespace TestSystem.FormManage
{
    public partial class UC_DataForm : UserControl
    {

        public ArrayList xhallArray = new ArrayList();
        public xhdyClass runxhdyClass = null;

        fzcdCellClass ttfzcdCellClass = null;
        string tempstr = "";
        string[] firstsp = { "#" };
        string[] secondsp = { ":" };

        private ISchemeTable bll_SchemeTable = BLL_Reference<ISchemeTable>.CreateObj("SchemeTable");
        private ISchemeInfo bll_SchemeInfo = BLL_Reference<ISchemeInfo>.CreateObj("SchemeInfo");
        private ISchemeColumn bll_SchemeColumn = BLL_Reference<ISchemeColumn>.CreateObj("SchemeColumn");
        private ISchemeRow bll_SchemeRow = BLL_Reference<ISchemeRow>.CreateObj("SchemeRow");
        private DataTable dt_grid1;

        ISchemeRowBO schemeRowBO = new TestSystem.BusinessRule.SchemeModule.SchemeRowBO();

        string baseInfo_TableName = "";
        string baseInfo_TableID = "";
        SchemeInfo _schemeInfo = null;
        public UC_DataForm()
        {

            InitializeComponent();
            _schemeInfo = bll_SchemeInfo.SelectSchemeInfo(StaticModule.Scheme_ID, "", "");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;

        }

        private void UC_DataForm_Load(object sender, EventArgs e)
        {


            SchemeTable _schemeTable = bll_SchemeTable.SelectSchemeTable(StaticModule.Scheme_ID, "", "", "BaseInfo", "");

            baseInfo_TableID = _schemeTable.Table_ID;
            baseInfo_TableName = _schemeInfo.Scheme_Code + "_" + _schemeTable.Table_Code;




            DataSet ds = bll_SchemeColumn.SelectSchemeColumn(baseInfo_TableID, "", "", "");

            if (cmbQueryType.DataSource != null)
                cmbQueryType.DataSource = null;
            cmbQueryType.DataSource = ds.Tables[0].DefaultView;
            cmbQueryType.ValueMember = "Col_Code";
            cmbQueryType.DisplayMember = "Col_Name";


            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";


            if (DirectPrintClass.ifDirect)
            {
                this.FindForm().Visible = false;
                cmbQueryType.Text = DirectPrintClass.querytype;
                cmbQueryStr.Text = DirectPrintClass.queryStr;
                btnQuery_Click(sender, e);

                if (dataGridView1.Rows.Count >= 1)
                {
                    dataGridView1_CellClick(sender, new DataGridViewCellEventArgs(0, 0));
                }
                if (dataGridView2.Rows.Count >= 1)
                {
                    dataGridView2.Rows[0].Selected = true;
                    btnPrintPrv_Click(sender, e);
                }
                timer1.Enabled = true;
            }
        }

        private void cmbQueryType_TextChanged(object sender, EventArgs e)
        {
            string colCode = (cmbQueryType.SelectedValue == null) ? "" : cmbQueryType.SelectedValue.ToString();

            SchemeColumn _SchemeCol = bll_SchemeColumn.SelectSchemeColumnObj("", "", "", colCode);
            if (_SchemeCol != null)
            {
                string colType = _SchemeCol.Col_Type;

                if (colType == "日期")
                {
                    cmbQueryStr.Text = "";
                    cmbQueryStr.Visible = false;

                    this.dateTimePicker1.Visible = true;
                    this.dateTimePicker2.Visible = true;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    this.lblZhi.Visible = true;
                }
                else
                {

                    cmbQueryStr.DataSource = null;
                    if (!ObjectValidate.IsNullOrNone(cmbQueryType.SelectedValue))
                    {
                        DataSet ds = bll_SchemeRow.SelectRowContent(baseInfo_TableName, cmbQueryType.SelectedValue.ToString()); //根据新建表名查询数据
                        if (ds != null)
                        {
                            cmbQueryStr.DataSource = ds.Tables[0];
                            cmbQueryStr.DisplayMember = "" + colCode;
                            cmbQueryStr.ValueMember = "" + colCode;
                        }

                    }
                    cmbQueryStr.Visible = true;



                    this.dateTimePicker1.Visible = false;
                    this.dateTimePicker2.Visible = false;
                    dateTimePicker1.Enabled = false;
                    dateTimePicker2.Enabled = false;
                    this.lblZhi.Visible = false;


                }
            }
        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Columns.Clear();

            DataSet dsSchemeCol = bll_SchemeColumn.SelectSchemeColumn(baseInfo_TableID, "", "", "");

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
                this.dataGridView1.Columns.Add(column);

                for (int i = 0; i < dsSchemeCol.Tables[0].Rows.Count; i++)
                {
                    DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                    column1.DataPropertyName = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString(); ;
                    column1.HeaderText = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString(); ;
                    column1.Name = dsSchemeCol.Tables[0].Rows[i]["Col_Name"].ToString(); ;
                    column1.DataPropertyName = dsSchemeCol.Tables[0].Rows[i]["Col_Code"].ToString();
                    column1.ReadOnly = true;
                    column1.Width = 150;

                    this.dataGridView1.Columns.Add(column1);

                }

                //填充表格
                string colCode = (cmbQueryType.SelectedValue == null) ? "" : cmbQueryType.SelectedValue.ToString();
                if (colCode != "")
                {

                    string sQuery = this.cmbQueryStr.Text.Trim();
                    string t1 = "";
                    string t2 = "";

                    if (sQuery == "")
                    {
                        if (dateTimePicker1.Enabled)
                        {
                            t1 = dateTimePicker1.Text.ToString();
                            t2 = dateTimePicker2.Text.ToString();
                        }

                    }

                    DataSet ds = bll_SchemeRow.SelectData(baseInfo_TableName, colCode, sQuery, t1, t2);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    dt_grid1 = ds.Tables[0];
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
                string sQuery = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                DataSet ds = bll_SchemeRow.SelectData(TestDataTName, colCode, sQuery, "", "");
                dataGridView2.DataSource = ds.Tables[0].DefaultView;


            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DispalyDataBase(false);
        }
        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintPrv_Click(object sender, EventArgs e)
        {
            DispalyDataBase(true);
        }

        public void DispalyDataBase(bool a)
        {
            InsertPictureToExcel ipt = new InsertPictureToExcel();
            GoldPrinter.ExcelAccess excel = new GoldPrinter.ExcelAccess();
            string picpathhz = "";
            string picpath1fz = "";
            try
            {
                if((dataGridView2.CurrentRow != null)&&(dataGridView1.CurrentRow.Cells["主断路器型号"].Value.ToString() != "SS3"))
                {
                    string imageUrlHZ = this.dataGridView2.CurrentRow.Cells["合闸图形"].Value.ToString();
                    string imageUrlFZ = this.dataGridView2.CurrentRow.Cells["分闸图形"].Value.ToString();

                    string strFileName = "真空主断路器试验记录模版3.xls";//报表模板文件路径
                    string strExcelTemplateFile = Application.StartupPath + @"\Report\" + strFileName;//完整的路径地址  + "\\" 
                    //ipt.Open(strExcelTemplateFile);

                    ////int p = 15;


                    //string rangeName = "A34";//+ p.ToString();
                    //string rangeName2 = "A46"; //+ p.ToString();
                    //try
                    //{

                    //    picpathhz = imageUrlHZ;// Application.StartupPath + @"\picture\" + imageUrlHZ;
                    //    picpath1fz = imageUrlFZ;// Application.StartupPath + @"\picture\" + imageUrlFZ;

                    //    ipt.DeletePicture(rangeName);
                    //    ipt.DeletePicture(rangeName2);
                    //    ipt.InsertPicture(rangeName, picpathhz, 482, 130);

                    //    ipt.InsertPicture(rangeName2, picpath1fz, 482, 130);

                    //    //ipt.DeletePicture(picpathhz);
                    //    //ipt.DeletePicture(picpath1fz);

                    //    //ipt.InsertPicture(rangeName, picpathhz, 482, 227);

                    //    //ipt.InsertPicture(rangeName2, picpath1fz, 482, 227);
                    //    //ipt.InsertPicture(rangeName2, picpath1fz, 450, 297);
                    //    try
                    //    {
                    //        ipt.SaveFile(strExcelTemplateFile);
                    //    }

                    //    catch
                    //    {
                    //    }


                    //}
                    //catch { }
                    //finally
                    //{
                    //    ipt.Dispose();
                    //}
                    //GC.Collect();
                    excel.Open(strExcelTemplateFile);//打开上面保存好的excel文件
                    excel.IsVisibledExcel = false;
                    excel.FormCaption = "真空主断路器试验报告";

                    string xj = dataGridView2.CurrentRow.Cells["分合闸主要检测参数"].Value.ToString();
                    string[] SJJH=xj.Split(':','#');
                    string A0 = SJJH[1];
                    string A1 = SJJH[3];
                    string A2 = SJJH[5];
                    string A3 = SJJH[7];
                    string A4 = SJJH[9];
                    string A5 = SJJH[11];
                    string A6 = SJJH[13];
                    string A7 = SJJH[15];
                    string A8 = SJJH[17];
                    string A9 = SJJH[19];
                    string A10 = SJJH[21];
                    string A11 = SJJH[23];
                    string A12 = SJJH[25];
                   
                 
                    try
                    {

                        excel.SetCellText(4, "B", dataGridView1.CurrentRow.Cells["车号"].Value.ToString());
                        excel.SetCellText(4, "D", dataGridView1.CurrentRow.Cells["主断路器编号"].Value.ToString());
                        excel.SetCellText(4, "F", dataGridView1.CurrentRow.Cells["检测人员"].Value.ToString());
                        excel.SetCellText(4, "H", dataGridView1.CurrentRow.Cells["检测时间"].Value.ToString());


                        excel.SetCellText(6, "G", A0);
                        excel.SetCellText(7, "G", A2);
                        excel.SetCellText(8, "G", A5);
                        excel.SetCellText(9, "G", dataGridView2.CurrentRow.Cells["真空断路器主触头电阻"].Value.ToString()); 
                        excel.SetCellText(11, "G", dataGridView2.CurrentRow.Cells["合闸线圈电阻"].Value.ToString());
                        excel.SetCellText(12, "G", dataGridView2.CurrentRow.Cells["分闸线圈电阻"].Value.ToString());
                        excel.SetCellText(13, "G", dataGridView2.CurrentRow.Cells["操作性能试验"].Value.ToString());
                        excel.SetCellText(10, "G", dataGridView2.CurrentRow.Cells["气压泄漏量"].Value.ToString());
                        //excel.SetCellText(11, "G", A5);
                        //excel.SetCellText(12, "G", A6);
                        //excel.SetCellText(13, "G", A8);
                        //excel.SetCellText(14, "G", dataGridView2.CurrentRow.Cells["性能试验"].Value.ToString());
                        //excel.SetCellText(15, "G", dataGridView2.CurrentRow.Cells["绝缘电阻测量3"].Value.ToString());
                        //excel.SetCellText(29, "H", dataGridView1.CurrentRow.Cells["检测人员"].Value.ToString());
                        //excel.SetCellText(30, "H", dataGridView1.CurrentRow.Cells["检测时间"].Value.ToString());
                        int loopIndex = 0, loopIndex1 = 0;
                        if (dataGridView2.CurrentRow.Cells["辅助触点试验结果"].Value != null && dataGridView2.CurrentRow.Cells["辅助触点试验结果"].Value.ToString().Length > 0)
                        {
                            string[] resstr = dataGridView2.CurrentRow.Cells["辅助触点试验结果"].Value.ToString().Split(firstsp, StringSplitOptions.None);
                            if (resstr.Length > 0)
                            {
                                for (int ri = 0; ri < resstr.Length; ri++)
                                {
                                    if (resstr[ri] == null || resstr[ri].Length <= 0)
                                        continue;

                                    //if (ri % 2 == 0)
                                    //{
                                        string[] ssstr = resstr[ri].Split(secondsp, StringSplitOptions.None);
                                        //if (resstr[ri].CompareTo(dataGridView1.CurrentRow.Cells["辅助触点试验结果"].Value.ToString()) == 0)
                                        //        {
                                        if (ssstr[0].Contains("常开"))
                                        {
                                            excel.SetCellText(16 + loopIndex1, "G", ssstr[0].Substring(8));
                                            //excel.SetCellText(17 + loopIndex, "C", ssstr[1]);
                                            excel.SetCellText(16 + loopIndex1, "H", ssstr[1]);
                                            //excel.SetCellText(17 + loopIndex, "G", ssstr[3]);
                                            //excel.SetCellText(25 + loopIndex, "G", ssstr[0]);
                                            //excel.SetCellText(25 + loopIndex, "H", ssstr[4]);
                                            loopIndex1++;
                                        }

                                        else
                                        {
                                            //string[] ssstr = resstr[ri].Split(secondsp, StringSplitOptions.None);
                                            excel.SetCellText(21 + loopIndex, "G", ssstr[0].Substring(8));
                                            //excel.SetCellText(17 + loopIndex, "C", ssstr[1]);
                                            excel.SetCellText(21 + loopIndex, "H", ssstr[1]);
                                            //excel.SetCellText(17 + loopIndex, "G", ssstr[3]);
                                            //excel.SetCellText(25 + loopIndex, "I", ssstr[0]);
                                            //excel.SetCellText(25 + loopIndex, "J", ssstr[4]);
                                            loopIndex++;
                                        }
                                    //}
                                }

                            }
                        }

                    }
                    catch
                    { }
                    finally
                    {
                        //ipt.Dispose();
                    }
                    //GC.Collect();
                    if (a)
                    {
                        excel.PrintPreview();

                    }
                    else
                    {
                        excel.Print();
                    }
                    if (wjsc)
                    {
                        excel.SaveAs(saveSttr, true);
                        //excel.PrintPreview();
                        //excel.SaveAs(Application.StartupPath + @"\Report\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "zdlq.xls", true);
                    }

                }
                else
                {
                    //MessageBox.Show("试验数据不能为空！！！");
                }
                //空气型
                if (dataGridView1.CurrentRow.Cells["主断路器型号"].Value.ToString() == "SS3")
                {
                    string imageUrlHZ = this.dataGridView2.CurrentRow.Cells["合闸图形"].Value.ToString();
                    string imageUrlFZ = this.dataGridView2.CurrentRow.Cells["分闸图形"].Value.ToString();

                    string strFileName = "空气主断路器试验记录模版3.xls";//报表模板文件路径
                    string strExcelTemplateFile = Application.StartupPath + @"\Report\" + strFileName;//完整的路径地址  + "\\" 
                    //ipt.Open(strExcelTemplateFile);

                    ////int p = 15;


                    //string rangeName = "A34";//+ p.ToString();
                    //string rangeName2 = "A51"; //+ p.ToString();
                    //try
                    //{

                    //    picpathhz = imageUrlHZ;// Application.StartupPath + @"\picture\" + imageUrlHZ;
                    //    picpath1fz = imageUrlFZ;// Application.StartupPath + @"\picture\" + imageUrlFZ;

                    //    ipt.DeletePicture(rangeName);
                    //    ipt.DeletePicture(rangeName2);
                    //    ipt.InsertPicture(rangeName, picpathhz, 482, 130);

                    //    ipt.InsertPicture(rangeName2, picpath1fz, 482, 130);

                    //    //ipt.DeletePicture(picpathhz);
                    //    //ipt.DeletePicture(picpath1fz);

                    //    //ipt.InsertPicture(rangeName, picpathhz, 482, 227);

                    //    //ipt.InsertPicture(rangeName2, picpath1fz, 482, 227);
                    //    //ipt.InsertPicture(rangeName2, picpath1fz, 450, 297);
                    //    try
                    //    {
                    //        ipt.SaveFile(strExcelTemplateFile);
                    //    }

                    //    catch
                    //    {
                    //    }


                    //}
                    //catch { }
                    //finally
                    //{
                    //    ipt.Dispose();
                    //}
                    //GC.Collect();
                    excel.Open(strExcelTemplateFile);//打开上面保存好的excel文件
                    excel.IsVisibledExcel = false;
                    excel.FormCaption = "空气主断路器试验报告";

                    string xj = dataGridView2.CurrentRow.Cells["分合闸主要检测参数"].Value.ToString();
                    string[] SJJH = xj.Split(':', '#');
                    string A0 = SJJH[1];
                    string A1 = SJJH[3];
                    string A2 = SJJH[5];
                    string A3 = SJJH[7];
                    string A4 = SJJH[9];
                    string A5 = SJJH[11];
                    string A6 = SJJH[13];
                    string A7 = SJJH[15];
                    string A8 = SJJH[17];
                    string A9 = SJJH[19];
                    string A10 = SJJH[21];
                    string A11 = SJJH[23];
                    string A12 = SJJH[25];


                    try
                    {

                        excel.SetCellText(4, "B", dataGridView1.CurrentRow.Cells["车号"].Value.ToString());
                        excel.SetCellText(4, "D", dataGridView1.CurrentRow.Cells["主断路器编号"].Value.ToString());
                        excel.SetCellText(4, "F", dataGridView1.CurrentRow.Cells["检测人员"].Value.ToString());
                        excel.SetCellText(4, "H", dataGridView1.CurrentRow.Cells["检测时间"].Value.ToString());


                        excel.SetCellText(6, "G", A0);
                        excel.SetCellText(7, "G", A2);
                        excel.SetCellText(8, "G", A7);
                        excel.SetCellText(9, "G", dataGridView2.CurrentRow.Cells["空气断路器灭弧触头电阻"].Value.ToString());
                        excel.SetCellText(10, "G", dataGridView2.CurrentRow.Cells["空气断路器闸刀电阻"].Value.ToString());
                        excel.SetCellText(12, "G", dataGridView2.CurrentRow.Cells["合闸线圈电阻"].Value.ToString());
                        excel.SetCellText(13, "G", dataGridView2.CurrentRow.Cells["分闸线圈电阻"].Value.ToString());
                        excel.SetCellText(14, "G", dataGridView2.CurrentRow.Cells["操作性能试验"].Value.ToString());
                        excel.SetCellText(11, "G", dataGridView2.CurrentRow.Cells["气压泄漏量"].Value.ToString());
                        //excel.SetCellText(11, "G", A5);
                        //excel.SetCellText(12, "G", A6);
                        //excel.SetCellText(13, "G", A8);
                        //excel.SetCellText(14, "G", dataGridView2.CurrentRow.Cells["性能试验"].Value.ToString());
                        //excel.SetCellText(15, "G", dataGridView2.CurrentRow.Cells["绝缘电阻测量3"].Value.ToString());
                        //excel.SetCellText(29, "H", dataGridView1.CurrentRow.Cells["检测人员"].Value.ToString());
                        //excel.SetCellText(30, "H", dataGridView1.CurrentRow.Cells["检测时间"].Value.ToString());
                        int loopIndex = 0, loopIndex1 = 0;
                        if (dataGridView2.CurrentRow.Cells["辅助触点试验结果"].Value != null && dataGridView2.CurrentRow.Cells["辅助触点试验结果"].Value.ToString().Length > 0)
                        {
                            string[] resstr = dataGridView2.CurrentRow.Cells["辅助触点试验结果"].Value.ToString().Split(firstsp, StringSplitOptions.None);
                            if (resstr.Length > 0)
                            {
                                for (int ri = 0; ri < resstr.Length; ri++)
                                {
                                    if (resstr[ri] == null || resstr[ri].Length <= 0)
                                        continue;

                                    //if (ri % 2 == 0)
                                    //{
                                    string[] ssstr = resstr[ri].Split(secondsp, StringSplitOptions.None);
                                    //if (resstr[ri].CompareTo(dataGridView1.CurrentRow.Cells["辅助触点试验结果"].Value.ToString()) == 0)
                                    //        {
                                    if (ssstr[0].Contains("常开"))
                                    {
                                        excel.SetCellText(17 + loopIndex1, "G", ssstr[0].Substring(8));
                                        //excel.SetCellText(17 + loopIndex, "C", ssstr[1]);
                                        excel.SetCellText(17 + loopIndex1, "H", ssstr[1]);
                                        //excel.SetCellText(17 + loopIndex, "G", ssstr[3]);
                                        //excel.SetCellText(25 + loopIndex, "G", ssstr[0]);
                                        //excel.SetCellText(25 + loopIndex, "H", ssstr[4]);
                                        loopIndex1++;
                                    }

                                    else
                                    {
                                        //string[] ssstr = resstr[ri].Split(secondsp, StringSplitOptions.None);
                                        excel.SetCellText(22 + loopIndex, "G", ssstr[0].Substring(8));
                                        //excel.SetCellText(17 + loopIndex, "C", ssstr[1]);
                                        excel.SetCellText(22 + loopIndex, "H", ssstr[1]);
                                        //excel.SetCellText(17 + loopIndex, "G", ssstr[3]);
                                        //excel.SetCellText(25 + loopIndex, "I", ssstr[0]);
                                        //excel.SetCellText(25 + loopIndex, "J", ssstr[4]);
                                        loopIndex++;
                                    }
                                    //}
                                }

                            }
                        }

                    }
                    catch
                    { }
                    finally
                    {
                        //ipt.Dispose();
                    }
                    //GC.Collect();
                    if (a)
                    {
                        excel.PrintPreview();

                    }
                    else
                    {
                        excel.Print();
                    }
                    if (wjsc)
                    {
                        excel.SaveAs(saveSttr, true);
                      
                    }

                }
                else
                {
                    //MessageBox.Show("试验数据不能为空！！！");
                }
            }


            catch
            {
            }
            finally
            {
                excel.Close();
            }
            //GC.Collect();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ZKZDLQ.SystemTest.frmReportDataSave frm = new ZKZDLQ.SystemTest.frmReportDataSave(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            //frm.ShowDialog();
        }
        public bool wjsc = false;

        private string saveSttr = "";
        private void glassButton1_Click(object sender, EventArgs e)
        {

            wjsc = true;
            //DispalyDataBase(true);

            //Common.InsertPictureToExcel ipt = new InsertPictureToExcel();
            GoldPrinter.ExcelAccess excel = new GoldPrinter.ExcelAccess();



            int rowIndex = 1;
            int collndex = 0;



            saveFileDialog1.InitialDirectory = @"\Report\";
            saveFileDialog1.FileName = "*.xls";
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                saveSttr = saveFileDialog1.FileName;
                if (saveSttr.Length > 0)
                {
                    DispalyDataBase(true);
                    //excel.PrintPreview();
                    //excel.SaveAs(Application.StartupPath + @"\Report\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "zdlq.xls", true);

                }
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

    }
}
