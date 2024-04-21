using Core.Domain.Database;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WarpPortalAPI.Service;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WarpPortalAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TransController : ControllerBase
    {
        private IDatabeseService _databeseService;

        public IJwtUtils _jwtUtils;
        private IToolsxService _toolsService { get; set; }

        public TransController(IDatabeseService databeseService, IJwtUtils jwtUtils, IToolsxService toolsService)
        {
            _databeseService = databeseService;
            _jwtUtils = jwtUtils;
            _toolsService = toolsService;
        }

        [HttpPost]
        [Authz]
        public ActionResult GenerateTokenVer(MdlGenVer mdlGenVer)
        {
            HttpContext context = HttpContext;
            var user = (TblAccount)context.Items["User"];
            string tokenkey = _jwtUtils.GenerateTokenVer(user, mdlGenVer, 2);

            Response reponseModel = new Response();
            reponseModel.IsSuccess = true;
            reponseModel.Token = tokenkey;


            return Ok(reponseModel);
        }

        [HttpGet]
        public ActionResult GetLogsMsgsms()
        {
            var data = _databeseService.GetLogsMsgsms();


            return Ok(data);
        }


        [HttpPost]
        public ActionResult Savetransaction(MdlSaveTran mdlSaveTran)
        {

            TransVer transVer = new TransVer();
            transVer.LogId = Convert.ToInt32(mdlSaveTran.LogId);
           // transVer.OrderId = mdlSaveTran.OrderId;
            transVer.Amout = mdlSaveTran.Amount;
            transVer.State = mdlSaveTran.state;
            transVer.BankCode = mdlSaveTran.Bankcode;
            var res=  _databeseService.AddTransVer(transVer);

            SaveRes saveRes = new SaveRes();
            saveRes.IsSuccess = true;
          
            return Ok(saveRes);
        }



        [HttpGet]
        public ActionResult GetTransVer()
        {
            var data = _databeseService.GetTransVers();


            return Ok(data);
        }


        [HttpPost]
        public ActionResult VerifyData(MdlData data)
        {

            LogsMsgsm logEvent = new LogsMsgsm();
            logEvent.Code = data.Code;
            logEvent.Sender = data.sender;
            logEvent.Detail = JsonConvert.SerializeObject(data);
            logEvent.Msg = data.msg;
            int LogId = _databeseService.AddLogsMsgsms(logEvent);
       

            switch(data.sender)
            {
                case "Krungthai":

                    Transaction transaction = new Transaction();
                    transaction.LogId = LogId;
                    if (data.msg.Contains("เงินเข้า"))
                    {
                        transaction.AccRef = data.Code;
                        transaction.Sender = data.sender;
               
                        transaction.TransTypeId = 1;
                        transaction.TransBankId = 6;
                        string Amout = data.msg.Split(' ')[2].ToString().Replace("บ","");
                        string Total = data.msg.Split(' ')[4].ToString().Replace("บ", "");
                        
                        string date = data.msg.Split(' ')[0].ToString();
                        string datefor = date.Replace("@", " ");


                        int day = Convert.ToInt32(datefor.Substring(0,2));
                        int month = Convert.ToInt32(datefor.Substring(3, 2));

                        int h = Convert.ToInt32(datefor.Substring(6, 2));
                        int m = Convert.ToInt32(datefor.Substring(9, 2));

                        DateTime dateTime = new DateTime(DateTime.Now.Year,month,day,h,m,0);
                        transaction.DateTimeSlip = dateTime;
                        try
                        {
                            transaction.Amout = Convert.ToDecimal(Amout);
                        }
                        catch (Exception ex){ }
                        try
                        {
                            transaction.Total = Convert.ToDecimal(Total);
                        }
                        catch (Exception ex) { }

                       
                    }
                    else
                    {

                        transaction.AccRef = data.Code;
                        transaction.Sender = data.sender;
                        transaction.TransTypeId = 2;
                        transaction.TransBankId = 6;
                        string Amout = data.msg.Split(' ')[2].ToString().Replace("บ", "");
                        string Total = data.msg.Split(' ')[4].ToString().Replace("บ", "");
                        string date = data.msg.Split(' ')[0].ToString();
                        string datefor = date.Replace("@", " ");


                        int day = Convert.ToInt32(datefor.Substring(0, 2));
                        int month = Convert.ToInt32(datefor.Substring(3, 2));

                        int h = Convert.ToInt32(datefor.Substring(6, 2));
                        int m = Convert.ToInt32(datefor.Substring(9, 2));

                        DateTime dateTime = new DateTime(DateTime.Now.Year, month, day, h, m, 0);
                        transaction.DateTimeSlip = dateTime;
                        try
                        {
                            transaction.Amout = Convert.ToDecimal(Amout);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            transaction.Total = Convert.ToDecimal(Total);
                        }
                        catch (Exception ex) { }


                    }
                    _databeseService.AddTransactions(transaction);

                    break;
                
            }



            return Ok();
        }




        [Authz]
        [HttpPost]
        public ActionResult Verifyslip(IFormCollection data)
        {
            HttpContext context = HttpContext;
            var verify = (MdlGenVer)context.Items["verify"];

            SingleFileModel singleFileModel = new SingleFileModel();
            if (data.Files.Count > 0)
            {
                singleFileModel.File = data.Files[0];


                if (data.Keys.Count > 0 && !string.IsNullOrEmpty(data["Amount"]) && !string.IsNullOrEmpty(data["OrderId"]))
                {



                    verify.Amount = data["Amount"];
                    verify.OrderId = data["OrderId"];
                  
                    singleFileModel.File = singleFileModel.File;
                    LogEvent logEvent = new LogEvent() { Code = "10", Remark = "Verifyslip", Detail = JsonConvert.SerializeObject(singleFileModel), Addr = context.Connection.RemoteIpAddress.ToString() };
                    _databeseService.AddlogEventSync(logEvent);
                    try
                    {

                        var banktolist =   _databeseService.GetTransection(null).OrderByDescending(m=>m.CeatedDate).ToList();

                        ResultUpload mdlDataRes = _toolsService.Upload(singleFileModel);
                        if (mdlDataRes.IsSuccess)
                        {



                            switch(mdlDataRes.BankName)
                            {
                                case "BBL":
                                    mdlDataRes.Amt = mdlDataRes.Amt.Replace("THB", "");
                                    break;
                                case "KTB":
                                    mdlDataRes.Amt = mdlDataRes.Amt.Replace("บาท", "");
                                    break;
                                case "BAY":
                                    mdlDataRes.Amt = mdlDataRes.Amt.Replace("THB", "");
                                    break;
                            }

                        

                            LogSlip logSlip = new LogSlip();
                            logSlip.Ref = mdlDataRes.Ref;
                            logSlip.Datetime = mdlDataRes.Datetime;
                            logSlip.Amt = mdlDataRes.Amt;
                          //  logSlip.AccInput = verify.AccInput;
                            logSlip.Bank = mdlDataRes.BankName;
                            logSlip.Message = mdlDataRes.Message;
                            logSlip.IsSuccess = mdlDataRes.IsSuccess;
                            logSlip.OrId = verify.OrderId;
                             var res = _databeseService.AddlogSlipSync(logSlip);
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SlipDB");

                            //create folder if not exist
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            //get file extension
                            FileInfo fileInfo = new FileInfo(singleFileModel.File.FileName);
                            string fileNameext = fileInfo.Extension;

                            string fileNameWithPath = Path.Combine(path, logSlip.Ref + fileNameext);
                            byte[] dataArray = new byte[100000];
                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                singleFileModel.File.CopyTo(stream);
                            }

                            decimal a = Convert.ToDecimal(mdlDataRes.Amt);
                            ResVerSlip resVerSlip = new ResVerSlip();

                           var datalist =   banktolist.Where(m => (m.Amout == a && m.DateTimeSlip > DateTime.Now.AddMinutes(-30))).FirstOrDefault();
                            if (datalist != null)
                            {
                                resVerSlip.Verify = true;
                            }
                            //banktolist.ForEach(m => {
                            //    if (m.Amout == a)
                            //    {
                            //        resVerSlip.Verify = true;
                            //    }

                            //});



                            resVerSlip.Amt = mdlDataRes.Amt;
                            resVerSlip.Datetime = mdlDataRes.Datetime;
                            resVerSlip.Bank = mdlDataRes.BankName;
                            resVerSlip.CreatedTime = DateTime.Now;
                          
                            resVerSlip.VerifyAI = true;
                       
                        
                       


                            if (Convert.ToDouble(mdlDataRes.Amt) != Convert.ToDouble(verify.Amount))
                            {
                                resVerSlip.IsSuccess = false;
                                resVerSlip.Message = "Amount Mismatch!!";
                                logEvent = new LogEvent() { Code = "10", Remark = "ResVerifyslip", Detail = JsonConvert.SerializeObject(resVerSlip) };
                                _databeseService.AddlogEventSync(logEvent);
                                return Ok(resVerSlip);
                            }
                            else
                            {
                                resVerSlip.IsSuccess = true;
                                resVerSlip.Message = "Verify Is OK!";
                                logEvent = new LogEvent() { Code = "10", Remark = "ResVerifyslip", Detail = JsonConvert.SerializeObject(resVerSlip) };
                                _databeseService.AddlogEventSync(logEvent);
                                return Ok(resVerSlip);
                            }



                          
                        }
                        else
                        {
                            LogSlip logSlip = new LogSlip();
                            mdlDataRes.Ref = Guid.NewGuid().ToString();

                            logSlip.Ref = mdlDataRes.Ref;
                            logSlip.Datetime = mdlDataRes.Datetime;
                            logSlip.Amt = mdlDataRes.Amt;
                            logSlip.Bank = mdlDataRes.BankName;
                            logSlip.Message = mdlDataRes.Message;
                            logSlip.IsSuccess = mdlDataRes.IsSuccess;
                            logSlip.OrId = verify.OrderId;
                              var res = _databeseService.AddlogSlipSync(logSlip);
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SlipDBError");

                            logEvent = new LogEvent() { Code = "10", Remark = "ResVerifyslip", Detail = JsonConvert.SerializeObject(logSlip) };
                            _databeseService.AddlogEventSync(logEvent);

                            //create folder if not exist
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            //get file extension
                            FileInfo fileInfo = new FileInfo(singleFileModel.File.FileName);
                            string fileNameext = fileInfo.Extension;

                            string fileNameWithPath = Path.Combine(path, logSlip.Ref + fileNameext);
                            byte[] dataArray = new byte[100000];
                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                singleFileModel.File.CopyTo(stream);
                            }


                            return Ok(mdlDataRes);
                        }
                    }
                    catch (Exception ex)
                    {
                        ResVerSlip resVerSlip = new ResVerSlip();
                        resVerSlip.CreatedTime = DateTime.Now;
                        resVerSlip.IsSuccess = false;
                        resVerSlip.Message = ex.Message;//"Verify Error!.OCR catch";
                        return Ok(resVerSlip);
                    }

                }
                else
                {
                    ResVerSlip resVerSlip = new ResVerSlip();
                    resVerSlip.CreatedTime = DateTime.Now;
                    resVerSlip.IsSuccess = false;
                    resVerSlip.Message = "Verify Error!.Check Input Key.";
                    return Ok(resVerSlip);
                }



            }
            else
            {
                ResVerSlip resVerSlip = new ResVerSlip();
                resVerSlip.CreatedTime = DateTime.Now;
                resVerSlip.IsSuccess = false;
                resVerSlip.Message = "Verify Error!!";
                return Ok(resVerSlip);
            }


            return Ok();
        }



        [Authz]
        [HttpPost]
        public ActionResult Transaction(intrans model)
        {
            HttpContext context = HttpContext;
            var user = (TblAccount)context.Items["User"];

            LogEvent logEvent = new LogEvent() { Code = "10", Remark = "Transaction", Detail = JsonConvert.SerializeObject(model), Addr = context.Connection.RemoteIpAddress.ToString() };
            _databeseService.AddlogEventSync(logEvent);


            return Ok();
        }


        [Authz]
        [HttpPost]
        public ActionResult GetTransaction(MdlGetBank BankId)
        {

            return Ok(_databeseService.GetTransection(BankId));
        }


        [Authz]
        [HttpPost(Name = "PayQRCode")]
        public IActionResult GenerateQRCode(MdlPayInput mdlPayInput)
        {
            HttpContext context = HttpContext;
            LogEvent logEvent = new LogEvent() { Code = "10", Remark = "GenerateQRCode", Detail = JsonConvert.SerializeObject(mdlPayInput), Addr = context.Connection.RemoteIpAddress.ToString() };
            _databeseService.AddlogEventSync(logEvent);
            List<string> randomAcc = new List<string>();



            var user = (TblAccount)context.Items["User"];
            if (user != null)
            {
                //var payid = _databeseService.GetTransBankbyAccRef(user.AccRef);
                //payid.ForEach(m =>
                //{

                //    randomAcc.Add(m.PromNo);
                //});


                Random randNum = new Random();
                int aRandomPos = randNum.Next(randomAcc.Count);


                UriBuilder _url = new UriBuilder();
                _url.Scheme = "https";
                _url.Host = "promptpay.io";
                //_url.Path = "/" + randomAcc[aRandomPos] + "/" + mdlPayInput.Amount;
                _url.Path = "/" + mdlPayInput.AccountNo + "/" + mdlPayInput.Amount;
                string urlquery = _url.Uri.AbsoluteUri;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlquery);


                req.Headers.Add("Content-Transfer-Encoding", "8bit");
                req.ContentType = "application/json";
                req.KeepAlive = true;
                req.Method = "GET";
                req.Timeout = Timeout.Infinite;

                // logEvent = new LogEvent() { Code = "10", Remark = "ResGenerateQRCode", Detail = JsonConvert.SerializeObject(req) };
                //_databeseService.AddlogEventSync(logEvent);

                var response = req.GetResponse();


                FileStreamResult fileStreamResult = File(response.GetResponseStream(), response.ContentType);
                byte[] data = StreamExtensions.ReadAllBytes(fileStreamResult.FileStream);

                Transaction transaction = new Transaction();
                transaction.AccRef = user.AccRef;
                transaction.Verfify = false;
                transaction.TransTypeId = 1;
                transaction.TransBankId = mdlPayInput.TransBankId;
                transaction.Amout = Convert.ToDecimal(mdlPayInput.Amount);
                _databeseService.AddTransactions(transaction);



                return File(data, "image/jpeg");
            }
            return Unauthorized();

        }
    }
}