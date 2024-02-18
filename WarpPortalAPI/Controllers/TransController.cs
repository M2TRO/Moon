using Core.Domain.Database;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WarpPortalAPI.Service;
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
        public ActionResult GenerateToken()
        {

            return Ok();
        }




        [Authz]
        [HttpPost]
        public ActionResult Verifyslip(IFormCollection data)
        {
            HttpContext context = HttpContext;
            SingleFileModel singleFileModel = new SingleFileModel();
            if (data.Files.Count > 0)
            {
                singleFileModel.File = data.Files[0];


                if (data.Keys.Count > 0 && !string.IsNullOrEmpty(data["Amount"]) && !string.IsNullOrEmpty(data["UserId"]))
                {

                    singleFileModel.Amt = data["Amount"];
                    singleFileModel.UserName = data["UserId"];
                    singleFileModel.File = singleFileModel.File;
                    LogEvent logEvent = new LogEvent() { Code = "10", Remark = "Verifyslip", Detail = JsonConvert.SerializeObject(singleFileModel),Addr = context.Connection.RemoteIpAddress.ToString() };
                    _databeseService.AddlogEventSync(logEvent);
                    try
                    {
                        ResultUpload mdlDataRes = _toolsService.Upload(singleFileModel);
                        if (mdlDataRes.IsSuccess)
                        {
                            LogSlip logSlip = new LogSlip();
                            logSlip.Ref = mdlDataRes.Ref;
                            logSlip.Datetime = mdlDataRes.Datetime;
                            logSlip.Amt = Convert.ToDecimal(mdlDataRes.Amt);
                            logSlip.Bank = mdlDataRes.BankName;
                            logSlip.Message = mdlDataRes.Message;
                            logSlip.IsSuccess = mdlDataRes.IsSuccess;
                            //         var res = _dbLogService.SaveLogSlip(logSlip);
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

                            ResVerSlip resVerSlip = new ResVerSlip();
                            resVerSlip.Amt = mdlDataRes.Amt;
                            resVerSlip.Datetime = mdlDataRes.Datetime;
                            resVerSlip.Bank = mdlDataRes.BankName;
                            resVerSlip.CreatedTime = DateTime.Now;
                            resVerSlip.IsSuccess = true;
                            resVerSlip.Message = "Verify Is OK!";
                            logEvent = new LogEvent() { Code = "10", Remark = "ResVerifyslip", Detail = JsonConvert.SerializeObject(resVerSlip) };
                            _databeseService.AddlogEventSync(logEvent);
                            return Ok(resVerSlip);
                        }
                        else
                        {
                            LogSlip logSlip = new LogSlip();
                            mdlDataRes.Ref = Guid.NewGuid().ToString();

                            logSlip.Ref = mdlDataRes.Ref;
                            logSlip.Datetime = mdlDataRes.Datetime;
                            logSlip.Amt = Convert.ToDecimal(mdlDataRes.Amt);
                            logSlip.Bank = mdlDataRes.BankName;
                            logSlip.Message = mdlDataRes.Message;
                            logSlip.IsSuccess = mdlDataRes.IsSuccess;
                            //     var res = _dbLogService.SaveLogSlip(logSlip);
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


                            return BadRequest(mdlDataRes);
                        }
                    }
                    catch (Exception ex)
                    {
                        ResVerSlip resVerSlip = new ResVerSlip();
                        resVerSlip.CreatedTime = DateTime.Now;
                        resVerSlip.IsSuccess = false;
                        resVerSlip.Message = ex.Message;//"Verify Error!.OCR catch";
                        return BadRequest(resVerSlip);
                    }

                }
                else
                {
                    ResVerSlip resVerSlip = new ResVerSlip();
                    resVerSlip.CreatedTime = DateTime.Now;
                    resVerSlip.IsSuccess = false;
                    resVerSlip.Message = "Verify Error!.Check Input Key.";
                    return BadRequest(resVerSlip);
                }



            }
            else
            {
                ResVerSlip resVerSlip = new ResVerSlip();
                resVerSlip.CreatedTime = DateTime.Now;
                resVerSlip.IsSuccess = false;
                resVerSlip.Message = "Verify Error!!";
                return BadRequest(resVerSlip);
            }


            return Ok();
        }

        [Authz]
        [HttpPost]
        public ActionResult Transaction(intrans model)
        {
            HttpContext context = HttpContext;
            var user = (TblAccount)context.Items["User"];


            return Ok();
        }


        [Authz]
        [HttpPost(Name = "PayQRCode")]
        public IActionResult GenerateQRCode(MdlPayInput mdlPayInput)
        {
            HttpContext context = HttpContext;
            LogEvent logEvent = new LogEvent() { Code = "10", Remark = "GenerateQRCode", Detail = JsonConvert.SerializeObject(mdlPayInput),Addr = context.Connection.RemoteIpAddress.ToString() };
            _databeseService.AddlogEventSync(logEvent);
            List<string> randomAcc = new List<string>();

          
      
            var user = (TblAccount)context.Items["User"];
            if (user != null)
            {
                var payid = _databeseService.GetTransBankbyId(user.AccRef);
                payid.ForEach(m =>
                {

                    randomAcc.Add(m.PromNo);
                });
            }

            Random randNum = new Random();
            int aRandomPos = randNum.Next(randomAcc.Count);


            UriBuilder _url = new UriBuilder();
            _url.Scheme = "https";
            _url.Host = "promptpay.io";
            _url.Path = "/" + randomAcc[aRandomPos] + "/" + mdlPayInput.Amount;
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



            return File(data, "image/jpeg");

        }
    }
}
