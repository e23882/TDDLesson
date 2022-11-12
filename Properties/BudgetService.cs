using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetService
{
    public class BudgetService
    {
        #region Declarations
        private readonly IBudgetRepo repo;
        #endregion

        #region MemberFunction
        public BudgetService(IBudgetRepo repo)
        {
            this.repo = repo;
        }

        public decimal Query(string startDate, string endDate)
        {
            decimal result = 0;
            DateTime startDateTime = new DateTime(
                int.Parse(startDate.Substring(0, 4)),
                int.Parse(startDate.Substring(4, 2)),
                int.Parse(startDate.Substring(6, 2)));
            DateTime endDateTime = new DateTime(
                int.Parse(endDate.Substring(0, 4)),
                int.Parse(endDate.Substring(4, 2)),
                int.Parse(endDate.Substring(6, 2)));
            if (endDateTime < startDateTime)
            {
                return 0;
            }

            if(startDateTime.Year == endDateTime.Year && startDateTime.Month == endDateTime.Month)
            {
                int days = (endDateTime - startDateTime).Days + 1;
                int monthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
                int amount = repo.GetAll().Where(x => x.YearMonth == startDateTime.ToString("yyyyMM")).FirstOrDefault().Amount;
                return amount / monthDays * days;
            }

            return result;
            //repo.GetAll().Where(x=>x.YearMonth )
        }
        #endregion
    }
}
