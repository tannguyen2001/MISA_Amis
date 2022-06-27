using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Exceptions
{
    public class MISAValidateException:Exception
    {
        string _messenger;
        IDictionary _errorMsg;
        public MISAValidateException(string msg, List<String> errorMsg)
        {
            _messenger = msg;
            _errorMsg = new Dictionary<string, List<String>>();
            _errorMsg.Add("Employee",errorMsg);
        }
        public override string Message => this._messenger;
        public override IDictionary Data => this._errorMsg;
    }
}
