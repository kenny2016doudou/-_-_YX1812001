using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZZ.Serial
{
    public class FileOperator
    {
        #region 读写文件

        public static Dictionary<string,int> GetFileStr(string filepath)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            if (File.Exists(filepath))
            {
                try
                {
                    StreamReader sr = new StreamReader(filepath, System.Text.Encoding.UTF8);
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        dic.Add(s.Split('=')[0], int.Parse(s.Split('=')[1]));
                    }
                    sr.Close();
                }
                catch {}

            }

            return dic;
        }

        public static Dictionary<string, double> GetFileStrtoDouble(string filepath)
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();
            if (File.Exists(filepath))
            {
                try
                {
                    StreamReader sr = new StreamReader(filepath, System.Text.Encoding.UTF8);
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        dic.Add(s.Split('=')[0], double.Parse(s.Split('=')[1]));
                    }
                    sr.Close();
                }
                catch { }

            }

            return dic;
        }


        public static string GetFileString(string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    StreamReader sr = new StreamReader(filepath, System.Text.Encoding.UTF8);
                    string s = "";
                    if ((s = sr.ReadLine()) != null)
                    {
                        return s;
                    }
                    sr.Close();
                }
                catch { }

            }

            return "";
        }

        public static void WriterFile(string filepath, string str)
        {
            StreamWriter sw=null;

            try
            {
                if (!File.Exists(filepath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filepath));

                }
                sw = new StreamWriter(filepath);

                sw.Write(str);
                

            }
            catch (Exception e)
            {                
                WriterFile(filepath, str);
                throw e;
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            
         
        }


        #endregion
    }
}
