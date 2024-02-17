using Core.Domain.Database;
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

        Task<IEnumerable<Application>> GetApplication(int AccId);
        Task<int> AddApplication(Application application);
        Task<int> UpdateApplication(Application application);

        Task<IEnumerable<TblScheduler>> GetScheduler(int AccId);
        Task<int> AddScheduler(TblScheduler tblScheduler);

        Task<int> UpdateScheduler(TblScheduler tblScheduler);

        Task<int> AddlogEvent(LogEvent logEvent);
        int AddlogEventSync(LogEvent logEvent);
        List<TransBank> GetTransBankbyId(int AccId);
    }
}
