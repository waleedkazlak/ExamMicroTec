using ExamMicroTec.Models;

namespace ExamMicroTec.Services
{
    public interface IAccountService
    {
       List<AccountReportModelView> PrepareAccountReport();


        List<AccountReportModelView> PrepareAccountDetails(string AccountID);
    }
}
