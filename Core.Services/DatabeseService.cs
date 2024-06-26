﻿using Core.Domain.Database;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Core.Services
{
    public class DatabeseService: IDatabeseService
    {
        private RpaControlDBContext _rpaControlDBContext { get; }
        public DatabeseService(RpaControlDBContext rpaControlDBContext) {
            _rpaControlDBContext = rpaControlDBContext;


        }
        public async Task<TblAccount> GetAccount(TblAccount tblAccount)
        {

            return await _rpaControlDBContext.TblAccounts.Where(m => m.AccName == tblAccount.AccName && m.AccPwd == tblAccount.AccPwd).FirstOrDefaultAsync();
        }

        public TblAccount GetAccountsync(TblAccount tblAccount)
        {
            var x = _rpaControlDBContext.TblAccounts.Where(m => m.AccTel == tblAccount.AccTel && m.AccPwd == tblAccount.AccPwd).FirstOrDefault();

            return x;
        }
        public async Task<TblAccount> GetAccountbyId(int accId)
        {

            return await _rpaControlDBContext.TblAccounts.Where(m => m.Id == accId).FirstOrDefaultAsync();
        }
        public async Task<int> AddAccount(TblAccount tblAccount)
        {
       
            return await _rpaControlDBContext.TblAccounts.Add(tblAccount).Context.SaveChangesAsync();
        }
        public async Task<int> UpdateAccount(TblAccount tblAccount)
        {

            return await _rpaControlDBContext.TblAccounts.Update(tblAccount).Context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Application>> GetApplication(string AccRef)
        {
            return await _rpaControlDBContext.Applications.Where(m=>m.AccRef == AccRef).ToListAsync();
        }

        public async Task<int> AddApplication(Application application)
        {
            return await _rpaControlDBContext.Applications.Add(application).Context.SaveChangesAsync();
        }
        public async Task<int> UpdateApplication(Application application)
        {

            return await _rpaControlDBContext.Applications.Update(application).Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TblScheduler>> GetScheduler(int AccId)
        {
            return await _rpaControlDBContext.TblSchedulers.Where(m => m.AccId == AccId).ToListAsync();
        }

        public async Task<int> AddScheduler(TblScheduler  tblScheduler)
        {
            return await _rpaControlDBContext.TblSchedulers.Add(tblScheduler).Context.SaveChangesAsync();
        }
        public async Task<int> UpdateScheduler(TblScheduler tblScheduler)
        {

            return await _rpaControlDBContext.TblSchedulers.Update(tblScheduler).Context.SaveChangesAsync();
        }

        public async Task<int> AddlogEvent(LogEvent logEvent)
        {
            return await _rpaControlDBContext.LogEvents.Add(logEvent).Context.SaveChangesAsync();
        }
        public  int AddlogEventSync(LogEvent logEvent)
        {
            return  _rpaControlDBContext.LogEvents.Add(logEvent).Context.SaveChanges();
        }

        public List<LogEvent> GetLogEvents()
        {
            return _rpaControlDBContext.LogEvents.Where(e=>e.Remark == "polling").OrderByDescending(e=>e.CreatedTime).Take(15).ToList();
        }

        public List<TransBank> GetTransBankbyId(int tid)
        {
            return _rpaControlDBContext.TransBanks.Where(m => m.Id == tid).ToList();
        }

        public List<TransBank> GetTransBankbyAccRef(string AccRef)
        {
            return _rpaControlDBContext.TransBanks.Where(m => m.AccRef == AccRef).ToList();
        }

        public List<MtBank> GetMTBanks()
        {
            return _rpaControlDBContext.MtBanks.ToList();
        }

        public async Task<int> AddTransBankstSync(TransBank  transBank)
        {
            return await _rpaControlDBContext.TransBanks.Add(transBank).Context.SaveChangesAsync();
        }

        public async Task<int> UpdateTransBankstSync(TransBank transBank)
        {
            var update = _rpaControlDBContext.TransBanks.Where(m => m.Id == transBank.Id).FirstOrDefault();

            if (update != null)
            {
                update.Active = transBank.Active; 
              //  update.PromNo= transBank.PromNo;
                update.BankId = transBank.BankId;

                return await _rpaControlDBContext.TransBanks.Update(update).Context.SaveChangesAsync();
            }
            else
            {
                return await _rpaControlDBContext.TransBanks.Add(transBank).Context.SaveChangesAsync();

            }



        }

        public async Task<int> AddLogSlip(LogSlip  logSlip)
        {
            return await _rpaControlDBContext.LogSlips.Add(logSlip).Context.SaveChangesAsync();
        }
        

        public int AddlogSlipSync(LogSlip logSlip)
        {
            return _rpaControlDBContext.LogSlips.Add(logSlip).Context.SaveChanges();
        }


        public async Task<int> AddTransactions(Transaction  transaction)
        {

            var update = _rpaControlDBContext.Transactions.Where(m => m.Id == transaction.Id).FirstOrDefault();

            if (update != null)
            {
                update.Verfify = transaction.Verfify;
                //  update.PromNo= transBank.PromNo;
                update.ModifyDate = DateTime.Now;

                return await _rpaControlDBContext.Transactions.Update(update).Context.SaveChangesAsync();
            }
            else
            {
                return await _rpaControlDBContext.Transactions.Add(transaction).Context.SaveChangesAsync();
            }
        }

        public List<Transaction> GetTransection(MdlGetBank BankId)
        {
            if(BankId != null )
            {
                return _rpaControlDBContext.Transactions.Where(m=>m.TransBankId == BankId.BankId).ToList();
            }
            else
            {
                return _rpaControlDBContext.Transactions.ToList();
            }

          
        }

        public List<LogsMsgsm> GetLogsMsgsms()
        {
            var data = _rpaControlDBContext.LogsMsgsms.Select(m => new { m.Id, m.Code, m.Msg, m.Amout ,m.CreatedTime,m.Sender,m.Active}).OrderByDescending(m=>m.CreatedTime).ToList();

            List<LogsMsgsm> logsMsgsms = new List<LogsMsgsm>();
            data.ForEach(m => {
                if(m.Active == true)
                logsMsgsms.Add(new LogsMsgsm {  Id = m.Id, Code = m.Code, Amout = m.Amout, Msg = m.Msg, CreatedTime = m.CreatedTime , Sender = m.Sender });
            });

            return logsMsgsms;
        }



        public List<TransVer> GetTransVers()
        {
            var data = _rpaControlDBContext.TransVers.ToList();
            return data;
        }
        public int AddLogsMsgsms(LogsMsgsm  logsMsgsm)
        {


            var s =   _rpaControlDBContext.LogsMsgsms.Add(logsMsgsm).Context.SaveChanges();
            return  logsMsgsm.Id;
        }
      
        public int UpdateActiveLogs(LogsMsgsm logsMsgsm)
        {
            return _rpaControlDBContext.LogsMsgsms.Update(logsMsgsm).Context.SaveChanges();
        }
        public int AddTransVer(TransVer  transVer)
        {


            var s = _rpaControlDBContext.TransVers.Add(transVer).Context.SaveChanges();
          var Update=  _rpaControlDBContext.LogsMsgsms.Where(m => m.Id == transVer.LogId).FirstOrDefault();
            Update.Active = false;

            _rpaControlDBContext.LogsMsgsms.Update(Update).Context.SaveChanges() ;

            return transVer.Id;
        }

    }


}

