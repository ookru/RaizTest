using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRaizTest.Classes
{
    public class OperationResult
    {
        public bool Good { get; set; }
        public string Message { get; set; }
        public object Value{ get; set; }
        public OperationResult(bool good, string msg, object val=null)
        {
            Good = good;
            Message = msg;
            Value = val;
        }
    }
}
