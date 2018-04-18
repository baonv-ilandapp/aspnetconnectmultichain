using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.MultichainLib
{
    public class Admin: ServiceBase
    {

        public string GetInfo()
        {
            return jsonRpcClient.JsonRpcRequest("getinfo");
        }

        public Admin(JsonRpcClient c):base(c)
        {

        }
    }
}
