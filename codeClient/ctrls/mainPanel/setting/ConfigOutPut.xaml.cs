using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using nsVicoClient;
using System.Windows.Threading;
using nsDataMgr;
using System.IO;

namespace nsVicoClient.ctrls
{
    /// <summary>
    /// ConfigOutPut.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigOutPut : Window
    {
        private DispatcherTimer dt;

        public ConfigOutPut()
        {
            InitializeComponent();

            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(100);
            dt.Tick += new EventHandler(dt_Tick);
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (ProgressBar.Value == ProgressBar.Maximum)
            {
                ProgressBar.Value = 0;
            }

            ProgressBar.Value++;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = "d:\\Valmo\\Config";
            openFileDialog.Filter = "excel文件|*.xls";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbPath.Content = openFileDialog.FileName;

                dt.Start();

                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + openFileDialog.FileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                OleDbConnection conn = new OleDbConnection(strConn);
                OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM[Sheet1$]", strConn);
                DataSet myDataSet = new DataSet();
                myCommand.Fill(myDataSet);
                conn.Close();

                tbResult.AppendText("已读取到" + myDataSet.Tables[0].Rows.Count + "个参数\n");

                List<string> lstErr = new List<string>();

                using (FileStream fs = new FileStream(@"D:\Valmo\Config\" + DateTime.Now.ToString("yy_MM_dd") + ".config", FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fs);

                    foreach (DataRow dr in myDataSet.Tables[0].Rows)
                    {
                        string str = dr[0].ToString();

                        string[] strs = str.Split('\\');

                        if (strs.Length != 1)
                        {
                            str = string.Empty;
                            foreach (string s in strs)
                            {
                                str += s + "\\\\";
                            }
                            str = str.Substring(0, str.Length - 2);
                        }
                        sw.WriteLine("lstBasic.Add(\"" + str + "\");");
                    }
                    sw.Close();
                }

                if (lstErr.Count == 0)
                {
                    tbResult.AppendText("全部配置导出成功 \n");
                }
                else
                {
                    tbResult.AppendText("以下参数导出失败 \n");

                    foreach (string strError in lstErr)
                    {
                        tbResult.AppendText(strError+"\n");
                    }
                }
            }
        }

        private void outPortConfig()
        {

        }
    }
}
