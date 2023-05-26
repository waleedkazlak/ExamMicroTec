using ExamMicroTec.DAL;
using ExamMicroTec.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;

namespace ExamMicroTec.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _dbContext;
        decimal? accumulatedbalance = 0;
        List<AccountReportModelView> accountDetails ;
        public AccountService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AccountReportModelView> PrepareAccountReport()
        {

            List<AccountReportModelView> list = new List<AccountReportModelView>();
            var query = _dbContext.Accounts.Where(x => string.IsNullOrEmpty(x.ACC_Parent)).Select(x => x.Acc_Number).ToList();
            foreach (var item in query)
            {
                list.Add(new AccountReportModelView { TopLevelAccount = item, TotalBalance = GetChidernBalances(item) });
                accumulatedbalance = 0;
            }
            return list;


        }

        public List<AccountReportModelView> PrepareAccountDetails(string AccountID)
        {
            accountDetails = new List<AccountReportModelView>();
            GetChidernBalancesArray(AccountID);
            return accountDetails.Where(x => x.TotalBalance > 0).ToList();
        }

       
        private decimal? GetChidernBalances(string AccountID)
        {
            var query = _dbContext.Accounts.Where(x=>x.ACC_Parent == AccountID).Select(x => new AccountReportModelView() { TopLevelAccount = x.Acc_Number, TotalBalance = x.Balance }).ToList();
            
            foreach (var item in query)
            {
                accumulatedbalance += item.TotalBalance;
               
                GetChidernBalances(item.TopLevelAccount);
            }
            
            return accumulatedbalance;
        }

        private void GetChidernBalancesArray(string AccountID)
        {
            var acc = AccountID.Split("-",StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            var query = _dbContext.Accounts.Where(x => x.ACC_Parent == acc).Select(x => new AccountReportModelView() { TopLevelAccount = x.Acc_Number, TotalBalance = x.Balance }).ToList();

            foreach (var item in query)
            {
                
                accountDetails.Add(new AccountReportModelView { TopLevelAccount = AccountID+"-"+ item.TopLevelAccount.Trim()  , TotalBalance =item.TotalBalance }  );
                GetChidernBalancesArray(AccountID.Trim() + "-" + item.TopLevelAccount.Trim());
            }

            
        }
    }
}
