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
        static Log log = null;
        string path;
        StreamWriter sw;

        public static Log getInstance() {
            if (log == null) {
                log = new Log("log.txt");
            }
            return log;
        }

        public Log(string path)
        {
            this.path = path;

            /*if (!File.Exists(path))
            {
                sw = File.CreateText(path);
            }
            else
            {
                sw = File.AppendText(path);
            }*/
        }
        
        public void printLog(string msg)
        {
            if (!File.Exists(path))
            {
                sw = File.CreateText(path);
            }
            else
            {
                sw = File.AppendText(path);
            }

            Console.WriteLine(msg);
            sw.WriteLine(DateTime.Now + " | " + msg);
            sw.Flush();

            sw.Close();
        }
    }
}
