using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinFormsRaizTest.Classes
{
   public  class Config
    {
       private static string ConfigPath="Config";

       public string DBServer { get; set; }
       public string DB { get; set; }
       public Config()
       {
           string[] ConfigLines=File.ReadAllLines(ConfigPath);
           DBServer = ConfigLines.Where(x => x.StartsWith("DBServer=")).Select(x => x.Substring("DBServer=".Length)).First();
           DB = ConfigLines.Where(x => x.StartsWith("DB=")).Select(x => x.Substring("DB=".Length)).First();
       }
   
    }
}
