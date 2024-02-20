using Core.Domain.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MdlAccount
    {
        public string PhoneNumber { get; set; } = null!;
        public string AccPwd { get; set; } = null!;
    }

    public class MdlKey
    {
        public string Key { get; set; }
    }

    public class MdlRegister
    {
        [Required(ErrorMessage = "Required AccName!")]
        public string AccName { get; set; } = null!;

        [Required(ErrorMessage = "Required AccPwd!")]
        public string AccPwd { get; set; } = null!;

        [Required(ErrorMessage = "Required AccEmail!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string AccEmail { get; set; } = null!;

        [Required(ErrorMessage = "Required AccTel!")]
        public string AccTel { get; set; } = null!;
    }


    public class ResAccount :Response
    {
        public TblAccount tblAccount { get; set; }
      
   
    }
     public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }

    public class ResCustInfo
    {
        public TblAccount tblAccount { get; set; }
        public List<Application> application { get; set; }
        public List<TblScheduler>  tblSchedulers { get; set; }

       

    }




    public class ResTokenAuth
    {
        public string Token { get; set; }
    }
    public class MdlRegScheduler
    {
        public int? AccId { get; set; }
        public int? AppId { get; set; }

        [Required(ErrorMessage = "Required TitelName!")]
        public string? TitelName { get; set; }

        [Required(ErrorMessage = "Required Hashtag!")]
        public string? Hashtag { get; set; }
        public string? RefNo { get; set; }

        [Required(ErrorMessage = "Required ActionTime!")]
        public DateTime? ActionTime { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        public List<IFormFile> FromFile { get; set; }
        public bool? State { get; set; }
        public string? ModifyBy { get; set; }
        public string? ModifyTime { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class MdlFile : ResponseVer
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        public List<IFormFile> FromFile { get; set; }
    }

    public class MdlLoginPayAPI
    {
        public string user { get; set; }
        public string password { get; set; }
    }
    public class ResToken : Response
    {
        public string Token { get; set; }
    }

    public class ResponseVer
    {
        public string bank { get; set; }
        public string amt { get; set; }
        public string datetime { get; set; }
        public string createdTime { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }

}
