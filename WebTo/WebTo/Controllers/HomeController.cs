using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Reflection;
using System.Web.Http.Results;
using WebTo.Interfaces;
using WebTo.Models;
using WebTo.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebTo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly URLAPI _URLAPI;
        public IAppInterfaceService _AppInterfaceService;
        public HomeController(ILogger<HomeController> logger,IOptions<URLAPI> options, IAppInterfaceService appInterfaceService)
        {
            _logger = logger;
            _URLAPI = options.Value;
            _AppInterfaceService = appInterfaceService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MdlAccount auth)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult Auth(MdlAccount auth)
        {
            if(!ModelState.IsValid)
            {
                return View("Index");
            }

            var res = _AppInterfaceService.RestApiController(RestApiType.Post, _URLAPI.WarpPortalAPI + "api/Auth/Login", auth, null);
            if(res != null)
            {
                MdlResponse response = JsonConvert.DeserializeObject<MdlResponse>(res.ToString());
                if (response.IsSuccess)
                {
                    HttpContext.Session.SetString("Token", response.Token);
                    return RedirectToAction("Member");
                }
                else
                {
                    ModelState.AddModelError("Error1", response.Message);
                    return View("Index");
                }
            }
            else
            {

                ModelState.AddModelError("Error1", "User Invalid");


                return View("Index");
            }
        
        }

        public IActionResult Register()
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Member");
            }

            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(MdlRegister mdlRegister)
        {
       

            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Member");
            }

            var res = _AppInterfaceService.RestApiController(RestApiType.Post, _URLAPI.WarpPortalAPI + "api/Auth/Register", mdlRegister, null);
            if (res != null)
            {
                MdlResponse response = JsonConvert.DeserializeObject<MdlResponse>(res.ToString());  
                if(response.IsSuccess)
                return RedirectToAction("Member");
                else
                {
                    ModelState.AddModelError("Error1", response.Message);
                    return View("Register");
                }
            }



                return View("Register");
        }

        public IActionResult Member()
        {
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {

                var res = _AppInterfaceService.RestApiController(RestApiType.Get, _URLAPI.WarpPortalAPI + "api/Auth/GetCustInfo", null, token);
                if (res != null)
                {

                    ResCustInfo response = JsonConvert.DeserializeObject<ResCustInfo>(res.ToString());



                    var mtres = _AppInterfaceService.RestApiController(RestApiType.Get, _URLAPI.WarpPortalAPI + "api/Auth/GetMTBanks", null, token);
                    response.mtBanks = JsonConvert.DeserializeObject<List<MtBank>>(mtres.ToString());
                    response.transBanks.ForEach(m => { 
                        if ( m.BankId.HasValue)
                        {
                            m.BankCode = response.mtBanks.Where(x => x.Id == m.BankId).FirstOrDefault().Code;
                        }
          
                    });
                    return View("Member", response);
                }

                return View("index");

            }

            return RedirectToAction("index");

     
        }

        [HttpPost]
        public JsonResult Addbank([FromBody]TransBank  transBank)
        {

            var token = HttpContext.Session.GetString("Token");
            //if (string.IsNullOrEmpty(token))
            //{
            //    return RedirectToAction("index");
            //}
            transBank.Token = token;
            var res = _AppInterfaceService.RestApiController(RestApiType.Post, _URLAPI.WarpPortalAPI + "api/Auth/SaveBank", transBank, token);
            return Json(res);

           
        }

        [HttpPost]
        public JsonResult GenerateQRCode([FromBody] MdlPayInput mdlPayInput)
        {
            var token = HttpContext.Session.GetString("Token");

            var res = _AppInterfaceService.ResApiFileContent(RestApiType.Post, _URLAPI.WarpPortalAPI + "api/Trans/GenerateQRCode", mdlPayInput, token);

            var response = (HttpWebResponse)res;

            FileStreamResult fileStreamResult = File(response.GetResponseStream(), response.ContentType);
            byte[] data = StreamExtensions.ReadAllBytes(fileStreamResult.FileStream);
            var base64 = Convert.ToBase64String(data);
            return Json( Content(String.Format("data:image/jpeg;base64,{0}", base64)));
          //return Json(data, "image/jpeg");
           
        }

        [HttpPost]
        public JsonResult verifyslip(SingleFileModel formData)
        {
            var token = HttpContext.Session.GetString("Token");
            MdlGenVer genVer = new MdlGenVer() { AccInput = formData.AccInput, BankCode = formData.BankCode, Amount = formData.Amount };
            var res = _AppInterfaceService.RestApiController(RestApiType.Post, _URLAPI.WarpPortalAPI + "api/Trans/GenerateTokenVer", genVer, token);
            var resc = JsonConvert.DeserializeObject<MdlResponse>(res.ToString());
            if(resc.IsSuccess)
            {
         
                string urlquery = _URLAPI.WarpPortalAPI + "api/Trans/Verifyslip";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlquery);

                string authorization = resc.Token;
                req.Headers["Authorization"] = "Bearer " + authorization;
                req.PreAuthenticate = true;
                string boundaryString = "----------" + DateTime.Now.Ticks.ToString("x");
                req.ContentType = "multipart/form-data; boundary=" + boundaryString;
                req.KeepAlive = true;
                req.Method = "POST";
                req.Timeout = Timeout.Infinite;
                MemoryStream postDataStream = new MemoryStream();
                StreamWriter postDataWriter = new StreamWriter(postDataStream);

                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
               "AccInput", genVer.AccInput);

                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
         "Amount", formData.Amount);
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
"CreatedTime", DateTime.Now.ToString());
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
"BankCode", formData.BankCode);


                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data;"
                + "name=\"{0}\";"
                + "filename=\"{1}\""
                + "\r\nContent-Type: {2}\r\n\r\n",
                "myFile" + "0", formData.File.FileName, Path.GetExtension(formData.File.FileName));
                //Path.GetFileName(fileNameWithPath),
             //   Path.GetExtension(fileNameWithPath));
                postDataWriter.Flush();

                

                using (var readStream = formData.File.OpenReadStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = readStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        postDataStream.Write(buffer, 0, bytesRead);
                    }
                    readStream.Close();
                    postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
                    postDataWriter.Flush();
                }

                req.ContentLength = postDataStream.Length;
                using (Stream s = req.GetRequestStream())
                {
                    postDataStream.WriteTo(s);
                }
                postDataStream.Close();

                string strjson = "";
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    strjson = streamReader.ReadToEnd();
                }
                ResultUpload resultUpload = JsonConvert.DeserializeObject<ResultUpload>(strjson);
                return Json(resultUpload);
            }

            MdlResponse mdlResponse = new MdlResponse();
            mdlResponse.IsSuccess = false;
            return Json(mdlResponse);
        }

    }
}
