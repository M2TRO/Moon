using Core.Domain.Database;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WarpPortalAPI.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarpPortalAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IDatabeseService _databeseService;

        public IJwtUtils _jwtUtils;
        public AuthController(IDatabeseService databeseService, IJwtUtils jwtUtils)
        {
            _databeseService = databeseService;
            _jwtUtils = jwtUtils;
        }

        [HttpPost(Name = "Register")]
        [AllowAnonymous]
        public IActionResult Register(MdlRegister mdlRegister)
        {
            HttpContext context = HttpContext;
            if (ModelState.IsValid)
            {
                TblAccount tblAccount = new TblAccount()
                {
                    AccName = mdlRegister.AccName,
                    AccEmail = mdlRegister.AccEmail,
                    AccPwd = mdlRegister.Password,
                    AccTel = mdlRegister.AccTel,


                };
                Response response = new Response();
                try
                {
                    tblAccount.AccRef = Guid.NewGuid().ToString();
                    var state = _databeseService.AddAccount(tblAccount);

                    while (state.IsCompleted != true) {

                    }
                    if (state.IsCompleted)
                    {
                        if (state.IsCompletedSuccessfully)
                        {
                            LogEvent logEvent = new LogEvent() { Code = "10", Remark = "Register", Detail = JsonConvert.SerializeObject(tblAccount), Addr= context.Connection.RemoteIpAddress.ToString() };
                            _databeseService.AddlogEventSync(logEvent);
                            string tokenkey = _jwtUtils.GenerateToken(tblAccount,1);
                            response.Token = tokenkey;
                            response.IsSuccess = true;
                            response.Message = "Register Success.";
                            return Ok(response);
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = state.Exception.InnerExceptions.FirstOrDefault().InnerException.Message;

                          //LogEvent logEvent = new LogEvent() { Code = "10", Remark = "ResRegister", Detail = JsonConvert.SerializeObject(response), Addr = context.Connection.RemoteIpAddress.ToString() };
                        //    _databeseService.AddlogEventSync(logEvent);

                            if (response.Message.Contains("duplicate"))
                            {
          
                                response.Message = "NumberPhone is use.";
                                response.IsSuccess = false;

                                return Ok(response);
                            }
           
                        
                        }
                    }


                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.InnerException.Message;
                 //   LogEvent logEvent = new LogEvent() { Code = "10", Remark = "ResRegister", Detail = JsonConvert.SerializeObject(response), Addr = context.Connection.RemoteIpAddress.ToString() };
                  //  _databeseService.AddlogEventSync(logEvent);
                    return BadRequest(response);
                }

            }


            return Ok(ModelState);
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Auth([FromBody] MdlKey  mdlKey)
        //{
        //    string tokenkey = _jwtUtils.GenerateToken(response.tblAccount);
        //}


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] MdlAccount mdlLogin)
        {
            LogEvent logEvent = new LogEvent() { Code = "10", Remark = "Login", Detail = JsonConvert.SerializeObject(mdlLogin) };
            _databeseService.AddlogEventSync(logEvent);
            TblAccount tblAccount = new TblAccount()
            {
                AccTel = mdlLogin.AccTel,
                AccPwd = mdlLogin.AccPwd,


            };
            ResAccount response = new ResAccount();
            try
            {
                var state = _databeseService.GetAccountsync(tblAccount);
       

                if (state != null)
                {

                    //   response.tblAccount = state;
                    if (state.IsActive.HasValue)
                    {
                        if (state.IsActive.Value)
                        {
                            string tokenkey = _jwtUtils.GenerateToken(state, 1);
                            response.Token = tokenkey;
                            response.Message = "Login Success.";
                            response.IsSuccess = true;

                            logEvent = new LogEvent() { Code = "10", Remark = "ResLogin", Detail = JsonConvert.SerializeObject(response) };
                            _databeseService.AddlogEventSync(logEvent);

                            return Ok(response);
                        }
                        else
                        {
                            response.Message = "User InActive!";
                            response.IsSuccess = false;
                        }
                    }
                    else
                    {
                        response.Message = "User Invalid!";
                        response.IsSuccess = false;
                    }
                    return Ok(response);
                }
                else
                {
                    response.Message = "User Invalid!";
                    response.IsSuccess = false;
                    return Ok(response);
                }



            }
            catch
            {
                response.IsSuccess = false;
                logEvent = new LogEvent() { Code = "10", Remark = "ResLogin", Detail = JsonConvert.SerializeObject(response) };
                _databeseService.AddlogEventSync(logEvent);
                return BadRequest(response);
            }

        }




        [HttpPost(Name = "GetCustInfo")]
        [HttpGet]
        [Authz]
        public IActionResult GetCustInfo()
        {
            HttpContext context = HttpContext;
            var user = (TblAccount)context.Items["User"];
            ResCustInfo resAccount = new ResCustInfo();
            user.AccPwd = null;
            resAccount.tblAccount = user;
            var bank = _databeseService.GetTransBankbyId(user.AccRef);
            resAccount.transBanks = bank;
            //var App =   _databeseService.GetApplication(user.AccRef);
            //resAccount.application = App.Result.ToList();
            //var list = _databeseService.GetScheduler(user.Id);
            //resAccount.tblSchedulers = list.Result.ToList();

            return Ok(resAccount);
        }
      


        [HttpPost]
        public IActionResult SaveApplication()
        {
            if (!ModelState.IsValid)
            {

            }

            return Ok(ModelState);
        }


        }
}
