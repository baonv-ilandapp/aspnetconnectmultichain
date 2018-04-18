using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Class.MultichainLib;

namespace ConnectMultiChain.Controllers
{
    public class MultiChainController : Controller
    {
        // GET: MultiChain
        public ActionResult Index()
        {
            return View();
        }

        public string GetInfo(string rpcPassword, string ipAddress, int port, string chainName)
        {
            try
            {
                JsonRpcClient jsonRpcClient = new JsonRpcClient("multichainrpc", rpcPassword, ipAddress, port, chainName);
                Admin admin = new Admin(jsonRpcClient);
                string json = "";
                json = admin.GetInfo();
                return json;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}