using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiNodeStarter
{
    class Program
    {
        private static string nodePath = "C:/Users/AsusAdmin/Documents/Dev/Builds/RaiNode";
        private static string nodeParams = string.Format(@"--daemon --data_path=""{0}/config""", nodePath);

        static void Main(string[] args)
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("rai_node"))
            {
                process.Kill();
            }
            System.Diagnostics.Process.Start(string.Format("{0}/rai_node.exe", nodePath), nodeParams);
        }
    }
}
