using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
    public class Log
    {
        string path;
        StreamWriter sw;

        public Log(string path)
        {
            this.path = path;

            if (!File.Exists(path))
            {
                sw = File.CreateText(path);
            }
            else
            {
                sw = File.AppendText(path);
            }
        }
        
        public void printLog(string msg)
        {
            sw.WriteLine(msg);
            sw.Flush();
        }
    }
}
