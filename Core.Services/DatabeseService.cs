using Core.Domain.Database;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var x = _rpaControlDBContext.TblAccounts.Where(m => m.AccName == tblAccount.AccName && m.AccPwd == tblAccount.AccPwd).FirstOrDefault();

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
        public List<TransBank> GetTransBankbyId(string AccRef)
        {
            return _rpaControlDBContext.TransBanks.Where(m => m.AccRef == AccRef).ToList();
        }
    }


}

