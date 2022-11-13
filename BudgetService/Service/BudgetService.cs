using System;
using System.Linq;

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

        public decimal Query(DateTime startDateTime, DateTime endDateTime)
        {
            if (endDateTime < startDateTime)
            {
                return 0;
            }

            if(startDateTime.Year == endDateTime.Year && startDateTime.Month == endDateTime.Month)
            {
                int days = (endDateTime - startDateTime).Days + 1;
                int monthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
                int amount = repo.GetAll().Where(x => x.YearMonth == startDateTime.ToString("yyyyMM")).Sum(x=>x.Amount);
                return amount / monthDays * days;
            }
            else
            {
                int startMonthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
                int startDays = startMonthDays - startDateTime.Day + 1;
                int startAmount = repo.GetAll()
                    .Where(x => x.YearMonth == startDateTime.ToString("yyyyMM"))
                    .Sum(x => x.Amount) / startMonthDays * startDays;
                
                int endMonthDays = DateTime.DaysInMonth(endDateTime.Year, endDateTime.Month);
                int endAmount = repo.GetAll()
                    .Where(x => x.YearMonth == endDateTime.ToString("yyyyMM"))
                    .Sum(x => x.Amount) / endMonthDays * endDateTime.Day;
                
                if(startDateTime.AddMonths(1) == endDateTime)
                {
                    return startAmount + endAmount;
                }
                else
                {
                    int multiAmount = repo.GetAll().Where(x => int.Parse(x.YearMonth) > int.Parse(startDateTime.ToString("yyyyMM")) && int.Parse(x.YearMonth) < int.Parse(endDateTime.ToString("yyyyMM"))).Sum(x=>x.Amount);
                    return startAmount + endAmount + multiAmount;
                }
            }
        }
        #endregion
    }
}
