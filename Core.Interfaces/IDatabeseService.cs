﻿using Core.Domain.Database;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDatabeseService
    {
        Task<TblAccount> GetAccount(TblAccount tblAccount);
        TblAccount GetAccountsync(TblAccount tblAccount);
        Task<TblAccount> GetAccountbyId(int accId);
        Task<int> AddAccount(TblAccount tblAccount);
        Task<int> UpdateAccount(TblAccount tblAccount);

        Task<IEnumerable<Application>> GetApplication(string AccRef);
        Task<int> AddApplication(Application application);
        Task<int> UpdateApplication(Application application);

        Task<IEnumerable<TblScheduler>> GetScheduler(int AccId);
        Task<int> AddScheduler(TblScheduler tblScheduler);

        Task<int> UpdateScheduler(TblScheduler tblScheduler);

        Task<int> AddlogEvent(LogEvent logEvent);
        int AddlogEventSync(LogEvent logEvent);
        List<LogEvent> GetLogEvents();
        List<TransBank> GetTransBankbyAccRef(string AccRef);
        List<TransBank> GetTransBankbyId(int tid);
        Task<int> AddLogSlip(LogSlip logSlip);
        int AddlogSlipSync(LogSlip logSlip);
        Task<int> AddTransBankstSync(TransBank transBank);
        Task<int> UpdateTransBankstSync(TransBank transBank);
         List<MtBank> GetMTBanks();
         Task<int> AddTransactions(Transaction transaction);

        List<Transaction> GetTransection(MdlGetBank BankId);

        int AddLogsMsgsms(LogsMsgsm logsMsgsm);
        int UpdateActiveLogs(LogsMsgsm logsMsgsm);
        List<LogsMsgsm> GetLogsMsgsms();
        int AddTransVer(TransVer transVer);
        List<TransVer> GetTransVers();
    }
}
