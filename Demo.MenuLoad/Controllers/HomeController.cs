using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml;

namespace Demo.MenuLoad.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Test()
        {
            ViewBag.Message = "Your Test page.";

            return View();
        }

        public ActionResult Menu()
        {
            ViewBag.Message = "Your Test page.";

            return View();
        }
        public ActionResult Leave()
        {
            ViewBag.Message = "This is Leave Demo Page";
            return View();
        }
        public ActionResult SignIn()
        {
            ViewBag.Message = "Sign In";
            return View();
        }

        public ActionResult GoogleAuthTest()
        {
            return View();
        }
        public ActionResult GoogleAuthTest2()
        {
            return View();
        }

        //public static AndroidService.AGWebServiceSoapClient service =
        //    new AndroidService.AGWebServiceSoapClient("AGWebServiceSoap");

        //public static AndroidService.Menu[] menus = service.GetAllMenu();

        //public static List<AndroidService.Menu> LoadSubMenu(int parentId)
        //{
        //    var chiledMenus = menus.Where(x => x.intParentId == parentId).ToList();
        //    return chiledMenus;
        //}

        //public static List<AndroidService.Menu> LoadMenu()
        //{
        //    var rootMenu = menus.Where(x => x.intParentId == 0).Take(6).ToList();
        //    return rootMenu;
        //}

        public static MvcUrl CreateMvcUrl(string url)
        {
            MvcUrl mvcUrl = new MvcUrl();
            string[] items = url.Split('/');
            mvcUrl.Controller = items[0];
            mvcUrl.Action = items[items.Length-1].Substring(0, items[items.Length - 1].LastIndexOf('.'));
            return mvcUrl;
        }
        static List<string> iconList = new List<string>();

        public static void BindIconList()
        {
            if (iconList.Count==0)
            {
                iconList.Add("icon-purchase");
                iconList.Add("icon-trash");
                iconList.Add("icon-role");
                iconList.Add("icon-commercial");
                iconList.Add("icon-voucher");
                iconList.Add("icon-media");
                iconList.Add("fa fa-bank");
                iconList.Add("fa fa-bath");
                iconList.Add("fa fa-beer");
                iconList.Add("fa fa-birthday-cake");
                iconList.Add("fa fa-briefcase");
                iconList.Add("fa fa-building");
                iconList.Add("fa fa-calculator");
                iconList.Add("fa fa-certificate");
                iconList.Add("fa fa-cloud");
                iconList.Add("fa fa-cog");
                iconList.Add("fa fa-creative-commons");
                iconList.Add("fa fa-cubes");
                iconList.Add("fa fa-drivers-license");
                iconList.Add("fa fa-envelope");
                iconList.Add("fa fa-eyedropper");
                iconList.Add("fa fa-file-excel-o");
                iconList.Add("fa fa-film");
                iconList.Add("fa fa-dashboard");
                iconList.Add("fa fa-database");
                iconList.Add("fa fa-address-book");
                iconList.Add("fa fa-adjust");
                iconList.Add("fa fa-archive");
                iconList.Add("fa fa-area-chart");
                iconList.Add("fa fa-bank");
                iconList.Add("fa fa-bath");
                iconList.Add("fa fa-beer");
                iconList.Add("fa fa-birthday-cake");
                iconList.Add("fa fa-briefcase");
                iconList.Add("fa fa-building");
                iconList.Add("fa fa-calculator");
                iconList.Add("fa fa-certificate");
                iconList.Add("fa fa-cloud");
                iconList.Add("fa fa-cog");
                iconList.Add("fa fa-creative-commons");
                iconList.Add("fa fa-cubes");
                iconList.Add("fa fa-drivers-license");
                iconList.Add("fa fa-envelope");
                iconList.Add("fa fa-eyedropper");
                iconList.Add("fa fa-file-excel-o");
                iconList.Add("fa fa-film");
                iconList.Add("fa fa-dashboard");
                iconList.Add("fa fa-database");
                iconList.Add("fa fa-address-book");
                iconList.Add("fa fa-adjust");
                iconList.Add("fa fa-archive");
                iconList.Add("fa fa-area-chart");
                iconList.Add("fa fa-bank");
                iconList.Add("fa fa-bath");
                iconList.Add("fa fa-beer");
                iconList.Add("fa fa-birthday-cake");
                iconList.Add("fa fa-briefcase");
                iconList.Add("fa fa-building");
                iconList.Add("fa fa-calculator");
                iconList.Add("fa fa-certificate");
                iconList.Add("fa fa-cloud");
                iconList.Add("fa fa-cog");
                iconList.Add("fa fa-creative-commons");
                iconList.Add("fa fa-cubes");
                iconList.Add("fa fa-drivers-license");
                iconList.Add("fa fa-envelope");
                iconList.Add("fa fa-eyedropper");
                iconList.Add("fa fa-file-excel-o");
                iconList.Add("fa fa-film");
            }
        }
        public static string GetRandomIcon(int num)
        {
            BindIconList();
            string s= iconList[num];
            return s;
        }

        //private static Menu CreateMenu(AndroidService.Menu ServiceMenu)
        //{
        //    Menu menu = new Menu();
        //    ServiceMenu.intFuncID
        //    return menu;
        //}
        //public void CreateMenu()
        //{
        //   // string oRequest = "<?xml version=‘1.0’ encoding=‘utf-8’?><soap:Envelope xmlns:xsi=‘http://www.w3.org/2001/XMLSchema-instance’xmlns:xsd=‘http://www.w3.org/2001/XMLSchema’xmlns:soap=‘http://schemas.xmlsoap.org/soap/envelope/’><soap:Body><GetMenuList xmlns=‘http://akij.net/’><part>int</part><enroll>int</enroll><AllVariable>string</AllVariable></GetMenuList></soap:Body></soap:Envelope>";
        //   // HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://android.akij.net/AGWebService.asmx/GetMenuList?part=1&enroll=369116&AllVariable=string");
        //   // req.Headers.Add("SOAPAction", "\"http://akij.net/GetMenuList\"");
        //   // req.ContentType = "text/xml; charset=\"utf-8\"";
        //   // req.Accept = "text/xml";
        //   // req.Method = "POST";
        //   // req.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

        //   // //Passes the SoapRequest String to the WebService
        //   // using (Stream stm = req.GetRequestStream())
        //   // {
        //   //     using (StreamWriter stmw = new StreamWriter(stm))
        //   //     {
        //   //         stmw.Write(oRequest);
        //   //     }
        //   // }
        //   // //Gets the response
        //   // WebResponse response = req.GetResponse();
        //   // //Writes the Response
        //   // Stream responseStream = response.GetResponseStream();

        //   //string s = responseStream.ToString();


        //    string uri = "http://android.akij.net/AGWebService.asmx/GetMenuList?part=1&enroll=369116&AllVariable=";
        //    string soap = "<? xml version =‘1.0’ encoding =‘utf - 8’?>< soap:Envelope xmlns:xsi =‘http://www.w3.org/2001/XMLSchema-instance’xmlns:xsd=‘http://www.w3.org/2001/XMLSchema’xmlns:soap=‘http://schemas.xmlsoap.org/soap/envelope/’><soap:Body><GetMenuList xmlns=‘http://akij.net/’><part>int</part><enroll>int</enroll><AllVariable>string</AllVariable></GetMenuList></soap:Body></soap:Envelope>";

        //    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
        //    //request.Headers.Add("SOAPAction", "http://akij.net/GetMenuList");
        //    //request.ContentType = "text/xml; charset=utf-8";
        //    //request.Accept = "text/xml";
        //    request.Method = "GET";

        //    using (Stream stm = request.GetRequestStream())
        //    {
        //        using (StreamWriter stmw = new StreamWriter(stm))
        //        {
        //            stmw.Write(soap);
        //        }
        //    }

        //    using (WebResponse webResponse = request.GetResponse())
        //    {
        //        string s = webResponse.GetResponseStream()?.ToString();
        //    }
        //}

        public static void CallWebService()
        {
            var _url = "http://android.akij.net/AGWebService.asmx/GetMenuList?part=1&enroll=369116&AllVariable=";
            var _action = "http://akij.net/GetMenuList";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
                @"<?xml version='1.0' encoding='utf-8'?> <soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'> <soap:Body><GetMenuList xmlns='http://akij.net/'><part>int</part><enroll>int</enroll><AllVariable>string</AllVariable></GetMenuList></soap:Body></soap:Envelope>");
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

    }

    public class MvcUrl
    {
        public string Action { get; set; }
        public string Controller { get; set; }
    }
    public class Menu
    {
        public int intFuncID { get; set; }
        public string strFunc { get; set; }
        public int intParentId { get; set; }
        public string strDescription { get; set; }
        public bool ysnEnableView { get; set; }
        public bool ysnEnableInsert { get; set; }
        public bool ysnEnableModify { get; set; }
        public bool ysnEnableDelete { get; set; }
        public string strImage { get; set; }
        public string strRelativeURL { get; set; }
        public int intOrder { get; set; }
        public bool ysnActive { get; set; }
        public string strClassName { get; set; }
        public bool ysnPreActive { get; set; }
        public bool ysnHasChild { get; set; }
    }


}