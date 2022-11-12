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
            else
            {
                
                int startMonthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
                int startDays = startMonthDays - startDateTime.Day + 1;
                int startAmount = repo.GetAll().Where(x => x.YearMonth == startDateTime.ToString("yyyyMM")).FirstOrDefault().Amount / startMonthDays * startDays;
                

                int endMonthDays = DateTime.DaysInMonth(endDateTime.Year, endDateTime.Month);
                int endAmount = repo.GetAll().Where(x => x.YearMonth == endDateTime.ToString("yyyyMM")).FirstOrDefault().Amount / endMonthDays * endDateTime.Day;
                return startAmount + endAmount;
            }


            //if(startDateTime.Year == endDateTime.Year && (endDateTime.Month-startDateTime.Month) == 1 )
            //{
            //    int startMonthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);

            //}

            return result;
            //repo.GetAll().Where(x=>x.YearMonth )
        }
        #endregion
    }
}
